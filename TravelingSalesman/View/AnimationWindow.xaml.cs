using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using TravelingSalesman.Managers;
using TravelingSalesman.Models;
using TravelingSalesman.Seed;

namespace TravelingSalesman.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AnimationWindow : Window
    {
        private List<City> Cities { get; set; }

        public AnimationWindow(int cityCount, int rowCount, int columnCount)
        {
            InitializeComponent();

            this.WindowState = WindowState.Maximized;

            Cities = CitySeed.SeedData(cityCount, rowCount, columnCount);
            PrepareTable(rowCount, columnCount);

            DrawGrid();
            GenerateCities();
        }

        public async void Animate()
        {
            Random random = new Random();
            var start = Cities[random.Next(Cities.Count)];
            TravelManager manager = new TravelManager(start, Cities);

            var temp = Cities.ToArray();

            City next = null;
            double routeLength = 0d;
            for (int i = 0; i < temp.Length; i++)
            {
                var current = i == 0 ? start : next;
                next = manager.FindNearestNeighbour(current, ref routeLength);
                await ConnectCities(current, next);
            }
        }

        private Task ConnectCities(City first, City second)
        {
            Point firstPoint = CartesianGrid.Children.Cast<UIElement>().First(r =>
                r is Rectangle && Grid.GetRow(r) == (int)first.Location.Y &&
                Grid.GetColumn(r) == (int)first.Location.X).TransformToAncestor(this).Transform(new Point(0, 0));

            Point secondPoint = CartesianGrid.Children.Cast<UIElement>().First(r =>
                r is Rectangle && Grid.GetRow(r) == (int)second.Location.Y &&
                Grid.GetColumn(r) == (int)second.Location.X).TransformToAncestor(this).Transform(new Point(0, 0));


            Line line = new Line
            {
                X1 = firstPoint.X + ActualWidth / CartesianGrid.ColumnDefinitions.Count / 2,
                Y1 = firstPoint.Y + ActualHeight / CartesianGrid.RowDefinitions.Count / 2,
                X2 = secondPoint.X + ActualWidth / CartesianGrid.ColumnDefinitions.Count / 2,
                Y2 = secondPoint.Y + ActualHeight / CartesianGrid.RowDefinitions.Count / 2,
                Stroke = new SolidColorBrush(new Color()
                {
                    A = 150,
                    R = 0,
                    G = 255,
                    B = 0
                }),
                StrokeThickness = 5d,
                Visibility = Visibility.Visible,
            };

            Grid.SetRowSpan(line, CartesianGrid.RowDefinitions.Count);
            Grid.SetColumnSpan(line, CartesianGrid.ColumnDefinitions.Count);

            CartesianGrid.Children.Add(line);
            return Task.Delay(1000);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var children in CartesianGrid.Children)
            {
                if (children is Border border)
                    border.Visibility = Visibility.Visible;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var children in CartesianGrid.Children)
            {
                if (children is Border border)
                    border.Visibility = Visibility.Hidden;
            }
        }

        private void GenerateCities()
        {
            foreach (var city in Cities)
            {
                Rectangle rectangle = new Rectangle()
                {
                    Width = CartesianGrid.Width / CartesianGrid.ColumnDefinitions.Count,
                    Height = CartesianGrid.Height / CartesianGrid.RowDefinitions.Count,
                    Fill = new SolidColorBrush(new Color()
                    {
                        A = 255,
                        R = 255,
                        G = 0,
                        B = 0
                    }),
                    Visibility = Visibility.Visible
                };

                Grid.SetRow(rectangle, (int)city.Location.Y);
                Grid.SetColumn(rectangle, (int)city.Location.X);

                CartesianGrid.Children.Add(rectangle);

            }
        }

        private void PrepareTable(int rowCount, int columnCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                RowDefinition rowDefinition = new RowDefinition { Height = new GridLength(1, GridUnitType.Star) };
                CartesianGrid.RowDefinitions.Add(rowDefinition);
            }

            for (int i = 0; i < columnCount; i++)
            {
                ColumnDefinition rowDefinition = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
                CartesianGrid.ColumnDefinitions.Add(rowDefinition);
            }
        }

        private void DrawGrid()
        {
            int rowCount = CartesianGrid.RowDefinitions.Count;
            int columnCount = CartesianGrid.ColumnDefinitions.Count;

            //Drawing grid
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    Border border = new Border
                    {
                        BorderThickness = new Thickness(0,
                            i == 0 ? 1 : 0, j == columnCount ? 0 : 1, 1),
                        BorderBrush = new SolidColorBrush(new Color()
                        {
                            A = 100,
                            R = 0,
                            G = 0,
                            B = 0
                        }),
                        Visibility = Visibility.Visible,
                    };

                    Grid.SetRow(border, i);
                    Grid.SetColumn(border, j);

                    CartesianGrid.Children.Add(border);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Animate();
        }
    }
}
