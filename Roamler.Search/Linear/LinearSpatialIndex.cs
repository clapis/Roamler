using System.Collections.Generic;
using System.Linq;
using Roamler.Model;
using Roamler.Search.Queries;

namespace Roamler.Search.Linear
{
    public class LinearSpatialIndex : ISpatialIndex
    {
        private readonly List<ISpatialDocument> _docs;

        public LinearSpatialIndex()
        {
            _docs = new List<ISpatialDocument>();
        }

        public KnnQueryResult KnnSearch(KnnQuery query)
        {
            var search =
                from doc in _docs
                let distance = query.Coordinate.CalculateDistance(doc.Coordinates)
                where distance <= query.MaxDistance
                orderby distance
                select new KnnQueryResultItem(doc, distance);

            // list results taking top max
            var results = search.Take(query.MaxResults).ToList();

            return KnnQueryResult.Success(results);
        }

        public void Add(ISpatialDocument doc)
        {
            _docs.Add(doc);
        }
    }
}
