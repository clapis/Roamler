using Roamler.SpatialSearch.Queries;

namespace Roamler.SpatialSearch
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
