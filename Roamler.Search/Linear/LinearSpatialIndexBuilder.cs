using System;
using System.Linq;
using Roamler.Model;

namespace Roamler.Search.Linear
{
    public class LinearSpatialIndexBuilder : ISpatialIndexBuilder
    {
        public ISpatialIndex BuildIndex(IQueryable<ISpatialDocument> documents)
        {
            var index = new LinearSpatialIndex();

            foreach (var doc in documents)
            {
                index.Add(doc);
            }

            return index;
        }
        
    }
}
