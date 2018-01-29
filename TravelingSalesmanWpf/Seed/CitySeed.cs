using System;
using System.Collections.Generic;
using System.Windows;
using TravelingSalesmanWpf.Models;

namespace TravelingSalesmanWpf.Seed
{
    public static class CitySeed
    {
        public static List<City> SeedData(int count, int maxRow, int maxColumn)
        {
            List<City> cities = new List<City>();
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                var x = random.Next() % maxColumn;
                var y = random.Next() % maxRow;
                
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