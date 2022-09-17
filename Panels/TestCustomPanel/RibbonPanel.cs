using System;
using System.Collections.Generic;
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

            UIElement _firstChild = (UIElement)Children[0];

            _firstChild.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            if (Children.Count == 1)
            {
                return _firstChild.DesiredSize;
            }

            double _child0_Width = _firstChild.DesiredSize.Width;

            double _child_MaxWidth = 0;
            for (int i = 1; i < Children.Count; i++)
            {
                Children[i].Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                if (_child_MaxWidth < Children[i].DesiredSize.Width)
                {
                    _child_MaxWidth = Children[i].DesiredSize.Width;
                }
            }

            foreach (UIElement child in Children)
            {
                child.Measure(availableSize);
            }

            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            int _im = this.Children.Count;

            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Arrange(new Rect(new Point(i * 100, i * 50), finalSize));
            }

            return base.ArrangeOverride(finalSize);
        }

    }

}
