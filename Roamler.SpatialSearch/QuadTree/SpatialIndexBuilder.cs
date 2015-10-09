using System.Linq;
using Roamler.Model;
using Roamler.QuadTree;

namespace Roamler.SpatialSearch.QuadTree
{
    public class SpatialIndexBuilder : ISpatialIndexBuilder
    {
        // this dependency is not looking good :/
        // need some data provider abstraction here
        private readonly IQueryable<ISpatialDocument> _ds;

        public SpatialIndexBuilder(IQueryable<ISpatialDocument> ds)
        {
            _ds = ds;
        }

        public ISpatialIndex BuildIndex()
        {
            if (!_ds.Any()) return new EmptySpatialIndex();

            var mercartor = new Boundary(new Point(0, 0), 180, 90);
            var quadTree = new QuadTree<SpatialDocument>(mercartor);

            foreach (var location in _ds)
            {
                quadTree.Insert(new SpatialDocument(location));
            }

            var index = new SpatialIndex(quadTree);

            return index;
        }

    }

}