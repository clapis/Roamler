using System.Collections.Generic;
using System.Linq;
using Roamler.Model;
using Roamler.SpatialSearch.Queries;

namespace Roamler.SpatialSearch.Linear
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
            var results =
                from doc in _docs
                let distance = query.Coordinate.CalculateDistance(doc.Coordinates)
                where distance <= query.MaxDistance
                orderby distance
                select new KnnQueryResultItem(doc, distance);

            // reduce set to max results
            results = results.Take(query.MaxResults);

            return KnnQueryResult.Success(results.ToList());
        }

        public void Add(ISpatialDocument doc)
        {
            _docs.Add(doc);
        }
    }
}
