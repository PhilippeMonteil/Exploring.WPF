
# WPF Tricks

## Resources at runtime

    // Static
    tbTest4.Foreground = (Brush)(this.FindResource("PurpleBrushKey"));

    // Dynamic
    tbTest4.SetResourceReference(Button.ForegroundProperty, SharedResources.RedBrushKey);
    tbTest4.SetResourceReference(Button.ForegroundProperty, "PurpleBrushKey");

avec :

    public static class SharedResources
    {
        static ComponentResourceKey s_RedBrushKey = new ComponentResourceKey(typeof(SharedResources), "RedBrushKey");
        public static ComponentResourceKey RedBrushKey => s_RedBrushKey;
    }

    <ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:local="clr-namespace:ResourceAssembly">
    {
        <SolidColorBrush 
            x:Key="{ComponentResourceKey {x:Type local:SharedResources}, RedBrushKey}" 
            Color="Red"/>

## [Exportation d'un ResourceDictionary](https://alexfeinberg.wordpress.com/2015/08/16/safely-export-wpf-resources/)

### .xaml -> .cs : 'x:Class'

    <ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    x:Class="ResourceAssembly.ExportResourceDictionary1">

        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="Dictionary1.xaml"/>
        </ResourceDictionary.MergedDictionaries>
    
    </ResourceDictionary>

- génération d'une classe dans un .g.cs : ExportResourceDictionary1 
- ajout de la classe partielle ExportResourceDictionary1

    public partial class ExportResourceDictionary1
    {
        //Expose it as singleton to avoid multiple instances of this dictionary
        private static readonly ExportResourceDictionary1 _instance = new ExportResourceDictionary1();

        public static ExportResourceDictionary1 Instance => _instance;

        public ExportResourceDictionary1()
        {
            InitializeComponent();
        }
    }

- mapping namespace XML / namespace .Net: XmlnsDefinitionAttribute
   
    [assembly: XmlnsDefinitionAttribute("http://my.schemas.com/ResourceAssembly", "ResourceAssembly")]

### importation dans un ResourceDictionary

```
<Application x:Class="TestXAMLResources.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:TestXAMLResources"
             xmlns:rax="http://my.schemas.com/ResourceAssembly"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="Dictionary1.xaml" />
                <ResourceDictionary Source="Dictionary2.xaml" />
                <x:Static Member="rax:ExportResourceDictionary1.Instance" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

La ressource de key DarkRedKey contenue dans le ResourceDictionary ResourceAssembly.Dictionary1.xaml
est ainsi utilisable :

    <TextBlock Text="Text5" 
        FontSize="32" 
        Foreground="{DynamicResource DarkRedKey}"/>

### x:Static

    <x:Static Member="rax:ExportResourceDictionary1.Instance" />

## Themes : 

- themes/\<ThemeName>.\<ThemeColor>.xaml
- fallback : themes/generic.xaml
