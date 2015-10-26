using Roamler.Search.Queries;

namespace Roamler.Search
{
    /// <summary>
    /// A spatial index that exposes spatial queries support
    /// </summary>
    public interface ISpatialIndex
    {
        /// <summary>
        /// Finds the k nearest indexed documents to a given geolocation
        /// </summary>
        /// <param name="query">Search parameters</param>
        KnnQueryResult KnnSearch(KnnQuery query);
    }
}
