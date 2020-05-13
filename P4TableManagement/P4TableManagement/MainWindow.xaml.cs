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

// Known bugs: Hvis du scroller for hurtigt n�r du starter programmet s� crasher det, da alle elementer i listen ikker n�et at "loade".



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
        Reservation highlightedReservation;

        ReservationList list = new ReservationList();
        string path = @"C:\P4\test.xlsx";

        //public List<Reservation> reservationList = new List<Reservation>();
        

        public MainWindow()
        {
            InitializeComponent();

            //ReservationList list = new ReservationList();
            //string path = @"C:\P4\test.xlsx";
            //List<Reservation> reservationList = list.PopulateReservationList(path, 1);

            tableManagementSystem.ReservationList = list.PopulateReservationList(path, 1);
            foreach (Reservation item in tableManagementSystem.ReservationList)
            {
                item.stringTime = item.timeStart.ToShortTimeString();
            }

            tableManagementSystem.AssignedReservationList = tableManagementSystem.ReservationList.Where(x => x.id == 1).ToList();
            tableManagementSystem.AssignedReservationList.Remove(tableManagementSystem.AssignedReservationList.Find(x => x.id == 1));

            ListView.ItemsSource = tableManagementSystem.ReservationList;
            AssResListView.ItemsSource = tableManagementSystem.AssignedReservationList;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DrawGameArea();
            CreateTables();
            //MessageBox.Show(tableManagementSystem.TableList.Count.ToString());
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

                // find the reservation from the reservationList
                highlightedReservation = tableManagementSystem.ReservationList.Find(x => x.id == selectedBooking.id);
                //helper_headline.Content = highlightedReservation.ToString();
                
                // Umiddelbar m�de til at opdatere listview
                ICollectionView view = CollectionViewSource.GetDefaultView(tableManagementSystem.ReservationList);
                view.Refresh();

                MessageBox.Show("SelectedItem from list is: " + selectedBooking.id);
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
                // Kontrolstruktur der afg�r om det er et SmallTable eller LargeTable

                SmallTable smallTable = new SmallTable(butt.ActualWidth, butt.ActualHeight, Canvas.GetLeft(butt), Canvas.GetTop(butt));

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
            int tableID = 1;
            bool firstButtonDrawn = true;
            bool tablesDone = false;
            Random rand = new Random();
            int random = 0;

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
                    x = 1;
                    y++;
                }

                if (nextY >= Area.ActualHeight - tableSize)
                {
                    doneDrawingBackground = true;
                    nextIsOdd = false;
                }
            }
            // Drawing tables
            while (tablesDone == false)
            {
                Button butt = new Button()
                {
                    Width = tableSize,
                    Height = tableSize,
                    ToolTip = $"Table: {tableID}\nSeats: 4\nStatus: Unassigned\nX: {nextX}\nY: {nextY}",
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
        public CombinedTable<Table> currentCombinedTable;
        Button sourceButton = default;

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
                    tableManagementSystem.AssignedReservationList.Add(highlightedReservation);
                    tableManagementSystem.ReservationList.Remove(tableManagementSystem.ReservationList.Find(x => x.id == highlightedReservation.id));

                    ListView.Items.Refresh();
                    AssResListView.Items.Refresh();
    

                    // Updates the button to now display it's updated state
                    Table updateTable = tableManagementSystem.TableList.Find(x => $"Table {x.tableNumber}" == (string)clickedButton.Content);
                    clickedButton.ToolTip = $"Table: {updateTable.tableNumber}\nSeats: {updateTable.seats}\nStatus: {updateTable.state}\nX: {updateTable.placementX}\nY: {updateTable.placementY}\n BookingID: {updateTable.bookingID}";
                    //clickedButton.Content += $"\nAssigned to {highlightedReservation.id}";
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

                    
                    // Hj�lpefunktion der tjekker SourceTable's naboer (rektangler) og farver dem som er ledige
                    CheckNeighbours(sourceButton);
                    

                    //foreach (var item in currentCombinedTable.combinedTables)
                    //{
                    //    MessageBox.Show($"Combined Table exist of Table {item.tableNumber}");
                    //}
                    
                }
                else // Setting the Source table
                {
                    sourceButton = clickedButton;
                    combineTableSource = tableManagementSystem.TableList.Find(x => $"Table {x.tableNumber}" == (string)clickedButton.Content);

                    if (combineTableSource is null)
                    {
                        combineTableSource = AllCombinedTables.Find(x => $"Table {x.tableNumber}" == (string)clickedButton.Content);
                    }
                    
                    

                    MessageBox.Show($"Table {combineTableSource.tableNumber} er nu source table");
                }
                
            }
            else
            {
                MessageBox.Show("No event has been triggered...");
            }

        }

        private void DrawCombinedTable(CombinedTable<Table> combinedTable)
        {
            double sourceX = Canvas.GetLeft(sourceButton);
            double sourceY = Canvas.GetTop(sourceButton);
            double newX = Canvas.GetLeft(HitRectangle);
            double newY = Canvas.GetTop(HitRectangle);
            bool tableLocation = false;

            //MessageBox.Show($"SourceX: {sourceX} og newX: {newX}\nSourceY: {sourceY} og newY: {newY}\n" +
            //    $"Venstre: {sourceX} > {newX} && {sourceY} == {newY + 10}\n" +
            //    $"H�jre: {sourceX} < {newX} && {sourceY} == {newY + 10}\n" +
            //    $"Bund: {sourceY} > {newY} && {sourceX} == {newX + 10}\n" +
            //    $"Top: {sourceY} < {newY} && {sourceX} == {newX + 10}\n");

            // Vi tegner det nye kombinerede bord - kr�ver en position hvor vi kan tegne det
            // Vi skal fjerne secondTable og sourceTable
            // Vi skal lave et nyt bord (button) der har st�rrelsen p� sourceTable + secondTable (Width og Height er afh�ngig af hvad der bliver valgt)
            //Area.Children.Remove(combinedTable.combinedTables.Find());
            foreach (Table item in combinedTable.combinedTables)
            {
                Area.Children.Remove(AllButtons.Find(x => (string)x.Content == $"Table {item.tableNumber}"));
            }

            // Left
            if (sourceX > newX && sourceY == newY + 10)
            {
                Button butt = new Button()
                {
                    Width = tableSize + 100,
                    Height = tableSize,
                    ToolTip = $"{sourceButton.Content}\nSeats: X\nStatus: Unassigned\nX: {sourceX}\nY: {sourceY}",
                    Content = $"{sourceButton.Content}"
                };
                butt.Margin = new Thickness(10);
                Canvas.SetTop(butt, newY);
                Canvas.SetLeft(butt, newX);

                Area.Children.Add(butt);
                AllButtons.Add(butt);
                butt.Click += new RoutedEventHandler(Button_Click);
            }
            // Right
            else if (sourceX < newX && sourceY == newY + 10)
            {
                Button butt = new Button()
                {
                    Width = tableSize + 100,
                    Height = tableSize,
                    ToolTip = $"{sourceButton.Content}\nSeats: X\nStatus: Unassigned\nX: {sourceX}\nY: {sourceY}",
                    Content = $"{sourceButton.Content}"
                };

                //butt.Margin = new Thickness(5);
                Canvas.SetTop(butt, sourceY);
                Canvas.SetLeft(butt, sourceX);

                Area.Children.Add(butt);
                AllButtons.Add(butt);
                butt.Click += new RoutedEventHandler(Button_Click);
            }
            // Bottom
            else if (sourceY < newY && sourceX == newX + 10)
            {
                Button butt = new Button()
                {
                    Width = tableSize,
                    Height = tableSize + 100,
                    ToolTip = $"{sourceButton.Content}\nSeats: X\nStatus: Unassigned\nX: {sourceX}\nY: {sourceY}",
                    Content = $"{sourceButton.Content}"
                };

                //butt.Margin = new Thickness(5);
                Canvas.SetTop(butt, sourceY);
                Canvas.SetLeft(butt, sourceX);

                Area.Children.Add(butt);
                AllButtons.Add(butt);
                butt.Click += new RoutedEventHandler(Button_Click);
            }
            // Top
            else if (sourceY > newY && sourceX == newX + 10)
            {
                Button butt = new Button()
                {
                    Width = tableSize,
                    Height = tableSize + 100,
                    ToolTip = $"{sourceButton.Content}\nSeats: X\nStatus: Unassigned\nX: {sourceX}\nY: {sourceY}",
                    Content = $"{sourceButton.Content}"
                };

                butt.Margin = new Thickness(10);
                Canvas.SetTop(butt, newY);
                Canvas.SetLeft(butt, newX);

                Area.Children.Add(butt);
                AllButtons.Add(butt);
                butt.Click += new RoutedEventHandler(Button_Click);
            }



        }

        //Checks the neighbours of the button to see if there is a free spot and marks the rectangles green 
        private void CheckNeighbours(Button button)
        {
            double sourceButtonX = Canvas.GetLeft(button);
            double sourceButtonY = Canvas.GetTop(button);

            double leftNeighbour = sourceButtonX - 100;
            double topNeighbour = sourceButtonY - 100;
            double rightNeighbour = sourceButtonX + 100;
            double bottomNeighbour = sourceButtonY + 100;

            bool thereIsNotARightNeighbour = true;
            bool thereIsNotALeftNeighbour = true;
            bool thereIsNotATopNeighbour = true;
            bool thereIsNotABottomNeighbour = true;

            //helper_headline.Content = $"The chosen button is: {button.Content} " +
            //$"MouseHitType is: {MouseHitType} " +
            //$"The X:{sourceButtonX} Y:{sourceButtonY} " +
            //$"The Neighbour to the right is X:{rightNeighbour} Y:{sourceButtonY} " +
            //$"The Neighbour to the left is X:{leftNeighbour} Y:{sourceButtonY} " +
            //$"The Neighbour to the top is X:{sourceButtonX} Y:{topNeighbour}" +
            //$"The Neighbour to the bottom is X:{sourceButtonX} Y:{bottomNeighbour}";
           
            // We check every button in the canvas to see if any of them is a neighbour to our sourceButton
            foreach (Button butt in Area.Children.OfType<Button>())
            {
                // Checks RIGHT neighbour
                if (Canvas.GetTop(butt) == sourceButtonY && Canvas.GetLeft(butt) == rightNeighbour)
                {
                    thereIsNotARightNeighbour = false;
                }
                // Checks LEFT neighbour
                if (Canvas.GetTop(butt) == sourceButtonY && Canvas.GetLeft(butt) == leftNeighbour)
                {
                    thereIsNotALeftNeighbour = false;
                }
                // Checks TOP neighbour
                if (Canvas.GetTop(butt) == topNeighbour && Canvas.GetLeft(butt) == sourceButtonX)
                {
                    thereIsNotATopNeighbour = false;
                }
                // Checks BOTTOM neighbour
                if (Canvas.GetTop(butt) == bottomNeighbour && Canvas.GetLeft(butt) == sourceButtonX)
                {
                    thereIsNotABottomNeighbour = false;
                }
            }

            // We color the rectangles green if there isn't a neighbour
            foreach (Rectangle rect in Area.Children.OfType<Rectangle>())
            {
                if (thereIsNotARightNeighbour)
                {
                    if (Canvas.GetTop(rect) == sourceButtonY - 10 && Canvas.GetLeft(rect) == rightNeighbour - 10)
                    {
                        rect.Fill = Brushes.LightGreen;
                    }
                }
                if (thereIsNotALeftNeighbour)
                {
                    if (Canvas.GetTop(rect) == sourceButtonY - 10 && Canvas.GetLeft(rect) == leftNeighbour - 10)
                    {
                        rect.Fill = Brushes.LightGreen;
                    }
                }
                if (thereIsNotATopNeighbour)
                {
                    if (Canvas.GetTop(rect) == topNeighbour - 10 && Canvas.GetLeft(rect) == sourceButtonX - 10)
                    {
                        rect.Fill = Brushes.LightGreen;
                    }
                }
                if (thereIsNotABottomNeighbour)
                {
                    if (Canvas.GetTop(rect) == bottomNeighbour - 10 && Canvas.GetLeft(rect) == sourceButtonX - 10)
                    {
                        rect.Fill = Brushes.LightGreen;
                    }
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

        // Umiddelbart h�jre click interaction i canvas
        private void Area_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FindHit(Mouse.GetPosition(Area));
 
            if (HitRectangle is Rectangle)
            {
                if (HitRectangle.Fill == Brushes.LightGreen)
                {
                    MessageBox.Show("Gr�n");

                    DrawCombinedTable(currentCombinedTable);
                    foreach (Rectangle rect in Area.Children.OfType<Rectangle>())
                    {
                        rect.Fill = Brushes.White;
                    }
                }
                //MessageBox.Show("ik gr�n");
            }


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
                
                if (combineEventActivated)
                {
                    combineEventActivated = false;
                    helper_headline.Content += $"combineEventActivated = {combineEventActivated}";
                    combinebtn.Background = Brushes.White;
                    combineTableSource = default;
                    combineTableSecond = default;
                }
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
                
                if (assignEventActivated)
                {
                    assignEventActivated = false;
                    helper_headline.Content += $"assignEventActivated = {assignEventActivated}";
                    assignbtn.Background = Brushes.White;
                }
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
            System.Windows.Forms.Application.Restart();
            System.Windows.Application.Current.Shutdown();
        }

        // Help
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("These are the commands:\n" +
                "\nAssign: V�lg en reservation fra listen -> Try p� assign knap -->  Tryk p� bord - Assigned\n" +
                "\nUnassign, Seperate, Annulere = long press bord -> menu opens -> select option\n" +
                "\nCombine = combine knap -> tryk p� bordSource -> tryk p� bordTarget -> tryk p� en af de gr�nne omr�der ved bordSource for placering\n" +
                "\nSeperate event: de to bord kommer tilbage til deres orginal plads, hvis det ikke kan lade sig g�re, s� s�tter de bare ved siden af hinanden\n");
        }
    }
}
