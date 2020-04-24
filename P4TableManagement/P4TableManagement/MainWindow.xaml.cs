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

            //List<Cell> items = new List<Cell>();
            //items.Add(new Cell() { X = 69, Y = 42, Z = 420});

            ////lvCells.ItemsSource = items;
            

            

            ListView.ItemsSource = AllButtons;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DrawGameArea();
            //PopulateButtonsList();
        }

        private void ListView_MouseLeftButtonDown (object sender, MouseButtonEventArgs e)
        {
            string selectedRect;
            if (e.OriginalSource is Rectangle)
            {
                var test = e.Source as FrameworkElement;
                selectedRect = test.Name;

                TextBox textBox = new TextBox
                {
                    Name = selectedRect
                };
            }
        }


        const int SquareSize = 100;
        public List<Button> AllButtons = new List<Button>();

        private void DrawGameArea()
        {
            bool doneDrawingBackground = false;
            int nextX = 0, nextY = 0;
            int rowCounter = 0;
            bool nextIsOdd = false;
            int ID = 1;
            char letter = 'a';

            while (doneDrawingBackground == false)
            {
                Button button = new Button
                {
                    Width = SquareSize,
                    Height = SquareSize,
                    //Background = Brushes.White,
                    Content = $"{letter}{ID}",
                    BorderBrush = Brushes.Black
                };
                //ID++;
                letter++;


                Area.Children.Add(button);
                AllButtons.Add(button);
                Canvas.SetTop(button, nextY);
                Canvas.SetLeft(button, nextX);

                nextIsOdd = !nextIsOdd;
                nextX += SquareSize;
                if (nextX >= Area.ActualWidth)
                {
                    nextX = 0;
                    nextY += SquareSize;
                    rowCounter++;
                    nextIsOdd = (rowCounter % 2 != 0);
                    letter = 'a';
                    ID++;
                }

                if (nextY >= Area.ActualHeight)
                    doneDrawingBackground = true;
            }
        }

        // The part of the rectangle the mouse is over. (We use body)
        private enum HitType
        {
            None, Body, UL, UR, LR, LL, L, R, T, B
        };

        // The part of the rectangle under the mouse.
        private HitType MouseHitType = HitType.None;

        // The Rectangle that was hit.
        private Button HitButton = null;

        // The Rectangles that the user can move and resize.
        private readonly List<Button> Buttons = new List<Button>();

        private void PopulateButtonList()
        {
            foreach (UIElement child in Area.Children)
            {
                if (child is Button)
                    Buttons.Add(child as Button);
            }

            // Reverse the list so the Rectangles on top come first.
            Buttons.Reverse();
        }

        // If the point is over any Rectangle,
        // return the Rectangle and the hit type.
        private void FindHit(Point point)
        {
            HitButton = null;
            MouseHitType = HitType.None;

            foreach (Button butt in AllButtons)
            {
                MouseHitType = SetHitType(butt, point);
                if (MouseHitType != HitType.None)
                {
                    HitButton = butt;
                    return;
                }
            }
            // We didn't find a hit.
            return;
        }

        // Return a HitType value to indicate what is at the point.
        private HitType SetHitType(Button butt, Point point)
        {
            double left = Canvas.GetLeft(butt);
            double top = Canvas.GetTop(butt);
            double right = left + butt.Width;
            double bottom = top + butt.Height;
            if (point.X < left) return HitType.None;
            if (point.X > right) return HitType.None;
            if (point.Y < top) return HitType.None;
            if (point.Y > bottom) return HitType.None;

            // The code uses the mouse's coordinates and the rectangle's coordinates to decide whether the mouse is over a rectangle corner, edge, or body, and it returns the correct HitType.
            const double GAP = 10;
            //if (point.X - left < GAP)
            //{
            //    // Left edge.
            //    if (point.Y - top < GAP) return HitType.UL;
            //    if (bottom - point.Y < GAP) return HitType.LL;
            //    return HitType.L;
            //}
            //if (right - point.X < GAP)
            //{
            //    // Right edge.
            //    if (point.Y - top < GAP) return HitType.UR;
            //    if (bottom - point.Y < GAP) return HitType.LR;
            //    return HitType.R;
            //}
            //if (point.Y - top < GAP) return HitType.T;
            if (bottom - point.Y < GAP) return HitType.B;
            return HitType.Body;
        }


        private void Area_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FindHit(Mouse.GetPosition(Area));
            //SetMouseCursor();
            if (MouseHitType == HitType.None) return;

            if (HitButton.Background == Brushes.White)
            {
                HitButton.Background = Brushes.Red;
            }
            else
            {
                HitButton.Background = Brushes.White;
            }
        }



    }
}
