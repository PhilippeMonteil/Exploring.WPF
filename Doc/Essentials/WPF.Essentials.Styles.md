
# Styles
      
## explicit, implicit, default style 

### explicit style

      <Button Style="{StaticResource {x:Type Button}" />

      <Button>
        <Button.Style>
            <Style>
            ...
            </Style>
        </Button.Style>
      </Button>

### implicit style

- Style inscrit dans un ResourceDictionary avec un TargetType mais pas de x:Key

      <Style TargetType={x:Type Button} >
        <Setter Property="Control.FontSize" Value="32" />
      </Style>

### default style

- Style inscrit dans un ResourceDictionary, avec un TargetType mais pas de x:Key,
  dans le ResourceDirecty associ� au Theme courant, ou dans le ResourceDirecty par d�faut
  (Themes/generic.xaml)

      <Style TargetType={x:Type Button} >
        <Setter Property="Control.FontSize" Value="32" />
      </Style>

## notion de DefaultStyleKeyProperty, Key utilis�e pour trouver le Style implicite ou par d�faut d'un FrameworkElement

- la r�solution d'un Style par une Markup Extension StaticResource ou DynamicResource se fait comme 
  celle de toute autre ressource (FrameworkElement.FindResource)

- si la Key n'est pas de type ComponentResourceKey :
    - parcours ascendant de l'arbre visuel � partir du FrameworkElement citant la ressource
    - examen de Application.Resources
    - examen du ResourceDictionary du th�me courant point� par l'assembly dont est issu le FrameworkElement,
      si son attribut ThemeInfo l'indique
    - examen du ResourceDictionary par d�faut (Themes/generic.xaml) de l'assembly dont est issu le FrameworkElement,
      si son attribut ThemeInfo l'indique

- si la Key est de type ComponentResourceKey : par examen direct du Themes/generic.xaml de l'assembly
  cit�e par la ComponentResourceKey

- la key utilis�e pour la r�solution d'un Style implicite ou par d�faut 
  est indiqu�e par la propri�t� .DefaultStyleKey du FrameworkElement faisant l'objet de la recherche.

## Style dans un ResourceDictionary : Key, TargetType 

- un Style devrait pr�ciser son TargetType, de fa�on � v�rifier ses Setters, ... 
- un Style inscrit dans un ResourceDictionary doit avoir une x:Key,
  si ce n'est pas le cas son TargetType fait office de x:Key
 
    <Style x:Key="MyStyle">
        <Setter Property="Control.FontSize" Value="32" />
    </Style>

    <Style x:Key="MyStyle" TargetType={x:Type Button} >
        <Setter Property="Control.FontSize" Value="32" />
    </Style>

    <Style x:Key="{x:Type ItemsControl}" TargetType="{x:Type ItemsControl}">
        <Setter Property="Template">
            <Setter.Value>
                <ControlTemplate TargetType="{x:Type ItemsControl}">
                    <Border Background="{TemplateBinding Background}"
                            BorderBrush="{TemplateBinding BorderBrush}"
                            BorderThickness="{TemplateBinding BorderThickness}"
                            Padding="{TemplateBinding Padding}"
                            SnapsToDevicePixels="true">
                        <ItemsPresenter SnapsToDevicePixels="{TemplateBinding SnapsToDevicePixels}"/>
                    </Border>
                </ControlTemplate>
            </Setter.Value>
        </Setter>
    </Style>

## CustomControl : DefaultStyleKeyProperty, ThemeInfo, Themes/generic.xaml

La cr�ation d'un CustomControl MyCustomControl par VisualStudio s'accompage de la g�n�ration 
de plusieurs �l�ments.

### MyCustomControl.cs : DefaultStyleKeyProperty.OverrideMetadata

    public class MyCustomControl : Control 
    { 

        static MyCustomControl() 
        { 
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyCustomControl), 
                                    new FrameworkPropertyMetadata(typeof(MyCustomControl))); 
        }

     }

### AssemblyInfo.cs : ThemeInfo : ResourceDictionary Locations / Themes, Generic

    // themeDictionaryLocation     : The location of theme-specific resources.
    // genericDictionaryLocation   : The location of generic, not theme-specific, resources.
    [assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly )]

L'assembly ainsi annot�e indique qu'elle contient un Themes/generic.xaml 

    genericDictionaryLocation = ResourceDictionaryLocation.SourceAssembly

### Themes/generic.xaml

- Generic.xaml contains your default styles in a ResourceDictionary 
- You can�t rename it or move it to a different directory.

