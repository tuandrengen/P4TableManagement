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
using System.Windows.Shapes;

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


            // We make the "plus icon" button
            Button button = new Button()
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
            Canvas.SetTop(button, nextY + Margin);
            Canvas.SetLeft(button, nextX + Margin);

            // Adds it to the Canvas
            MapSectionsCanvas.Children.Add(button);

            // Subscribes the click event
            button.Click += new RoutedEventHandler(CreateMapSection);
        } //end of DrawCanvas

        // Event for creating a new map section
        private void CreateMapSection(object sender, RoutedEventArgs e)
        {
            MapSectionEditor mapSectionEditor = new MapSectionEditor();
            mapSectionEditor.ShowDialog();
            this.Close();
        }
    }
}
