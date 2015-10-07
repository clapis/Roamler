using System.Collections.Generic;
using Roamler.Services.Results;

namespace Roamler.Web.Contracts.Location
{
    public class SearchResponse 
    {
        public List<LocationSearchResultItem> Results { get; set; }
    }

}