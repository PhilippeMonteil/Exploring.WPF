
# Resources

## [Resources in .NET apps](https://docs.microsoft.com/en-us/dotnet/core/extensions/resources)

## [Retrieve resources in .NET apps](https://docs.microsoft.com/en-us/dotnet/core/extensions/retrieve-resources?source=recommendations)

## [ResourceManager Class](https://docs.microsoft.com/en-us/dotnet/api/system.resources.resourcemanager?view=net-6.0)

## [ResourceReader Class](https://docs.microsoft.com/en-us/dotnet/api/system.resources.resourcereader?view=net-6.0)

## GetManifestResourceNames

### Example

    Assembly assembly = Assembly.GetExecutingAssembly();

    // assembly = Application.ResourceAssembly;

    {
        foreach (var resourceName in assembly.GetManifestResourceNames())
        {
            Debug.WriteLine($"resourceName={resourceName}");

            ResourceSet set = new ResourceSet(Application.ResourceAssembly.GetManifestResourceStream(resourceName));

            foreach (DictionaryEntry resource in set)
            {
                Debug.WriteLine("  resource=[{0}] \t{1}", resource.Key, resource.Value);
            }

            Debug.WriteLine("--------------");
        }
    }

#### Debug

    // .g.resources : Built as 'Content', 'Resource'

    resourceName=TestResources0.g.resources
        resource=[resources/folder0/image0.jpg] 	System.IO.UnmanagedMemoryStream
        resource=[mainwindow.baml] 	System.IO.UnmanagedMemoryStream

    resourceName=TestResources0.Properties.Resources.resources

    // Resource1.resx
    resourceName=TestResources0.Resource1.resources
        resource=[String1] 	Value1
        resource=[String2] 	Value2








## [Binary Resources](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/app-development/wpf-application-resource-content-and-data-files?view=netframeworkdesktop-4.8)

### Application data file types

- Resource Files: Data files that are compiled into either an executable or library WPF assembly.

- Content Files: Standalone data files that have an explicit association with an executable WPF assembly.

- Site of Origin Files: Standalone data files that have no association with an executable WPF assembly.

### Resource Files

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

    //
    Uri pageUri = new Uri("/PageResourceFile.xaml", UriKind.Relative);
    this.pageFrame.Source = pageUri;

    //
    <Frame Name="pageFrame" Source="PageResourceFile.xaml" />

### Example :

    <!--Build Action = Resource-->
    <Image Width="200" Height="100" Margin="4" Stretch="UniformToFill" Source="Resources/Folder0/Image0.jpg"></Image>
    <!--Build Action = Content + Copy to Output = true-->
    <Image Width="200" Height="100" Margin="4" Stretch="UniformToFill" Source="Resources/Folder1/Image0.jpg"></Image>
    <Image Width="200" Height="100" Margin="4" Stretch="UniformToFill" Source="c:/TEMP/Image0.jpg"></Image>
    <Image Width="200" Height="100" Margin="4" Stretch="UniformToFill" Source="pack://application:,,,/ResourceAssembly;Component/Folder0/Image0.jpg"></Image>

    imTest.Source = new BitmapImage(new Uri("pack://application:,,,/ResourceAssembly;Component/Folder0/Image0.jpg"));

## [WPF Resources](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/app-development/wpf-application-resource-content-and-data-files?view=netframeworkdesktop-4.8)

## [Pack URIs in WPF](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/app-development/pack-uris-in-wpf?view=netframeworkdesktop-4.8)

