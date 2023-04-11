
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;

namespace TestLocalization0
{

    [MarkupExtensionReturnType(typeof(string))]
    public class MarkupExtensionLocalization : MarkupExtension, IDisposable
    {

        #region --- static

        class Target
        {
            public readonly FrameworkElement frameworkElement;
            public readonly DependencyProperty property;
            public readonly string id;

            CultureInfo cultureInfo;

            public Target(FrameworkElement frameworkElement, DependencyProperty property, string id)
            {
                this.frameworkElement = frameworkElement;
                this.property = property;
                this.id = id;
            }

            public bool Match(FrameworkElement frameworkElement, DependencyProperty property, string id)
            {
                return Object.ReferenceEquals(this.frameworkElement, frameworkElement)
                    && Object.ReferenceEquals(this.property, property)
                    && string.Equals(this.id, id);
            }

            public bool Match(FrameworkElement frameworkElement)
            {
                return Object.ReferenceEquals(this.frameworkElement, frameworkElement);
            }

            public bool Update(CultureInfo cultureInfo)
            {
                if (this.cultureInfo == cultureInfo) return false;

                this.cultureInfo = cultureInfo;

                frameworkElement.Dispatcher.BeginInvoke(() =>
                {
                    frameworkElement.SetCurrentValue(property, $"{this.cultureInfo}[{this.id}]");
                });

                return true;
            }

        }

        static List<Target> s_aTarget = new List<Target>();
        static SortedDictionary<int, FrameworkElement> s_sd_FrameworkElement = new SortedDictionary<int, FrameworkElement>();

        static Target registerTarget(FrameworkElement frameworkElement, DependencyProperty property, string id)
        {
            Target _target = s_aTarget.FirstOrDefault(target => target.Match(frameworkElement, property, id));
            if (_target == null)
            {
                _target = new Target(frameworkElement, property, id);
                s_aTarget.Add(_target);
            }

            if (s_sd_FrameworkElement.ContainsKey(frameworkElement.GetHashCode()) == false)
            {
                s_sd_FrameworkElement.Add(frameworkElement.GetHashCode(), frameworkElement);
                frameworkElement.Unloaded += frameworkElement_Unloaded;
            }

            return _target;
        }

        private static void frameworkElement_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Debug.WriteLine($"{nameof(frameworkElement_Unloaded)}(-) sender={sender}");

                FrameworkElement frameworkElement = sender as FrameworkElement;
                if (frameworkElement == null) return;

                frameworkElement.Unloaded -= frameworkElement_Unloaded;

                s_sd_FrameworkElement.Remove(frameworkElement.GetHashCode());

                {
                    List<Target> _aremove = new List<Target>();
                    foreach (Target _target in s_aTarget)
                    {
                        if (_target.Match(frameworkElement))
                        {
                            _aremove.Add(_target);
                        }
                    }
                    foreach (Target _target in _aremove)
                    {
                        s_aTarget.Remove(_target);
                    }
                }

            }
            finally
            {
                Debug.WriteLine($"{nameof(frameworkElement_Unloaded)}(+)");
            }
        }

        static System.Windows.Threading.DispatcherTimer s_Timer;

        static void update_Targets()
        {
            if (s_Timer == null)
            {
                s_Timer = new System.Windows.Threading.DispatcherTimer();
                s_Timer.Tick += new EventHandler((sender, e) =>
                {
                    _update_Targets(Thread.CurrentThread.CurrentUICulture);
                });
                s_Timer.Interval = TimeSpan.FromMilliseconds(100);
                s_Timer.Start();
            }
        }

        static int s__update_Targets;

        static void _update_Targets(CultureInfo cultureInfo)
        {
            int _update_Targets_count = s__update_Targets++;
            //Debug.WriteLine($"{nameof(_update_Targets)}[{_update_Targets_count}](-) cultureInfo={cultureInfo}");

            int _targetCount = 0;
            int _updateCount = 0;
            foreach (Target target in s_aTarget)
            {
                if (target == null) continue;
                _targetCount++;
                if (target.Update(cultureInfo)) _updateCount++;
            }

            //Debug.WriteLine($"{nameof(_update_Targets)}[{_update_Targets_count}](+) _updateCount={_updateCount} / _targetCount={_targetCount}");
        }

        #endregion

        public MarkupExtensionLocalization(string id)
        {
            this.ID = id;
            Debug.WriteLine($"{GetType().Name}[{GetHashCode()}].constructor");
        }

        ~MarkupExtensionLocalization()
        {
            Debug.WriteLine($"{GetType().Name}[{GetHashCode()}].finalizer");
        }

        public void Dispose()
        {
            Debug.WriteLine($"{GetType().Name}[{GetHashCode()}].Dispose");
        }

        [ConstructorArgument("id")]
        public string ID { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            try
            {
                Debug.WriteLine($"{GetType().Name}[{GetHashCode()}].ProvideValue(-)");

                IProvideValueTarget provideValueTarget = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;

                FrameworkElement frameworkElement = provideValueTarget.TargetObject as FrameworkElement;
                if (frameworkElement == null) return null;

                DependencyProperty dependencyProperty = provideValueTarget.TargetProperty as DependencyProperty;

                Debug.WriteLine($"  frameworkElement={frameworkElement}");
                Debug.WriteLine($"  dependencyProperty={dependencyProperty}");

                registerTarget(frameworkElement, dependencyProperty, ID);

                update_Targets();

                return string.Empty;
            }
            finally
            {
                Debug.WriteLine($"{GetType().Name}[{GetHashCode()}].ProvideValue(+)");
            }
        }
    }

}
