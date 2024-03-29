
# Styles

## En r�sum�

- Style

```c#
[Localizability(LocalizationCategory.Ignore)]
[Markup.ContentProperty("Setters")]
[Markup.DictionaryKeyProperty("TargetType")]
public class Style : Threading.DispatcherObject, 
                                Markup.IAddChild, 
                                Markup.INameScope, 
                                Markup.IQueryAmbient
{
    ...
}
```

- d�rive de DispatcherObject
- properties

```c#
public Style BasedOn { get; set; }
public Type TargetType { get; set; }
public bool IsSealed { get; }
public ResourceDictionary Resources { get; set; }
public SetterBaseCollection Setters { get; }
public TriggerCollection Triggers { get; }
```

  - ContentProperty : Setters
  - DictionaryKeyProperty : TargetType
        propri�t� utilis�e comme key pour inscrire un Style dans un ResourceDictionary si aucune key n'est pr�cis�e

- FrameworkElement 

    - properties

        - Style 
        - DefaultStyleKeyProperty
        - FocusVisualStyle, SystemParameters.FocusVisualStyleKey
        - OverridesDefaultStyle

            public bool OverridesDefaultStyle { get; set; }

            - true if this element does not use theme style properties; all style-originating properties 
              come from local application styles, and theme style properties do not apply. 
            - false if application styles apply first, and then theme styles apply for properties that were 
              not specifically set in application styles. 
            - The default is false.

    - methods

        - protected internal virtual void OnStyleChanged (System.Windows.Style oldStyle, System.Windows.Style newStyle);
        - public bool ShouldSerializeStyle ();
        - public void UpdateDefaultStyle ();

- styles explicites, implicites, par Theme (par d�faut)

- Themes/generic.xaml
 
- AssemblyInfo.cs : ThemeInfo : ResourceDictionary Locations / Themes, Generic

- CustomControl : DefaultStyleKeyProperty, ThemeInfo, Themes/generic.xaml

- contr�ler les Styles par Theme de Contr�les externes
    - en les d�rivant et surchargeant la MetaData de DefaultStyleKeyProperty
    - � l'aide d'une ThemeDictionaryExtension 

- FocusVisualStyle
    - Style appliqu� � un arbre visuel temporaire, plac� au dessus de celui du contr�le.
    - Ce Style doit contenir un Setter de Template d�finissant cet arbre visuel.
    - un Th�me devrait contenir un FocusVisualStyle, Style de Key SystemParameters.FocusVisualStyleKey

## [Style Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.style?view=windowsdesktop-7.0)

    [Localizability(LocalizationCategory.Ignore)]
    [Markup.ContentProperty("Setters")]
    [Markup.DictionaryKeyProperty("TargetType")]
    public class Style : Threading.DispatcherObject, 
                                Markup.IAddChild, 
                                Markup.INameScope, 
                                Markup.IQueryAmbient

- attribut DictionaryKeyProperty : indique que la propri�t� TargetType sert de Key lors
  d'une insertion d'une instance de Style dans un ResourceDictionary

- properties

    [Markup.Ambient]
    public Style BasedOn { get; set; }

    public bool IsSealed { get; }

    [Markup.Ambient]
    public ResourceDictionary Resources { get; set; }

    public SetterBaseCollection Setters { get; }

    [Markup.Ambient]
    public Type TargetType { get; set; }

    public TriggerCollection Triggers { get; }

- methods

    public void Seal ();

## BasedOn

- exemple :

````
<Window.Resources>
    <!-- .... other resources .... -->

    <!--A Style that affects all TextBlocks-->
    <Style TargetType="TextBlock">
        <Setter Property="HorizontalAlignment" Value="Center" />
        <Setter Property="FontFamily" Value="Comic Sans MS"/>
        <Setter Property="FontSize" Value="14"/>
    </Style>
    
    <!--A Style that extends the previous TextBlock Style with an x:Key of TitleText-->
    <Style BasedOn="{StaticResource {x:Type TextBlock}}"
           TargetType="TextBlock"
           x:Key="TitleText">
        <Setter Property="FontSize" Value="26"/>
        <Setter Property="Foreground">
            <Setter.Value>
                <LinearGradientBrush StartPoint="0.5,0" EndPoint="0.5,1">
                    <LinearGradientBrush.GradientStops>
                        <GradientStop Offset="0.0" Color="#90DDDD" />
                        <GradientStop Offset="1.0" Color="#5BFFFF" />
                    </LinearGradientBrush.GradientStops>
                </LinearGradientBrush>
            </Setter.Value>
        </Setter>
    </Style>
</Window.Resources>
````

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

- Style inscrit dans un ResourceDictionary 'ascendant', jusqu'� App.xaml, avec un TargetType 
  mais pas de x:Key

      <Style TargetType={x:Type Button} >
        <Setter Property="Control.FontSize" Value="32" />
      </Style>

