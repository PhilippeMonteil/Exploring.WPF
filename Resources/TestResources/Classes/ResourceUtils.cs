using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

            // GetManifestResourceNames
            foreach (string resourceName in assembly.GetManifestResourceNames())
            {
                try
                {
                    Debug.WriteLine("");
                    Debug.WriteLine($"  resourceName={resourceName}");

                    // GetManifestResourceStream
                    Stream? stream = assembly.GetManifestResourceStream(resourceName);
                    Debug.WriteLine($"  stream={stream}");

                    // ResourceSet
                    ResourceSet set = new ResourceSet(stream);

                    Debug.WriteLine($"    stream={stream}");
                    Debug.WriteLine($"    set={set}");

                    // DictionaryEntry
                    foreach (DictionaryEntry resource in set)
                    {
                        Debug.WriteLine("      resource.Key=[{0}] .Value='{1}'", resource.Key, resource.Value);
                    }
                }
                catch (Exception E)
                {
                    Debug.WriteLine($"Exception={E.Message}");
                }
                finally
                {
                    Debug.WriteLine("  --------------");
                }
            }

            Debug.WriteLine($"{nameof(ResourceUtils)}{nameof(DebugResources)}(+)");
        }

    }

}
