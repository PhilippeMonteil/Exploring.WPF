
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
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

        static void Test_GetResourceStream(string Path, UriKind uriKind)
        {
            try
            {
                log("");
                log($"{nameof(Test_GetResourceStream)}(-) '{Path}' uriKind={uriKind}");
                Uri uri = new Uri(Path, uriKind);
                StreamResourceInfo info = Application.GetResourceStream(uri);
                log($"info .ContentType='{info?.ContentType}' .Stream{info?.Stream}");
            }
            catch (Exception E)
            {
                log($"{nameof(Test_GetResourceStream)} EXCEPTION={E.Message}");
            }
            finally
            {
                log($"{nameof(Test_GetResourceStream)}(+)");
            }
        }

        static void Test_GetContentStream(string Path, UriKind uriKind)
        {
            try
            {
                log("");
                log($"{nameof(Test_GetContentStream)}(-) '{Path}' uriKind={uriKind}");
                Uri uri = new Uri(Path, uriKind);
                StreamResourceInfo info = Application.GetContentStream(uri);
                log($"info .ContentType='{info?.ContentType}' .Stream{info?.Stream}");
            }
            catch (Exception E)
            {
                log($"{nameof(Test_GetContentStream)} EXCEPTION={E.Message}");
            }
            finally
            {
                log($"{nameof(Test_GetContentStream)}(+)");
            }
        }

        static void Test_ManifestResourceStreams(Assembly assembly)
        {
            try
            {
                log("");
                log($"{nameof(Test_ManifestResourceStreams)}(-) assembly={assembly}");

                foreach (string name in assembly.GetManifestResourceNames())
                {
                    Stream? _stream = assembly.GetManifestResourceStream(name);
                    log($"name='{name}' _stream={_stream}");

                    try
                    {
                        using (ResourceSet _rs = new ResourceSet(_stream))
                        {
                            foreach (DictionaryEntry _r in _rs)
                            {
                                log($"  _r.Key={_r.Key} _r.Value={_r.Value}");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        log($"Exception='{e.Message}'");
                    }
                }
            }
            catch (Exception E)
            {
                log($"{nameof(Test_ManifestResourceStreams)} EXCEPTION={E.Message}");
            }
            finally
            {
                log($"{nameof(Test_ManifestResourceStreams)}(+)");
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
            Test_ManifestResourceStreams(AppDomain.CurrentDomain.Load("ResourceAssembly"));
            return;

            Test_GetContentStream("/BinaryResources/Folder0/ContentImage.PNG", UriKind.Relative);
            Test_GetContentStream("/BinaryResources/Folder0/ResourceImage.PNG", UriKind.Relative);

            Test_GetResourceStream("/BinaryResources/Folder0/ResourceImage.PNG", UriKind.Relative);
            Test_GetResourceStream("pack://application:,,,/BinaryResources/Folder0/ResourceImage.PNG", UriKind.Absolute);

            Test_GetResourceStream("/BinaryResources/Folder0/ContentImage.PNG", UriKind.Relative);
            Test_GetResourceStream("pack://siteOfOrigin:,,,/Folder0/Content0.jpg", UriKind.Absolute);
            Test_GetResourceStream("pack://application:,,,/Folder0/Content0.jpg", UriKind.Absolute);
        }

    }

}
