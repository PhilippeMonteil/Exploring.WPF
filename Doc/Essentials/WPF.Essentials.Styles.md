
# Styles

## Style assign� directement

- la classe FramewokElement expose plusieurs propri�t�s de type System.Windows.Style : Style, FocusVisualStyle
- ces propri�t�s peuvent faire l'objet d'assignations directes par la description XAML d'une instance de FramewokElement 

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

- la r�solution d'un Style par une Markup Extension StaticResource ou DynamicResource se fait comme 
  celle de toute autre ressource
  - si la Key n'est pas de type ComponentResourceKey :
      - parcours ascendant de l'arbre visuel � partir du FrameworkElement citant la ressource,
      - examen de Application.Resources
      - examen du ResourceDictionary du th�me courant
      - examen du ResourceDictionary par d�faut (Themes/generic.xaml) de l'assembly dont est issu le FrameworkElement
  - si la Key est de type ComponentResourceKey : par examen direct du Themes/generic.xaml de l'assembly
    cit�e par la ComponentResourceKey
      
## explicit, implicit, default style 

### explicit style

      <Button Style="{StaticResource {x:Type Button}" />

### implicit style

- Style inscrit dans un ResourceDictionary avec un TargetType mais pas de x:Key

      <Style TargetType={x:Type Button} >
        <Setter Property="Control.FontSize" Value="32" />
      </Style>

### default style

- Style par d�faut d'un CustomControl
- pr�cise son Template par d�faut
- un CustomControl doit 'OverrideMetadata' la DependencyProperty DefaultStyleKeyProperty
  en pr�cisant son propre type comme valeur par d�faut (FrameworkPropertyMetadata)
  pour �tabir une connexion avec son style par defaut :
  - stock� dans Themes/generic.xaml
  - de TargetType, et donc Key, son propre type

### default ResourceDictionary : Themes/generic.xaml

### cr�ation d'un CustomControl : DefaultStyleKeyProperty, ThemeInfo, Themes/generic.xaml

### code g�n�r�

    public class MyCustomButton : Control 
    { 

        static MyCustomButton() 
        { 
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyCustomButton), 
                                    new FrameworkPropertyMetadata(typeof(MyCustomButton))); 
        }

     }

### AssemblyInfo.cs g�n�r� : ThemeInfo : ResourceDictionary Locations / Themes, Generic

    // themeDictionaryLocation     : The location of theme-specific resources.
    // genericDictionaryLocation   : The location of generic, not theme-specific, resources.
    [assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly )]

L'assembly ainsi annot�e indique qu'elle contient un Themes/generic.xaml 

    genericDictionaryLocation = ResourceDictionaryLocation.SourceAssembly

### Themes/generic.xaml g�n�r�

Generic.xaml is a special file that contains your default styles. 
You can�t rename it or move it to a different directory.

exemple :

    <Style TargetType=�{x:Type local:MyCustomButton}�> 
        <Setter Property=�Foreground� Value=�White�/> 
        <Setter Property=�Background� Value=�Black�/> 
        <Setter Property=�Padding� Value=�2�/> 
        <Setter Property=�Template�> 
            <Setter.Value> 
                <ControlTemplate TargetType=�{x:Type local:MyCustomButton}�> 
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









## FrameworkElement : propri�t�s li�es aux Styles

### [Style](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.style?view=windowsdesktop-7.0)

### [FocusVisualStyle](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.focusvisualstyle?view=windowsdesktop-7.0)

### [DefaultStyleKey ](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.defaultstylekey?view=windowsdesktop-7.0)

### [OverridesDefaultStyle](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.overridesdefaultstyle?view=windowsdesktop-7.0)



## Notion de ResourceKey

### propri�t� expos�e par un CustomControl

- utilis�e par son template
- peut �tre assign�e � un ressources dans un ResourceDictionary pour 'surcharger' la ressource
  de m�me ResourceKey par les contr�les 'dans le scope' du ResourceDictionary

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