### Theme, default style

- Style inscrit dans un ResourceDictionary, avec un TargetType mais pas de x:Key,
  dans le ResourceDirecty associ� au Theme courant, ou dans le ResourceDictionary par d�faut
  (Themes/generic.xaml)

      <Style TargetType={x:Type Button} >
        <Setter Property="Control.FontSize" Value="32" />
      </Style>

## notion de FrameworkElement.DefaultStyleKeyProperty

    protected internal object DefaultStyleKey { get; set; }

    Gets or sets the key to use to reference the style for this control, when theme styles are used or defined.

- Key utilis�e pour trouver le Style implicite ou par d�faut d'un FrameworkElement

- la r�solution d'un Style par une Markup Extension StaticResource ou DynamicResource,
  la recherche du Theme ou default Style se fait comme celle de toute autre ressource 
  (FrameworkElement.FindResource), � partir de la valeur de la propri�t� DefaultStyleKey

      public object FindResource (object resourceKey);

      protected internal object DefaultStyleKey { get; set; }

  du DependencyObject cible de la Markup Extension.

- cette valeur est le plus souvent la valeur par d�faut pr�cis�e par la MetaData d�finie lors de la cr�ation 
  par FrameworkElement de la DependencyProperty DefaultStyleKeyProperty, � savoir son type.

- une classe d�rivant de FrameworkElement peut surcharger la MetaData de FrameworkElement en red�finissant
  cette valeur par d�faut comme son type propre.

## Style dans un ResourceDictionary : Key, TargetType 

- un Style devrait pr�ciser son TargetType, de fa�on � v�rifier ses Setters, ... 
- un Style inscrit dans un ResourceDictionary doit avoir une x:Key,
  si ce n'est pas le cas son TargetType fait office de x:Key, comme indiqu� par:
```
    [Markup.DictionaryKeyProperty("TargetType")]
```
- pour �tre un implicite pour un type de FramewokElement particulier, un Style doit pr�ciser
  un TargetType mais pas de x:Key

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
de plusieurs �l�ments assurant son support des Themes, Style par d�faut.

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

#### [public enum ResourceDictionaryLocation](https://learn.microsoft.com/en-us/dotnet/api/system.windows.resourcedictionarylocation?view=windowsdesktop-7.0)

| Syntax | Description |
| - | - |
| ExternalAssembly | Theme dictionaries exist in assemblies external to the one defining the types being themed. They are named based on the original assembly with the theme name appended to it; for example, PresentationFramework.Luna.dll. These dictionaries share the same version and key as the original assembly.|
| SourceAssembly | Theme dictionaries exist in the assembly that defines the types being themed. |

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

- propri�t�s
    - public System.Windows.Style Style { get; set; }
    - protected internal object DefaultStyleKey { get; set; }
    - public System.Windows.Style FocusVisualStyle { get; set; }
    - public bool OverridesDefaultStyle { get; set; }

- methodes
    - protected internal virtual void OnStyleChanged (System.Windows.Style oldStyle, System.Windows.Style newStyle);
    - public bool ShouldSerializeStyle ();
    - public void UpdateDefaultStyle ();

### [Style](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.style?view=windowsdesktop-7.0)

Style est une propri�t� assignable comme les autres, m�me si son assignation se traduit par celle des propri�t�s
faisant l'objet de ses Setters.

### [FocusVisualStyle](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.focusvisualstyle?view=windowsdesktop-7.0)

- Style appliqu� � un arbre visuel temporaire, plac� au dessus de celui du contr�le (Adorner).
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
    - The themes for controls include a default focus visual style behavior that becomes the 
      focus visual style for all controls in the theme. 
    - This theme style is identified by the value of the static key SystemParameters.FocusVisualStyleKey. 
    - When you declare your own focus visual style at the application level, you replace this 
      default style behavior from the themes. 
    - Alternatively, if you define the entire theme, then you should use this same key to define 
      the style for the default behavior for your entire theme.

### [DefaultStyleKey](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.defaultstylekey?view=windowsdesktop-7.0)

    protected internal object DefaultStyleKey { get; set; }

- La valeur assign�e � une instance d'un type d�riv� de FrameworkElement est le plus souvent celle par d�faut, 
  pr�cis�e � l'enregistrement ou � la surcharge de la prorpi�t� DefaultStyleKeyProperty.

- DefaultStyleKey est utilis� comme Key pour la recherche du Style par d�faut d'une instance de FrameworkElement.  

### [OverridesDefaultStyle](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.overridesdefaultstyle?view=windowsdesktop-7.0)

    public bool OverridesDefaultStyle { get; set; }

- true if this element does not use theme style properties; all style-originating properties come from 
  local application styles, and theme style properties do not apply. 
- false if application styles apply first, and then theme styles apply for properties that were 
  not specifically set in application styles. 
- The default is false.
