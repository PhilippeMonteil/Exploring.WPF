
# TestResources

## Règles à retenir

- les XAML des App, Windows sont compilés sous forme de .baml et regroupés avec
  les ressources 'Resource' dans un ResourceSet sérialisé dans le ManifestResourceStream
  nommé 'TestResources.g.resources'

- ressources 'Content' : 

    - pour chaque ressource de ce type un attribut d'assembly AssemblyAssociatedContentFile est
      inscrit dans le fichier TestResources_Content.g.cs 
      généré dans le répertoire \obj\Debug\net6.0-windows:

        - TestResources_Content.g.cs
        [assembly: System.Windows.Resources.AssemblyAssociatedContentFileAttribute("binaryresources/folder0/contentimage.png")]

## Ressources : Binary resources et .resx

- BinaryResources/Folder0
    - ContentImage.PNG : Build Action : 'Content'; Copy to Output : Copy Always
    - ResourceImage.png : Build Action : 'Resource'
    - EmbeddedResourceImage.jpeg : Build Action : 'Embedded Resource'

- Resources
    - Resource1.resx
    - Resource1.en.resx
    - ...

### .csproj

    <Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <OutputType>WinExe</OutputType>
        <TargetFramework>net6.0-windows</TargetFramework>
        <Nullable>enable</Nullable>
        <UseWPF>true</UseWPF>
        <Configurations>Debug;Release;Test</Configurations>
        <Platforms>AnyCPU;ARM64;x86;ARM32</Platforms>
    </PropertyGroup>

    <ItemGroup>
        <PackageReference Include="System.Resources.ResourceManager" Version="4.3.0" />
    </ItemGroup>
    <ItemGroup>
        <ProjectReference Include="..\ResourceAssembly\ResourceAssembly.csproj" />
    </ItemGroup>

    <ItemGroup>
        <Content Include="BinaryResources\Folder0\ContentImage.PNG">
            <CopyToOutputDirectory>Always</CopyToOutputDirectory>
        </Content>
    </ItemGroup>

    <ItemGroup>
        <EmbeddedResource Include="BinaryResources\Folder0\EmbeddedResourceImage.jpeg" />
    </ItemGroup>

    <ItemGroup>
        <Resource Include="BinaryResources\Folder0\ResourceImage.png" />
    </ItemGroup>

    <ItemGroup>
        <Compile Update="Resources\Resource1.Designer.cs">
            <DesignTime>True</DesignTime>
            <AutoGen>True</AutoGen>
            <DependentUpon>Resource1.resx</DependentUpon>
        </Compile>
        <Compile Update="Resources\Settings1.Designer.cs">
            <DesignTimeSharedInput>True</DesignTimeSharedInput>
            <AutoGen>True</AutoGen>
        <DependentUpon>Settings1.settings</DependentUpon>
        </Compile>
    </ItemGroup>

    <ItemGroup>
        <EmbeddedResource Update="Resources\Resource1.resx">
            <Generator>ResXFileCodeGenerator</Generator>
            <LastGenOutput>Resource1.Designer.cs</LastGenOutput>
        </EmbeddedResource>
    </ItemGroup>

    <ItemGroup>
        <None Update="Resources\Settings1.settings">
            <Generator>SettingsSingleFileGenerator</Generator>
            <LastGenOutput>Settings1.Designer.cs</LastGenOutput>
        </None>
    </ItemGroup>

    </Project>


## GetManifestResourceNames

### DebugResources : GetManifestResourceNames, GetManifestResourceStream, ResourceSet

     public static void DebugResources(Assembly assembly)
     {
        Debug.WriteLine($"{nameof(ResourceUtils)}{nameof(DebugResources)}(-)");

        // GetManifestResourceNames
        foreach (var resourceName in assembly.GetManifestResourceNames())
        {
            Debug.WriteLine($"  resourceName={resourceName}");

            // GetManifestResourceStream
            ResourceSet set = new ResourceSet(assembly.GetManifestResourceStream(resourceName));

            foreach (DictionaryEntry resource in set)
            {
                Debug.WriteLine("    resource.Key=[{0}] .Value='{1}'", resource.Key, resource.Value);
            }

            Debug.WriteLine("  --------------");
        }

        Debug.WriteLine($"{nameof(ResourceUtils)}{nameof(DebugResources)}(+)");
     }

### Debug

