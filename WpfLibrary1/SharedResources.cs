using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfLibrary1
{ 

    public static class SharedResources
    {
        static ComponentResourceKey s_ComponentResourceKey = new ComponentResourceKey(typeof(SharedResources), "MyKey0");
        public static ComponentResourceKey MyKey0 => s_ComponentResourceKey;
    }

}
