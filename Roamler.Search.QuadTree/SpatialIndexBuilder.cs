using System;
using System.Linq;
using Roamler.Model;
using Roamler.QuadTree;

namespace Roamler.Search.QuadTree
{
    public class SpatialIndexBuilder : ISpatialIndexBuilder
    {

        public ISpatialIndex BuildIndex(IQueryable<ISpatialDocument> documents)
        {
            if (!documents.Any()) return new EmptySpatialIndex();

            var mercator = new Boundary(new Point(0, 0), 180, 90);
            var quadTree = new QuadTree<SpatialDocument>(mercator);

            foreach (var doc in documents)
            {
                quadTree.Insert(new SpatialDocument(doc));
            }

            var index = new SpatialIndex(quadTree);

            return index;
        }

    }

}