using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ResourceAssembly
{

    public static class SharedResources
    {
        static ComponentResourceKey s_RedBrushKey = new ComponentResourceKey(typeof(SharedResources), "RedBrushKey");

        public static ComponentResourceKey RedBrushKey => s_RedBrushKey;

    }

}
