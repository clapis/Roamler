using System;
using System.Collections.Generic;
using System.Linq;
using Roamler.QuadTree;
using Roamler.SpatialSearch.Queries;

namespace Roamler.SpatialSearch.QuadTree
{
    /// <summary>
    /// Implementation of spatial index supporting Knn queries using QuadTrees.
    /// QuadTree is not really suitable for such queries - ideally we would 
    /// implement an RTree. However due to its simplicity, it should help
    /// to reduce the searched dataset.
    /// </summary>
    public class QuadTreeSpatialIndex : ISpatialIndex
    {
        private readonly QuadTree<QuadTreeSpatialDocument> _quadTree;

        public QuadTreeSpatialIndex(QuadTree<QuadTreeSpatialDocument> quadTree)
        {
            _quadTree = quadTree;
        }

        public KnnQueryResult KnnSearch(KnnQuery query)
        {
            if (query == null) 
                throw new ArgumentException("Parameter query cannot be null");

            // validate query
            if (!IsQueryValid(query)) 
                return KnnQueryResult.Fail("Query is not valid. Please verify search parameters");

            // execute search
            var result = InternalKnnSearch(query);

            return result;

        }

        #region Private Methods

        private bool IsQueryValid(KnnQuery query)
        {
            // Coordinates are required
            if (query.Coordinate == null) return false;

            // Max results should be greater than zero
            if (query.MaxResults <= 0) return false;

            // Max distance should be greater than zeo
            if (query.MaxDistance <= 0) return false;

            // Other validation rules
            // Max distance shouldn't be greater than ... setttings..

            return true;
        }

        private KnnQueryResult InternalKnnSearch(KnnQuery query)
        {
            // get search area
            var searchArea = GetSearchArea(query);

            // query the quadtree
            var docs = _quadTree.Search(searchArea);

            // almost done, but remember we searched a rectangle
            // so we need to exclude some of these results. 
            // Let's do that while calculating the distance

            var results = new List<KnnQueryResultItem>();

            foreach (var doc in docs)
            {
                var distance = query.Coordinate.CalculateDistance(doc.Coordinates);
                if (distance <= query.MaxDistance) results.Add(new KnnQueryResultItem(doc, distance));
            }

            // this could be done more efficiently by inserting into an ordered structure
            // unfortunatelly SortedList<> doesn't allow duplicated keys. 
            // but there is definetely space for optimization here
            results.Sort((x, y) => x.Distance.CompareTo(y.Distance));
            
            // reduce set to max results
            results = results.Take(query.MaxResults).ToList();

            return KnnQueryResult.Success(results);

        }

        private Boundary GetSearchArea(KnnQuery query)
        {
            var top = query.Coordinate.Add(query.MaxDistance, 0);
            var bottom = query.Coordinate.Add(-query.MaxDistance, 0);
            var right = query.Coordinate.Add(0, query.MaxDistance);
            var left = query.Coordinate.Add(0, -query.MaxDistance);

            var halfLatitude = (top.Latitude - bottom.Latitude) / 2;
            var halfLongitude = (right.Longitude - left.Longitude) / 2;

            var center = new Point(left.Longitude + halfLongitude, bottom.Latitude + halfLatitude);
            var searchArea = new Boundary(center, halfLongitude, halfLatitude);

            return searchArea;
        }

        #endregion
    }
}
