using System.Collections.Generic;
using Roamler.Model;

namespace Roamler.Search.Queries
{

    public class KnnQueryResult
    {
        /// <summary>
        /// Whether the search has succeeded.
        /// </summary>
        public bool Succeeded { get; private set; }

        /// <summary>
        /// The search results ordered by distance
        /// </summary>
        public List<KnnQueryResultItem> Results { get; private set; }

        /// <summary>
        /// Error details in case the search hasn't succeeded
        /// </summary>
        public string Error { get; private set; }

        private KnnQueryResult() { }


        #region Static Factory Methods

        public static KnnQueryResult Success(List<KnnQueryResultItem> results)
        {
            return new KnnQueryResult { Succeeded = true, Results = results };
        }

        public static KnnQueryResult ZeroResults()
        {
            return new KnnQueryResult { Succeeded = true, Results = new List<KnnQueryResultItem>() };
        }

        public static KnnQueryResult Fail(string error)
        {
            return new KnnQueryResult { Succeeded = false, Error = error };
        }

        #endregion
    }

    public class KnnQueryResultItem
    {
        public KnnQueryResultItem(ISpatialDocument document, double distance)
        {
            Document = document;
            Distance = distance;
        }

        /// <summary>
        /// Distance in meters from the searched coordinate
        /// </summary>
        public double Distance { get; private set; }

        /// <summary>
        /// Indexed document
        /// </summary>
        public ISpatialDocument Document { get; private set; }
    }

}
