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
    /// Interaction logic for EditReservationWindow.xaml
    /// </summary>
    public partial class EditReservationWindow : Window
    {
        string Parameters;
        readonly TableManagementSystem tableManagementSystem = ((MainWindow)Application.Current.MainWindow).tableManagementSystem;
        private Reservation reservation = ((MainWindow)Application.Current.MainWindow).highlightedReservation;
        private bool IsGap = false;

        public EditReservationWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Confirm button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // We get numberofguest_etc, date, parameters and comments
            string guests = new TextRange(RichGuest.Document.ContentStart, RichGuest.Document.ContentEnd).Text;
            string time = new TextRange(RichTime.Document.ContentStart, RichTime.Document.ContentEnd).Text;
            string name = new TextRange(RichName.Document.ContentStart, RichName.Document.ContentEnd).Text; 
            string phone = new TextRange(RichTelephoneNumber.Document.ContentStart, RichTelephoneNumber.Document.ContentEnd).Text; 
            string comment = new TextRange(RichComment.Document.ContentStart, RichComment.Document.ContentEnd).Text;

            // Control that makes sure that Richtextboxes aren't empty or whitespace
            if (string.IsNullOrWhiteSpace(guests))
            {
                MessageBox.Show("du har ikke skrevet i første felt");
                return;
            }
            else if (string.IsNullOrWhiteSpace(time))
            {
                MessageBox.Show("du har ikke skrevet i Date");
                return;
            }
            else if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("du har ikke skrevet i Date");
                return;
            }
            else if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("du har ikke skrevet i Date");
                return;
            }
            else if (string.IsNullOrWhiteSpace(comment))
            {
                MessageBox.Show("du har ikke skrevet i comments");
                return;
            }
            GetParameters();
            EditReservation(guests, time, name, phone, comment);
        }

        private void GetParameters()
        {
            if (CheckAquarium.IsChecked == true)
            {
                Parameters += $"{CheckAquarium.Content} ";
            }
            if (CheckWindow.IsChecked == true)
            {
                Parameters += $"{CheckWindow.Content} ";
            }
            if (CheckFlag.IsChecked == true)
            {
                Parameters += $"{CheckFlag.Content} ";
            }
            if (CheckGap.IsChecked == true)
            {
                IsGap = true;
            }
            if (CheckBuffet.IsChecked == true)
            {
                Parameters += $"{CheckBuffet.Content} ";
            }
            if (CheckPlayroom.IsChecked == true)
            {
                Parameters += $"{CheckPlayroom.Content} ";
            }
            if (CheckBabychair.IsChecked == true)
            {
                Parameters += $"{CheckBabychair.Content} ";
            }
            else
            {
                Parameters += "no paramters";
            }
        }
        private void EditReservation(string guests, string time, string name, string phone, string comment)
        {
            // Opdaterer reservationen
            reservation.numberOfGuests = Int32.Parse(guests);
            reservation.stringTime = time;
            reservation.name = name;
            reservation.phoneNumber = Int32.Parse(phone);
            reservation.comment = comment;
            reservation.isGap = IsGap;

            ((MainWindow)Application.Current.MainWindow).ReservationListView.Items.Refresh();
            this.Close();
        }
    }
}
