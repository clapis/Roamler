using System.Web.Http;
using Roamler.Services;
using Roamler.Services.Requests;
using Roamler.Web.Contracts.Location;

namespace Roamler.Web.Controllers
{
    [RoutePrefix("api/location")]
    public class LocationController : ApiController
    {

        private readonly ILocationSearchService _locationSearchService;

        public LocationController(ILocationSearchService locationSearchService)
        {
            _locationSearchService = locationSearchService;
        }


        [Route("search")]
        public SearchResponse Search(SearchRequest request)
        {
            var locationSearchRequest = new LocationSearchRequest
            {
                Coordinate = request.Coordinate,
                MaxDistance = request.MaxDistance,
                MaxResults = request.MaxResults
            };

            var results = _locationSearchService.Search(locationSearchRequest);

            return new SearchResponse { Results = results.Results };

        } 


    }
}