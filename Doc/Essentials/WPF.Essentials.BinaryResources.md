
# [Binary Resources](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/app-development/wpf-application-resource-content-and-data-files?view=netframeworkdesktop-4.8)

## Application data file types

- Resource Files: Data files that are compiled into either an executable or library WPF assembly.

- Content Files: Standalone data files that have an explicit association with an executable WPF assembly.

- Site of Origin Files: Standalone data files that have no association with an executable WPF assembly.

### Example (TestXAMLBinaryResources)

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

### [Application.GetResourceStream](https://learn.microsoft.com/en-us/dotnet/api/system.windows.application.getresourcestream?view=windowsdesktop-7.0)

'
    public static System.Windows.Resources.StreamResourceInfo GetResourceStream (Uri uriResource);

    public class StreamResourceInfo
    {
        // The Multipurpose Internet Mail Extensions (MIME) content type.
        public string ContentType { get; }
        public System.IO.Stream Stream { get; }
    }
'

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
