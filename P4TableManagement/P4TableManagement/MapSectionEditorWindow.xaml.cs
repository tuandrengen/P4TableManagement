using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace P4TableManagement
{
    /// <summary>
    /// Interaction logic for MapSectionEditor.xaml
    /// </summary>
    public partial class MapSectionEditor : Window
    {
        public string filePath { get; set; }
        public MapSectionEditor(string fileName)
        {
            InitializeComponent();

            filePath = $@"C:\P4\MapSections\{fileName}.csv";
        }


        private readonly List<Rectangle> AllRectangles = new List<Rectangle>();
        private static int _tableno = 0;

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

                LoadMapElements();
            }
        }

        void LoadMapElements()
        {
            LoadTables();
            LoadDecorationElements();
        }

        int tableSize = 80;

        void LoadTables()
        {
            List<string[]> tables = new List<string[]>();

            using (var reader = new StreamReader(filePath))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var table = line.Split(';');

                    tables.Add(table);
                }
                reader.Close();
            }

            foreach (var table in tables)
            {
                Button button = new Button()
                {
                    Width = tableSize,
                    Height = tableSize,
                    Content = $"Table { table[0] }",
                    Background = Brushes.White,
                    BorderThickness = new Thickness(2.0)
                };

                Canvas.SetTop(button, int.Parse(table[2]));
                Canvas.SetLeft(button, int.Parse(table[1]));

                Canvas.Children.Add(button);
                _tableno++;
            }
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
            using (var writer = new StreamWriter(filePath, false))
            {
                writer.WriteLine("no;x;y");

                foreach (Button button in Canvas.Children.OfType<Button>())
                {
                    string id = button.Content.ToString().Replace("Table ", "");
                    //string id = button.Content.
                    string log = $"{ id };{ Canvas.GetLeft(button) };{ Canvas.GetTop(button) }";

                    writer.WriteLine(log);
                    writer.Close();
                }
            }
        }

        void SaveDecorationElements()
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //SaveMapElements();
            MainWindow mainWindow = new MainWindow();
            mainWindow.ShowDialog();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = new Button()
            {
                Height = tableSize,
                Width = tableSize,
                Content = $"Table { _tableno }"
            };
            
            // Change to dynamic location (Drag and drop)
            Canvas.SetLeft(button, 410);
            Canvas.SetTop(button, 210);
            Canvas.Children.Add(button);
        }
    }
}
