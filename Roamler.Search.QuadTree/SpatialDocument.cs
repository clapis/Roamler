using Roamler.Model;
using Roamler.QuadTree;

namespace Roamler.Search.QuadTree
{
    public class SpatialDocument : ISpatialDocument, IPoint
    {
        private readonly int _id;
        private readonly GeoCoordinate _coordinates;

        public SpatialDocument(ISpatialDocument entity)
        {
            _id = entity.Id;
            _coordinates = entity.Coordinates;
        }

        public int Id { get { return _id; } }
        public GeoCoordinate Coordinates { get { return _coordinates; } }
        public double X { get { return _coordinates.Longitude; } }
        public double Y { get { return _coordinates.Latitude; } }
    }
}
