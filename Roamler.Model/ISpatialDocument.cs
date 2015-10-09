namespace Roamler.Model
{
    /// <summary>
    /// A document that can be spatially indexed
    /// </summary>
    public interface ISpatialDocument
    {
        int Id { get; }
        GeoCoordinate Coordinates { get; }
    }
}