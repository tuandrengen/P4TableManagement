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
    /// Interaction logic for MapSectionEditor.xaml
    /// </summary>
    public partial class MapSectionEditor : Window
    {
        public MapSectionEditor()
        {
            InitializeComponent();
            //DrawCanvas();
        }


        private readonly List<Rectangle> AllRectangles = new List<Rectangle>();

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DrawCanvas();
        }

        private void DrawCanvas()
        {
            int SquareSize = 100;
            bool doneDrawingBackground = false;
            int nextX = 0, nextY = 0;

            // Drawing the grid of rectangles
            while (!doneDrawingBackground)
            {
                Rectangle rectangle = new Rectangle
                {
                    Width = SquareSize,
                    Height = SquareSize,
                    Stroke = Brushes.Black,
                    Fill = Brushes.White
                };

                Canvas.Children.Add(rectangle);
                AllRectangles.Add(rectangle);
                Canvas.SetTop(rectangle, nextY);
                Canvas.SetLeft(rectangle, nextX);

                nextX += SquareSize;
                if (nextX >= Canvas.ActualWidth - SquareSize)
                {
                    nextX = 0;
                    nextY += SquareSize;
                }

                if (nextY >= Canvas.ActualHeight - SquareSize)
                {
                    doneDrawingBackground = true;
                }
            }
        }

    }
}
