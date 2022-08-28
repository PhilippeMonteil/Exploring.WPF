using ResourceAssembly;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace TestResources0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Class1.Test0();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            imTest.Source = new BitmapImage(new Uri("pack://application:,,,/ResourceAssembly;Component/Folder0/Image0.jpg"));
        }
    }
}
