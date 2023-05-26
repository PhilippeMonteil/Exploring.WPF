
# Logical Resources

## [Overview of XAML resources (WPF .NET)](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/systems/xaml-resources-overview?view=netdesktop-6.0&redirectedfrom=MSDN&viewFallbackFrom=netdesktop-6.0)

## System.Windows.ResourceDictionary Class

    [System.Windows.Localizability(System.Windows.LocalizationCategory.Ignore)]
    [System.Windows.Markup.Ambient]
    [System.Windows.Markup.UsableDuringInitialization(true)]
    public class ResourceDictionary : System.Collections.IDictionary, 
                                        System.ComponentModel.ISupportInitialize, 
                                        System.Windows.Markup.INameScope, 
                                        System.Windows.Markup.IUriContext

    public Uri Source { get; set; }
    public System.Collections.ICollection Keys { get; }
    public System.Collections.ICollection Values { get; }
    public object this[object key] { get; set; }
    public Collection<ResourceDictionary> MergedDictionaries { get; }

remarque :

    - .Source
        - specified as a Pack URI, which references the location of a resource dictionary that is included as a noncompiled Resource or Content build action by your application building project.
        - souvent assigné aux ResourceDictionary apparaissant dans .MergedDictionaries

'
    <Page.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="myresourcedictionary.xaml"/>
                <ResourceDictionary Source="myresourcedictionary2.xaml"/>
            </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
    </Page.Resources>
'

## System.Windows.FrameworkElement : .Resources Property, .FindResource method

    public System.Windows.ResourceDictionary Resources { get; set; }
    public object FindResource (object resourceKey);

### Example

    <Window.Resources>
        <SolidColorBrush x:Key="PurpleBrushKey" Color="Purple"/>
    </Window.Resources>

    <Button Background="{StaticResource PurpleBrushKey}" />

## lookup : StaticResource

 La XAML Extension StaticResource parcourt de façon ascendante les ResourceDictionary
 à la recherche de la ressource de Key donnée. 
 Cette recherche se fait à travers l'arbre visuel, à partir de l'élément
 pour lequel la Key est cherchée, puis la .Resources de l'application 
 puis (cf Theming) ...

 ## Static vs Dynamic

### [StaticResource Markup Extension](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/staticresource-markup-extension?view=netframeworkdesktop-4.8)

Provides a value for any XAML property attribute by looking up a reference to an already defined resource. 
Lookup behavior for that resource is analogous to load-time lookup, 
which will look for resources that were previously loaded from the markup of the current XAML page 
as well as other application sources, and will generate that resource value as the property value 
in the run-time objects.

### [DynamicResource Markup Extension](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/dynamicresource-markup-extension?view=netframeworkdesktop-4.8)

Provides a value for any XAML property attribute by deferring that value to be a reference to a defined resource. 
Lookup behavior for that resource is analogous to run-time lookup.

## Resource Dictionaries

### Exemple

    <Application.Resources>

        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="file1.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>

    </Application.Resources>

Le fichier file1.xaml doit alors être de racine ResourceDictionary

### Resources non partagées : x:Shared="False"

## Manipulation de Ressources par programme : FrameworkElement.FindResource, .TryFindResource, .SetResourceReference

### [FrameworkElement.FindResource](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.findresource?view=windowsdesktop-7.0)

    public object FindResource (object resourceKey);

**Resource keys are not necessarily strings, cf ResourceKey class.** 

For instance, styles for controls at the theme level are deliberately keyed to the 
Type of the control, and application or page styles for controls typically use 
this same key convention. 

### [FrameworkElement.TryFindResource](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.tryfindresource?view=windowsdesktop-7.0)

    public object TryFindResource (object resourceKey);

### [FrameworkElement.SetResourceReference](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.setresourcereference?view=windowsdesktop-7.0)

    public void SetResourceReference (System.Windows.DependencyProperty dp, object name);

- A resource reference is similar to the use of a DynamicResource Markup Extension in markup. 
- The resource reference creates an internal expression that supplies the value of the specified property
  on a run-time deferred basis. 
- The expression will be re-evaluated whenever the resource dictionary indicates a changed value 
  through internal events, or whenever the current element is reparented 
  (a parent change would change the dictionary lookup path).

### exemples :

    window.Resources.Add("Key0", new SolidColorBrush("Red"));
    // static
    button.Background = (Brush)button.FindResource("Red");
    // static sans recherche
    button.Background = (Brush)windo.Resources("Red");
    // dynamic
    button.SetResourceReference(Button.BackgroundProperty, "Red");

## DynamicResource vs StaticResource

## Chargement et injection de ressources à l'exécution

### [Application.LoadComponent](https://learn.microsoft.com/fr-fr/dotnet/api/system.windows.application.loadcomponent?view=windowsdesktop-7.0)

    public static object LoadComponent (Uri resourceLocator);

Returns an instance of the root element specified by the XAML file loaded.

### Exemple

