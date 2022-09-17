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
            int _im = this.Children.Count;

            foreach (UIElement child in Children)
            {
                child.Measure(availableSize);
            }

            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            int _im = this.Children.Count;

            foreach (UIElement child in Children)
            {
                child.Arrange(new Rect(new Point(0, 0), finalSize));
            }

            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Arrange(new Rect(new Point(i * 100, i * 50), finalSize));
            }

            return base.ArrangeOverride(finalSize);
        }

    }

}
