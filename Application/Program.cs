using Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
    public static class Program
    {
        public static List<Position> DuplicateChecker(List<Position> positions, Position position)
        {
            foreach (Position pos in positions)
            {
                if (pos.Equals(position))
                {
                    throw new ArgumentException(String.Format("Position with latitude {0} and longitude {1} is already present", position.Latitude, position.Longitude), "num");
                }
            }
            positions.Add(position);
            return positions;
        }

        public static bool Parse(string line, out double result)
        {
            result = 0;
            bool parsed = double.TryParse(line, out double value);

            if (!parsed || double.IsNaN(value) || double.IsInfinity(value))
            {
                return false;
            }
            else
            {
                result = value;
                return true;
            }
        }

        public static List<Position> ReadPositions()
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

                    if (!Parse(readLine, out double value)) break;
                    
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
                        positions = DuplicateChecker(positions, position);
                    }

                }

                if (positions.Count == 4)
                {
                    finished = true;
                }

            }
            return positions;
        }

        public static List<Position> OrderElements(List<Position> positions)
        {
            positions = positions.OrderBy(x => x.Longitude).ToList();
            positions = positions.OrderByDescending(x => x.Latitude).ToList();
            
            return positions;
        }
        public static void Main(string[] args)
        {
            List<Position> positions = ReadPositions();
            
            for(int i = 1; i < positions.Count;i++)
            {
                double distance = Calculations.Distance(positions[i - 1], positions[i]);
                Console.WriteLine("The distance between the {0}. and {1}. position is {2}", i - 1, i, distance);
            }

            positions = OrderElements(positions);
            positions.ForEach(x => x.Print());
        }
    }
}
