using System.Linq;
using Roamler.Model;

namespace Roamler.SpatialSearch.Linear
{
    public class LinearSpatialIndexBuilder : ISpatialIndexBuilder
    {
        private readonly IQueryable<ISpatialDocument> _ds;

        public LinearSpatialIndexBuilder(IQueryable<ISpatialDocument> ds)
        {
            _ds = ds;
        }

        public ISpatialIndex BuildIndex()
        {
            var index = new LinearSpatialIndex();

            foreach (var doc in _ds)
            {
                index.Add(doc);
            }

            return index;
        }
    }
}
