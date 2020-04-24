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
            

            

            lvCells.ItemsSource = AllRectangles;
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
            PopulateRectanglesList();
        }

        const int SquareSize = 50;
        public List<Rectangle> AllRectangles = new List<Rectangle>();

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

                Rectangle rect = new Rectangle
                {
                    Width = SquareSize,
                    Height = SquareSize,
                    Fill = Brushes.White,
                    Stroke = Brushes.Red
                };
                //rect.Fill = new SolidColorBrush(Colors.White);

                Area.Children.Add(rect);
                AllRectangles.Add(rect);
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





        // The drag's last point.
        private Point LastPoint;

        // The part of the rectangle the mouse is over.
        private enum HitType
        {
            None, Body, UL, UR, LR, LL, L, R, T, B
        };

        // The part of the rectangle under the mouse.
        private HitType MouseHitType = HitType.None;

        // The Rectangle that was hit.
        private Rectangle HitRectangle = null;

        // The Rectangles that the user can move and resize.
        private List<Rectangle> Rectangles = new List<Rectangle>();

        private void PopulateRectanglesList()
        {
            foreach (UIElement child in Area.Children)
            {
                if (child is Rectangle)
                    Rectangles.Add(child as Rectangle);
            }

            // Reverse the list so the Rectangles on top come first.
            Rectangles.Reverse();
        }

        // If the point is over any Rectangle,
        // return the Rectangle and the hit type.
        private void FindHit(Point point)
        {
            HitRectangle = null;
            MouseHitType = HitType.None;

            foreach (Rectangle rect in AllRectangles)
            {
                MouseHitType = SetHitType(rect, point);
                if (MouseHitType != HitType.None)
                {
                    HitRectangle = rect;
                    return;
                }
            }

            // We didn't find a hit.
            return;
        }

        // Return a HitType value to indicate what is at the point.
        private HitType SetHitType(Rectangle rect, Point point)
        {
            double left = Canvas.GetLeft(rect);
            double top = Canvas.GetTop(rect);
            double right = left + rect.Width;
            double bottom = top + rect.Height;
            if (point.X < left) return HitType.None;
            if (point.X > right) return HitType.None;
            if (point.Y < top) return HitType.None;
            if (point.Y > bottom) return HitType.None;

            const double GAP = 10;
            if (point.X - left < GAP)
            {
                // Left edge.
                if (point.Y - top < GAP) return HitType.UL;
                if (bottom - point.Y < GAP) return HitType.LL;
                return HitType.L;
            }
            if (right - point.X < GAP)
            {
                // Right edge.
                if (point.Y - top < GAP) return HitType.UR;
                if (bottom - point.Y < GAP) return HitType.LR;
                return HitType.R;
            }
            if (point.Y - top < GAP) return HitType.T;
            if (bottom - point.Y < GAP) return HitType.B;
            return HitType.Body;
        }

        // Start dragging.
        private void Area_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FindHit(Mouse.GetPosition(Area));
            //SetMouseCursor();
            if (MouseHitType == HitType.None) return;

            LastPoint = Mouse.GetPosition(Area);
            //DragInProgress = true;

            if (HitRectangle.Fill == Brushes.White)
            {
                HitRectangle.Fill = Brushes.Red;
            }
            else
            {
                HitRectangle.Fill = Brushes.White;
            }

        }
















    }
}
