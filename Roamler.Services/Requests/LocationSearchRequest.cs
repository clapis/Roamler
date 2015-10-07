using Roamler.Model;

namespace Roamler.Services.Requests
{
    public class LocationSearchRequest
    {
        /// <summary>
        /// Coordinate of search
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
