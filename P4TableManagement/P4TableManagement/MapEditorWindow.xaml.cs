using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace P4TableManagement
{
    /// <summary>
    /// Interaction logic for MapEditorWindow.xaml
    /// </summary>
    /// 
    /// TODO:
    /// 1. Save table placement for that map section
    ///     - Save the tables in a list, and return it to the main window.
    ///     - Eventually save the table placement to a .csv file, and load the table placement from that when the system starts.
    /// 2. Load the table placement in the main window
    /// 
    public partial class MapEditorWindow : Window
    {
        public MapEditorWindow()
        {
            InitializeComponent();

            //Draw map sections
            DrawCanvas();
            // ...

        }

        string filePath = @"C:\P4\MapSections";

        private void DrawCanvas()
        {
            // Vi skal tegne alle eksisterende map sections (indlæst fra en local? fil)
            // En map section tegnes med en rektangel/knap med et navn f.eks. Section 1.
            // CreateMapSection skal have sin egen knap som bliver en knap med et plus icon i midten.

            int nextX = 0, nextY = 0;
            int rowCounter = 0;
            //int startXY = 10;
            int squareSize = 100;
            bool doneDrawing = false;
            int Margin = 10;


            // Først skal der tegnes de eksisterende map sections, derefter kommer CreateMapSection knappen.
            //while (!doneDrawing)
            //{
            // Draw map sections from file
            // create button for each map section yada yada

            // Makes sure we don't continue drawing to the right when we have reached the end of the canvas
            // resets the nextX value so we draw 
            //if (nextX >= MapSectionsCanvas.ActualWidth)
            //{
            //    nextX = Margin;
            //    nextY += squareSize;
            //}
            // When we have drawn a button for each map section that exists from the given file, then we stop.
            //if (end of line = true)
            //{
            //  doneDrawing = true;
            //}
            //}
            
            string[] filePaths = Directory.GetFiles(filePath);

            foreach (var file in filePaths)
            {
                Button mapSectionButton = new Button()
                {
                    Width = squareSize,
                    Height = squareSize,
                    Background = Brushes.White,
                    Content = Path.GetFileNameWithoutExtension(file)
                };

                Canvas.SetTop(mapSectionButton, nextY + Margin);
                Canvas.SetLeft(mapSectionButton, nextX + Margin);
                //nextY += squareSize;
                nextX += squareSize+20;

                MapSectionsCanvas.Children.Add(mapSectionButton);
            }

            foreach (Button button in MapSectionsCanvas.Children.OfType<Button>())
            {
                button.Click += new RoutedEventHandler(EnterMapSection);
            }

            // We make the "plus icon" button
            Button addMapSection = new Button()
            {
                Width = squareSize,
                Height = squareSize,
                Background = Brushes.White,
                Content = new Image
                {
                    Source = new BitmapImage(new Uri("C:/P4/plus.png"))
                }
            };

            // Sets position
            Canvas.SetTop(addMapSection, nextY + Margin);
            Canvas.SetLeft(addMapSection, nextX + Margin);

            // Adds it to the Canvas
            MapSectionsCanvas.Children.Add(addMapSection);
            // Subscribes the click event
            addMapSection.Click += new RoutedEventHandler(CreateMapSection);


        } //end of DrawCanvas

        // Event for creating a new map section
        private void CreateMapSection(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "NewMapSection";
            dlg.DefaultExt = ".csv";
            dlg.Filter = "Text documents(.csv)|*.csv";
            dlg.InitialDirectory = filePath;

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                this.Hide();
                MapSectionEditor mapSectionEditor = new MapSectionEditor(Path.GetFileNameWithoutExtension(dlg.FileName));
                using (FileStream fw = File.Create(dlg.FileName))
                {
                    fw.Close();
                }

                using (var writer = new StreamWriter(dlg.FileName))
                {
                    writer.WriteLine("no;category;x;y");
                    writer.Close();
                }

                mapSectionEditor.Closed += (s, args) => this.Close();
                mapSectionEditor.Show();
            }
        }

        private void EnterMapSection(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var button = (Button)sender;
            MapSectionEditor mapSectionEditor = new MapSectionEditor(button.Content.ToString());
            mapSectionEditor.Closed += (s, args) => this.Close();
            mapSectionEditor.Show();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            main.Closed += (s, args) => this.Close();
            this.Hide();
        }
    }
}
