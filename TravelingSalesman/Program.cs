using System;
using System.Diagnostics;
using TravelingSalesman.Models;

namespace TravelingSalesman
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Please specify number of cities.");
            int cityCount;
            while (!int.TryParse(Console.ReadLine(), out cityCount))
            {
                Console.WriteLine("Provided number is inappropriate.");
            }

            Random random = new Random();
            var cities = CitySeed.SeedData(cityCount);
            var start = cities[random.Next(cityCount)];
            string path = $"{start.Name} ";

            TravelManager manager = new TravelManager(start, cities);

            var temp = cities.ToArray();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            City next = null;
            double routeLength = 0d;
            for (int i = 0; i < temp.Length; i++)
            {
                var current = i == 0 ? start : next;
                next = manager.FindNearestNeighbour(current, ref routeLength);

                path += $"{next.Name} ";
            }

            stopwatch.Stop();

            Console.Clear();
            Console.WriteLine("Shortest route is: \n");
            Console.WriteLine(path);

            Console.WriteLine("\n\t INFO");
            Console.WriteLine($"Number of cities: {cityCount}");
            Console.WriteLine($"Starting point: {start.Name}");
            Console.WriteLine($"Time elapsed: {stopwatch.Elapsed.ToString()}");
            Console.WriteLine($"Route length: {Math.Round(routeLength, 3)} units.");

            Console.ReadKey();
        }
    }
}
