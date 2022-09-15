
using Microsoft.Xaml.Behaviors;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{

    public class BehaviorTest : Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            UIElement associatedObject = this.AssociatedObject; // <T>
            Type associatedType = this.AssociatedType;
            Debug.WriteLine($"{nameof(BehaviorTest)}.{nameof(OnAttached)}() associatedObject={associatedObject} associatedType={associatedType}");
        }

        protected override void OnDetaching()
        {
            DependencyObject associatedObject = this.AssociatedObject;
            Type associatedType = this.AssociatedType;
            Debug.WriteLine($"{nameof(BehaviorTest)}.{nameof(OnDetaching)}() associatedObject={associatedObject} associatedType={associatedType}");
            base.OnDetaching();
        }

    }

}
