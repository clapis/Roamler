using System.Collections.Generic;
using Roamler.Model;

namespace Roamler.Services.Results
{
    public class LocationSearchResults
    {
        public List<LocationSearchResultItem> Results { get; private set; }
 
        private LocationSearchResults() {}


        #region Static Factory Methods

        public static LocationSearchResults ZeroResults()
        {
            return new LocationSearchResults { Results = new List<LocationSearchResultItem>() };
        }

        public static LocationSearchResults Success(List<LocationSearchResultItem> locations)
        {
            return new LocationSearchResults { Results = locations };
        }

        #endregion
    }

    public class LocationSearchResultItem
    {
        public LocationSearchResultItem(Location location, double distance)
        {
            Location = location;
            Distance = distance;
        }

        /// <summary>
        /// Distance in meters from the searched coordinate
        /// </summary>
        public double Distance { get; private set; }

        /// <summary>
        /// Location
        /// </summary>
        public Location Location { get; private set; }

    }
}