exemple :

    <ResourceDictionary
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:CustomControlLib">

        <Style TargetType=�{x:Type local:MyCustomControl}�> 
            <Setter Property=�Foreground� Value=�White�/> 
            <Setter Property=�Background� Value=�Black�/> 
            <Setter Property=�Padding� Value=�2�/> 
            <Setter Property=�Template�> 
                <Setter.Value> 
                    <ControlTemplate TargetType=�{x:Type local:MyCustomControl}�> 
                        <Border Background=�{TemplateBinding Background}� 
                                BorderBrush=�{TemplateBinding BorderBrush}� 
                                BorderThickness=�{TemplateBinding BorderThickness}� 
                                Padding=�{TemplateBinding Padding}� > 
                                <ContentPresenter /> 
                        </Border> 
                    </ControlTemplate> 
                </Setter.Value> 
            </Setter> 
        </Style>
        ...

## FrameworkElement : propri�t�s li�es aux Styles

### [Style](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.style?view=windowsdesktop-7.0)

Style est une propri�t� assignable comme les autres, m�me si son assignation se traduit par celle des propri�t�s
faisant l'objet de ses Setters.

### [FocusVisualStyle](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.focusvisualstyle?view=windowsdesktop-7.0)

- Style appliqu� � un arbre visuel temporaire, plac� au dessus de celui du contr�le.
- Ce Style doit contenir un Setter de Template d�finissant cet arbre visuel.
- un Th�me devrait contenir un FocusVisualStyle, Style de Key SystemParameters.FocusVisualStyleKey

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

- [Styling for Focus in Controls, and FocusVisualStyle](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/styling-for-focus-in-controls-and-focusvisualstyle?view=netframeworkdesktop-4.8)

    - the "focus visual style" creates a separate visual tree for an adorner that draws on top of the control, 
      rather than changing the visual tree of the control or other UI element by replacing it. 
    - Focus visual styles act only when the focus action was initiated by the keyboard. 
    - If you want UI changes for any type of focus, whether via mouse, keyboard, or programmatically, 
      then you should not use focus visual styles, and should instead use setters and triggers 
      in styles or templates that are working from the value of general focus properties such as 
      IsFocused or IsKeyboardFocusWithin.

### [DefaultStyleKey](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.defaultstylekey?view=windowsdesktop-7.0)

    protected internal object DefaultStyleKey { get; set; }

- La valeur assign�e � une instance d'un type d�riv� de FrameworkElement est le plus souvent celle par d�faut, 
  pr�cis�e � l'enregistrement ou � la surcharge de la prorpi�t� DefaultStyleKeyProperty.

- DefaultStyleKey est utilis� comme Key pour la recherche du Style par d�faut d'une instance de FrameworkElement.  

### [OverridesDefaultStyle](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.overridesdefaultstyle?view=windowsdesktop-7.0)

- true if this element does not use theme style properties; all style-originating properties come from 
  local application styles, and theme style properties do not apply. 
- false if application styles apply first, and then theme styles apply for properties that were 
  not specifically set in application styles. 
- The default is false.

## ResourceKey expos�es statiquement par un CustomControl

- certains CustomControls exposent sous forme de propri�t�s statiques les ResourceKeys
- ces ResourceKeys sont utilis�es par leurs templates
- Une telles ResourceKey peut �tre assign�e � une ressource dans un ResourceDictionary 
  pour �tre 'vue' par les contr�les 'dans le scope' du ResourceDictionary

#### Exemple : ToolBar.ButtonStyleKey Property

    public static System.Windows.ResourceKey ButtonStyleKey { get; }

    <Application>
      <Application.Resources>
        <Style x:Key="{x:Static ToolBar.ButtonStyleKey}" TargetType="{x:Type Button}">
        ...
        </Style>
      </Application.Resources>
    </Application>

## [SystemParameters](https://learn.microsoft.com/en-us/dotnet/api/system.windows.systemparameters?view=windowsdesktop-7.0)

- Certaines static properties vont par paire

    exemple :

    public static System.Windows.ResourceKey BorderKey { get; }

        Gets the ResourceKey for the Border property.

    public static int Border { get; }

    exemple: 

      Width="{DynamicResource {x:Static SystemParameters.VirtualScreenWidthKey}}"

- Certaines Keys peuvent �tre assign�es � des ressources dans un ResourceDictionary
 
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

- StaticPropertyChanged event

    public static event System.ComponentModel.PropertyChangedEventHandler StaticPropertyChanged;

## [SystemColors](https://learn.microsoft.com/en-us/dotnet/api/system.windows.systemcolors?view=windowsdesktop-7.0)

## [SystemFonts](https://learn.microsoft.com/en-us/dotnet/api/system.windows.systemfonts?view=windowsdesktop-7.0)

