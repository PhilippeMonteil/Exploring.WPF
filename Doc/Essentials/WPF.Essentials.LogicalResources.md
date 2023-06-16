
# Logical Resources : définition, résolution ...

## En résumé

- System.Windows.ResourceDictionary : 
    - dérive de IDictionary : Keys, Values
    - propriété Uri Source : 
        - vers un .xaml Resource ou Content
        - décrivant une instance ResourceDictionary
    - propriété Collection<ResourceDictionary> MergedDictionaries

- System.Windows.FrameworkElement : 
 
    - propriété : ResourceDictionary Resources 

    - méthodes : FindResource TryFindResource
        public object FindResource (object resourceKey);
        public object TryFindResource (object resourceKey);

    - méthodes : FindResource SetResourceReference
        public void SetResourceReference (System.Windows.DependencyProperty dp, object resourceKey);

- ResourceKey
    - public abstract class ResourceKey : System.Windows.Markup.MarkupExtension
    - attribut [System.Windows.Markup.MarkupExtensionReturnType(typeof(System.Windows.ResourceKey))]
    - Derived : ComponentResourceKey, TemplateKey, SystemResourceKey (type caché, keys exposées par SystemParameters, ...)

- Themes, default resources (Themes/generic.xaml)

- ThemeInfo assembly attribute : 
    - paramètres constructeur : ResourceDictionaryLocation themeDictionaryLocation, genericDictionaryLocation, 
    - ResourceDictionaryLocation : 
        - SourceAssembly :
            - Theme dictionaries exist in the assembly that defines the types being themed.
        - ExternalAssembly :
            - They are named based on the original assembly with the theme name appended to it; 
              for example, PresentationFramework.Luna.dll. 
              These dictionaries share the same version and key as the original assembly.

- ThemeDictionary MarkupExtension
    - <ResourceDictionary Source="{ThemeDictionary assemblyUri}" ... />  
    - By using this extension, the contents of a control-specific resource dictionary can be automatically 
      invalidated and reloaded to be specific for another theme when required.

- création d'un CustomControl, aspects liés aux Ressources : 
  DefaultStyleKeyProperty, assembly attribute ThemeInfo, ressource 'Page' Themes/generic.xaml

- Skinning

- chargement et injection de ressources à l'exécution : 
  simuler une mécanisme de Dll.Resources similaire à Application.Resources
    - Application.LoadComponent : Uri -> .xaml -> objet (XamlReader.Load)
    - .Resources.MergedDictionaries.Add

- SystemResourceKey

    [TypeConverter(typeof(System.Windows.Markup.SystemKeyConverter))]
    internal class SystemResourceKey : ResourceKey
 
- SystemParameters, SystemColors, SystemFonts

    - paires ResourceKey / static field

    ex: 
        public static System.Windows.ResourceKey BorderKey { get; }
        public static int Border { get; }

    ex:
        Width="{x:Static SystemParameters.IconGridWidth}"
        Width="{DynamicResource {x:Static SystemParameters.IconGridWidthKey}}"

- SystemParameters.StaticPropertyChanged Event

    public static event System.ComponentModel.PropertyChangedEventHandler StaticPropertyChanged;

## FrameWorkElement.FindResource en résumé

### notions mises en oeuvre : 

- appelée par Static / DynamicResource ?
- resource key : String, ResourceKey, ComponentResourceKey, SystemResourceKey
- ThemeInfo assembly attribute : themeDictionaryLocation, genericDictionaryLocation, 
  ResourceDictionaryLocation.None, .SourceAssembly, .ExternalAssembly
- Themes/generic.xaml
- ThemeDictionaryExtension
- sources de SystemResourceKeys traitées par FrameworkElement.FindResource : 
    SystemParameters, SystemColors, SystemFonts
- certains Controles exposent les Keys référencées par leur Template en tant que public static
  ce qui les rend assignables à des ressources ancrées 'plus' haut dans l'arbre de recherche de ressources

### pseudo algo

- la résolution d'une MarkupExtension Static/DynamicResource (ProvideValue) se fait par un appel 
  à FrameWorkElement.FindResource sur le DependencyObject cible de la MarkupExtension 
  (IServiceProvide.TargetValue.TargetObject) en passant la Key avec laquelle la MarkupExtension 
  a été construite.

- cette Key peut être une String, une ResourceKey (ComponentResourceKey, SystemResourceKey) ...

- la résolution dépend de la nature la Key concernée
 
    - String ou Type :

        - parcours ascendant l'arbre visuel, recherche dans les .Resources (ResourceDictionary) de chaque 
          FrameworkElement d'une ressource pour la Key

        - inspection de Application.Resources

        - dans l'assembly implémentant le FrameWorkElement object de l'appel à .FindResource :
            - si son ThemeInfo le permet : recheche dans le ResourceDictionary correspondant au Thème actif
            - si son ThemeInfo le permet : recheche dans le ResourceDictionary par défaut : Themes/generic.xaml

        - si la .Source d'un des ResourceDictionary inspecté est fournie par une MarkupExtension de type ThemeDictionary
          celle ci précise le nom d'une Assembly fournissant un jeu de ResourceDictionary correspondant 
          chacun à un Thème. La recherche de la Key se fait dans le ResourceDictionary fourni par l'assembly
          pour le Thème courant.

        - l'inspection se termine dès qu'une ressource a été trouvée pour la Key recherchée.

    - ComponentResourceKey : recherche dans l'assembly spécifié par le ComponentResourceKey et pour l'id qu'il indique

    - SystemResourceKey : 
        - type caché, dérivant de ResourceKey
        - propriété de SystemParameters / SystemColors / SystemFonts correspondant 
          correspond à une propriété statique de la classe exposant la Key dont la valeur 
          est retournée par FindResource.

