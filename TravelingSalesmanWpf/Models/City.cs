using System;
using System.Windows;

namespace TravelingSalesmanWpf.Models
{
    [Serializable]
    public class City
    {
        public string Name { get; set; }
        public Point Location { get; set; }
    }
}