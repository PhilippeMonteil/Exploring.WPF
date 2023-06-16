
# [Binary Resources](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/app-development/wpf-application-resource-content-and-data-files?view=netframeworkdesktop-4.8)

## En résumé

- Build Actions
    - Resource : embed the file in an assembly manifest resource file with the extension .g.resources.,
                 désérialisable dans un ResourceSet
    - Content :  lié à l'assembly, non embarqué, accessible par Application.GetContentStream
    - Embedded Resource : embarqué, accessible par Assembly.GetManifestResourceStream

- ressource Content : AssemblyAssociatedContentFileAttribute

- Image.Source : accès aux ressources image 

- Application.GetContentStream

    public static System.Windows.Resources.StreamResourceInfo GetContentStream (Uri uriContent);

- Application.GetResourceStream

    public static System.Windows.Resources.StreamResourceInfo GetResourceStream (Uri uriResource);

- Assembly.GetManifestResourceNames, GetManifestResourceStream
    public virtual string[] GetManifestResourceNames ();
    public virtual System.IO.Stream? GetManifestResourceStream (Type type, string name);
    public virtual System.IO.Stream? GetManifestResourceStream (string name);

- ManifestResource '.g.resources'

    - chaque assembly contient une ManifestResource nommée '<Name>.g.resources' 
      dont le ManifestResourceStream contient une ResourceSet contenant
      - les .balm des .xaml de l'assembly : App.baml, mainwindow.baml
      - les fichiers enregistrées comme 'Ressources'
    
    - exemple :

````
    resourceName=TestResources.g.resources
    stream=System.Reflection.RuntimeAssembly+ManifestResourceStream

    set=System.Resources.ResourceSet
      resource.Key=[binaryresources/folder0/resourceimage.png] .Value='System.IO.UnmanagedMemoryStream'
      resource.Key=[mainwindow.baml] .Value='System.IO.UnmanagedMemoryStream'
      resource.Key=[app.baml] .Value='System.IO.UnmanagedMemoryStream'
````

- ResourceSet

- System.AppDomain.ResourceResolve event

## [WPF Application Resource, Content, and Data Files](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/app-development/wpf-application-resource-content-and-data-files?view=netframeworkdesktop-4.8#content-files)

## [Build Actions](https://learn.microsoft.com/en-us/visualstudio/ide/build-actions?view=vs-2022)

- Build Actions

    - WPF
        - ApplicationDefinition	: The file that defines your application. When you first create a project, this is App.xaml.
        - Page                  : Compile a XAML file to a binary .baml file for faster loading at run time.
        - Resource	            : Specifies to embed the file in an assembly manifest resource file with the extension .g.resources.
        - DesignData	        : Used for XAML ViewModel files, to enable user controls to be viewed at design time, with dummy types and sample data.
        - Splash Screen         : Specifies an image file to be displayed at run time when the app is starting up.

    - .Net
        - Content               : A file marked as Content can be retrieved as a stream by calling Application.GetContentStream.
        - Embedded Resource 	: The file is passed to the compiler as a resource to be embedded in the assembly. 
                                  You can call System.Reflection.Assembly.GetManifestResourceStream 
                                  to read the file from the assembly.

- Content Files: Standalone data files that have an explicit association with an executable WPF assembly.
- Site of Origin Files: Standalone data files that have no association with an executable WPF assembly.

- Exemples :

````
  <ItemGroup>
    <Content Include="Folder0\Content0.jpg">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
    <Content Include="Folder0\Content1.jpg" />
  </ItemGroup>

  <ItemGroup>
    <Resource Include="Folder0\Resource0.jpg" />
  </ItemGroup>
````

## [Content resource : AssemblyAssociatedContentFileAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.windows.resources.assemblyassociatedcontentfileattribute?view=windowsdesktop-7.0)

- attribut d'assembly marquant une référence à une ressource 'Content'
  
- When the project is built, an AssemblyAssociatedContentFileAttribute attribute is compiled into the 
  metadata of the assembly for each content file.

    [assembly: AssemblyAssociatedContentFile("ContentFile.xaml")]

