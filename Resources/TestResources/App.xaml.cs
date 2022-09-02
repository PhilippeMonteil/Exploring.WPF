using System.Reflection;
using System.Windows;
using TestResources.Classes;

namespace TestResources
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
            : base()
        {
            ResourceUtils.DebugResources(Assembly.GetExecutingAssembly());
        }

    }
}