```
ResourceUtilsDebugResources(-)

  resourceName=TestResources.g.resources
  stream=System.Reflection.RuntimeAssembly+ManifestResourceStream
    stream=System.Reflection.RuntimeAssembly+ManifestResourceStream
    set=System.Resources.ResourceSet
      resource.Key=[binaryresources/folder0/resourceimage.png] .Value='System.IO.UnmanagedMemoryStream'
      resource.Key=[mainwindow.baml] .Value='System.IO.UnmanagedMemoryStream'
      resource.Key=[app.baml] .Value='System.IO.UnmanagedMemoryStream'
  --------------

  resourceName=TestResources.Resources.Resource1.resources
  stream=System.Reflection.RuntimeAssembly+ManifestResourceStream
    stream=System.Reflection.RuntimeAssembly+ManifestResourceStream
    set=System.Resources.ResourceSet
      resource.Key=[String1] .Value='Value1'
      resource.Key=[String2] .Value='Value2'
  --------------

  resourceName=TestResources.BinaryResources.Folder0.EmbeddedResourceImage.jpeg
  stream=System.Reflection.RuntimeAssembly+ManifestResourceStream
Exception thrown: 'System.ArgumentException' in System.Private.CoreLib.dll
Exception=Stream is not a valid resource file.
  --------------

ResourceUtilsDebugResources(+)
```

__Remarque__ : TestResources.g.resources

- Les 'ressources binaires' __Resource__ d'une assembly, TestResources0 ici, apparaissent comme des entrées
dans le __ResourceSet__ TestResources.g.resources.
- les .xaml : App, MainWindow, ... apparaissent sous leur forme compilée, .balm, dans le __ResourceSet__ TestResources.g.resources.  

__Remarque__ : TestResources.Resources.Resource1.resources

- chaque .resx fait l'objet d'un ResourceSet séparé : TestResources.Resources.Resource1.resources ici.

### Localisation de ressources .resx

- Créer une ressource __\<resource>.resx__
- Créer des ressources __\<resource>.\<culture>.resx__

Le 'CustomTool' (ResXFileCodeGenerator) associé à \<resource>.resx génère une classe
__TestResources.Resources.Resource1

    namespace TestResources.Resources 
    {
        using System;
    
        /// <summary>
        ///   A strongly-typed resource class, for looking up localized strings, etc.
        /// </summary>
        // This class was auto-generated by the StronglyTypedResourceBuilder
        // class via a tool like ResGen or Visual Studio.
        // To add or remove a member, edit your .ResX file then rerun ResGen
        // with the /str option, or rebuild your VS project.
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        internal class Resource1 {

qui expose des propriétés statiques:

    internal static global::System.Globalization.CultureInfo Culture
    internal static string String1 // pour chaque Key

Pour chaque \<Culture> un assembly satellite de même nom, __TestResources.resources.dll__,
est créé et copié dans un sous-répertoire \<Culture>.

L'assignation de la propriété __CurrentThread.CurrentUICulture__ est prise en compte par 
la classe __Properties.Resources__ et par le __ResourceManager__ :

        ResourceManager rm = new ResourceManager(typeof(Resource1));

        private void bnTest_Click(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture =
                        new System.Globalization.CultureInfo("es");

            Debug.WriteLine($"Properties.Resources.String1={Resource1.String1}");
            Debug.WriteLine($"rm.String1={rm.GetString("String1")}");

            System.Threading.Thread.CurrentThread.CurrentUICulture =
                        new System.Globalization.CultureInfo("fr");

            Debug.WriteLine($"Properties.Resources.String1={Resource1.String1}");
            Debug.WriteLine($"rm.String1={rm.GetString("String1")}");
        }

Debug :

    Properties.Resources.String1=ES.Value1
    rm.String1=ES.Value1

    Properties.Resources.String1=FR.Value1
    rm.String1=FR.Value1



## URLs

- [Resources in .NET apps](https://docs.microsoft.com/en-us/dotnet/core/extensions/resources)

- [Retrieve resources in .NET apps](https://docs.microsoft.com/en-us/dotnet/core/extensions/retrieve-resources?source=recommendations)

- [ResourceSet Class](https://docs.microsoft.com/en-us/dotnet/api/system.resources.resourceset?view=net-6.0)

- [ResourceManager Class](https://docs.microsoft.com/en-us/dotnet/api/system.resources.resourcemanager?view=net-6.0)

- [WPF Globalization and Localization Overview](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/wpf-globalization-and-localization-overview?view=netframeworkdesktop-4.8)
