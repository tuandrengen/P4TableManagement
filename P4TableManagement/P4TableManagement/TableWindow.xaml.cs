using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace P4TableManagement
{
    /// <summary>
    /// Interaction logic for TableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window
    {
        // To access the data from MainWindow we: ((MainWindow)Application.Current.MainWindow).tableManagementSystem
        // This allows us to use the created object in the MainWindow

        readonly TableManagementSystem tableManagementSystem = ((MainWindow)Application.Current.MainWindow).tableManagementSystem;
        readonly Table table = ((MainWindow)Application.Current.MainWindow).currentTable;
        readonly CombinedTable<Table> combinedTable = ((MainWindow)Application.Current.MainWindow).currentCombinedTable;
        readonly Button button = ((MainWindow)Application.Current.MainWindow).sourceButton;
        readonly Button hitButton = ((MainWindow)Application.Current.MainWindow).HitButton;
        readonly Reservation reservation = ((MainWindow)Application.Current.MainWindow).highlightedReservation;

        public TableWindow()
        {
            InitializeComponent();
        }

        private void btnUnassign_Click(object sender, RoutedEventArgs e)
        {
            tableManagementSystem.UnassignTable(table);
            button.Background = Brushes.White;
            //Updates the ListViews
            tableManagementSystem.ReservationList.Add(reservation);
            tableManagementSystem.AssignedReservationList.Remove(tableManagementSystem.ReservationList.Find(x => x.id == reservation.id));

            ((MainWindow)Application.Current.MainWindow).ReservationListView.Items.Refresh();
            ((MainWindow)Application.Current.MainWindow).AssignedReservationListView.Items.Refresh();
        }

        private void btnSeperate_Click(object sender, RoutedEventArgs e)
        {
            tableManagementSystem.SeparateTables(combinedTable);

            foreach (var item in combinedTable.combinedTables)
            {
                ((MainWindow)Application.Current.MainWindow).Area.Children.OfType<Button>().ToList().Find(x => (string)x.Content == $"Table { item.tableNumber }").Visibility = Visibility.Visible;
            }
            var yes = ((MainWindow)Application.Current.MainWindow).Area.Children.OfType<Button>().ToList().Find(x => (string)x.Content == $"*{button.Content}");

            ((MainWindow)Application.Current.MainWindow).Area.Children.Remove(yes);
            MessageBox.Show("Seperated");
            this.Close();

            // Call function from mainwindow that draws the tables that were seperated
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Cross button right top corner
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RadioAvailable_Checked(object sender, RoutedEventArgs e)
        {
            hitButton.Background = Brushes.White;
            table.state = "Available";
        }

        private void RadioOccupied_Checked(object sender, RoutedEventArgs e)
        {
            hitButton.Background = Brushes.Red;
            table.state = "Occupied";
        }

        private void RadioPaid_Checked(object sender, RoutedEventArgs e)
        {
            hitButton.Background = Brushes.Yellow;
            table.state = "Paid";
        }
    }
}