'''''

// chargement d'un ResourceDictionary à l'exécution
System.Uri resourceLocater = new System.Uri("/CustomControlLibrary2;component/Dictionary1.xaml",
                                            System.UriKind.Relative);
_sharedDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);

...

// injection de ce ResourceDictionary dans les Ressources d'un framework element
this.Resources.MergedDictionaries.Add(SharedDictionaryManager.SharedDictionary);
'''''

## Ressources dans une autre Assembly : ComponentResourceKey

En résumé :

- le paramètre resourceKey de FrameworkElement.FindResource
 
    public object FrameworkElement.FindResource (object resourceKey);

  peut ne pas être une string mais une instance d'une classe dérivant de ResourceKey

- ResourceKey est une classe abstraite
    - dérivant de MarkupExtension 
    - dont dérive ComponentResourceKey
    - exposant la propriété abstraite Assembly

        public abstract System.Reflection.Assembly Assembly { get; }

        Gets an assembly object that indicates which assembly's dictionary to look in for 
        the value associated with this key.

- ComponentResourceKey dérive de ResourceKey
 
    - constructeur :  

        public ComponentResourceKey (Type typeInTargetAssembly, object resourceId);

        resourceId :
            A unique identifier to differentiate this ComponentResourceKey from 
            others associated with the typeInTargetAssembly type.

    - surcharge :

        public abstract System.Reflection.Assembly Assembly { get; }

        retourne l'assembly du paramètre typeInTargetAssembly au constructeur ?

- les ressources fournies par une assembly doivent être inscrites dans un ResourceDictionary
  dans le .xaml Themes/Generic.xaml et indexées par des keys de type ComponentResourceKey

        <ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:local="clr-namespace:WPFResourceAssembly">

            <SolidColorBrush 
                x:Key="{ComponentResourceKey {x:Type local:DummyClass}, MyComponentLibBrush}" 
                Color="DarkRed"/>

- elles peuvent alors être référencées ainsi :

        <Window x:Class="TestXAMLResources.MainWindow"
            xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
            xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
            xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
            xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
            xmlns:local="clr-namespace:TestXAMLResources"
            xmlns:rlib="clr-namespace:WPFResourceAssembly;assembly=WPFResourceAssembly"

            <TextBlock x:Name="tbTest4" Text="Text4" 
                   FontSize="32" 
                   Foreground="{DynamicResource {ComponentResourceKey {x:Type rlib:DummyClass}, MyComponentLibBrush}}"/>

## [ResourceKey Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.resourcekey?view=windowsdesktop-8.0)

### Class

    [System.Windows.Markup.MarkupExtensionReturnType(typeof(System.Windows.ResourceKey))]
    public abstract class ResourceKey : System.Windows.Markup.MarkupExtension

Inheritance : Object / MarkupExtension / ResourceKey

Derived :
    System.Windows.ComponentResourceKey
    System.Windows.TemplateKey

Attributes : MarkupExtensionReturnTypeAttribute

Resource keys are either strings or instances of a type.

### ResourceKey.Assembly Property

    public abstract System.Reflection.Assembly Assembly { get; }

Gets an assembly object that indicates which assembly's dictionary to look in for the 
value associated with this key.

### ResourceKey.ProvideValue(IServiceProvider) Method

    public override object ProvideValue (IServiceProvider serviceProvider);

Returns this ResourceKey. 
Instances of this class are typically used as a key in a dictionary.

## [ComponentResourceKey Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.componentresourcekey?view=windowsdesktop-8.0)

    [System.ComponentModel.TypeConverter(typeof(System.Windows.Markup.ComponentResourceKeyConverter))]
    public class ComponentResourceKey : System.Windows.ResourceKey
    {
        public ComponentResourceKey (Type typeInTargetAssembly, object resourceId);

    }

Le Type passé en paramètre du constructeur est retourné par le get overridant

    public abstract System.Reflection.Assembly Assembly { get; }

## Ressources dans une autre Assembly : ComponentResourceKey

### [enum System.Windows.ResourceDictionaryLocation](https://learn.microsoft.com/en-us/dotnet/api/system.windows.resourcedictionarylocation?view=windowsdesktop-7.0)

    None	            0	No theme dictionaries exist.
    SourceAssembly	    1	Theme dictionaries exist in the assembly that defines the types being themed.
    ExternalAssembly	2	Theme dictionaries exist in assemblies external to the one defining the types
                            being themed. They are named based on the original assembly with the 
                            theme name appended to it; for example, PresentationFramework.Luna.dll. 
                            These dictionaries share the same version and key as the original assembly.

### [attribut ThemeInfo](https://learn.microsoft.com/en-us/dotnet/api/system.windows.themeinfoattribute?view=windowsdesktop-7.0)

    public ThemeInfoAttribute (ResourceDictionaryLocation themeDictionaryLocation, 
                               ResourceDictionaryLocation genericDictionaryLocation);

    themeDictionaryLocation     : The location of theme-specific resources.
    genericDictionaryLocation   : The location of generic, not theme-specific, resources.

