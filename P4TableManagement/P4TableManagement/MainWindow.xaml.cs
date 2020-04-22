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

            List<Cell> items = new List<Cell>();
            items.Add(new Cell() { X = 69, Y = 42, Z = 420});

            lvCells.ItemsSource = items;

        }

        public class Cell
        {
            public int X { get; set; }

            public int Y { get; set; }

            public int Z { get; set; }
        }


        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DrawGameArea();
        }

        const int SquareSize = 50;

        private void DrawGameArea()
        {
            bool doneDrawingBackground = false;
            int nextX = 0, nextY = 0;
            int rowCounter = 0;
            bool nextIsOdd = false;

            while (doneDrawingBackground == false)
            {
                //Rectangle rect = new Rectangle
                //{
                //    Width = SquareSize,
                //    Height = SquareSize,
                //    //Fill = nextIsOdd ? Brushes.White : Brushes.Black,
                //    Stroke = Brushes.Black
                //};

                Button rect = new Button
                {
                    Width = SquareSize,
                    Height = SquareSize

                };

                Area.Children.Add(rect);
                Canvas.SetTop(rect, nextY);
                Canvas.SetLeft(rect, nextX);

                nextIsOdd = !nextIsOdd;
                nextX += SquareSize;
                if (nextX >= Area.ActualWidth)
                {
                    nextX = 0;
                    nextY += SquareSize;
                    rowCounter++;
                    nextIsOdd = (rowCounter % 2 != 0);
                }

                if (nextY >= Area.ActualHeight)
                    doneDrawingBackground = true;
            }
        }



















    }
}
