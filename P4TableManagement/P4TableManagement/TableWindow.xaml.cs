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
    /// Interaction logic for TableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window
    {
        public TableWindow()
        {
            InitializeComponent();
        }

        private void btnCombine_Click(object sender, RoutedEventArgs e)
        {
            TableTextBox.Text = $"Which table(s) do you want to combine with {TableTextBox.Text}";
            // husk at ændre størrelsen på textboxen
        }

        private void btnAssign_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUnassign_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSeperate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
