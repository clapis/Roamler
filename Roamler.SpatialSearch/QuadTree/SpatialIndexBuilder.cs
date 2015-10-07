using System;
using System.Linq;
using Roamler.Data.EntityFramework;
using Roamler.QuadTree;

namespace Roamler.SpatialSearch.QuadTree
{
    /// <summary>
    /// This should be able to build an index from any spatial entity and not only
    /// Location as is now. For the sake of time, I'm creating a dependency with
    /// DbContext and indexing Location entity directly. More abstractions needed.
    /// </summary>
    public class SpatialIndexBuilder : ISpatialIndexBuilder
    {
        private readonly RoamlerDbContext _dbContext;

        public SpatialIndexBuilder(RoamlerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ISpatialIndex BuildIndex()
        {
            var locations = _dbContext.Locations;

            if (!locations.Any()) return new EmptySpatialIndex();

            var maxLat = locations.Max(l => l.Coordinate.Latitude);
            var minLat = locations.Min(l => l.Coordinate.Latitude);
            var maxLng = locations.Max(l => l.Coordinate.Longitude);
            var minLng = locations.Min(l => l.Coordinate.Longitude);

            var indexArea = CreateIndexBoundary(maxLat, minLat, maxLng, minLng);
 
            var quadTree = new QuadTree<SpatialDocument>(indexArea);

            foreach (var location in locations)
            {
                quadTree.Insert(new SpatialDocument(location));
            }

            var index = new SpatialIndex(quadTree);

            return index;
        }

        private Boundary CreateIndexBoundary(double maxLat, double minLat, double maxLng, double minLng)
        {
            var center = new Point((maxLng - minLng) / 2, (maxLat - minLat) / 2);

            var latDelta = center.Y - minLat;
            var lngDelta = center.X - minLng;

            // Data might be aligned all in one dimension. Or there could be just one document to be indexed.
            // Could add some minimal value to deltas case they are zero, but that's a not priority at this momemnt
            if (latDelta == 0 || lngDelta == 0) throw new NotSupportedException("Data is aligned in one dimension");

            return new Boundary(center, lngDelta, latDelta);
            
        }
    }

}