When the themeDictionaryLocation is SourceAssembly, you can include files such as Luna.NormalColor.xaml.
These names are defined by the system theme files, which include the following.

    Classic - "Classic" Windows 9x/2000 look on Windows XP.
    Luna.NormalColor - Default blue theme on Windows XP.
    ...

### AssemblyInfo.cs généré pour une application WPF : attribut ThemeInfo

    using System.Windows;

    [assembly: ThemeInfo(
        // where theme specific resource dictionaries are located
        // (used if a resource is not found in the page,
        // or application resource dictionaries)
        themeDictionaryLocation : ResourceDictionaryLocation.None,    

        // where the generic resource dictionary is located
        // (used if a resource is not found in the page,
        // app, or any theme specific resource dictionaries
        genericDictionaryLocation : ResourceDictionaryLocation.SourceAssembly)]

### Première méthode

#### dans l'assembly de ressources, ex : WPFResourceAssembly

- créer un Folder Themes et un ResourceDictionary generic.xaml
- créer une classe 'DummyClass', y inscrire l'attribut

    [assembly: ThemeInfo(
        ResourceDictionaryLocation.None, 
        // where theme specific resource dictionaries are located
        // (used if a resource is not found in the page, 
        // or application resource dictionaries)
        ResourceDictionaryLocation.SourceAssembly 
        // where the generic resource dictionary is located
        // (used if a resource is not found in the page, 
        // app, or any theme specific resource dictionaries)
    )]

- dans le ResourceDictionary : Themes/Generic.xaml :

````
    <SolidColorBrush 
        x:Key="{ComponentResourceKey {x:Type local:DummyClass}, MyComponentLibBrush}" 
        Color="DarkRed"/>
````

#### dans l'application cliente

- référencer l'assembly
- dans le XAML :

````
    xmlns:rlib="clr-namespace:WPFResourceAssembly;assembly=WPFResourceAssembly"

    <TextBlock x:Name="tbTest4" Text="Text4" 
        FontSize="32" 
        Foreground="{DynamicResource {ComponentResourceKey {x:Type rlib:DummyClass}, MyComponentLibBrush}}"/>
````

### Autre méthode : exposition d'une ComponentResourceKey

#### dans l'assembly de ressources

- idem méthode précédente
- plus :

    public class DummyClass
    {
        static ComponentResourceKey key0 = new ComponentResourceKey(typeof(DummyClass), "Key0");
        public static ComponentResourceKey Key0 => key0;
    }

````
    <SolidColorBrush 
        x:Key="{x:Static local:DummyClass.Key0}" Color="DarkGreen"/>
````

#### dans l'application cliente

- idem méthode précédente
- plus :

````
    <TextBlock x:Name="tbTest6" Text="Text7" 
        FontSize="32" 
        Foreground="{DynamicResource {x:Static rlib:DummyClass.Key0} }"/>
````

## System.Window.SystemColors, SystemFonts, SystemParameters Classes

Ces classes exposent des keys telles :

    public static System.Windows.ResourceKey BorderKey { get; }

qui peuvent être utilisées ainsi :

<SolidColorBrush Color="{DynamicResource {x:Static SystemColors.InactiveCaptionColorKey}}"/> 

## Skinning

### [WPF complete guide to Themes and Skins](https://michaelscodingspot.com/wpf-complete-guide-themes-skins/)

### Compiled and Static 

#### Notes

- un ResourceDictionary expose à la fois:
    -  MergedDictionaries, collection de sous ResourceDictionary
    -  un ensemble Keys, Values, Item[]
- si un ResourceDictionary inscrit dans MergedDictionaries est modifié,
  assignation de sa .Source par exemple, son ResourceDictionary l'est aussi

#### classe SkinResourceDictionary

SkinResourceDictionary expose deux Uris : RedSource et BlueSource et les redirige vers
base.Source (UpdateSource)

````
    public class SkinResourceDictionary : ResourceDictionary
    {
        private Uri _redSource;
        private Uri _blueSource;

        public Uri RedSource
        {
            get { return _redSource; }
            set
            {
                _redSource = value;
                UpdateSource();
            }
        }
        public Uri BlueSource
        {
            get { return _blueSource; }
            set
            {
                _blueSource = value;
                UpdateSource();
            }
        }

        private void UpdateSource()
        {
            var val = App.Skin == Skin.Red ? RedSource : BlueSource;
            if (val != null && base.Source != val)
            {
                base.Source = val;
            }
        }

    }
````

## [ThemeDictionary Markup Extension](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/themedictionary-markup-extension?view=netframeworkdesktop-4.8)

    <object property="{ThemeDictionary assemblyUri}" ... />  

    <object>  
        <object.property>  
            <ThemeDictionary AssemblyName="assemblyUri"/>  
        <object.property>  
    <object>

### [ThemeDictionaryExtension Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.themedictionaryextension?view=windowsdesktop-8.0)

    [System.Windows.Markup.MarkupExtensionReturnType(typeof(System.Uri))]
    public class ThemeDictionaryExtension : System.Windows.Markup.MarkupExtension
    {

        public ThemeDictionaryExtension (string assemblyName);

    }

