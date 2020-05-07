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
using System.Windows.Threading;

// Known bugs: Hvis du scroller for hurtigt når du starter programmet så crasher det, da alle elementer i listen ikker nået at "loade".



namespace P4TableManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        const int SquareSize = 100;
        int tableSize = 80;
        int tableCoordinate = 10;
        public List<Rectangle> AllRectangles = new List<Rectangle>();
        public List<Button> AllButtons = new List<Button>();
        public List<CombinedTable<Table>> AllCombinedTables = new List<CombinedTable<Table>>();
        //public TableManagementSystem tableManagementSystem = new TableManagementSystem();
        
        public TableManagementSystem tableManagementSystem { get; set; } = new TableManagementSystem();
        public Table currentTable;

        public bool assignEventActivated = false;
        public bool combineEventActivated = false;
        Booking highlightedReservation;

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
            MessageBox.Show(tableManagementSystem.TableList.Count.ToString());
        }

        //partial Table ReturnTable(Button clickedButton)
        //{

        //    return tableManagementSystem.TableList.Find(x => $"Table {x.tableNumber}" == (string)clickedButton.Content);
        //}

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

                // Should use ID instead of name (ID was broken at this time)
                highlightedReservation = reservationList.Find(x => x.id == selectedBooking.id);
                
                // Umiddelbar måde til at opdatere listview
                ICollectionView view = CollectionViewSource.GetDefaultView(reservationList);
                view.Refresh();

                MessageBox.Show("SelectedItem from list is: " + selectedBooking.name);
            }
            else
            {
                MessageBox.Show("Hallo min ven  " + selecteditem);
            }


        }

        public void CreateTables()
        {
            foreach (Button butt in Area.Children.OfType<Button>())
            {
                // Kontrolstruktur der afgør om det er et SmallTable eller LargeTable

                SmallTable smallTable = new SmallTable(butt.ActualWidth, butt.ActualHeight, Canvas.GetTop(butt), Canvas.GetLeft(butt));

                butt.ToolTip = $"Table: {smallTable.tableNumber}\nSeats: {smallTable.seats}\nStatus: {smallTable.state}\nX: {smallTable.placementX}\nY: {smallTable.placementY}" +
                    $"\n BookingID: {smallTable.bookingID}";

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
                    Name = $"_{x}_{y}"
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

                if (nextY >= Area.ActualHeight - tableSize)
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
                    //Tag = {tableID},
                    //Click += new RoutedEventHandler(button_Click),
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
                butt.Click += new RoutedEventHandler(Button_Click); // We assign which method that handles the event
                //tableManagementSystem.TableList.Add(smallTable); // tilgået korrekt?
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

                if (nextY >= Area.ActualHeight - tableSize)
                {
                    tablesDone = true;
                }

            }
        }

        private Table combineTableSource;
        private Table combineTableSecond;
        private CombinedTable<Table> currentCombinedTable;

        // Button representing a table on the map, Event handler
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // We get the button we clicked on from the sender
            Button clickedButton = (Button)sender;

            // Assign event has been triggered
            if (assignEventActivated)
            {
                // If highlightedReservation hasn't been set its value is null, so we try to catch it
                try
                {
                    // We assign the table with AssignTable 
                    tableManagementSystem.AssignTable(tableManagementSystem.TableList.Find(x => $"Table {x.tableNumber}" == (string)clickedButton.Content), highlightedReservation);
                    // Updates the button to now display it's updated state
                    Table updateTable = tableManagementSystem.TableList.Find(x => $"Table {x.tableNumber}" == (string)clickedButton.Content);
                    clickedButton.ToolTip = $"Table: {updateTable.tableNumber}\nSeats: {updateTable.seats}\nStatus: {updateTable.state}\nX: {updateTable.placementX}\nY: {updateTable.placementY}\n BookingID: {updateTable.bookingID}";
                    clickedButton.Content += $"\nAssigned to {highlightedReservation.id}";
                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show("A reservation has not been selected");
                    return;
                }
            }
            // Combine event has been triggered
            else if (combineEventActivated)
            {                      
                // Sets the second table and combines it with the source table if the source table already has been set and it isn't the one we clicked on
                if (combineTableSource != tableManagementSystem.TableList.Find(x => $"Table {x.tableNumber}" == (string)clickedButton.Content) && combineTableSource != default)
                {
                    
                    combineTableSecond = tableManagementSystem.TableList.Find(x => $"Table {x.tableNumber}" == (string)clickedButton.Content);
                    MessageBox.Show($"Table {combineTableSecond.tableNumber} er nu Second table");
                    currentCombinedTable = tableManagementSystem.CombineTables(combineTableSource, combineTableSecond);
                    AllCombinedTables.Add(currentCombinedTable);
                    
                    // soon tm
                    DrawCombinedTable(currentCombinedTable, clickedButton);

                    //foreach (var item in currentCombinedTable.combinedTables)
                    //{
                    //    MessageBox.Show($"Combined Table exist of Table {item.tableNumber}");
                    //}
                    
                }
                else // Setting the Source table
                {
                    
                    combineTableSource = tableManagementSystem.TableList.Find(x => $"Table {x.tableNumber}" == (string)clickedButton.Content);

                    MessageBox.Show($"Table {combineTableSource.tableNumber} er nu source table");
                }
                
            }
            else
            {
                MessageBox.Show("No event has been triggered...");
            }

        }

        private void DrawCombinedTable(CombinedTable<Table> combinedTable, Button button)
        {
            // Vi tegner det nye kombinerede bord - kræver en position hvor vi kan tegne det
            // Hjælpefunktion der tjekker SourceTable's naboer (rektangler) og farver dem som er ledige og returnere en position?
            CheckNeighbours(button);
        }

        private Point LastPoint;

        //Checks the neighbours of the button to see if there is a free spot and marks the rectangles green 
        private void CheckNeighbours(Button button)
        {
            Mouse.GetPosition(Area);
            

            MessageBox.Show($"The chosen button is: {button.Content}\n" +
                $"MouseHitType is: {MouseHitType}\n" +
                $"Mouse position is: {Mouse.GetPosition(Area).ToString()}\n" +
                $"The Neighbour to the right is ...\n" +
                $"The Neighbour to the left is ...\n" +
                $"The Neighbour to the top is ...\n" +
                $"The Neighbour to the bottom is ...\n");

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

        // If the point is over any Rectangle,
        // return the Rectangle and the hit type.
        private void FindHit(Point point)
        {
            HitRectangle = null;
            HitButton = null;
            MouseHitType = HitType.None;

            foreach (Button butt in AllButtons)
            {
                MouseHitType = SetHitType(butt, point);
                if (MouseHitType != HitType.None)
                {
                    HitButton = butt;
                    //return;
                }
            }

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

            if (bottom - point.Y < GAP) return HitType.B;
            return HitType.Body;
        }

        // Umiddelbart højre click interaction i canvas
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

                currentTable = tableManagementSystem.TableList.Find(x => $"Table {x.tableNumber}" == (string)HitButton.Content); // Lave en kontrolstruktur der tjekker om currentTable ikke er null VIGTIGT

                TableWindow tableWindow = new TableWindow();
                //tableWindow.Show();
                tableWindow.LabelText.Content = $"What do you want to do with {HitButton.Content}";
                tableWindow.ShowDialog(); // Can't be minimized now
            }
        }

        private void ListView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("MOUSEDOWNEVENT: you pressed something");
            
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

        // Assign button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            if (!assignEventActivated)
            {
                assignEventActivated = true;

                helper_headline.Content = $"assignEventActivated = {assignEventActivated}";
                clickedButton.Background = Brushes.LightCoral;
            }
            else
            {
                assignEventActivated = false;
                helper_headline.Content = $"assignEventActivated = {assignEventActivated}";
                clickedButton.Background = Brushes.White;
            }

            
        }

        // Comnbine button
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            if (!combineEventActivated)
            {
                combineEventActivated = true;
                helper_headline.Content = $"combineEventActivated = {combineEventActivated}";
                clickedButton.Background = Brushes.LightCoral;
            }
            else
            {
                combineEventActivated = false;
                helper_headline.Content = $"combineEventActivated = {combineEventActivated}";
                clickedButton.Background = Brushes.White;
            }
            
            // Resets the variables
            combineTableSource = default;
            combineTableSecond = default;
        }

        //Reset Tables button
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //Do some reset tables
        }

        //Help button
        private void Help_Button_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("These are the commands:\n" +
                "\nAssign: Vælg en reservation fra listen -> Try på assign knap -->  Tryk på bord - Assigned\n" +
                "\nUnassign, Seperate, Annulere = long press bord -> menu opens -> select option\n" +
                "\nCombine = combine knap -> tryk på bordSource -> tryk på bordTarget -> tryk på en af de grønne områder ved bordSource for placering\n" +
                "\nSeperate event: de to bord kommer tilbage til deres orginal plads, hvis det ikke kan lade sig gøre, så sætter de bare ved siden af hinanden\n");
        }
    }
}
