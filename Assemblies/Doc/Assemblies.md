
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

## [Global Assembly Cache](https://learn.microsoft.com/en-us/dotnet/framework/app-domains/gac)

- Each computer where the Common Language Runtime is installed has a machine-wide code cache called 
  the Global Assembly Cache. The Global Assembly Cache stores assemblies specifically designated 
  to be shared by several applications on the computer.

- Assemblies deployed in the Global Assembly Cache must have a strong name. 

- When an assembly is added to the Global Assembly Cache, integrity checks are performed on 
  all files that make up the assembly. 

- The cache performs these integrity checks to ensure that an assembly has not been tampered with, 
  for example, when a file has changed but the manifest does not reflect the change.

- You should share assemblies by installing them into the Global Assembly Cache only when you need to. 

- As a general guideline, keep assembly dependencies private, and locate assemblies in the 
  application directory unless sharing an assembly is explicitly required. 

## [Strong Name Signing](https://github.com/dotnet/runtime/blob/main/docs/project/strong-name-signing.md)

## [Strong-named assemblies](https://learn.microsoft.com/en-us/dotnet/standard/assembly/strong-named)

- Strong-naming an assembly creates a unique identity for the assembly, and can prevent assembly conflicts.

- When a strong-named assembly is created, it contains :
    - the simple text name of the assembly
    - the version number
    - optional culture information
    - a digital signature
    - the public key that corresponds to the private key used for signing.

## [Delay-sign an assembly](https://learn.microsoft.com/en-us/dotnet/standard/assembly/delay-sign)


