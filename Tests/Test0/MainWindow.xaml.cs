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

namespace Test0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DrawingVisual drawingVisual = null;

        public MainWindow()
        {
            InitializeComponent();

            drawingVisual = new DrawingVisual();

            using (DrawingContext dc = drawingVisual.RenderOpen())
            {
                DrawingGroup drawingGroup = new DrawingGroup();

                using (DrawingContext _dc = drawingGroup.Open())
                {
                    _dc.DrawRectangle(new SolidColorBrush(Colors.Red),
                        new Pen(new SolidColorBrush(Colors.Blue), 4),
                        new Rect(10, 10, 300, 300));
                }

                dc.DrawDrawing(drawingGroup);
            }

            drawingVisual.Transform = new RotateTransform(45);
        }

        protected override int VisualChildrenCount => drawingVisual != null ? 1 : 0;

        protected override Visual GetVisualChild(int index)
        {
            return index == 0 ? drawingVisual : null;
        }

    }
}
