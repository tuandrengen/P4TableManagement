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

// Known bugs: Hvis du scroller for hurtigt når du starter programmet så crasher det, da alle elementer i listen ikker nået at "loade".



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

            ReservationList list = new ReservationList();
            string path = @"C:\Users\T-Phamz\Desktop\test\test.xlsx";
            List<Reservation> reservationList = list.PopulateReservationList(path, 1);

            foreach (Reservation item in reservationList)
            {
                item.stringTime = item.timeStart.ToShortTimeString();
            }


            ListView.ItemsSource = reservationList;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DrawGameArea();
            //PopulateRectanglesList();
        }

        private void ListView_MouseLeftButtonDown (object sender, MouseButtonEventArgs e)
        {
            //string selectedRect;

            //if (e.OriginalSource is Rectangle)
            //{
            //    var test = e.Source as FrameworkElement;
            //    selectedRect = test.Name;

            //    TextBox textBox = new TextBox
            //    {
            //        Name = selectedRect
            //    };
            //}

        }


        const int SquareSize = 50;
        public List<Rectangle> AllRectangles = new List<Rectangle>();

        private void DrawGameArea()
        {
            bool doneDrawingBackground = false;
            int nextX = 0, nextY = 0;
            int rowCounter = 0;
            bool nextIsOdd = false;
            int x = 1;
            int y = 1;
            char block_Letter = 'A';
            char letter = 'a';


            


            while (doneDrawingBackground == false)
            {
                //string name = "_1";
                Rectangle rectangle = new Rectangle
                {
                    Width = SquareSize,
                    Height = SquareSize,
                    //Background = Brushes.White,
                    Stroke = Brushes.Black,
                    Fill = Brushes.White,
                    Name = $"_{x}_{y}",
                    //Content = $"{letter}{ID}",
                    //BorderBrush = Brushes.Black
                };
                x++;

                //ID++;
                //letter++;

                //name = $"{letter}{ID}".ToString();

                Area.Children.Add(rectangle);
                AllRectangles.Add(rectangle);
                Canvas.SetTop(rectangle, nextY);
                Canvas.SetLeft(rectangle, nextX);

                nextIsOdd = !nextIsOdd;
                nextX += SquareSize;
                if (nextX >= Area.ActualWidth)
                {
                    nextX = 0;
                    nextY += SquareSize;
                    rowCounter++;
                    nextIsOdd = (rowCounter % 2 != 0);
                    letter = 'a';
                    x = 1;
                    y++;
                    //ID++;
                    //name = $"{letter}{ID}".ToString();
                }

                if (nextY >= Area.ActualHeight)
                    doneDrawingBackground = true;

                //name = $"{letter}{ID}".ToString();
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
        private Rectangle HitRectangle = null;

        // The Rectangles that the user can move and resize.
        private readonly List<Rectangle> Rectangles = new List<Rectangle>();

        //private void PopulateRectangleList()
        //{
        //    foreach (UIElement child in Area.Children)
        //    {
        //        if (child is Rectangle)
        //            Rectangles.Add(child as Rectangle);
        //    }

        //    // Reverse the list so the Rectangles on top come first.
        //    Rectangles.Reverse();
        //}

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
        private HitType SetHitType(Rectangle butt, Point point)
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

        //// Try hard to set the margin of the Canvas Area so it fits perfectly with SquareSize
        //private void Divisible(double height, double width)
        //{
        //    double h = SystemParameters.FullPrimaryScreenHeight;
        //    double w = SystemParameters.FullPrimaryScreenWidth;

        //    if (height % h == 0)
        //    {
        //        Area.Margin = new Thickness(25,25,25,25);
                
        //    }




        //}

        private void Area_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FindHit(Mouse.GetPosition(Area));
            //SetMouseCursor();
            if (MouseHitType == HitType.None) return;

            if (HitRectangle.Fill == Brushes.White)
            {
                HitRectangle.Fill = Brushes.Red;
            }
            else
            {
                HitRectangle.Fill = Brushes.White;
            }
        }

        private void ListView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("you pressed something");
            
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ListViewItem item = sender as ListViewItem;
            //object obj = item.Content;


            //MessageBox.Show($"you pressed double click and selected {0}", (string)obj);


            //XmlElement item = ((ListViewItem)sender).Content as XmlElement;

            //MessageBox.Show("Time to order more copies of _" + item.GetType() +"_");

            ListView listView = sender as ListView;
            var selecteditem = listView.SelectedItem;
            //ListViewItem item = (ListViewItem)listView.ItemContainerGenerator.ContainerFromItem(selecteditem);

            if (selecteditem is Rectangle)
            {
                Rectangle selectedRectangle = selecteditem as Rectangle;

                selectedRectangle.Fill = Brushes.Red;
                
                MessageBox.Show("Time to order more copies of: " + selectedRectangle.Name);
            }
            else
            {
                MessageBox.Show("Hallo min ven " + selecteditem);
            }
            
        }
    }
}
