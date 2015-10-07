using Roamler.Model;
using Roamler.QuadTree;

namespace Roamler.SpatialSearch.QuadTree
{
    public class SpatialDocument : ISpatialDocument, IPoint
    {
        private readonly int _id;
        private readonly GeoCoordinate _coordinate;

        public SpatialDocument(Location location)
        {
            _id = location.Id;
            _coordinate = location.Coordinate;
        }

        public int Id { get { return _id; } }
        public GeoCoordinate Coordinates { get { return _coordinate; } }
        public double X { get { return _coordinate.Longitude; } }
        public double Y { get { return _coordinate.Latitude; } }
    }
}