- The value of the AssemblyAssociatedContentFileAttribute implies the path to the content file relative 
  to its position in the project. For example, if a content file was located in a project subfolder, the additional path information would be incorporated into the AssemblyAssociatedContentFileAttribute value.

    [assembly: AssemblyAssociatedContentFile("Resources/ContentFile.xaml")]

- The AssemblyAssociatedContentFileAttribute value is also the value of the path to the content file in 
  the build output folder.

## [MSBuild reference for .NET Desktop SDK projects](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/msbuild-props-desktop?source=recommendations)

### Exemples de mise en oeuvre de Binary Resources (TestXAMLBinaryResources)

    <StackPanel Background="DarkGray">

        <Image Source="BinaryResources/Folder0/ResourceImage.PNG" Height="100" Margin="8"/>
        <Image Source="BinaryResources/Folder0/ContentImage.PNG" Height="100" Margin="8"/>
        <Image Source="pack://siteOfOrigin:,,,/Folder0/Content0.jpg" Height="100" Margin="8"/>
        <Image Source="/ResourceAssembly;component/Folder0/Resource0.jpg" Height="100" Margin="8"/>
        
        <!-- KO : Content / assembly référencé -->
        <Image Source="/ResourceAssembly;component/Folder0/Content0.jpg" Height="100" Margin="8"/>
        <Image Source="/ResourceAssembly;component/Folder0/Content1.jpg" Height="100" Margin="8"/>
        
    </StackPanel>

- 'Resource' dans la même assembly
        <Image Source="BinaryResources/Folder0/ResourceImage.PNG" Height="100" Margin="8"/>

- 'Content' dans la même assembly (attribut)
        <Image Source="BinaryResources/Folder0/ContentImage.PNG" Height="100" Margin="8"/>

- fichier externe, apporté en tant que 'Content','always copy', par une assembly externe
        <Image Source="pack://siteOfOrigin:,,,/Folder0/Content0.jpg" Height="100" Margin="8"/>

- 'Resource' dans une assembly référencée
        <Image Source="/ResourceAssembly;component/Folder0/Resource0.jpg" Height="100" Margin="8"/>

### [Application.GetContentStream](https://learn.microsoft.com/en-us/dotnet/api/system.windows.application.getcontentstream?view=windowsdesktop-7.0)

    public static System.Windows.Resources.StreamResourceInfo GetContentStream (Uri uriContent);

### [Application.GetResourceStream](https://learn.microsoft.com/en-us/dotnet/api/system.windows.application.getresourcestream?view=windowsdesktop-7.0)

    public static System.Windows.Resources.StreamResourceInfo GetResourceStream (Uri uriResource);

````
    public class StreamResourceInfo
    {
        // The Multipurpose Internet Mail Extensions (MIME) content type.
        public string ContentType { get; }
        public System.IO.Stream Stream { get; }
    }
````

#### Exemples

##### chargement d'un bloc d'UI défini dans une ressource binaire .xaml 'Resource'

    //
    <Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003" ... >  
    ...  
        <ItemGroup>  
            <Resource Include="ResourceFile.xaml" />  
        </ItemGroup>  
    ...  
    </Project>  

    // Navigate to xaml page
    Uri uri = new Uri("/PageResourceFile.xaml", UriKind.Relative);
    StreamResourceInfo info = Application.GetResourceStream(uri);
    System.Windows.Markup.XamlReader reader = new System.Windows.Markup.XamlReader();
    Page page = (Page)reader.LoadAsync(info.Stream);
    this.pageFrame.Content = page;

    // Navigate to xaml page
    Uri pageUri = new Uri("/PageResourceFile.xaml", UriKind.Relative);
    this.pageFrame.Source = pageUri;

    //
    <Frame Name="pageFrame" Source="PageResourceFile.xaml" />

