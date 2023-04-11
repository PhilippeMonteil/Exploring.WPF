using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace TestLocalization0
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static CultureInfo FR = new CultureInfo("fr");
        static CultureInfo EN = new CultureInfo("en");
        static CultureInfo ES = new CultureInfo("es");

        public MainWindow()
        {
            Thread.CurrentThread.CurrentUICulture = FR;
            InitializeComponent();
        }

        private void bnTest_Click(object sender, RoutedEventArgs e)
        {
            if (Thread.CurrentThread.CurrentUICulture.Equals(FR))
            {
                Thread.CurrentThread.CurrentUICulture = EN;
            }
            else if (Thread.CurrentThread.CurrentUICulture.Equals(EN))
            {
                Thread.CurrentThread.CurrentUICulture = ES;
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = FR;
            }
            Debug.WriteLine($"{nameof(bnTest_Click)}(+) Thread.CurrentThread.CurrentUICulture={Thread.CurrentThread.CurrentUICulture}");
        }

        private void bnNewWindow_Click(object sender, RoutedEventArgs e)
        {
            var window = new Window1();
            window.Show();
        }
    }

}
