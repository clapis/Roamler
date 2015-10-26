using Roamler.Search.Queries;

namespace Roamler.Search
{
    public class EmptySpatialIndex : ISpatialIndex
    {
        public KnnQueryResult KnnSearch(KnnQuery query)
        {
            return KnnQueryResult.ZeroResults();
        }
    }
}
