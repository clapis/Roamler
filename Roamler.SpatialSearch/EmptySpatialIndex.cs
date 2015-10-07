using Roamler.SpatialSearch.Queries;

namespace Roamler.SpatialSearch
{
    public class EmptySpatialIndex : ISpatialIndex
    {
        public KnnQueryResult KnnSearch(KnnQuery query)
        {
            return KnnQueryResult.ZeroResults();
        }
    }
}
