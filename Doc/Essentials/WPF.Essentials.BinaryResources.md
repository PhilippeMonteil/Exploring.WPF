
# [Binary Resources](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/app-development/wpf-application-resource-content-and-data-files?view=netframeworkdesktop-4.8)

## Application data file types

- Resource Files: Data files that are compiled into either an executable or library WPF assembly.

- Content Files: Standalone data files that have an explicit association with an executable WPF assembly.

- Site of Origin Files: Standalone data files that have no association with an executable WPF assembly.

### Example

    <!--Build Action = Resource-->
    <Image Width="200" Height="100" Margin="4" Stretch="UniformToFill" Source="Resources/Folder0/Image0.jpg"></Image>
    <!--Build Action = Content + Copy to Output = true-->
    <Image Width="200" Height="100" Margin="4" Stretch="UniformToFill" Source="Resources/Folder1/Image0.jpg"></Image>
    <Image Width="200" Height="100" Margin="4" Stretch="UniformToFill" Source="c:/TEMP/Image0.jpg"></Image>
    <Image Width="200" Height="100" Margin="4" Stretch="UniformToFill" Source="pack://application:,,,/ResourceAssembly;Component/Folder0/Image0.jpg"></Image>

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

#### Exemple : chargement d'un bloc d'UI d�fini dans une ressource binaire .xaml


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

### Example : Assembly.GetManifestResourceNames, GetManifestResourceStream

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

- un ManifestResourceStream **TestResources.g.resources** a �t� g�n�r� 
- il contient un **ResourceSet**
- chaque **Resource** de ce ResourceSet pr�cise:
    - une **.Key** : nom de la Resource 
    - une **.Value** : UnmanagedMemoryStream contenant la ressource
