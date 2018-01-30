using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace TravelingSalesman.View
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

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(ColumnCountTextBox.Text, out int columnCount) ||
                !int.TryParse(RowCountTextBox.Text, out int rowCount) ||
                !int.TryParse(CityCountTextBox.Text, out int cityCount)) return;

            if (columnCount == 0 || rowCount == 0 || cityCount == 0)
                return;

            if (rowCount * columnCount < cityCount)
            {
                MessageBox.Show($"City count must be lower or equal to {rowCount * columnCount}.");
                return;
            }

            new AnimationWindow(cityCount, rowCount, columnCount).Show();
        }

        private void AreAllCharactersNumeric(object sender, TextCompositionEventArgs e)
        {
            if (!e.Text.Any(char.IsDigit))
                e.Handled = true;
        }
    }
}