### exemples

    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="myresourcedictionary.xaml"/>
            <ResourceDictionary Source="myresourcedictionary2.xaml"/>
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>

    <ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

        <Style x:Key="Dictionary1.Style0" TargetType="TextBlock">
            <Setter Property="Foreground" Value="Red" />
            <Setter Property="FontSize" Value="16" />
            <Setter Property="Margin" Value="8" />
        </Style>

        <SolidColorBrush 
            x:Key="{ComponentResourceKey {x:Type local:DummyClass}, MyComponentLibBrush}" 
            Color="DarkRed"/>

            ...

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

            <SolidColorBrush Color="{DynamicResource {x:Static SystemColors.InactiveCaptionColorKey}}"/> 

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
        - specified as a Pack URI, which references the location of a resource dictionary that is included 
          as a noncompiled Resource or Content build action by your application building project.
        - souvent assigné aux ResourceDictionary apparaissant dans .MergedDictionaries

        - exemple:

````
    <Page.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="myresourcedictionary.xaml"/>
                <ResourceDictionary Source="myresourcedictionary2.xaml"/>
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Page.Resources>
````

## [System.Windows.FrameworkElement](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement?view=windowsdesktop-7.0) : .Resources Property, .FindResource method

    public System.Windows.ResourceDictionary Resources { get; set; }
    public object FindResource (object resourceKey);
    public object TryFindResource (object resourceKey);
    public void SetResourceReference (System.Windows.DependencyProperty dp, object name);
    public bool ShouldSerializeResources (); //

### Example

    <Window.Resources>
        <SolidColorBrush x:Key="PurpleBrushKey" Color="Purple"/>
    </Window.Resources>

    <Button Background="{StaticResource PurpleBrushKey}" />

## FindResource, TryFindResource

cf résumé

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

## Ressources dans une autre Assembly : ComponentResourceKey, Themes/generic.xaml, ThemeInfo assembly attribute

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

- cet assembly doit être annoté d'un attribut ThemeInfo précisant qu'un generic.xaml s'y trouve ...

    [assembly: ThemeInfo(
                            ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                                             //(used if a resource is not found in the page, 
                                                             // or application resource dictionaries)
                            ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                                              //(used if a resource is not found in the page, 
                                                              // app, or any theme specific resource dictionaries)
    )]

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

- annoter l'assembly, pour indiquer qu'il contient un Themes/generic.xaml ...

    [assembly: ThemeInfo(
        // where theme specific resource dictionaries are located
        // (used if a resource is not found in the page,
        // or application resource dictionaries)
        themeDictionaryLocation : ResourceDictionaryLocation.None,    

        // where the generic resource dictionary is located
        // (used if a resource is not found in the page,
        // app, or any theme specific resource dictionaries
        genericDictionaryLocation : ResourceDictionaryLocation.SourceAssembly)]

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

## Themes : nommage et localisation des ResourceDictionaries

- créér un .xaml de ResourceDictionary par Theme
- le nommer <ThemeName>.<ThemeColor>.xaml
- l'inscrire dans le folder Themes
- vérifier l'Attribute ThemeInfo de l'Assembly embarquant les ResourceDictionary,
  et implémentant les Controles 'Thémés':

    // themeDictionaryLocation     : The location of theme-specific resources.
    // genericDictionaryLocation   : The location of generic, not theme-specific, resources.
    [assembly: ThemeInfo(ResourceDictionaryLocation.SourceAssembly, ResourceDictionaryLocation.SourceAssembly )]

