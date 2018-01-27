using System;
using System.Collections.Generic;
using System.Drawing;
using TravelingSalesman.Models;

namespace TravelingSalesman
{
    public static class CitySeed
    {
        public static List<City> SeedData(int count)
        {
            List<City> cities = new List<City>();
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                var x = random.Next() % 1001 - 500;
                var y = random.Next() % 1001 - 500;
                cities.Add(new City()
                {
                   Location = new Point(x, y),
                   Name = i.ToString()
                });    
            }

            return cities;
        }
    }
}