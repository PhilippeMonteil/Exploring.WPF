
# [Assemblies in .NET](https://docs.microsoft.com/en-us/dotnet/standard/assembly/)

## [Assembly class](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly?view=net-6.0)

### Example

    foreach (var res in Application.ResourceAssembly.GetManifestResourceNames())
    {
        Debug.WriteLine(res);

        ResourceSet set = new ResourceSet(Application.ResourceAssembly.GetManifestResourceStream(res));

        foreach (DictionaryEntry resource in set)
        {
            Debug.WriteLine("\n[{0}] \t{1}", resource.Key, resource.Value);
        }
    }

## [ResourceManager Class](https://docs.microsoft.com/en-us/dotnet/api/system.resources.resourcemanager?view=net-6.0)
