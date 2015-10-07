using Roamler.Model;

namespace Roamler.SpatialSearch.Queries
{
    public class KnnQuery
    {
        /// <summary>
        /// Coordinate of query
        /// </summary>
        public GeoCoordinate Coordinate { get; set; }

        /// <summary>
        /// Maximum distance in meter from coordinate
        /// </summary>
        public double MaxDistance { get; set; }

        /// <summary>
        /// Maximum number of results
        /// </summary>
        public int MaxResults { get; set; }
    }
}
