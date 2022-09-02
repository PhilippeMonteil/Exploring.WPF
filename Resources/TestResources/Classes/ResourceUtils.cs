using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace TestResources.Classes
{

    public static class ResourceUtils
    {

        public static void DebugResources(Assembly assembly)
        {
            Debug.WriteLine($"{nameof(ResourceUtils)}{nameof(DebugResources)}(-)");

            foreach (var resourceName in assembly.GetManifestResourceNames())
            {
                Debug.WriteLine($"  resourceName={resourceName}");

                ResourceSet set = new ResourceSet(assembly.GetManifestResourceStream(resourceName));

                foreach (DictionaryEntry resource in set)
                {
                    Debug.WriteLine("    resource.Key=[{0}] .Value='{1}'", resource.Key, resource.Value);
                }

                Debug.WriteLine("  --------------");
            }

            Debug.WriteLine($"{nameof(ResourceUtils)}{nameof(DebugResources)}(+)");
        }

    }

}
