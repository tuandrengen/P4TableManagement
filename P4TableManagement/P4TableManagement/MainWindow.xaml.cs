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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace P4TableManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // We initialize the Rows and Columns (and two colors)
            MapTest.Rows = 25;
            MapTest.Columns = 25;

            SolidColorBrush Color1 = new SolidColorBrush(Colors.YellowGreen);
            SolidColorBrush Color2 = new SolidColorBrush(Colors.White);

            // Populate the map with REEEctangles and adds a color to them
            for (int i = 0; i <= 625; i++)
            {
                MapTest.Children.Add(new Rectangle { Fill = Color1 });
                MapTest.Children.Add(new Rectangle { Fill = Color2});
            }

        }
    }
}
