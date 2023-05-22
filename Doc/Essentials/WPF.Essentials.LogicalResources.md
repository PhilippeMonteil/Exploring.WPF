
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
        - souvent assign� aux ResourceDictionary apparaissant dans .MergedDictionaries

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

 La XAML Extension StaticResource parcourt de fa�on ascendante les ResourceDictionary
 � la recherche de la ressource de Key donn�e. 
 Cette recherche se fait � travers l'arbre visuel, � partir de l'�l�ment
 pour lequel la Key est cherch�e, puis la .Resources de l'application 
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

Le fichier file1.xaml doit alors �tre de racine ResourceDictionary

### Resources non partag�es : x:Shared="False"

## Manipulation de Ressources par programme : FrameworkElement.FindResource, .TryFindResource, .SetResourceReference

### [FrameworkElement.FindResource](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.findresource?view=windowsdesktop-7.0)

    public object FindResource (object resourceKey);

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

## Ressources dans une autre Assembly : ComponentResourceKey

### Premi�re m�thode

#### dans l'assembly de ressources

- cr�er un Folder Themes et un ResourceDictionary generic.xaml
- cr�er une classe 'DummyClass', y inscrire l'attribut

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

- dans le ResourceDictionary:

````
    <SolidColorBrush 
        x:Key="{ComponentResourceKey {x:Type local:DummyClass}, MyComponentLibBrush}" 
        Color="DarkRed"/>
````

#### dans l'application cliente

- r�f�rencer l'assembly
- dans le XAML :

````
    xmlns:rlib="clr-namespace:WPFResourceAssembly;assembly=WPFResourceAssembly"

    <TextBlock x:Name="tbTest4" Text="Text4" 
        FontSize="32" 
        Foreground="{DynamicResource {ComponentResourceKey {x:Type rlib:DummyClass}, MyComponentLibBrush}}"/>
````

### Autre m�thode : exposition d'une ComponentResourceKey

#### dans l'assembly de ressources

- idem m�thode pr�c�dente
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

- idem m�thode pr�c�dente
- plus :

````
    <TextBlock x:Name="tbTest6" Text="Text7" 
        FontSize="32" 
        Foreground="{DynamicResource {x:Static rlib:DummyClass.Key0} }"/>
````

## Skinning

### [WPF complete guide to Themes and Skins](https://michaelscodingspot.com/wpf-complete-guide-themes-skins/)

### Compiled and Static 

#### Notes

- un ResourceDictionary expose � la fois:
    -  MergedDictionaries, collection de sous ResourceDictionary
    -  un ensemble Keys, Values, Item[]
- si un ResourceDictionary inscrit dans MergedDictionaries est modifi�,
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
