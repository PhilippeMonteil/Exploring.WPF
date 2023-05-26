
# Styles

## Style assigné directement

- la classe FramewokElement expose plusieurs propriétés de type System.Windows.Style : Style, FocusVisualStyle
- ces propriétés peuvent faire l'objet d'assignations directes par la description XAML d'une instance de FramewokElement 

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

- la résolution d'un Style par une Markup Extension StaticResource ou DynamicResource se fait comme 
  celle de toute autre ressource
  - si la Key n'est pas de type ComponentResourceKey :
      - parcours ascendant de l'arbre visuel à partir du FrameworkElement citant la ressource,
      - examen de Application.Resources
      - examen du ResourceDictionary du thème courant
      - examen du ResourceDictionary par défaut (Themes/generic.xaml) de l'assembly dont est issu le FrameworkElement
  - si la Key est de type ComponentResourceKey : par examen direct du Themes/generic.xaml de l'assembly
    citée par la ComponentResourceKey
      
## explicit, implicit, default style 

### explicit style

      <Button Style="{StaticResource {x:Type Button}" />

### implicit style

- Style inscrit dans un ResourceDictionary avec un TargetType mais pas de x:Key

      <Style TargetType={x:Type Button} >
        <Setter Property="Control.FontSize" Value="32" />
      </Style>

### default style

- Style par défaut d'un CustomControl
- précise son Template par défaut
- un CustomControl doit 'OverrideMetadata' la DependencyProperty DefaultStyleKeyProperty
  en précisant son propre type comme valeur par défaut (FrameworkPropertyMetadata)
  pour étabir une connexion avec son style par defaut :
  - stocké dans Themes/generic.xaml
  - de TargetType, et donc Key, son propre type

### default ResourceDictionary : Themes/generic.xaml

### création d'un CustomControl : DefaultStyleKeyProperty, ThemeInfo, Themes/generic.xaml

### code généré

    public class MyCustomButton : Control 
    { 

        static MyCustomButton() 
        { 
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyCustomButton), 
                                    new FrameworkPropertyMetadata(typeof(MyCustomButton))); 
        }

     }

### AssemblyInfo.cs généré : ThemeInfo : ResourceDictionary Locations / Themes, Generic

    // themeDictionaryLocation     : The location of theme-specific resources.
    // genericDictionaryLocation   : The location of generic, not theme-specific, resources.
    [assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly )]

L'assembly ainsi annotée indique qu'elle contient un Themes/generic.xaml 

    genericDictionaryLocation = ResourceDictionaryLocation.SourceAssembly

### Themes/generic.xaml généré

Generic.xaml is a special file that contains your default styles. 
You can’t rename it or move it to a different directory.

exemple :

    <Style TargetType=”{x:Type local:MyCustomButton}“> 
        <Setter Property=“Foreground” Value=“White”/> 
        <Setter Property=“Background” Value=“Black”/> 
        <Setter Property=“Padding” Value=“2”/> 
        <Setter Property=“Template”> 
            <Setter.Value> 
                <ControlTemplate TargetType=”{x:Type local:MyCustomButton}“> 
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









## FrameworkElement : propriétés liées aux Styles

### [Style](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.style?view=windowsdesktop-7.0)

### [FocusVisualStyle](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.focusvisualstyle?view=windowsdesktop-7.0)

### [DefaultStyleKey ](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.defaultstylekey?view=windowsdesktop-7.0)

### [OverridesDefaultStyle](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.overridesdefaultstyle?view=windowsdesktop-7.0)



## Notion de ResourceKey

### propriété exposée par un CustomControl

- utilisée par son template
- peut être assignée à un ressources dans un ResourceDictionary pour 'surcharger' la ressource
  de même ResourceKey par les contrôles 'dans le scope' du ResourceDictionary

#### Exemple : ToolBar.ButtonStyleKey Property

    public static System.Windows.ResourceKey ButtonStyleKey { get; }

    <Application>
      <Application.Resources>
        <Style x:Key="{x:Static ToolBar.ButtonStyleKey}" TargetType="{x:Type Button}"
      </Application.Resources>
    </Application>

## Triggers

### Property Triggers

     <Style.Triggers>

       <Trigger Property="IsMouseOver" Value="True">
         <Setter Property="" Value="" />
       </Trigger>

       <Trigger Property="Validation.HasErrors" Value="True">
         <Setter Property="Background" Value="Red" />
         <Setter Property="Tooltip" 
            Value="{Binding 
                    RelativeSource={RelativeSource Self}},
                    Path={Vaidation.Errors[0].ErrorContent} " />
       </Trigger>

     </Style.Triggers>

### Data Triggers

     <Style.Triggers>

       <DataTrigger 
            Binding="{Binding RelativeSource={RelativeSource Self}, Path=Text}" 
            Value="True">
         <Setter Property="IsEnabled" Value="False" />
       </DataTrigger>

     </Style.Triggers>

### MultiTrigger

     <Style.Triggers>

       <MultiTrigger>

         <MultiTrigger.Conditions>
           <Condition Property="IsMouseOver" Value="True"/>
           <Condition Property="IsFocused" Value="True"/>
         </MultiTrigger.Conditions>

         <Setter Property="IsEnabled" Value="False" />

       </MultiTrigger>

     </Style.Triggers>

### Event Trigger

     <Button.Triggers>
       <EventTrigger RoutedEvent="Button.Click">
         <EventTrigger.Actions>
           <BeginStoryBoard>
             <StoryBoard>

             </StoryBoard>
           </BeginStoryBoard>
         </EventTrigger.Actions>
       </EventTrigger>
     </Button.Triggers>
