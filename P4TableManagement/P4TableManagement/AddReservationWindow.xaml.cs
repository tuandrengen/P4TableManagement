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
    /// Interaction logic for AddReservationWindow.xaml
    /// </summary>
    public partial class AddReservationWindow : Window
    {
        string Parameters;
        readonly TableManagementSystem tableManagementSystem = ((MainWindow)Application.Current.MainWindow).tableManagementSystem;
        private bool IsGap = false;

        public AddReservationWindow()
        {
            InitializeComponent();
        }

        // Cross button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Add button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // We get numberofguest_etc, date, parameters and comments
            string richText1 = new TextRange(NumberofguestETCRich.Document.ContentStart, NumberofguestETCRich.Document.ContentEnd).Text;
            string richTextDate = new TextRange(DateRich.Document.ContentStart, DateRich.Document.ContentEnd).Text; // Burde nok opdateres så den bliver til en DateTime

            string comment = new TextRange(CommentsRich.Document.ContentStart, CommentsRich.Document.ContentEnd).Text;

            if (string.IsNullOrWhiteSpace(richText1))
            {
                MessageBox.Show("du har ikke skrevet i første felt");
                return;
            }
            else if (string.IsNullOrWhiteSpace(richTextDate))
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
            CreateReservation(richText1, comment);
        }

        private void GetParameters()
        {
            if (AquariumCheckBox.IsChecked == true)
            {
                Parameters += $"{AquariumCheckBox.Content} ";
            }
            if (WindowCheckBox.IsChecked == true)
            {
                Parameters += $"{WindowCheckBox.Content} ";
            }
            if (FlagCheckBox.IsChecked == true)
            {
                Parameters += $"{FlagCheckBox.Content} ";
            }
            if (GapCheckBox.IsChecked == true)
            {
                IsGap = true;
            }
            if (BuffetCheckBox.IsChecked == true)
            {
                Parameters += $"{BuffetCheckBox.Content} ";
            }
            if (PlayroomCheckBox.IsChecked == true)
            {
                Parameters += $"{PlayroomCheckBox.Content} ";
            }
            if (BabychairCheckBox.IsChecked == true)
            {
                Parameters += $"{BabychairCheckBox.Content} ";
            }
            else
            {
                Parameters += "INGENTING";
            }
        }

        // Mangler kontrolstrukturer der tjekker om der er skrevet i den korrekte format e.g. "4, 18:30, karsten, 69696969"
        private void CreateReservation(string s, string comment)
        {
            string[] seperator = { ", ", ", "};

            string[] list = s.Split(seperator, 4, StringSplitOptions.None);

            // DateTime skal ikke være Now xD og ID er bare sat til den næste umiddelbart
            Reservation reservation = new Reservation(8 ,list[2], DateTime.Now, IsGap, Int32.Parse(list[0]), Int32.Parse(list[3]), Parameters, comment);
            reservation.stringTime = list[1];

            tableManagementSystem.ReservationList.Add(reservation);
            ((MainWindow)Application.Current.MainWindow).ReservationListView.Items.Refresh();
            this.Close();
        }
    }
}
