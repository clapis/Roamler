using System;

namespace Roamler.Model
{
    public class GeoCoordinate
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public GeoCoordinate() { }

        public GeoCoordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Creates a new GeoCoordinate that is <paramref name="offsetLat"/>, <paramref name="offsetLon"/> meters from this geoCoordinate.
        /// </summary>
        public GeoCoordinate Add(double offsetLat, double offsetLon)
        {
            double latitude = Latitude + (offsetLat / 111111d);
            double longitude = Longitude + (offsetLon / (111111d * Math.Cos(Math.PI * latitude / 180)));

            return new GeoCoordinate(latitude, longitude);
        }

        /// <summary>
        /// Calculates the distance between this geoCoordinate and another one, in meters.
        /// </summary>
        public double CalculateDistance(GeoCoordinate geoCoordinate)
        {
            var rlat1 = Math.PI * Latitude / 180;
            var rlat2 = Math.PI * geoCoordinate.Latitude / 180;
            var rlon1 = Math.PI * Longitude / 180;
            var rlon2 = Math.PI * geoCoordinate.Longitude / 180;
            var theta = Longitude - geoCoordinate.Longitude;
            var rtheta = Math.PI * theta / 180;
            var dist = Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            return dist * 1609.344;
        }

        public override string ToString()
        {
            return Latitude + ", " + Longitude;
        }

    }
}
