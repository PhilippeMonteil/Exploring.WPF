
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
using System.Resources;
using System.Diagnostics;

using TestResources.Resources;
using System.Threading;

namespace TestResources
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ResourceManager rm = new ResourceManager(typeof(Resource1));

        private void bnTest_Click(object sender, RoutedEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es");

            Debug.WriteLine($"");
            Debug.WriteLine($"Thread.CurrentThread.CurrentUICulture={Thread.CurrentThread.CurrentUICulture}");
            Debug.WriteLine($"Properties.Resources.String1={Resource1.String1}");
            Debug.WriteLine($"rm.String1={rm.GetString("String1")}");

            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr");

            Debug.WriteLine($"");
            Debug.WriteLine($"Thread.CurrentThread.CurrentUICulture={Thread.CurrentThread.CurrentUICulture}");
            Debug.WriteLine($"Properties.Resources.String1={Resource1.String1}");
            Debug.WriteLine($"rm.String1={rm.GetString("String1")}");
        }
    }
}
