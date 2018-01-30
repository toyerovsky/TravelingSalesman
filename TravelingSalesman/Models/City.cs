using System;
using System.Windows;

namespace TravelingSalesman.Models
{
    [Serializable]
    public class City
    {
        public string Name { get; set; }
        public Point Location { get; set; }
    }
}