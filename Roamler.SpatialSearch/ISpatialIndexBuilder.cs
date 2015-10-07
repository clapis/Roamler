namespace Roamler.SpatialSearch
{
    /// <summary>
    /// Exposes a method to build a spatial index
    /// </summary>
    public interface ISpatialIndexBuilder
    {
        ISpatialIndex BuildIndex();
    }
}
