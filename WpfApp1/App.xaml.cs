
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        SplashScreen splashScreen;

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            splashScreen = new SplashScreen("LeGeographe.PNG");
            splashScreen.Show(false, true);
            await Task.Delay(1000);
            splashScreen.Close(TimeSpan.FromSeconds(1.0d));
        }
    }

}
