using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        public List<CombinedTable<Table>> AllCombinedTables = new List<CombinedTable<Table>>();
        
        public TableManagementSystem tableManagementSystem { get; set; } = new TableManagementSystem();
        public Table currentTable;

        public bool assignEventActivated = false;
        public bool combineEventActivated = false;
        public Reservation highlightedReservation;

        ReservationList list = new ReservationList();
        string path = @"C:\P4\test.xlsx";
        

        public MainWindow()
        {
            InitializeComponent();

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
        }

        // Tik Tok
        private void timer_Tick(object sender, EventArgs e)
        {
            var dateNow = DateTime.Now;
            int hour = dateNow.Hour;
            int min = dateNow.Minute;
            Button foundButton;

            ClockLabel.Content = DateTime.Now.ToString("HH:mm");

            // Tilf�je noget med reservation hvis tiden passer til stringTime s� skal bordets farve �ndres...
            foreach (Table table in tableManagementSystem.TableList.Where(x => x.state == "Assigned"))
            {
                foreach (Reservation reservation in tableManagementSystem.AssignedReservationList)
                {
                    if (table.bookingID == reservation.id)
                    {
                        string[] list = reservation.stringTime.Split(':');

                        DateTime datenowtime = DateTime.Parse("30/12/1899 " + DateTime.Now.ToShortTimeString());

                        int compareStart = DateTime.Compare(reservation.timeStart, datenowtime);
                        int compareEnd = DateTime.Compare(reservation.timeEnd, datenowtime);

                        //MessageBox.Show($"hour: {hour} {list[0]} og {list[1]}");

                        if (compareStart <= 0)
                        {
                            foundButton = Area.Children.OfType<Button>().ToList().Find(x => (string)x.Content == $"Table { table.tableNumber }");
                            foundButton.Background = Brushes.Red;
                            table.state = "Occupied";
                            foundButton.ToolTip = $"Table: { table.tableNumber }" +
                                $"\nSeats: { table.seats }" +
                                $"\nStatus: { table.state }" +
                                $"\nX: { table.placementX }" +
                                $"\nY: { table.placementY }" +
                                $"\nBookingID: { table.bookingID }" +
                                $"\nEstimated leave: { datenowtime.AddHours(2).ToShortTimeString() }";
                        }
                    }
                }
            }
        }

        private void ListView_MouseLeftButtonUp (object sender, MouseButtonEventArgs e)
        {
            ListView listView = sender as ListView;
            var selecteditem = listView.SelectedItem;
            
            if (selecteditem is Reservation)
            {   
                Reservation selectedBooking = (Reservation)selecteditem;

                // find the reservation from the reservationList
                highlightedReservation = tableManagementSystem.ReservationList.Find(x => x.id == selectedBooking.id);
                
                // Umiddelbar m�de til at opdatere listview
                ICollectionView view = CollectionViewSource.GetDefaultView(tableManagementSystem.ReservationList); // tror dette er overfl�digt
                view.Refresh();

                //MessageBox.Show("SelectedItem from list is: " + selectedBooking.id);
            }
        }

        public void CreateTables()
        {
            foreach (Button button in Area.Children.OfType<Button>())
            {
                var values = button.Name.Split('_');
                Table table;
                if (values[0] == "S")
                {
                    table = new SmallTable(button.ActualWidth, button.ActualHeight, Canvas.GetLeft(button), Canvas.GetTop(button));

                }
                else
                {
                    table = new LargeTable(button.ActualWidth, button.ActualHeight, Canvas.GetLeft(button), Canvas.GetTop(button));
                }

                button.ToolTip = $"Table: { table.tableNumber }" +
                            $"\nSeats: { table.seats }" +
                            $"\nStatus: { table.state }" +
                            $"\nX: { table.placementX }" +
                            $"\nY: { table.placementY }" +
                            $"\nBookingID: {table.bookingID}";
                
                tableManagementSystem.AddTableToList(table);

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
            CreateTables();
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
                    button.Name = $"S_{ table[0] }";
                    button.Width = tableSize;
                    button.Height = tableSize;
                }
                else if (table[1] == "L")
                {
                    button.Name = $"L_{ table[0] }";
                    button.Width = tableSize * 2 + 20;
                    button.Height = tableSize;
                }

                Canvas.SetTop(button, int.Parse(table[3]));
                Canvas.SetLeft(button, int.Parse(table[2]));

                Area.Children.Add(button);
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

                Area.Children.Add(ellipse);
            }
        }

        private Table combineTableSource;
        private Table combineTableSecond;
        public CombinedTable<Table> currentCombinedTable;
        public Button sourceButton = default;
        public Button secondButton = default;

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
                    Table table = tableManagementSystem.TableList.Find(x => $"Table { x.tableNumber }" == (string)clickedButton.Content);

                    if (table == null)
                    {
                        table = tableManagementSystem.TableList.Find(x => $"*Table { x.tableNumber }" == (string)clickedButton.Content);
                        if (table == null)
                        {
                            MessageBox.Show("Error: Could not find table");
                            return;
                        }
                    }
                    //tableManagementSystem.TableList.Find(x => $"*Table { x.tableNumber }" == (string)clickedButton.Content).state == "Occupied"
                    // Checks if the table is already occupied
                    if (table.state == "Occupied")
                    {
                        //throw new TableAlreadyAssignedException("Error: Cannot assign an occupied table!");
                        MessageBox.Show("The table is already occupied and could not be assigned");
                        return;
                    }
                    // We assign the table with AssignTable 
                    else
                    {
                        tableManagementSystem.AssignTable(table, highlightedReservation);
                        tableManagementSystem.AssignedReservationList.Add(highlightedReservation);
                        tableManagementSystem.ReservationList.Remove(tableManagementSystem.ReservationList.Find(x => x.id == highlightedReservation.id));

                        ReservationListView.Items.Refresh();
                        AssignedReservationListView.Items.Refresh();

                        clickedButton.Background = Brushes.Orange;
                        sourceButton = clickedButton;

                        // Updates the button to now display it's updated state
                        sourceButton.ToolTip = $"Table: { table.tableNumber }" +
                            $"\nSeats: { table.seats }" +
                            $"\nStatus: { table.state }" +
                            $"\nX: { Canvas.GetLeft(sourceButton) }" +
                            $"\nY: { Canvas.GetTop(sourceButton) }" +
                            $"\nBookingID: { table.bookingID }";

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
                if (combineTableSource != tableManagementSystem.TableList.Find(x => $"*Table { x.tableNumber }" == (string)clickedButton.Content) && combineTableSource != default)
                {
                    secondButton = clickedButton;
                    combineTableSecond = tableManagementSystem.TableList.Find(x => $"Table { x.tableNumber }" == (string)clickedButton.Content);
                    MessageBox.Show($"Table { combineTableSecond.tableNumber } er nu Second table");
                    currentCombinedTable = tableManagementSystem.CombineTables(combineTableSource, combineTableSecond);
                    AllCombinedTables.Add(currentCombinedTable);
                    
                    // Hj�lpefunktion der tjekker SourceTable's naboer (rektangler) og farver dem som er ledige
                    CheckNeighbours(sourceButton);
                    tableManagementSystem.AddTableToList(currentCombinedTable);
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
        }

        private void DrawCombinedTable(CombinedTable<Table> combinedTable)
        {
            combinedTable.placementX = Canvas.GetLeft(sourceButton);
            combinedTable.placementY = Canvas.GetTop(sourceButton);
            double newX = Canvas.GetLeft(_hitRectangle);
            double newY = Canvas.GetTop(_hitRectangle);
            bool tableLocation = false;
            // Before the new, combined table is being drawn we hide the tables being combined.
            // This way, when we separate the tables again we change the visibility of the tables back to show,
            // and they're back to their original position.
            foreach (Table table in combinedTable.combinedTables)
            {
                if (Area.Children.OfType<Button>().ToList().Find(x => (string)x.Content == $"Table { table.tableNumber }") != null)
                {
                    Area.Children.OfType<Button>().ToList().Find(x => (string)x.Content == $"Table {table.tableNumber}").Visibility = Visibility.Hidden;
                }
            }

            // Left
            if (combinedTable.placementX > newX && combinedTable.placementY == newY + 10)
            {
                Button button = new Button()
                {
                    Content = $"*Table { combinedTable.tableNumber }",
                    Width = tableSize + 100,
                    Height = tableSize,
                    Background = Brushes.White,
                    BorderThickness = new Thickness(2),
                    ToolTip = $"{ Content }" +
                        $"\nSeats: { combinedTable.seats }" +
                        $"\nStatus: { combinedTable.state }" +
                        $"\nX: { newX + 10 }" +
                        $"\nY: { newY + 10 }",
                };

                button.Margin = new Thickness(10);
                Canvas.SetTop(button, newY);
                Canvas.SetLeft(button, newX);

                Area.Children.Add(button);
                button.Click += new RoutedEventHandler(Button_Click);
            }
            // Right
            else if (combinedTable.placementX < newX && combinedTable.placementY == newY + 10)
            {
                Button button = new Button()
                {
                    Content = $"*Table { combinedTable.tableNumber }",
                    Width = tableSize + 100,
                    Height = tableSize,
                    Background = Brushes.White,
                    BorderThickness = new Thickness(2),
                    ToolTip = $"{ Content }" +
                        $"\nSeats: { combinedTable.seats }" +
                        $"\nStatus: { combinedTable.state }" +
                        $"\nX: { combinedTable.placementX }" +
                        $"\nY: { combinedTable.placementY }",
                };

                Canvas.SetTop(button, combinedTable.placementY);
                Canvas.SetLeft(button, combinedTable.placementX);

                Area.Children.Add(button);
                button.Click += new RoutedEventHandler(Button_Click);
            }
            // Bottom
            else if (combinedTable.placementY < newY && combinedTable.placementX == newX + 10)
            {
                Button button = new Button()
                {
                    Content = $"*Table { combinedTable.tableNumber }",
                    Width = tableSize,
                    Height = tableSize + 100,
                    Background = Brushes.White,
                    BorderThickness = new Thickness(2),
                    ToolTip = $"{ Content }" +
                        $"\nSeats: { combinedTable.seats }" +
                        $"\nStatus: { combinedTable.state }" +
                        $"\nX: { combinedTable.placementX }" +
                        $"\nY: { combinedTable.placementY }",
                };

                Canvas.SetTop(button, combinedTable.placementY);
                Canvas.SetLeft(button, combinedTable.placementX);

                Area.Children.Add(button);
                button.Click += new RoutedEventHandler(Button_Click);
            }
            // Top
            else if (combinedTable.placementY > newY && combinedTable.placementX == newX + 10)
            {
                Button button = new Button()
                {
                    Content = $"*Table { combinedTable.tableNumber }",
                    Width = tableSize,
                    Height = tableSize + 100,
                    Background = Brushes.White,
                    BorderThickness = new Thickness(2),
                    ToolTip = $"{ Content }" +
                        $"\nSeats: { combinedTable.seats }" +
                        $"\nStatus: { combinedTable.state }" +
                        $"\nX: { newX + 10 }" +
                        $"\nY: { newY + 10 }",
                };

                button.Margin = new Thickness(10);
                Canvas.SetTop(button, newY);
                Canvas.SetLeft(button, newX);

                Area.Children.Add(button);
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

            foreach (Button button in Area.Children.OfType<Button>())
            {
                _mouseHitType = SetHitType(button, point);
                if (_mouseHitType != HitType.None)
                {
                    HitButton = button;
                    //return;
                }
            }

            foreach (Rectangle rectangle in Area.Children.OfType<Rectangle>())
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

        // Umiddelbart h�jre click interaction i canvas
        private void Area_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FindHit(Mouse.GetPosition(Area));
 
            if (_hitRectangle is Rectangle)
            {
                if (_hitRectangle.Fill == Brushes.LightGreen)
                {
                    MessageBox.Show("Gr�n");

                    DrawCombinedTable(currentCombinedTable);
                    foreach (Rectangle rect in Area.Children.OfType<Rectangle>())
                    {
                        rect.Fill = Brushes.White;
                    }
                }
            }

            if (_mouseHitType == HitType.None) return;

            if (HitButton is Button)
            {
                //HitButton.Background = Brushes.Red;

                currentTable = tableManagementSystem.TableList.Find(x => $"Table { x.tableNumber }" == (string)HitButton.Content); // Lave en kontrolstruktur der tjekker om currentTable ikke er null VIGTIGT

                TableWindow tableWindow = new TableWindow();
                tableWindow.OverskriftLabel.Content = $"{ HitButton.Content }";
                
                // Checks what state the table is in
                if (HitButton.Background == Brushes.White)
                {
                    tableWindow.RadioAvailable.IsChecked = true;
                }
                if (HitButton.Background == Brushes.Red)
                {
                    tableWindow.RadioOccupied.IsChecked = true;
                }
                if (HitButton.Background == Brushes.Yellow)
                {
                    tableWindow.RadioPaid.IsChecked = true;
                }

                tableWindow.ShowDialog(); // Can't be minimized now
            }
        }

        // Assign button
        private void AssignButton_Click(object sender, RoutedEventArgs e)
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
        private void CombineButton_Click(object sender, RoutedEventArgs e)
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

        // Help
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("These are the commands:\n" +
                "\nAssign: V�lg en reservation fra listen -> Try p� assign knap -->  Tryk p� bord - Assigned\n" +
                "\nUnassign, Seperate, Annulere = long press bord -> menu opens -> select option\n" +
                "\nCombine = combine knap -> tryk p� bordSource -> tryk p� bordTarget -> tryk p� en af de gr�nne omr�der ved bordSource for placering\n" +
                "\nSeperate event: de to bord kommer tilbage til deres orginal plads, hvis det ikke kan lade sig g�re, s� s�tter de bare ved siden af hinanden\n");
        }

        // Add new Reservation
        private void AddReservationbtn_Click(object sender, RoutedEventArgs e)
        {
            AddReservationWindow addReservationWindow = new AddReservationWindow();
            addReservationWindow.ShowDialog();
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
            Reservation selectedBooking = (Reservation)ReservationListView.SelectedItem;
            highlightedReservation = tableManagementSystem.ReservationList.Find(x => x.id == selectedBooking.id);

            EditReservationWindow editReservationWindow = new EditReservationWindow();



            // Fill up the textboxes in the window with the reservation data
            editReservationWindow.RichGuest.Selection.Text = selectedBooking.numberOfGuests.ToString();
            editReservationWindow.RichTime.Selection.Text = selectedBooking.stringTime;
            editReservationWindow.RichName.Selection.Text = selectedBooking.name;
            editReservationWindow.RichTelephoneNumber.Selection.Text = selectedBooking.phoneNumber.ToString();
            editReservationWindow.RichComment.Selection.Text = selectedBooking.comment;

            // Fills out the checkboxes (Parameters)
            string[] splitString = selectedBooking.parameter.Split(',');
            foreach (string item in splitString)
            {
                if (item == "aquarium")
                {
                    editReservationWindow.CheckAquarium.IsChecked = true;
                }
                if (item == "window")
                {
                    editReservationWindow.CheckWindow.IsChecked = true;
                }
                if (item == "flag")
                {
                    editReservationWindow.CheckFlag.IsChecked = true;
                }
                if (item == "buffet")
                {
                    editReservationWindow.CheckBuffet.IsChecked = true;
                }
                if (item == "playroom")
                {
                    editReservationWindow.CheckPlayroom.IsChecked = true;
                }
                if (item == "babychair")
                {
                    editReservationWindow.CheckBabychair.IsChecked = true;
                }
            }

            // Checking Gap
            if (selectedBooking.isGap)
            {
                editReservationWindow.CheckGap.IsChecked = true;
            }

            editReservationWindow.ShowDialog();
            
            // Inds�t variabler fra selectedBooking editReservationWindow
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
        private void MapEditor_Click(object sender, RoutedEventArgs e)
        {
            MapEditorWindow mapEditorWindow = new MapEditorWindow();
            mapEditorWindow.Show();
            mapEditorWindow.Closed += (s, args) => this.Close();

            // Vil nok gerne lukke MainWindow og starte det igen n�r mapEditorWindow lukkes igen, s� mappet tegnes ud fra hvad der blev valgt i mapEditorWindow.
            this.Hide();
        }

        private void ResetTables_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to reset the tables?", "Are you sure?", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Area.Children.Clear();
                tableManagementSystem = new TableManagementSystem();


                tableManagementSystem.ReservationList = list.PopulateReservationList(path, 1);

                foreach (Reservation reservation in tableManagementSystem.ReservationList)
                {
                    reservation.stringTime = reservation.timeStart.ToShortTimeString();
                }

                tableManagementSystem.AssignedReservationList = new List<Reservation>();

                ReservationListView.ItemsSource = tableManagementSystem.ReservationList;
                AssignedReservationListView.ItemsSource = tableManagementSystem.AssignedReservationList;
                Table._tableID = 0;
                
                DrawGrid();

                // Clock
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += timer_Tick;
                timer.Start();
            }
        }

        private void FilterSpecificNumberOfGuests_Click(object sender, RoutedEventArgs e)
        {
            
            // 8 skal v�lges af brugeren...
            ReservationListView.ItemsSource = list.FilterBySpecificNumberOfGuests(tableManagementSystem.ReservationList,8);
            ReservationListView.Items.Refresh();
        }

        private void FilterRangeNumberOfGuests_Click(object sender, RoutedEventArgs e)
        {
            ReservationListView.ItemsSource = list.FilterByRangeNumberOfGuests(tableManagementSystem.ReservationList, 10,4);
            ReservationListView.Items.Refresh();
        }

        private void FilterSpecificTimeStart_Click(object sender, RoutedEventArgs e)
        {
            ReservationListView.ItemsSource = list.FilterBySpecificTimeStart(tableManagementSystem.ReservationList, "19:00");
            ReservationListView.Items.Refresh();
        }

        private void FilterRangeTimeStart_Click(object sender, RoutedEventArgs e)
        {
            ReservationListView.ItemsSource = list.FilterByRangeTimeStart(tableManagementSystem.ReservationList, "18:00","16:30");
            ReservationListView.Items.Refresh();
        }

        private void FilterSpecificParameter_Click(object sender, RoutedEventArgs e)
        {
            ReservationListView.ItemsSource = list.FilterBySpecificParameter(tableManagementSystem.ReservationList, "window");
            ReservationListView.Items.Refresh();
        }

        private void FilterMoreParameters_Click(object sender, RoutedEventArgs e)
        {
            List<string> parameterList = new List<string>();
            parameterList.Add("window");
            parameterList.Add("flag");
            parameterList.Add("babychair");

            ReservationListView.ItemsSource = list.FilterByMoreParameters(tableManagementSystem.ReservationList, parameterList);
            ReservationListView.Items.Refresh();
        }

        private void FilterGap_Click(object sender, RoutedEventArgs e)
        {
            ReservationListView.ItemsSource = list.FilterByisgap(tableManagementSystem.ReservationList, true);
            ReservationListView.Items.Refresh();
        }

        // Back to normal
        private void FilterReset_Click(object sender, RoutedEventArgs e)
        {
            ReservationListView.ItemsSource = tableManagementSystem.ReservationList;
            ReservationListView.Items.Refresh();
        }
    }
}
