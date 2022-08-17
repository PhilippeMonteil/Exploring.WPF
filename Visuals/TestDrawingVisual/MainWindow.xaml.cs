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

namespace TestDrawingVisual
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DrawingVisual drawingVisual;

        public MainWindow()
        {
            InitializeComponent();

            DrawingGroup drawingGroup = new DrawingGroup();
            {
                using (DrawingContext drawingContext = drawingGroup.Open())
                {
                    drawingContext.DrawRectangle(new SolidColorBrush(Colors.DarkGray),
                        new Pen(new SolidColorBrush(Colors.Green), 1),
                        new Rect(5, 5, 200, 100));
                }
            }

            drawingVisual = new DrawingVisual();

            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawRectangle(new SolidColorBrush(Colors.Blue),
                    new Pen(new SolidColorBrush(Colors.Red), 1),
                    new Rect(50, 100, 500, 300));

                drawingContext.DrawDrawing(drawingGroup);
            }
        }

        protected override int VisualChildrenCount => drawingVisual != null ? 1 : 0;

        protected override Visual GetVisualChild(int index)
        {
            return index == 0 ? drawingVisual : null;
        }

    }
}
