using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TestCustomPanel
{

    public class RibbonPanel : Panel
    {

        protected override Size MeasureOverride(Size availableSize)
        {
            if (Children.Count <= 0) return new Size(0, 0);

            {
                Debug.WriteLine($"MeasureOverride(-) availableSize={availableSize}");
                foreach (UIElement ui in Children)
                {
                    ui.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    Debug.WriteLine($"  ui.DesiredSize={ui.DesiredSize}");
                }
            }

            UIElement _firstChild = (UIElement)Children[0];

            _firstChild.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            if (Children.Count == 1)
            {
                return _firstChild.DesiredSize;
            }

            double _numCol = Math.Ceiling((Children.Count - 1) / 3d);

            double _child_MaxWidth = 0;
            for (int i = 1; i < Children.Count; i++)
            {
                UIElement child = Children[i];
                child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                _child_MaxWidth = Math.Max(_child_MaxWidth, child.DesiredSize.Width);
            }

            Size vret = new Size(_firstChild.DesiredSize.Width + _numCol * _child_MaxWidth,
                                    _firstChild.DesiredSize.Height);

            Debug.WriteLine($"MeasureOverride(+) vret={vret}");

            return vret;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Size vret = finalSize;
            try
            {
                Debug.WriteLine($"ArrangeOverride(-) finalSize={finalSize}");

                if (Children.Count < 1) return finalSize;

                UIElement firstChild = (UIElement)Children[0];
                Point childOrigin = new Point(0, 0);
                Size firstChildSize = new Size(firstChild.DesiredSize.Width, finalSize.Height);

                firstChild.Arrange(new Rect(childOrigin, firstChildSize));

                if (Children.Count < 2) return finalSize;

                double numCol = Math.Ceiling((Children.Count - 1) / 3d);
                Size childSize = new Size((finalSize.Width - firstChildSize.Width) / numCol,
                                            finalSize.Height / 3);
                childOrigin.X += firstChildSize.Width;

                for (int i = 1; i < Children.Count; i++)
                {
                    UIElement child = Children[i];
                    child.Arrange(new Rect(childOrigin, childSize));
                    if (i % 3 == 0)
                    {
                        childOrigin.X += childSize.Width;
                        childOrigin.Y = 0;
                    }
                    else
                    {
                        childOrigin.Y += childSize.Height;
                    }
                }

                return vret;
            }
            finally
            {
                Debug.WriteLine($"ArrangeOverride(+) vret={vret}");
            }
        }

    }

}
