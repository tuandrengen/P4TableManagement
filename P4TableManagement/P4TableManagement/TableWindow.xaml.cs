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
        // To access the data from MainWindow we: ((MainWindow)Application.Current.MainWindow).tableManagementSystem
        // This allows us to use the created object in the MainWindow

        readonly TableManagementSystem tableManagementSystem = ((MainWindow)Application.Current.MainWindow).tableManagementSystem;
        readonly Table table = ((MainWindow)Application.Current.MainWindow).currentTable;

        public TableWindow()
        {
            InitializeComponent();
            
        }

        private void btnUnassign_Click(object sender, RoutedEventArgs e)
        {

            //MessageBox.Show(tableManagementSystem.TableList.Count.ToString());
            //MessageBox.Show(table.ToString());

            //Table table = tableManagementSystem.TableList.Find(x => $"Table {x.tableNumber}" == (string)clickedButton.Content);


            //MessageBox.Show(table.ToString());

            //Table test = tableManagementSystem.TableList.Find(x => $"Table {x.tableNumber}" == (string)clickedButton.Content);
            //MessageBox.Show(test.tableNumber.ToString());

            //tableManagementSystem.UnassignTable(tableManagementSystem.TableList.Find(x => $"Table {x.tableNumber}" == (string)clickedButton.Content));

            tableManagementSystem.UnassignTable(table);
        }

        private void btnSeperate_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
