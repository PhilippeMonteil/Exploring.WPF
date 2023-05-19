
# [Binary Resources](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/app-development/wpf-application-resource-content-and-data-files?view=netframeworkdesktop-4.8)

## Application data file types

- Resource Files: Data files that are compiled into either an executable or library WPF assembly.

- Content Files: Standalone data files that have an explicit association with an executable WPF assembly.

- Site of Origin Files: Standalone data files that have no association with an executable WPF assembly.

### exemple : TestXAMLBinaryResources

#### .csproj :

    <ItemGroup>
        <ProjectReference Include="..\ResourceAssembly\ResourceAssembly.csproj" />
    </ItemGroup>

    <ItemGroup>
        <Content Include="BinaryResources\Folder0\ContentImage.PNG">
            <CopyToOutputDirectory>Always</CopyToOutputDirectory>
        </Content>
    </ItemGroup>

    <ItemGroup>
        <Resource Include="BinaryResources\Folder0\ResourceImage.png">
            <CopyToOutputDirectory>Never</CopyToOutputDirectory>
        </Resource>
    </ItemGroup>

#### .xaml :

    <StackPanel Background="DarkGray">
        <Image Source="BinaryResources/Folder0/ResourceImage.PNG" Height="100" Margin="8"/>
        <Image Source="BinaryResources/Folder0/ContentImage.PNG" Height="100" Margin="8"/>
        <Image Source="pack://siteOfOrigin:,,,/Folder0/Image0.jpg" Height="100" Margin="8"/>
        <Image Source="/ResourceAssembly;component/Folder0/Image1.jpg" Height="100" Margin="8"/>
    </StackPanel>

### accès à une ressource à l'exécution

    imTest.Source = new BitmapImage(new Uri("pack://application:,,,/ResourceAssembly;Component/Folder0/Image0.jpg"));

## .xaml Resource File

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

## [WPF Resources](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/app-development/wpf-application-resource-content-and-data-files?view=netframeworkdesktop-4.8)

## [Pack URIs in WPF](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/app-development/pack-uris-in-wpf?view=netframeworkdesktop-4.8)

