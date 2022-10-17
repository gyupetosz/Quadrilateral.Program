using System;
using System.Numerics;

namespace Library
{
    public sealed class Position
    {
        public Position(double latitude, double longitude, double height)
        {
            if (latitude < -90.0 || latitude > 90.0)
            {
                throw new ArgumentOutOfRangeException("latitude");
            }else
            {
                Latitude = latitude;
            }
            if(longitude < 0 || longitude > 180.0)
            {
                throw new ArgumentOutOfRangeException("longitude");
            }
            else
            {
                Longitude = longitude;
            }
            
            Height = height;
        }

        /// <summary>
        /// Latitude in decimal degrees
        /// </summary>
        public double Latitude
        {
            get;
        }

        /// <summary>
        /// Longitude in decimal degrees
        /// </summary>
        public double Longitude
        {
            get;
        }

        /// <summary>
        /// Height in metres
        /// </summary>
        public double Height
        {
            get;
        }

        public bool Equals(Position p)
        {
            return (p.Height == this.Height && p.Latitude == this.Latitude && p.Longitude == this.Longitude); 
        }

        public void Print()
        {
            Console.WriteLine("Position {0} latitude, {1} longitude, {2} height.",Latitude,Longitude,Height);
        }

}
}
