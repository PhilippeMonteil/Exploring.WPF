
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
using System.Windows.Resources;
using System.Windows.Shapes;

namespace TestXAMLBinaryResources
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

        static void log(string Txt)
        {
            Debug.WriteLine($"{Txt}");
        }

        static void Test(string Path, UriKind uriKind)
        {
            try
            {
                log($"{nameof(Test)}(-) '{Path}' uriKind={uriKind}");
                Uri uri = new Uri(Path, uriKind);
                StreamResourceInfo info = Application.GetResourceStream(uri);
                log($"info .ContentType='{info?.ContentType}' .Stream{info?.Stream}");
            }
            catch (Exception E)
            {
                log($"{nameof(Test)} EXCEPTION={E.Message}");
            }
            finally
            {
                log($"{nameof(Test)}(+)");
            }
        }

        //<Image Source = "BinaryResources/Folder0/ResourceImage.PNG" Height="100" Margin="8"/>
        //<Image Source = "BinaryResources/Folder0/ContentImage.PNG" Height="100" Margin="8"/>

        //<!-- / ResourceAssembly, Content, Always Copy -->
        //<Image Source = "pack://siteOfOrigin:,,,/Folder0/Content0.jpg" Height="100" Margin="8"/>
        
        //<!-- KO -->
        //<Image Source = "/ResourceAssembly;component/Folder0/Content0.jpg" Height="100" Margin="8"/>
        //<Image Source = "/ResourceAssembly;component/Folder0/Content1.jpg" Height="100" Margin="8"/>
        
        //<Image Source = "/ResourceAssembly;component/Folder0/Resource0.jpg" Height="100" Margin="8"/>

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Test("/BinaryResources/Folder0/ResourceImage.PNG", UriKind.Relative);
            Test("pack://application:,,,/BinaryResources/Folder0/ResourceImage.PNG", UriKind.Absolute);

            Test("/BinaryResources/Folder0/ContentImage.PNG", UriKind.Relative);
            Test("pack://siteOfOrigin:,,,/Folder0/Content0.jpg", UriKind.Absolute);
            Test("pack://application:,,,/Folder0/Content0.jpg", UriKind.Absolute);
        }

    }

}