##### chargement de ressources 'Resource'

    public static System.Windows.Resources.StreamResourceInfo GetResourceStream (Uri uriResource);

        static void Test(string Path, UriKind uriKind)
        {
            try
            {
                log($"{nameof(Test)}(-) '{Path}' uriKind={uriKind}");
                Uri uri = new Uri(Path, uriKind);
                StreamResourceInfo info = Application.GetResourceStream(uri);
                log($"info .ContentType='{info?.ContentType}' .Stream{info?.Stream}");
            }
            catch (Exception E)
            {
                log($"{nameof(Test)} EXCEPTION={E.Message}");
            }
            finally
            {
                log($"{nameof(Test)}(+)");
            }
        }
    
    // OK
    Test("/BinaryResources/Folder0/ResourceImage.PNG", UriKind.Relative);
    Test("pack://application:,,,/BinaryResources/Folder0/ResourceImage.PNG", UriKind.Absolute);

    // Not OK
    Test("/BinaryResources/Folder0/ContentImage.PNG", UriKind.Relative);
    Test("pack://siteOfOrigin:,,,/Folder0/Content0.jpg", UriKind.Absolute);
    Test("pack://application:,,,/Folder0/Content0.jpg", UriKind.Absolute);

### Assembly.GetManifestResourceNames, GetManifestResourceStream

    public virtual string[] GetManifestResourceNames ();
    public virtual System.IO.Stream? GetManifestResourceStream (string name);
    public virtual System.IO.Stream? GetManifestResourceStream (Type type, string name);

        For example, if the full name specified for type is "MyNameSpace.MyClasses" and name is "Net", 
        this method overload searches for a resource named "MyNameSpace.Net".

#### exemple

        public static void DebugResources(Assembly assembly)
        {
            Debug.WriteLine($"{nameof(ResourceUtils)}{nameof(DebugResources)}(-)");

            foreach (var resourceName in assembly.GetManifestResourceNames())
            {
                Debug.WriteLine($"  resourceName={resourceName}");

                Stream? stream = assembly.GetManifestResourceStream(resourceName);
                ResourceSet set = new ResourceSet(stream);

                Debug.WriteLine($"    stream={stream}");
                Debug.WriteLine($"    set={set}");

                foreach (DictionaryEntry resource in set)
                {
                    Debug.WriteLine("      resource.Key=[{0}] .Value='{1}'", resource.Key, resource.Value);
                }

                Debug.WriteLine("  --------------");
            }

            Debug.WriteLine($"{nameof(ResourceUtils)}{nameof(DebugResources)}(+)");
        }

 output :

    ResourceUtilsDebugResources(-)

    resourceName=TestResources.g.resources
    stream=System.Reflection.RuntimeAssembly+ManifestResourceStream

    set=System.Resources.ResourceSet
      resource.Key=[binaryresources/folder0/resourceimage.png] .Value='System.IO.UnmanagedMemoryStream'
      resource.Key=[mainwindow.baml] .Value='System.IO.UnmanagedMemoryStream'
      resource.Key=[app.baml] .Value='System.IO.UnmanagedMemoryStream'
    --------------

    resourceName=TestResources.Resources.Resource1.resources
      stream=System.Reflection.RuntimeAssembly+ManifestResourceStream
      set=System.Resources.ResourceSet
        resource.Key=[String1] .Value='Value1'
        resource.Key=[String2] .Value='Value2'
    --------------

    ResourceUtilsDebugResources(+)

- un ManifestResourceStream **TestResources.g.resources** a été généré 
- il contient un **ResourceSet**
- chaque **Resource** de ce ResourceSet précise:
    - une **.Key** : nom de la Resource 
    - une **.Value** : UnmanagedMemoryStream contenant la ressource

### [System.AppDomain.ResourceResolve event](https://learn.microsoft.com/en-us/dotnet/api/system.appdomain.resourceresolve?view=net-7.0)

    public event ResolveEventHandler? ResourceResolve;

    public delegate System.Reflection.Assembly? ResolveEventHandler(object? sender, ResolveEventArgs args);

        - returns the assembly that resolves the type, assembly, or resource; or null if the assembly cannot be resolved.

    ResolveEventArgs 

        - properties
            - public string Name { get; }
            - public System.Reflection.Assembly? RequestingAssembly { get; }

- Occurs when the resolution of a resource fails because the resource is not a valid linked or 
  embedded resource in the assembly.

- The ResolveEventHandler for this event can attempt to locate the assembly containing the resource 
  and return it.
