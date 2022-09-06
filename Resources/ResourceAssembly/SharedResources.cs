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
        public static ComponentResourceKey RedBrushKey
        {
            get { return new ComponentResourceKey(typeof(SharedResources), "RedSolidBrush"); }
        }

    }

}
