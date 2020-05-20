using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for AddWalkInWindow.xaml
    /// </summary>
    public partial class AddWalkInWindow : Window
    {
        string Parameters;
        readonly TableManagementSystem tableManagementSystem = ((MainWindow)Application.Current.MainWindow).tableManagementSystem;
        private bool IsGap = false;

        public AddWalkInWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GetParameters()
        {
            if (GapCheckBox.IsChecked == true)
            {
                IsGap = true;
            }
            else
            {
                Parameters += "INGENTING";
            }
        }

        private void CreateWalkIn(string s)
        {
            //WalkIn walkIn = new WalkIn(Int32.Parse(s),IsGap);

            Reservation reservation = new Reservation("Walk-in", DateTime.Now, IsGap, Int32.Parse(s), 69696969); // hehe ReservationListView viser kun fra reservationslisten så burde man gøre det til en liste der har bookings istedet? List<Booking> etc...?
            reservation.stringTime = reservation.timeStart.ToString("HH:mm"); //converts the Datetime.Now to a string only showing the HH(hour) MM(minutes)

            tableManagementSystem.ReservationList.Add(reservation);
            ((MainWindow)Application.Current.MainWindow).ReservationListView.Items.Refresh();
            this.Close();
        }

        // Add button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // We get numberofguest_etc, date, parameters and comments
            string richText1 = new TextRange(NumberOfGuestRich.Document.ContentStart, NumberOfGuestRich.Document.ContentEnd).Text;

            if (string.IsNullOrWhiteSpace(richText1))
            {
                MessageBox.Show("du har ikke skrevet i første felt");
                return;
            }

            GetParameters();
            CreateWalkIn(richText1);
        }
    }
}
