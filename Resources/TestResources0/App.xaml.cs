using ResourceAssembly;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Navigation;

namespace TestResources0
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
            :base()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            // assembly = Application.ResourceAssembly;

            {
                foreach (var resourceName in assembly.GetManifestResourceNames())
                {
                    Debug.WriteLine($"resourceName={resourceName}");

                    ResourceSet set = new ResourceSet(Application.ResourceAssembly.GetManifestResourceStream(resourceName));

                    foreach (DictionaryEntry resource in set)
                    {
                        Debug.WriteLine("  resource=[{0}] \t{1}", resource.Key, resource.Value);
                    }

                    Debug.WriteLine("--------------");
                }
            }

        }

    }

}
