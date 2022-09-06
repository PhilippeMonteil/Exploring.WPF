using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TestXAMLResources
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            currentDomain.ResourceResolve += CurrentDomain_ResourceResolve;
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
