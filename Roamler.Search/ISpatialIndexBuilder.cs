using Roamler.Model;
using System.Linq;

namespace Roamler.Search
{
    /// <summary>
    /// Exposes a method to build a spatial index
    /// </summary>
    public interface ISpatialIndexBuilder
    {
        ISpatialIndex BuildIndex(IQueryable<ISpatialDocument> documents);
    }
}
