using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    internal static class Calculations
    {
        private static double ToRadians(double degrees)
        {
            return (degrees *= Math.PI / 180);
        }
        public static double Distance(Position p1, Position p2)
        {
            double deltaLat = p1.Latitude > p2.Latitude ? p1.Latitude - p2.Latitude : p2.Latitude - p1.Latitude;
            double deltaLong = p1.Longitude > p2.Longitude ? p1.Longitude - p2.Longitude : p2.Longitude - p1.Longitude;
            deltaLat = ToRadians(deltaLat);
            deltaLong = ToRadians(deltaLong);
            double dist = Math.Pow(Math.Sin(deltaLat / 2), 2) + Math.Cos(ToRadians(p1.Latitude)) * Math.Cos(ToRadians(p2.Latitude)) * Math.Pow(Math.Sin(deltaLong / 2), 2);
            dist = 2 * Math.Asin(Math.Sqrt(dist));
            dist *= 6378137;
            return dist;
        }
    }
}
