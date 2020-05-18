using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for MapSectionEditor.xaml
    /// </summary>
    public partial class MapSectionEditor : Window
    {
        public MapSectionEditor()
        {
            InitializeComponent();
            //DrawCanvas();
        }


        private readonly List<Rectangle> AllRectangles = new List<Rectangle>();

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DrawCanvas();
        }

        private void DrawCanvas()
        {
            int SquareSize = 100;
            bool doneDrawingBackground = false;
            int nextX = 0, nextY = 0;
            int rowCounter = 0; // If _nextIsOdd is not being used, then same for this boy
            bool _nextIsOdd = false; //Not being used we only set it all the time?
            int x = 1;
            int y = 1;

            // Drawing the grid of rectangles
            while (!doneDrawingBackground)
            {
                //string name = "_1";
                Rectangle rectangle = new Rectangle
                {
                    Width = SquareSize,
                    Height = SquareSize,
                    //Background = Brushes.White,
                    Stroke = Brushes.Black,
                    Fill = Brushes.White,
                    Name = $"_{ x }_{ y }"
                };
                x++;

                //ID++;
                //letter++;

                Canvas.Children.Add(rectangle);
                AllRectangles.Add(rectangle);
                Canvas.SetTop(rectangle, nextY);
                Canvas.SetLeft(rectangle, nextX);

                _nextIsOdd = !_nextIsOdd;
                nextX += SquareSize;
                if (nextX >= Canvas.ActualWidth)
                {
                    nextX = 0;
                    nextY += SquareSize;
                    rowCounter++;
                    _nextIsOdd = (rowCounter % 2 != 0);
                    x = 1;
                    y++;
                }

                if (nextY >= Canvas.ActualHeight - 80)
                {
                    doneDrawingBackground = true;
                    _nextIsOdd = false;
                }
            }
        }

        void LoadMapElements()
        {
            LoadTables();
            LoadDecorationElements();
        }

        void LoadTables()
        {

        }

        void LoadDecorationElements()
        {

        }

        void SaveMapElements()
        {
            SaveTables();
            SaveDecorationElements();
        }

        void SaveTables()
        {
            string path = @"C:\P4\Tables.csv";
            using(var writer = new StreamWriter(path, false))
            {
                foreach (Button button in Canvas.Children)
                {
                    string[] category = button.Content.ToString().Split('L', 'S', 'C');
                    string log;

                    if (category[0] == "L")
                    {
                        log = $"{ category[1] };{ category[0] };{ Canvas.GetLeft(button) };{ Canvas.GetTop(button) };{ button.Height };{ button.Width }";
                    }
                    else if (category[0] == "S")
                    {
                        log = $"{ category[1] };{ category[0] };{ Canvas.GetLeft(button) };{ Canvas.GetTop(button) };{ button.Height };{ button.Width }";
                    }
                    else if (category[0] == "C")
                    {
                        log = $"{ category[1] };{ category[0] };{ Canvas.GetLeft(button) };{ Canvas.GetTop(button) };{ button.Height };{ button.Width }";
                    }
                    else
                    {
                        throw new Exception();
                    }
                    
                    writer.WriteLine(log);
                    writer.Close();
                }
            }
        }

        void SaveDecorationElements()
        {

        }

    }
}
