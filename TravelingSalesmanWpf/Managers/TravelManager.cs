using System.Collections.Generic;
using TravelingSalesman.Extensions;
using TravelingSalesman.Models;

namespace TravelingSalesman.Managers
{
    public class TravelManager
    {
        private List<City> Cities { get; }

        public TravelManager(City start, List<City> cities)
        {
            Cities = cities;
            Cities.Remove(start);
        }

        public City FindNearestNeighbour(City city, ref double routeLength)
        {
            int CompareByLength(City city1, City city2)
            {
                if (ReferenceEquals(city, city1))
                    return -1;

                if (ReferenceEquals(city, city2))
                    return 1;

                double locationToFirst = city.Location.DistanceTo(city1.Location);
                double locationToSecond = city.Location.DistanceTo(city2.Location);

                if (locationToFirst < locationToSecond)
                    return -1; //city1 closer

                if (locationToFirst > locationToSecond)
                    return 1; //city2 closer


                return 0;
            }

            Cities.Sort(CompareByLength);
            var nearestCity = Cities[0];

            routeLength += city.Location.DistanceTo(nearestCity.Location);

            Cities.Remove(nearestCity);
            return nearestCity;
        }
    }
}