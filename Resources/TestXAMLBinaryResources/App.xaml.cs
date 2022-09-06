
using ResourceAssembly;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using System.Windows;

namespace TestXAMLBinaryResources
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            AppDomain.CurrentDomain.Load("ResourceAssembly");

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            currentDomain.ResourceResolve += CurrentDomain_ResourceResolve;

            Object _o = ExportResourceDictionary1.Instance;
        }

        private System.Reflection.Assembly? CurrentDomain_ResourceResolve(object? sender, ResolveEventArgs args)
        {
            Debug.WriteLine($"CurrentDomain_ResourceResolve .Name={args.Name} .RequestingAssembly={args.RequestingAssembly}");
            return null;
        }

        private System.Reflection.Assembly? CurrentDomain_AssemblyResolve(object? sender, ResolveEventArgs args)
        {
            Debug.WriteLine($"CurrentDomain_AssemblyResolve .Name={args.Name} .RequestingAssembly={args.RequestingAssembly}");
            return null;
        }

    }

}
