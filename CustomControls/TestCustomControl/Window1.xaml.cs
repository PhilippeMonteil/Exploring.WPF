using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace TestCustomControl
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        //protected override void OnClosing(CancelEventArgs e)
        //{
        //    base.OnClosing(e);
        //}

        //protected override void OnClosed(EventArgs e)
        //{
        //    base.OnClosed(e);
        //}

        MainWindow mainWindow;
        Class1? class1;

        private void Button0_Click(object sender, RoutedEventArgs e)
        {
            mainWindow = new MainWindow();

            mainWindow.Closed += MainWindow_Closed;

            mainWindow.Control1.FileNameChanged += Control1_FileNameChanged;

            class1 = new Class1();

            mainWindow.Control1.FileNameChanged += class1.FileNameChanged;

            //mainWindow.Closing += (object? sender, CancelEventArgs e) =>
            //{
            //    Debug.WriteLine($"mainWindow.Closing()");
            //    mainWindow.Control1.FileNameChanged -= class1.FileNameChanged;
            //};

            mainWindow.Show();
        }

        private void MainWindow_Closed(object? sender, EventArgs e)
        {
            Debug.WriteLine($"{nameof(MainWindow_Closed)}(-)");

            // mainWindow.Control1.FileNameChanged -= class1.FileNameChanged;
            // class1 = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Debug.WriteLine($"{nameof(MainWindow_Closed)}(+)");
        }

        private void Control1_FileNameChanged(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"{nameof(Control1_FileNameChanged)}()");
        }

    }

}
