using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TravelingSalesmanWpf.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnWindowInitialized(object sender, EventArgs e)
        {
            DrawGrid();
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
                        Visibility = Visibility.Hidden,
                        Width = CartesianGrid.Width / CartesianGrid.ColumnDefinitions.Count,
                        Height = CartesianGrid.Height / CartesianGrid.RowDefinitions.Count
                    };


                    Grid.SetRow(border, i);
                    Grid.SetColumn(border, j);

                    CartesianGrid.Children.Add(border);
                }
            }
        }
    }
}
