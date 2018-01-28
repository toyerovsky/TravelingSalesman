using System;
using System.Windows;

namespace TravelingSalesmanWpf.Extensions
{
    public static class PointExtensions
    {
        public static double DistanceTo(this Point point1, Point point2) =>
            Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
    }
}