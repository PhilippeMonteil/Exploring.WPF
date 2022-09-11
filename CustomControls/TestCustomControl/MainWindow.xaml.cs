using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace TestCustomControl
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

        private void myUserControl_FileNameChanged(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"{nameof(myUserControl_FileNameChanged)} {e.Source}");
        }

        private void CustomControl1_FileNameChanged(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"{nameof(CustomControl1_FileNameChanged)} {e.Source}");
        }
    }
}
