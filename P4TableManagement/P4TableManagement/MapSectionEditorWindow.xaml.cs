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
                    Name = $"_{ x }_{ y }",
                    Opacity = 0.2
                };
                x++;

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
            LoadMapElements();
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
                Content = $"Table "
            };
            
            // Change to dynamic location (Drag and drop)
            Canvas.SetLeft(button, 410);
            Canvas.SetTop(button, 210);
            Canvas.Children.Add(button);
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Button button = (Button)sender;
            DataObject dataObj = new DataObject(new Button() { Content = "Table", Width = tableSize, Height = tableSize, BorderThickness = new Thickness(2), Background = Brushes.White });
            DragDrop.DoDragDrop(button, dataObj, DragDropEffects.Copy);
        }

        private void Canvas_Drop(object sender, DragEventArgs e)
        {
            var button = (Button)e.Data.GetData(typeof(Button));

            FindHit(e.GetPosition(Canvas));

            if (_hitRectangle is Rectangle)
            {  
                Canvas.SetTop(button, Canvas.GetTop(_hitRectangle) + 10);
                Canvas.SetLeft(button, Canvas.GetLeft(_hitRectangle) + 10);

                Canvas.Children.Add(button);
            }

            if (_mouseHitType == HitType.None) return;

            if (HitButton is Button)
            {

            }
            //var x = Mouse.GetPosition(Canvas);

            //Canvas.SetLeft(button, Mouse.GetPosition(Canvas).X);
            //Canvas.SetTop(button, Mouse.GetPosition(Canvas).Y);
            ////Canvas.SetLeft(button, );
            ////Canvas.SetTop(button, Mouse.GetPosition(Canvas));

            //Canvas.SetLeft(button, 410);
            //Canvas.SetTop(button, 210);

            //Canvas.Children.Add(button);
        }

        private void FindHit(Point point)
        {
            _hitRectangle = null;
            HitButton = null;
            _mouseHitType = HitType.None;

            foreach (Button button in Canvas.Children.OfType<Button>())
            {
                _mouseHitType = SetHitType(button, point);
                if (_mouseHitType != HitType.None)
                {
                    HitButton = button;
                    //return;
                }
            }

            foreach (Rectangle rectangle in Canvas.Children.OfType<Rectangle>())
            {
                _mouseHitType = SetHitType(rectangle, point);
                if (_mouseHitType != HitType.None)
                {
                    _hitRectangle = rectangle;
                    return;
                }
            }

            // We didn't find a hit.
            return;
        }

        private HitType SetHitType(Button button, Point point)
        {
            double left = Canvas.GetLeft(button);
            double top = Canvas.GetTop(button);
            double right = left + button.Width;
            double bottom = top + button.Height;
            if (point.X < left) return HitType.None;
            if (point.X > right) return HitType.None;
            if (point.Y < top) return HitType.None;
            if (point.Y > bottom) return HitType.None;

            const double GAP = 10;

            if (bottom - point.Y < GAP) return HitType.B;
            return HitType.Body;
        }

        private HitType SetHitType(Rectangle rectangle, Point point)
        {
            double left = Canvas.GetLeft(rectangle);
            double top = Canvas.GetTop(rectangle);
            double right = left + rectangle.Width;
            double bottom = top + rectangle.Height;
            if (point.X < left) return HitType.None;
            if (point.X > right) return HitType.None;
            if (point.Y < top) return HitType.None;
            if (point.Y > bottom) return HitType.None;

            const double GAP = 10;

            if (bottom - point.Y < GAP) return HitType.B;
            return HitType.Body;
        }

        private enum HitType
        {
            None, Body, UL, UR, LR, LL, L, R, T, B
        };

        private HitType _mouseHitType = HitType.None;

        // The Rectangle that was hit.
        private Rectangle _hitRectangle = null;

        public Button HitButton = null;

        // The Rectangles that the user can move and resize.
        private readonly List<Rectangle> Rectangles = new List<Rectangle>();
    }
}
