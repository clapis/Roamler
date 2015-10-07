using System.Collections.Generic;
using System.Linq;
using Roamler.Data;
using Roamler.Services.Requests;
using Roamler.Services.Results;
using Roamler.SpatialSearch;
using Roamler.SpatialSearch.Queries;

namespace Roamler.Services
{
    public class LocationSearchService : ILocationSearchService
    {
        private readonly ISpatialIndex _spatialIndex;
        private readonly ILocationRepository _locationRepository;

        public LocationSearchService(ISpatialIndex spatialIndex, ILocationRepository locationRepository)
        {
            _spatialIndex = spatialIndex;
            _locationRepository = locationRepository;
        }


        public LocationSearchResults Search(LocationSearchRequest search)
        {
            // TODO: validate search parameter

            var query = new KnnQuery
            {
                Coordinate = search.Coordinate, 
                MaxDistance = search.MaxDistance, 
                MaxResults = search.MaxResults
            };

            // use spatial index to reduce data set
            var knnQueryResult = _spatialIndex.KnnSearch(query);

            if (!knnQueryResult.Results.Any()) 
                return LocationSearchResults.ZeroResults();

            // now load entities from database
            var ids = knnQueryResult.Results.Select(i => i.Document.Id).ToArray();

            var locations = _locationRepository.FindIn(ids);

            // build return results by joining Location and Distance
            var results = new List<LocationSearchResultItem>();

            var locationsIndex = locations.ToDictionary(l => l.Id);

            foreach (var item in knnQueryResult.Results)
            {
                if (locationsIndex.ContainsKey(item.Document.Id))
                {
                    var location = locationsIndex[item.Document.Id];
                    results.Add(new LocationSearchResultItem(location, item.Distance));
                }
            }

            return LocationSearchResults.Success(results);
        }

        
    }

}
