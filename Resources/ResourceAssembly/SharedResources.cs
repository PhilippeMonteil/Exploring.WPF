
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
        static ComponentResourceKey s_DarkGreenKey = 
                                    new ComponentResourceKey(typeof(SharedResources), "DarkGreen");

        public static ComponentResourceKey DarkGreenKey => s_DarkGreenKey;

    }

}
