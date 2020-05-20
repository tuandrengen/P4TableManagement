using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

// Known bugs: Hvis du scroller for hurtigt når du starter programmet så crasher det, da alle elementer i listen ikker nået at "loade".
// Anh Tuan (13-05-2020 22:17): Køb en hurtigere computer

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
        public Reservation highlightedReservation;

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
            foreach (Reservation reservation in tableManagementSystem.ReservationList)
            {
                reservation.stringTime = reservation.timeStart.ToShortTimeString();
            }

            tableManagementSystem.AssignedReservationList = new List<Reservation>();

            ReservationListView.ItemsSource = tableManagementSystem.ReservationList;
            AssignedReservationListView.ItemsSource = tableManagementSystem.AssignedReservationList;

            // Clock
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DrawGrid();
            CreateTables();
        }

        //partial Table ReturnTable(Button clickedButton)
        //{

        //    return tableManagementSystem.TableList.Find(x => $"Table {x.tableNumber}" == (string)clickedButton.Content);
        //}

        // Tik Tok
        private void timer_Tick(object sender, EventArgs e)
        {
            ClockLabel.Content = DateTime.Now.ToLongTimeString();
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
                Reservation selectedBooking = (Reservation)selecteditem;

                // find the reservation from the reservationList
                highlightedReservation = tableManagementSystem.ReservationList.Find(x => x.id == selectedBooking.id);
                //helper_headline.Content = highlightedReservation.ToString();
                
                // Umiddelbar måde til at opdatere listview
                ICollectionView view = CollectionViewSource.GetDefaultView(tableManagementSystem.ReservationList); // tror dette er overflødigt
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
            foreach (Button button in Area.Children.OfType<Button>())
            {
                SmallTable smallTable = new SmallTable(button.ActualWidth, button.ActualHeight, Canvas.GetLeft(button), Canvas.GetTop(button));

                button.ToolTip = $"Table: { smallTable.tableNumber }" +
                        $"\nSeats: { smallTable.seats }" +
                        $"\nStatus: { smallTable.state }" +
                        $"\nX: { smallTable.placementX }" +
                        $"\nY: { smallTable.placementY }" +
                        $"\n BookingID: {smallTable.bookingID}";

                tableManagementSystem.AddTableToList(smallTable);
            }
        }
        

        private void DrawGrid()
        {
            bool doneDrawingBackground = false;
            int nextX = 0, nextY = 0;
            int rowCounter = 0; // If _nextIsOdd is not being used, then same for this boy
            bool _nextIsOdd = false; //Not being used we only set it all the time?
            int x = 1;
            int y = 1;
            Random rand = new Random();

            //Drawing the grid of rectangles
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

                Area.Children.Add(rectangle);
                AllRectangles.Add(rectangle);
                Canvas.SetTop(rectangle, nextY);
                Canvas.SetLeft(rectangle, nextX);

                _nextIsOdd = !_nextIsOdd;
                nextX += SquareSize;
                if (nextX >= Area.ActualWidth)
                {
                    nextX = 0;
                    nextY += SquareSize;
                    rowCounter++;
                    _nextIsOdd = (rowCounter % 2 != 0);
                    x = 1;
                    y++;
                }

                if (nextY >= Area.ActualHeight - tableSize)
                {
                    doneDrawingBackground = true;
                    _nextIsOdd = false;
                }
            }
            // Drawing tables
            LoadTables();
            LoadDecorationElements();
        }

        void LoadTables()
        {
            string path = $@"C:\P4\MapSections\Section1.csv";
            List<string[]> tables = new List<string[]>();

            using (var reader = new StreamReader(path))
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
                    Content = $"Table { table[0] }",
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

                Area.Children.Add(button);
                AllButtons.Add(button);
                button.Click += new RoutedEventHandler(Button_Click);
            }
        }

        void LoadDecorationElements()
        {
            string path = $@"C:\P4\DecorationElements\Section1.csv";
            List<string[]> de = new List<string[]>();

            using (var reader = new StreamReader(path))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var element = line.Split(';');

                    de.Add(element);
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
                if (element[1] == "Window")
                {
                    ellipse.Fill = Brushes.Aqua;
                }

                Canvas.SetTop(ellipse, int.Parse(element[3]));
                Canvas.SetLeft(ellipse, int.Parse(element[2]));

                Area.Children.Add(ellipse);
            }
        }

        private Table combineTableSource;
        private Table combineTableSecond;
        public CombinedTable<Table> currentCombinedTable;
        public Button sourceButton = default;

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
                    // Checks if the table is already occupied
                    if (tableManagementSystem.TableList.Find(x => $"Table { x.tableNumber }" == (string)clickedButton.Content).state == "Occupied")
                    {
                        //throw new TableAlreadyAssignedException("Error: Cannot assign an occupied table!");
                        MessageBox.Show("The table is already occupied and could not be assigned");
                        return;
                    }
                    // We assign the table with AssignTable 
                    else
                    {
                        tableManagementSystem.AssignTable(tableManagementSystem.TableList.Find(x => $"Table { x.tableNumber }" == (string)clickedButton.Content), highlightedReservation);
                        tableManagementSystem.AssignedReservationList.Add(highlightedReservation);
                        tableManagementSystem.ReservationList.Remove(tableManagementSystem.ReservationList.Find(x => x.id == highlightedReservation.id));

                        ReservationListView.Items.Refresh();
                        AssignedReservationListView.Items.Refresh();

                        // Updates the button to now display it's updated state
                        Table updateTable = tableManagementSystem.TableList.Find(x => $"Table { x.tableNumber }" == (string)clickedButton.Content);
                        clickedButton.ToolTip = $"Table: { updateTable.tableNumber }" +
                            $"\nSeats: { updateTable.seats }" +
                            $"\nStatus: { updateTable.state }" +
                            $"\nX: { updateTable.placementX }" +
                            $"\nY: { updateTable.placementY }" +
                            $"\n BookingID: { updateTable.bookingID }";

                        clickedButton.Background = Brushes.Orange;
                        sourceButton = clickedButton;

                        // Reset...
                        assignEventActivated = false;
                        assignbtn.Background = Brushes.White;
                    }
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
                if (combineTableSource != tableManagementSystem.TableList.Find(x => $"Table { x.tableNumber }" == (string)clickedButton.Content) && combineTableSource != default)
                {
                    combineTableSecond = tableManagementSystem.TableList.Find(x => $"Table { x.tableNumber }" == (string)clickedButton.Content);
                    MessageBox.Show($"Table { combineTableSecond.tableNumber } er nu Second table");
                    currentCombinedTable = tableManagementSystem.CombineTables(combineTableSource, combineTableSecond);
                    AllCombinedTables.Add(currentCombinedTable);
                    
                    // Hjælpefunktion der tjekker SourceTable's naboer (rektangler) og farver dem som er ledige
                    CheckNeighbours(sourceButton);
                    
                    //foreach (var item in currentCombinedTable.combinedTables)
                    //{
                    //    MessageBox.Show($"Combined Table exist of Table {item.tableNumber}");
                    //}
                    
                }
                else // Setting the Source table
                {
                    sourceButton = clickedButton;
                    combineTableSource = tableManagementSystem.TableList.Find(x => $"Table { x.tableNumber }" == (string)clickedButton.Content);

                    if (combineTableSource is null)
                    {
                        combineTableSource = AllCombinedTables.Find(x => $"Table { x.tableNumber }" == (string)clickedButton.Content);
                    }

                    MessageBox.Show($"Table { combineTableSource.tableNumber } er nu source table");
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
            double newX = Canvas.GetLeft(_hitRectangle);
            double newY = Canvas.GetTop(_hitRectangle);
            bool tableLocation = false;

            // Vi tegner det nye kombinerede bord - kræver en position hvor vi kan tegne det
            // Vi skal fjerne secondTable og sourceTable
            // Vi skal lave et nyt bord (button) der har størrelsen på sourceTable + secondTable (Width og Height er afhængig af hvad der bliver valgt)
            //Area.Children.Remove(combinedTable.combinedTables.Find());
            foreach (Table table in combinedTable.combinedTables)
            {
                Area.Children.Remove(AllButtons.Find(x => (string)x.Content == $"Table { table.tableNumber }"));
            }

            // Left
            if (sourceX > newX && sourceY == newY + 10)
            {
                Button button = new Button()
                {
                    Width = tableSize + 100,
                    Height = tableSize,
                    ToolTip = $"{ sourceButton.Content }" +
                        $"\nSeats: { combinedTable.seats }" +
                        $"\nStatus: Unassigned" +
                        $"\nX: { sourceX }" +
                        $"\nY: { sourceY }",
                    Content = $"{sourceButton.Content}"
                };
                button.Margin = new Thickness(10);
                Canvas.SetTop(button, newY);
                Canvas.SetLeft(button, newX);

                Area.Children.Add(button);
                AllButtons.Add(button);
                button.Click += new RoutedEventHandler(Button_Click);
            }
            // Right
            else if (sourceX < newX && sourceY == newY + 10)
            {
                Button button = new Button()
                {
                    Width = tableSize + 100,
                    Height = tableSize,
                    ToolTip = $"{ sourceButton.Content }" +
                        $"\nSeats: { combinedTable.seats }" +
                        $"\nStatus: Unassigned" +
                        $"\nX: { sourceX }" +
                        $"\nY: { sourceY }",
                    Content = $"{ sourceButton.Content }"
                };

                //butt.Margin = new Thickness(5);
                Canvas.SetTop(button, sourceY);
                Canvas.SetLeft(button, sourceX);

                Area.Children.Add(button);
                AllButtons.Add(button);
                button.Click += new RoutedEventHandler(Button_Click);
            }
            // Bottom
            else if (sourceY < newY && sourceX == newX + 10)
            {
                Button button = new Button()
                {
                    Width = tableSize,
                    Height = tableSize + 100,
                    ToolTip = $"{ sourceButton.Content }" +
                        $"\nSeats: { combinedTable.seats }" +
                        $"\nStatus: Unassigned" +
                        $"\nX: { sourceX }" +
                        $"\nY: { sourceY }",
                    Content = $"{ sourceButton.Content }"
                };

                //butt.Margin = new Thickness(5);
                Canvas.SetTop(button, sourceY);
                Canvas.SetLeft(button, sourceX);

                Area.Children.Add(button);
                AllButtons.Add(button);
                button.Click += new RoutedEventHandler(Button_Click);
            }
            // Top
            else if (sourceY > newY && sourceX == newX + 10)
            {
                Button button = new Button()
                {
                    Width = tableSize,
                    Height = tableSize + 100,
                    ToolTip = $"{ sourceButton.Content }" +
                        $"\nSeats: { combinedTable.seats }" +
                        $"\nStatus: Unassigned" +
                        $"\nX: { sourceX }" +
                        $"\nY: { sourceY }",
                    Content = $"{sourceButton.Content}"
                };

                button.Margin = new Thickness(10);
                Canvas.SetTop(button, newY);
                Canvas.SetLeft(button, newX);

                Area.Children.Add(button);
                AllButtons.Add(button);
                button.Click += new RoutedEventHandler(Button_Click);
            }
        }

        //Checks the neighbours of the button to see if there is a free spot and marks the rectangles green 
        private void CheckNeighbours(Button buttonIn)
        {
            double sourceButtonX = Canvas.GetLeft(buttonIn);
            double sourceButtonY = Canvas.GetTop(buttonIn);

            double leftNeighbour = sourceButtonX - 100;
            double topNeighbour = sourceButtonY - 100;
            double rightNeighbour = sourceButtonX + 100;
            double bottomNeighbour = sourceButtonY + 100;

            bool noRightNeighbour = true;
            bool noLeftNeighbour = true;
            bool noTopNeighbour = true;
            bool noBottomNeighbour = true;
           
            // We check every button in the canvas to see if any of them is a neighbour to our sourceButton
            foreach (Button button in Area.Children.OfType<Button>())
            {
                // Checks RIGHT neighbour
                if (Canvas.GetTop(button) == sourceButtonY && Canvas.GetLeft(button) == rightNeighbour)
                {
                    noRightNeighbour = false;
                }
                // Checks LEFT neighbour
                if (Canvas.GetTop(button) == sourceButtonY && Canvas.GetLeft(button) == leftNeighbour)
                {
                    noLeftNeighbour = false;
                }
                // Checks TOP neighbour
                if (Canvas.GetTop(button) == topNeighbour && Canvas.GetLeft(button) == sourceButtonX)
                {
                    noTopNeighbour = false;
                }
                // Checks BOTTOM neighbour
                if (Canvas.GetTop(button) == bottomNeighbour && Canvas.GetLeft(button) == sourceButtonX)
                {
                    noBottomNeighbour = false;
                }
            }

            // We color the rectangles green if there isn't a neighbour
            foreach (Rectangle rectangle in Area.Children.OfType<Rectangle>())
            {
                if (noRightNeighbour)
                {
                    if (Canvas.GetTop(rectangle) == sourceButtonY - 10 && Canvas.GetLeft(rectangle) == rightNeighbour - 10)
                    {
                        rectangle.Fill = Brushes.LightGreen;
                    }
                }
                if (noLeftNeighbour)
                {
                    if (Canvas.GetTop(rectangle) == sourceButtonY - 10 && Canvas.GetLeft(rectangle) == leftNeighbour - 10)
                    {
                        rectangle.Fill = Brushes.LightGreen;
                    }
                }
                if (noTopNeighbour)
                {
                    if (Canvas.GetTop(rectangle) == topNeighbour - 10 && Canvas.GetLeft(rectangle) == sourceButtonX - 10)
                    {
                        rectangle.Fill = Brushes.LightGreen;
                    }
                }
                if (noBottomNeighbour)
                {
                    if (Canvas.GetTop(rectangle) == bottomNeighbour - 10 && Canvas.GetLeft(rectangle) == sourceButtonX - 10)
                    {
                        rectangle.Fill = Brushes.LightGreen;
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
        private HitType _mouseHitType = HitType.None;

        // The Rectangle that was hit.
        private Rectangle _hitRectangle = null;

        public Button HitButton = null;

        // The Rectangles that the user can move and resize.
        private readonly List<Rectangle> Rectangles = new List<Rectangle>();

        // If the point is over any Rectangle,
        // return the Rectangle and the hit type.
        private void FindHit(Point point)
        {
            _hitRectangle = null;
            HitButton = null;
            _mouseHitType = HitType.None;

            foreach (Button button in AllButtons)
            {
                _mouseHitType = SetHitType(button, point);
                if (_mouseHitType != HitType.None)
                {
                    HitButton = button;
                    //return;
                }
            }

            foreach (Rectangle rectangle in AllRectangles)
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

        // Return a HitType value to indicate what is at the point.
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

        // Umiddelbart højre click interaction i canvas
        private void Area_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FindHit(Mouse.GetPosition(Area));
 
            if (_hitRectangle is Rectangle)
            {
                if (_hitRectangle.Fill == Brushes.LightGreen)
                {
                    MessageBox.Show("Grøn");

                    DrawCombinedTable(currentCombinedTable);
                    foreach (Rectangle rect in Area.Children.OfType<Rectangle>())
                    {
                        rect.Fill = Brushes.White;
                    }
                }
                //MessageBox.Show("ik grøn");
            }


            if (_mouseHitType == HitType.None) return;

            if (HitButton is Button)
            {
                //HitButton.Background = Brushes.Red;

                currentTable = tableManagementSystem.TableList.Find(x => $"Table { x.tableNumber }" == (string)HitButton.Content); // Lave en kontrolstruktur der tjekker om currentTable ikke er null VIGTIGT

                TableWindow tableWindow = new TableWindow();
                //tableWindow.Show();
                tableWindow.LabelText.Content = $"What do you want to do with { HitButton.Content }";
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

                helper_headline.Content = $"assignEventActivated = { assignEventActivated }";
                clickedButton.Background = Brushes.LightCoral;
                
                if (combineEventActivated)
                {
                    combineEventActivated = false;
                    helper_headline.Content += $"combineEventActivated = { combineEventActivated }";
                    combinebtn.Background = Brushes.White;
                    combineTableSource = default;
                    combineTableSecond = default;
                }
            }
            else
            {
                assignEventActivated = false;
                helper_headline.Content = $"assignEventActivated = { assignEventActivated }";
                clickedButton.Background = Brushes.White;
            }
        }

        // Combine button
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            if (!combineEventActivated)
            {
                combineEventActivated = true;
                helper_headline.Content = $"combineEventActivated = { combineEventActivated }";
                clickedButton.Background = Brushes.LightCoral;
                
                if (assignEventActivated)
                {
                    assignEventActivated = false;
                    helper_headline.Content += $"assignEventActivated = { assignEventActivated }";
                    assignbtn.Background = Brushes.White;
                }
            }
            else
            {
                combineEventActivated = false;
                helper_headline.Content = $"combineEventActivated = { combineEventActivated }";
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
                "\nAssign: Vælg en reservation fra listen -> Try på assign knap -->  Tryk på bord - Assigned\n" +
                "\nUnassign, Seperate, Annulere = long press bord -> menu opens -> select option\n" +
                "\nCombine = combine knap -> tryk på bordSource -> tryk på bordTarget -> tryk på en af de grønne områder ved bordSource for placering\n" +
                "\nSeperate event: de to bord kommer tilbage til deres orginal plads, hvis det ikke kan lade sig gøre, så sætter de bare ved siden af hinanden\n");
        }

        // Add new Reservation
        private void AddReservationbtn_Click(object sender, RoutedEventArgs e)
        {
            AddReservationWindow addReservationWindow = new AddReservationWindow();
            addReservationWindow.ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        // Add new Walk-in
        private void Walk_in_Click(object sender, RoutedEventArgs e)
        {
            AddWalkInWindow addWalkInWindow = new AddWalkInWindow();
            addWalkInWindow.ShowDialog();
        }

        // Edit selected item from ReservationListView
        private void EditReservationListView_Click(object sender, RoutedEventArgs e)
        {
            //ListView listView = sender as ListView;
            //var selecteditem = listView.SelectedItem;
            Reservation selectedBooking = (Reservation)ReservationListView.SelectedItem;
            EditReservationWindow editReservationWindow = new EditReservationWindow();

            //MessageBox.Show($"EDIT: We want to edit {selectedBooking.id} call edit window...");
            editReservationWindow.ShowDialog();
            
            // Indsæt variabler fra selectedBooking editReservationWindow
            //editReservationWindow.NumberOfGuestText...


            //ReservationListView.Items.Refresh();
        }

        // Delete selected item from ReservationListView
        private void DeleteReservationListView_Click(object sender, RoutedEventArgs e)
        {
            Reservation selectedBooking = (Reservation)ReservationListView.SelectedItem;

            tableManagementSystem.ReservationList.Remove(tableManagementSystem.ReservationList.Find(x => x.id == selectedBooking.id));

            ReservationListView.Items.Refresh();
        }

        // Map Editor click event
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            MapEditorWindow mapEditorWindow = new MapEditorWindow();
            mapEditorWindow.Show();
            mapEditorWindow.Closed += (s, args) => this.Close();

            // Vil nok gerne lukke MainWindow og starte det igen når mapEditorWindow lukkes igen, så mappet tegnes ud fra hvad der blev valgt i mapEditorWindow.
            this.Hide();
        }
    }
}
