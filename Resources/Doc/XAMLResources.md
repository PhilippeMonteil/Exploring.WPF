
# [Overview of XAML resources (WPF .NET)](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/systems/xaml-resources-overview?view=netdesktop-6.0&redirectedfrom=MSDN&viewFallbackFrom=netdesktop-6.0)

# [ComponentResourceKey Markup Extension](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/componentresourcekey-markup-extension?view=netframeworkdesktop-4.8)

## Première méthode

### dans l'assembly de ressources

- créer un Folder Themes et un ResourceDictionary generix.xaml
- crééer une classe 'Dummy', y inscrire l'attribut

    [assembly: ThemeInfo(
        ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page, 
                                     // or application resource dictionaries)
        ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page, 
                                              // app, or any theme specific resource dictionaries)
    )]

- dans le ResourceDictionary:

````
    <SolidColorBrush 
        x:Key="{ComponentResourceKey {x:Type local:DummyClass}, MyComponentLibBrush}" 
        Color="DarkRed"/>
````

### dans l'application cliente

- référencer l'assembly
- dans le XAML :

````
    xmlns:rlib="clr-namespace:WPFResourceAssembly;assembly=WPFResourceAssembly"

    <TextBlock x:Name="tbTest4" Text="Text4" 
        FontSize="32" 
        Foreground="{DynamicResource {ComponentResourceKey {x:Type rlib:DummyClass}, MyComponentLibBrush}}"/>
````

## Autre méthode : exposition d'une ComponentResourceKey

### dans l'assembly de ressources

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

### dans l'application cliente

- idem méthode précédente
- plus :

````
    <TextBlock x:Name="tbTest6" Text="Text7" 
        FontSize="32" 
        Foreground="{DynamicResource {x:Static rlib:DummyClass.Key0} }"/>
````
