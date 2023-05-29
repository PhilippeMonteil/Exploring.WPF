
# Styles

## En résumé

- styles explicites, implicites, par Theme, par défaut
- FrameworkElement .DefaultStyleKeyProperty, .FocusVisualStyle, .OverridesDefaultStyle
- ResourceKeys : exposées par SystemParameters, par un CustomControl,  ...
- contrôler les Styles par Theme de Contrôles externes
    - en les dérivant et surchargeant la MetaData de DefaultStyleKeyProperty
    - à l'aide d'une ThemeDictionaryExtension 

## [Style Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.style?view=windowsdesktop-7.0)

    [System.Windows.Localizability(System.Windows.LocalizationCategory.Ignore)]
    [System.Windows.Markup.ContentProperty("Setters")]
    [System.Windows.Markup.DictionaryKeyProperty("TargetType")]
    public class Style : System.Windows.Threading.DispatcherObject, 
                                System.Windows.Markup.IAddChild, 
                                System.Windows.Markup.INameScope, 
                                System.Windows.Markup.IQueryAmbient

- attribut DictionaryKeyProperty : indique que la propriété TargetType sert de Key lors d'une inserction
  d'une instance de Style dans un ResourceDictionary

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

- Style inscrit dans un ResourceDictionary 'ascendant' avec un TargetType mais pas de x:Key

      <Style TargetType={x:Type Button} >
        <Setter Property="Control.FontSize" Value="32" />
      </Style>

### Theme, default style

- Style inscrit dans un ResourceDictionary, avec un TargetType mais pas de x:Key,
  dans le ResourceDirecty associé au Theme courant, ou dans le ResourceDictionary par défaut
  (Themes/generic.xaml)

      <Style TargetType={x:Type Button} >
        <Setter Property="Control.FontSize" Value="32" />
      </Style>

## notion de DefaultStyleKeyProperty, Key utilisée pour trouver le Style implicite ou par défaut d'un FrameworkElement

- la résolution d'un Style par une Markup Extension StaticResource ou DynamicResource,
  la recherche du Theme ou default Style se fait comme celle de toute autre ressource 
  (FrameworkElement.FindResource), à partir de la valeur de la propriété DefaultStyleKey

    protected internal object DefaultStyleKey { get; set; }

  du DependencyObject cible de la Markup Extension.

- cette valeur est le plus souvent la valeur par défaut précisée par la MetaData précisée lors de la création 
  par FrameworkElement de la DependencyProperty DefaultStyleKeyProperty, à savoir son type.

- une classe dérivant de FrameworkElement peut surcharger la MetaData de FrameworkElement en redéfinissant
  cette valeur par défaut comme son type propre.

## Style dans un ResourceDictionary : Key, TargetType 

- un Style devrait préciser son TargetType, de façon à vérifier ses Setters, ... 
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

La création d'un CustomControl MyCustomControl par VisualStudio s'accompage de la génération 
de plusieurs éléments assurant son support des Themes, Style par défaut.

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

L'assembly ainsi annotée indique qu'elle contient un Themes/generic.xaml 

    genericDictionaryLocation = ResourceDictionaryLocation.SourceAssembly

### Themes/generic.xaml

- Generic.xaml contains your default styles in a ResourceDictionary 
- You can’t rename it or move it to a different directory.

exemple :

    <ResourceDictionary
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:CustomControlLib">

        <Style TargetType=”{x:Type local:MyCustomControl}“> 
            <Setter Property=“Foreground” Value=“White”/> 
            <Setter Property=“Background” Value=“Black”/> 
            <Setter Property=“Padding” Value=“2”/> 
            <Setter Property=“Template”> 
                <Setter.Value> 
                    <ControlTemplate TargetType=”{x:Type local:MyCustomControl}“> 
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
        ...

## FrameworkElement : propriétés liées aux Styles

### [Style](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.style?view=windowsdesktop-7.0)

Style est une propriété assignable comme les autres, même si son assignation se traduit par celle des propriétés
faisant l'objet de ses Setters.

### [FocusVisualStyle](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.focusvisualstyle?view=windowsdesktop-7.0)

- Style appliqué à un arbre visuel temporaire, placé au dessus de celui du contrôle.
- Ce Style doit contenir un Setter de Template définissant cet arbre visuel.
- un Thème devrait contenir un FocusVisualStyle, Style de Key SystemParameters.FocusVisualStyleKey

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

- La valeur assignée à une instance d'un type dérivé de FrameworkElement est le plus souvent celle par défaut, 
  précisée à l'enregistrement ou à la surcharge de la prorpiété DefaultStyleKeyProperty.

- DefaultStyleKey est utilisé comme Key pour la recherche du Style par défaut d'une instance de FrameworkElement.  

### [OverridesDefaultStyle](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.overridesdefaultstyle?view=windowsdesktop-7.0)

- true if this element does not use theme style properties; all style-originating properties come from 
  local application styles, and theme style properties do not apply. 
- false if application styles apply first, and then theme styles apply for properties that were 
  not specifically set in application styles. 
- The default is false.
