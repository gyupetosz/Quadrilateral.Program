using Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
    internal class Program
    {

        static List<Position> ReadPositions()
        {
            Console.WriteLine("Add longitude then latitude then height press enter after each line of data");
            Console.WriteLine("Type break to start over");

            string readLine = "";
            bool finished = false;
            double latitude = 0;
            double longitude = 0;
            double height = 0;

            List<Position> positions = new List<Position>();

            while (!finished)
            {
                Console.WriteLine("Give 4 points of data:");
                for (int i = 0; i < 12; i++)
                {

                    readLine = Console.ReadLine();
                    if (readLine == "break")
                    {
                        break;
                    }
                    bool parsed = double.TryParse(readLine, out double value);

                    if (!parsed || double.IsNaN(value) || double.IsInfinity(value))
                    {
                        Console.WriteLine("Give a number in the proper format!");
                        positions.Clear();
                        break;
                    }

                    if (i % 3 == 0)
                    {
                        latitude = value;
                    }
                    else if (i % 3 == 1)
                    {
                        longitude = value;
                    }
                    else
                    {
                        height = value;
                        Position position = new Position(latitude, longitude, height);
                        foreach (Position pos in positions)
                        {
                            if(pos.Equals(position))
                            {
                                throw new ArgumentException(String.Format("Position with latitude {0} and longitude {1} is already present", position.Latitude, position.Longitude),"num");
                            }
                        }
                        positions.Add(position);
                    }

                }

                if (positions.Count == 4)
                {
                    finished = true;
                }

            }
            return positions;
        }
        static void Main(string[] args)
        {
            List<Position> positions = ReadPositions();
            positions = positions.OrderByDescending(x => x.Latitude).ToList();
            positions = positions.OrderBy(x => x.Longitude).ToList();
            positions.ForEach(x => x.Print());
            
        }
    }
}
