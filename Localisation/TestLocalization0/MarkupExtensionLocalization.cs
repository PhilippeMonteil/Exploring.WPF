
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace TestLocalization0
{

    [MarkupExtensionReturnType(typeof(string))]
    public class MarkupExtensionLocalization : MarkupExtension
    {

        public MarkupExtensionLocalization(string id)
        {
            this.ID = id;
        }

        [ConstructorArgument("id")]
        public string ID { get; set; }

        static List<Window> s_aWindow = new List<Window>();
        static List<FrameworkElement> s_aFrameworkElement = new List<FrameworkElement>();

        static void check(DependencyObject dependencyObject)
        {
            Window window = null;

            {
                FrameworkElement _frameworkElement = dependencyObject as FrameworkElement;
                while (_frameworkElement != null)
                {
                    if (_frameworkElement is Window)
                    {
                        window = _frameworkElement as Window;
                        break;
                    }
                    _frameworkElement = _frameworkElement.Parent as FrameworkElement;
                }
            }
            if (window == null) return;

            // s_aWindow
            {
                int _index = s_aWindow.IndexOf(window);
                if (_index < 0)
                {
                    s_aWindow.Add(window);
                    window.Closing += Window_Closing;
                    _index = s_aWindow.IndexOf(window);
                }
            }

            // s_aFrameworkElement
            {
                FrameworkElement _frameworkElement = dependencyObject as FrameworkElement;
                int _index = s_aFrameworkElement.IndexOf(_frameworkElement);
                if (_index < 0)
                {
                    s_aFrameworkElement.Add(window);
                }
            }

        }

        private static void Window_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            Window _window = sender as Window;
            int _index = s_aWindow.IndexOf(_window);
            if (_index < 0) return;
            s_aWindow.Remove(_window);
            _window.Closing-= Window_Closing;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            IProvideValueTarget provideValueTarget = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            Debug.WriteLine($"ProvideValue");

            DependencyObject dependencyObject = provideValueTarget.TargetObject as DependencyObject;
            DependencyProperty dependencyProperty = provideValueTarget.TargetProperty as DependencyProperty;

            Debug.WriteLine($"  TargetObject={dependencyObject}");
            Debug.WriteLine($"  TargetProperty={dependencyProperty}");

            check(dependencyObject);

            string vret = $"ID[{Thread.CurrentThread.CurrentUICulture.ToString()}]={ID}";

            Task.Run(() =>
            {
                Thread.Sleep(1000);
                dependencyObject.Dispatcher.BeginInvoke(() =>
                {
                    dependencyObject.SetCurrentValue(dependencyProperty, $"{vret}(++)");
                });
            });

            return vret;
        }

    }

}
