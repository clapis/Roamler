using Roamler.Services.Requests;
using Roamler.Services.Results;

namespace Roamler.Services
{
    public interface ILocationSearchService
    {
        /// <summary>
        /// Find the closest locations to a given coordinate.
        /// </summary>
        LocationSearchResults Search(LocationSearchRequest search);
    }
}
