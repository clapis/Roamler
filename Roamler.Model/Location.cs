namespace Roamler.Model
{
    public class Location : ISpatialDocument
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public GeoCoordinate Coordinates { get; set; }

    }
}
