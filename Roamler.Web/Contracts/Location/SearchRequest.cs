using System.ComponentModel.DataAnnotations;
using Roamler.Model;

namespace Roamler.Web.Contracts.Location
{
    public class SearchRequest
    {
        /// <summary>
        /// Coordinate of search
        /// </summary>
        [Required]
        public GeoCoordinate Coordinate { get; set; }

        /// <summary>
        /// Maximum distance in meter from coordinate
        /// </summary>
        //[Range(0.01, 20000)]
        [Range(1, double.MaxValue)]
        public int MaxDistance { get; set; }

        /// <summary>
        /// Maximum number of results
        /// </summary>
        //[Range(1, 100)]
        [Range(1, double.MaxValue)]
        public int MaxResults { get; set; }
    }
}