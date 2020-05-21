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
        public string tableFilePath { get; set; }
        public string decorationElementFilePath { get; set; }
        public MapSectionEditor(string fileName)
        {
            InitializeComponent();

            LabelTitle.Content = fileName;
            tableFilePath = $@"C:\P4\MapSections\{fileName}.csv";
            decorationElementFilePath = $@"C:\P4\DecorationElements\{fileName}.csv";
        }

        private readonly List<Rectangle> AllRectangles = new List<Rectangle>();

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DrawCanvas();
            LoadMapElements();
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
                Rectangle rectangle = new Rectangle
                {
                    Width = SquareSize,
                    Height = SquareSize,
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

            using (var reader = new StreamReader(tableFilePath))
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
                    Content = $"{ table[1] };Table { table[0] }",
                    Background = Brushes.White,
                    BorderThickness = new Thickness(2.0)
                };
                if (table[1] == "S")
                {
                    button.Width = tableSize;
                    button.Height = tableSize;
                }
                else if (table[1] == "L")
                {
                    button.Width = tableSize * 2 + 20;
                    button.Height = tableSize;
                }

                Canvas.SetTop(button, int.Parse(table[3]));
                Canvas.SetLeft(button, int.Parse(table[2]));

                button.MouseDown += MoveExistingTable_Event;

                Canvas.Children.Add(button);

                _tableid++;
            }
        }

        private void MoveExistingTable_Event(object sender, MouseButtonEventArgs e)
        {
            UIElement button = (UIElement)sender;
            if (button is Button)
            {
                _isButton = true;
                _isEllipse = false;
            }
            else
            {
                _isButton = false;
                _isEllipse = true;
            }
            Canvas.Children.Remove(button);
            DataObject dataObj = new DataObject(button);
            DragDrop.DoDragDrop(button, dataObj, DragDropEffects.Copy);
            if (_dropFailed)
            {
                Canvas.Children.Add(button);
            }
        }

        void LoadDecorationElements()
        {
            List<string[]> de = new List<string[]>();

            using (var reader = new StreamReader(decorationElementFilePath))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var table = line.Split(';');

                    de.Add(table);
                }
                reader.Close();
            }

            foreach (var element in de)
            {
                Ellipse ellipse = new Ellipse()
                {
                    Width = 100,
                    Height = 100,
                    Name = $"{ element[1] }_{ element[0] }"
                };

                switch (element[1])
                {
                    case "Window":
                        ellipse.Fill = Brushes.Aqua;
                        break;
                    case "Bar":
                        ellipse.Fill = Brushes.Green;
                        break;
                    case "Aquarium":
                        ellipse.Fill = Brushes.Chartreuse;
                        break;
                    case "Softice":
                        ellipse.Fill = Brushes.Beige;
                        break;
                    case "Kitchen":
                        ellipse.Fill = Brushes.Tomato;
                        break;
                    case "Buffet":
                        ellipse.Fill = Brushes.BurlyWood;
                        break;
                    default:
                        break;
                }

                Canvas.SetTop(ellipse, int.Parse(element[3]));
                Canvas.SetLeft(ellipse, int.Parse(element[2]));

                ellipse.MouseDown += MoveExistingTable_Event;

                Canvas.Children.Add(ellipse);

                _decorationElement++;
            }
        }

        void SaveMapElements()
        {
            SaveTables();
            SaveDecorationElements();
        }

        void SaveTables()
        {
            using (var writer = new StreamWriter(tableFilePath, false))
            {
                writer.WriteLine("no;category;x;y");

                foreach (Button button in Canvas.Children.OfType<Button>())
                {
                    string[] yes = button.Content.ToString().Split(';');
                    string category = yes[0];
                    string id = yes[1].Replace("Table ", "");
                    string log = $"{ id };{ category };{ Canvas.GetLeft(button) };{ Canvas.GetTop(button) }";

                    writer.WriteLine(log);
                }
                writer.Close();
            }
        }

        void SaveDecorationElements()
        {
            using (var writer = new StreamWriter(decorationElementFilePath, false))
            {
                writer.WriteLine("no;type;x;y");

                foreach (Ellipse ellipse in Canvas.Children.OfType<Ellipse>())
                {
                    string[] yes = ellipse.Name.Split('_');
                    string type = yes[0];
                    string id = yes[1];
                    string log = $"{ id };{ type };{ Canvas.GetLeft(ellipse) };{ Canvas.GetTop(ellipse) }";

                    writer.WriteLine(log);
                }
                writer.Close();
            }
        }

        private void MainWindow_ClickEvent(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            mainWindow.Closed += (s, args) => this.Close();
            this.Hide();
        }

        static int _tableid = 1;
        static int _decorationElement = 1;


        private void DragElement_Event(object sender, MouseButtonEventArgs e)
        {
            Button button = (Button)sender;
            Button newButton = new Button() { 
                Content = $"Table { _tableid }", 
                Width = tableSize, 
                Height = tableSize, 
                BorderThickness = new Thickness(2), 
                Background = Brushes.White };
            newButton.MouseDown += MoveExistingTable_Event;
            DataObject dataObj = new DataObject(newButton);
            DragDrop.DoDragDrop(button, dataObj, DragDropEffects.Move);
            _tableid++;
        }

        bool _dropFailed = false;
        bool _isButton = false;
        bool _isEllipse = false;
        private void CanvasDrop_Event(object sender, DragEventArgs e)
        {
            UIElement button;
            if (_isEllipse)
            {
                button = (Ellipse)e.Data.GetData(typeof(Ellipse));
            }
            else
            {
                button = (Button)e.Data.GetData(typeof(Button));
            }

            _dropFailed = false;


            FindHit(e.GetPosition(Canvas));

            if (HitButton is Button)
            {
                System.Windows.Forms.MessageBox.Show("Table cannot be placed on top of another table!");
                _dropFailed = true;
                return;
            }

            if (_hitRectangle is Rectangle)
            {  
                if (_isButton)
                {
                    Canvas.SetTop(button, Canvas.GetTop(_hitRectangle) + 10);
                    Canvas.SetLeft(button, Canvas.GetLeft(_hitRectangle) + 10);
                }
                if (_isEllipse)
                {
                    Canvas.SetTop(button, Canvas.GetTop(_hitRectangle));
                    Canvas.SetLeft(button, Canvas.GetLeft(_hitRectangle));
                }

                Canvas.Children.Add(button);
            }

            if (_mouseHitType == HitType.None) return;

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
                    return;
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

        private void SmallTable_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Button button = (Button)sender;
            _isButton = true;
            _isEllipse = false;
            Button newButton = new Button()
            {
                Content = $"S;Table { _tableid }",
                Width = tableSize,
                Height = tableSize,
                BorderThickness = new Thickness(2),
                Background = Brushes.White
            };
            newButton.MouseDown += MoveExistingTable_Event;
            DataObject dataObj = new DataObject(newButton);
            DragDrop.DoDragDrop(button, dataObj, DragDropEffects.Copy);
            _tableid++;
        }

        private void LargeTable_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Button button = (Button)sender;
            _isButton = true;
            _isEllipse = false;
            Button newButton = new Button()
            {
                Content = $"L;Table { _tableid }",
                Width = tableSize + tableSize + 20,
                Height = tableSize,
                BorderThickness = new Thickness(2),
                Background = Brushes.White
            };
            newButton.MouseDown += MoveExistingTable_Event;
            DataObject dataObj = new DataObject(newButton);
            DragDrop.DoDragDrop(button, dataObj, DragDropEffects.Copy);
            _tableid++;
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Button button = (Button)sender;
            _isEllipse = true;
            _isButton = false;
            Ellipse newButton = new Ellipse()
            {
                Name = $"{ button.Content }_{ _decorationElement }",
                Width = 100,
                Height = 100,
                Fill = button.Background
            };
            newButton.MouseDown += MoveExistingTable_Event;
            DataObject dataObj = new DataObject(newButton);
            DragDrop.DoDragDrop(button, dataObj, DragDropEffects.Copy);
            _decorationElement++;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveMapElements();
            System.Windows.Forms.MessageBox.Show("The map section has been saved.");
        }

        private void ClearDecorationElements_Click(object sender, RoutedEventArgs e)
        {
            string text = "Do you want to clear ALL decoration elements?";
            string caption = "Are you sure?";
            MessageBoxResult result = MessageBox.Show(text, caption, MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                ClearDE();
            }
        }

        private void ClearTables_Click(object sender, RoutedEventArgs e)
        {
            string text = "Do you want to clear ALL tables?";
            string caption = "Are you sure?";
            MessageBoxResult result = MessageBox.Show(text, caption, MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                ClearTableElements();
            }
        }

        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            string text = "Do you want to clear everything?";
            string caption = "Are you sure?";
            MessageBoxResult result = MessageBox.Show(text, caption, MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                ClearTableElements();
                ClearDE();
            }
        }

        void ClearDE()
        {
            foreach (Ellipse item in Canvas.Children.OfType<Ellipse>().ToList())
            {
                Canvas.Children.Remove(item);
            }
            _decorationElement = 1;
        }

        void ClearTableElements()
        {
            foreach (Button item in Canvas.Children.OfType<Button>().ToList())
            {
                Canvas.Children.Remove(item);
            }
            _tableid = 1;
        }

        private void ResetTableNo_Click(object sender, RoutedEventArgs e)
        {
            string text = "Do you want to reset the table numbers?";
            string caption = "Are you sure?";
            MessageBoxResult result = MessageBox.Show(text, caption, MessageBoxButton.YesNo);
            
            if (result == MessageBoxResult.Yes)
            {
                List<Button> tables = new List<Button>();
                foreach (var item in Canvas.Children.OfType<Button>())
                {
                    tables.Add(item);
                }

                ClearTableElements();

                foreach (var item in tables)
                {
                    if (item.Content.ToString().Contains("L"))
                    {
                        item.Content = $"L;Table { _tableid }";
                    }
                    else if (item.Content.ToString().Contains("S"))
                    {
                        item.Content = $"S;Table { _tableid }";
                    }

                    Canvas.Children.Add(item);
                    _tableid++;
                }
            }

        }
    }
}