- chaque Controle 'Thémé' doit faire l'objet de Ressources dont le TargetType, Key implicite, correspond
  à son type (cf 'création d'un CustomControl' plus bas).

- il doit être possible de créer des Assemblies de ressources externes, un par Thème,
    - le ThemeInfo de l'assembly implémentant le Contrôle 'Thémé' doit faire apparaitre :
      themeDictionaryLocation = [ResourceDictionaryLocation.ExternalAssembly](https://learn.microsoft.com/en-us/dotnet/api/system.windows.resourcedictionarylocation?view=windowsdesktop-7.0#fields)

    - ResourceDictionaryLocation.ExternalAssembly :
        Theme dictionaries exist in assemblies external to the one defining the types being themed. 
        They are named based on the original assembly with the theme name appended to it; 
        for example, PresentationFramework.Luna.dll. 
        These dictionaries share the same version and key as the original assembly.

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

- override the theme styles for any element.
- reference any assembly containing a set of theme dictionaries.

    <Application>
        <Application.Resources>
            <ResourceDictionary>
                <ResourceDictionary.MergedDictionaries>
                    <ResourceDictionary Source="{ThemeDictionary assemblyUri}" />  
                </ResourceDictionary.MergedDictionaries>
            </ResourceDictionary>
        </Application.Resources>
    </Application>

### [ThemeDictionaryExtension Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.themedictionaryextension?view=windowsdesktop-8.0)

#### [ThemeDictionaryExtension](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/themedictionary-markup-extension?view=netframeworkdesktop-4.8)

    [System.Windows.Markup.MarkupExtensionReturnType(typeof(System.Uri))]
    public class ThemeDictionaryExtension : System.Windows.Markup.MarkupExtension
    {

        public ThemeDictionaryExtension (string assemblyName);

    }

````
    <object property="{ThemeDictionary assemblyUri}" ... />  

    <object>  
        <object.property>  
            <ThemeDictionary AssemblyName="assemblyUri"/>  
        <object.property>  
    <object>  
````

- This extension is intended to fill only one specific property value: ResourceDictionary.Source.

- By using this extension, you can specify a single resources-only assembly that contains some styles 
  to use only when the Windows Aero theme is applied to the user's system, other styles only when 
  the Luna theme is active, and so on. 

- By using this extension, the contents of a control-specific resource dictionary can be automatically 
  invalidated and reloaded to be specific for another theme when required.


## création d'un CustomControl : aspects liés aux Ressources:

### code généré

    public class MyCustomButton : Control 
    { 

        static MyCustomButton() 
        { 
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyCustomButton), 
                                    new FrameworkPropertyMetadata(typeof(MyCustomButton))); 
        }

     }

### AssemblyInfo.cs :

    // themeDictionaryLocation     : The location of theme-specific resources.
    // genericDictionaryLocation   : The location of generic, not theme-specific, resources.
    [assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly )]

### Themes/generic.xaml généré

exemple :

    <Style TargetType=”{x:Type local:MyCustomButton}“> 
        <Setter Property=“Foreground” Value=“White”/> 
        <Setter Property=“Background” Value=“Black”/> 
        <Setter Property=“Padding” Value=“2”/> 
        <Setter Property=“Template”> 
            <Setter.Value> 
                <ControlTemplate TargetType=”{x:Type local:MyButton}“> 
                    <Border Background=”{TemplateBinding Background}“ 
                            BorderBrush=”{TemplateBinding BorderBrush}“ 
                            BorderThickness=”{TemplateBinding BorderThickness}“ 
                            Padding=”{TemplateBinding Padding}“ > 
                            <ContentPresenter /> 
                    </Border> 
                </ControlTemplate> 
            </Setter.Value> 
        </Setter> 
    </Style>

## [SystemParameters](https://learn.microsoft.com/en-us/dotnet/api/system.windows.systemparameters?view=windowsdesktop-7.0)

- les Keys exposées comme membre statiques des classes SystemParameters, SystemColors, SystemFonts
  sont de type :

    [TypeConverter(typeof(System.Windows.Markup.SystemKeyConverter))]
    internal class SystemResourceKey : ResourceKey

- Certaines static properties vont par paire avec une ResourceKey (SystemResourceKey)

    exemple :

    public static System.Windows.ResourceKey BorderKey { get; }

        Gets the ResourceKey for the Border property.

    public static int Border { get; }

    exemple: 

        Width="{x:Static SystemParameters.IconGridWidth}">
        Width="{DynamicResource {x:Static SystemParameters.IconGridWidthKey}}"

    exemple :

        ResourceKey _resourceKey = SystemColors.ActiveBorderColorKey;
        Debug.WriteLine($"_resourceKey={_resourceKey.GetType().Name}");

        var _v = this.TryFindResource(_resourceKey);
        Debug.WriteLine($".TryFindResource -> _v={_v} .GetType={_v.GetType().Name}");

        var _vv = SystemColors.ActiveBorderColor;
        Debug.WriteLine($"_vv={_vv} .GetType={_vv.GetType().Name}");

- Certaines Keys peuvent être assignées à des ressources dans un ResourceDictionary
 
  exemple :

    <Style x:Key="{x:Static SystemParameters.FocusVisualStyleKey}">
      <Setter Property="Control.Template">
        <Setter.Value>
          <ControlTemplate>
            <Rectangle StrokeThickness="1"
              Stroke="Black"
              StrokeDashArray="1 2"
              SnapsToDevicePixels="true"/>
          </ControlTemplate>
        </Setter.Value>
      </Setter>
    </Style>

- SystemParameters.StaticPropertyChanged Event

    public static event System.ComponentModel.PropertyChangedEventHandler StaticPropertyChanged;

    SystemColors, SystemFonts ?

## [SystemColors](https://learn.microsoft.com/en-us/dotnet/api/system.windows.systemcolors?view=windowsdesktop-7.0)

## [SystemFonts](https://learn.microsoft.com/en-us/dotnet/api/system.windows.systemfonts?view=windowsdesktop-7.0)
