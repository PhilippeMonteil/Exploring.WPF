using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows;

namespace TestMarkupExtension
{

    [MarkupExtensionReturnType(typeof(string))]
    public class MarkupExtensionLocalization : MarkupExtension
    {
        
        public MarkupExtensionLocalization(string id)
        {
            this.ID = id;
            Debug.WriteLine($"{GetType().Name}[{GetHashCode()}].constructor");
        }

        ~MarkupExtensionLocalization()
        {
            Debug.WriteLine($"{GetType().Name}[{GetHashCode()}].finalizer");
        }

        [ConstructorArgument("id")]
        public string ID 
        { 
            get; 
            set; 
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            try
            {
                Debug.WriteLine($"{GetType().Name}[{GetHashCode()}].ProvideValue(-)");

                IProvideValueTarget provideValueTarget = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;

                // TargetObject, TargetProperty
                DependencyObject dependencyObject = provideValueTarget.TargetObject as DependencyObject;
                DependencyProperty dependencyProperty = provideValueTarget.TargetProperty as DependencyProperty;

                Debug.WriteLine($"  TargetObject={dependencyObject}");
                Debug.WriteLine($"  TargetProperty={dependencyProperty}");

                string vret = $"ID[{Thread.CurrentThread.CurrentUICulture.ToString()}]={ID}";

                Task.Run(() =>
                {
                    Thread.Sleep(1000);
                    dependencyObject.Dispatcher.BeginInvoke(() =>
                    {
                        // SetCurrentValue / (TargetObject, TargetProperty)
                        dependencyObject.SetCurrentValue(dependencyProperty, $"{vret}(++)");
                    });
                });

                return vret;
            }
            finally
            {
                Debug.WriteLine($"{GetType().Name}[{GetHashCode()}].ProvideValue(+)");
            }
        }

    }

}
