using System;
using System.Collections.Generic;
using System.ComponentModel;
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

// Known bugs: Hvis du scroller for hurtigt n�r du starter programmet s� crasher det, da alle elementer i listen ikker n�et at "loade".



namespace P4TableManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        const int SquareSize = 50;
        int tableSize = 80;
        int tableCoordinate = 10;
        public List<Rectangle> AllRectangles = new List<Rectangle>();
        public List<Button> AllButtons = new List<Button>();
        TableManagementSystem tableManagementSystem = new TableManagementSystem();

        ReservationList list = new ReservationList();
        string path = @"C:\P4\test.xlsx";
        public List<Reservation> reservationList = new List<Reservation>();
        

        public MainWindow()
        {
            InitializeComponent();

            //ReservationList list = new ReservationList();
            //string path = @"C:\P4\test.xlsx";
            //List<Reservation> reservationList = list.PopulateReservationList(path, 1);

            reservationList = list.PopulateReservationList(path, 1);
            foreach (Reservation item in reservationList)
            {
                item.stringTime = item.timeStart.ToShortTimeString();
            }

            ListView.ItemsSource = reservationList;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DrawGameArea();
            CreateTables();

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

            ListView listView = sender as ListView;
            var selecteditem = listView.SelectedItem;
            //ListViewItem item = (ListViewItem)listView.ItemContainerGenerator.ContainerFromItem(selecteditem);
            

            if (selecteditem is Reservation)
            {
                //selecteditem.
                
                Reservation selectedBooking = selecteditem as Reservation;

                //selectedBooking.name = "Test123";

                // Should use ID
                reservationList.Find(x => x.name == selectedBooking.name).name = "test123";
                
                ICollectionView view = CollectionViewSource.GetDefaultView(reservationList);
                view.Refresh();

                MessageBox.Show("Time to order more copies of: " + selectedBooking.name);
            }
            else
            {
                MessageBox.Show("Hallo min ven " + selecteditem);
            }


        }

        public void CreateTables()
        {
            foreach (Button butt in Area.Children.OfType<Button>())
            {
                // Kontrolstruktur der afg�r om det er et SmallTable eller LargeTable

                SmallTable smallTable = new SmallTable(butt.ActualWidth, butt.ActualHeight, Canvas.GetTop(butt), Canvas.GetLeft(butt));

                butt.ToolTip = $"Table: {smallTable.tableNumber}\nSeats: {smallTable.seats}\nStatus: {smallTable.state}\nX: {smallTable.placementX}\nY: {smallTable.placementY}";

                tableManagementSystem.AddTableToList(smallTable);


                //LargeTable largeTable = new LargeTable();

            }


        }
        

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
            int tableID = 1;
            bool firstButtonDrawn = true;
            bool tablesDone = false;
            Random rand = new Random();
            int random = 0;

            //int top = (int)GetValue(Canvas.TopProperty);
            //int left = (int)GetValue(Canvas.LeftProperty);
            


            // Drawing the grid of rectangles
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
                {
                    doneDrawingBackground = true;
                    nextIsOdd = false;
                    
                }
                    

                //name = $"{letter}{ID}".ToString();
            }
            // Drawing tables
            while (tablesDone == false)
            {
                //SmallTable smallTable = new SmallTable(tableSize, tableSize, top, left);

                Button butt = new Button()
                {
                    Width = tableSize,
                    Height = tableSize,
                    ToolTip = $"Table: {tableID}\nSeats: 4\nStatus: Unassigned\nX: {nextX}\nY: {nextY}",
                    //Click = "event",
                    Content = $"Table {tableID}"
                };

                if (firstButtonDrawn == true)
                {
                    Canvas.SetTop(butt, tableCoordinate);
                    Canvas.SetLeft(butt, tableCoordinate);
                    firstButtonDrawn = false;
                    nextY = 0;
                    nextY += tableCoordinate;
                    nextX = tableCoordinate;

                }
                else
                {
                    Canvas.SetTop(butt, nextY);
                    Canvas.SetLeft(butt, nextX);
                }
                
                

                random = rand.Next(1, 4);
                if (random == 1)
                {
                    nextX += 200;
                    
                }
                else if (random == 2)
                {
                    nextX += 400;
                    
                }
                else if (random == 3)
                {
                    nextX += 100;
                    
                }

                Area.Children.Add(butt);
                AllButtons.Add(butt);
                //tableManagementSystem.TableList.Add(smallTable); // tilg�et korrekt?
                tableID++;

                nextIsOdd = !nextIsOdd;
                //nextX += 100;
                if (nextX >= Area.ActualWidth)
                {
                    nextX = 10;
                    if (random == 1)
                    {
                        nextX += 200;
                        
                    }
                    else if (random == 2)
                    {
                        nextX += 400;
                        
                    }
                    else if (random == 3)
                    {
                        nextX += 100;
                        
                    }
                    nextY += 100;
                    rowCounter++;
                    nextIsOdd = (rowCounter % 2 != 0);
                    letter = 'a';
                    x = 1;
                    y++;
                    //ID++;
                    //name = $"{letter}{ID}".ToString();
                }

                if (nextY >= Area.ActualHeight)
                {
                    tablesDone = true;
                }

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

        public Button HitButton = null;

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
            HitButton = null;
            MouseHitType = HitType.None;

            //foreach (Rectangle rect in AllRectangles)
            //{
            //    MouseHitType = SetHitType(rect, point);
            //    if (MouseHitType != HitType.None)
            //    {
            //        HitRectangle = rect;
            //        return;
            //    }
            //}

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

            const double GAP = 10;

            if (bottom - point.Y < GAP) return HitType.B;
            return HitType.Body;
        }

        //private HitType SetHitType(Rectangle butt, Point point)
        //{
        //    double left = Canvas.GetLeft(butt);
        //    double top = Canvas.GetTop(butt);
        //    double right = left + butt.Width;
        //    double bottom = top + butt.Height;
        //    if (point.X < left) return HitType.None;
        //    if (point.X > right) return HitType.None;
        //    if (point.Y < top) return HitType.None;
        //    if (point.Y > bottom) return HitType.None;

        //    const double GAP = 10;

        //    if (bottom - point.Y < GAP) return HitType.B;
        //    return HitType.Body;
        //}

        private void Area_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FindHit(Mouse.GetPosition(Area));
            //SetMouseCursor();
            //if (MouseHitType == HitType.None) return;

            //if (HitRectangle.Fill == Brushes.White)
            //{
            //    HitRectangle.Fill = Brushes.Red;


            //}
            //else
            //{
            //    HitRectangle.Fill = Brushes.White;
            //}




            if (MouseHitType == HitType.None) return;

            if (HitButton is Button)
            {
                HitButton.Background = Brushes.Red;
                TableWindow tableWindow = new TableWindow();
                tableWindow.Show();

                tableWindow.TableTextBox.Text = $"{HitButton.Content}";
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
