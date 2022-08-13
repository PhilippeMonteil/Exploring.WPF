
# Control, ControlTemplate

## [Control Class](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.control?view=windowsdesktop-6.0)

	public class Control : System.Windows.FrameworkElement

### Inheritance

    Object
    DispatcherObject
    DependencyObject
    Visual
    UIElement
    FrameworkElement
    Control

### Properties

    public System.Windows.Controls.ControlTemplate Template { get; set; }


## [ControlTemplate Class](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.controltemplate?view=windowsdesktop-6.0)

    [System.Windows.Localizability(System.Windows.LocalizationCategory.None, Readability=System.Windows.Readability.Unreadable)]
    [System.Windows.Markup.DictionaryKeyProperty("TargetType")]
    public class ControlTemplate : System.Windows.FrameworkTemplate

Inheritance:

    Object
    DispatcherObject
    FrameworkTemplate
    ControlTemplate

Properties:

    [System.Windows.Markup.Ambient]
    public Type TargetType { get; set; }

    [System.Windows.Markup.DependsOn("VisualTree")]
    [System.Windows.Markup.DependsOn("Template")]
    public System.Windows.TriggerCollection Triggers { get; }

Exemple:

    <Style TargetType="Button">
        <!--Set to true to not get any properties from the themes.-->
        <Setter Property="OverridesDefaultStyle" Value="True"/>
        <Setter Property="Template">
            <Setter.Value>
                <ControlTemplate TargetType="Button">
                    <Grid>
                        <Ellipse Fill="{TemplateBinding Background}"/>
                        <ContentPresenter HorizontalAlignment="Center"
                            VerticalAlignment="Center"/>
                    </Grid>
                </ControlTemplate>
            </Setter.Value>
        </Setter>
    </Style>

## [FrameworkTemplate Class](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworktemplate?view=windowsdesktop-6.0)

    [System.Windows.Localizability(System.Windows.LocalizationCategory.NeverLocalize)]
    [System.Windows.Markup.ContentProperty("VisualTree")]
    public abstract class FrameworkTemplate : 
            System.Windows.Threading.DispatcherObject, 
            System.Windows.Markup.INameScope, 
            System.Windows.Markup.IQueryAmbient

Inheritance:

    Object
    DispatcherObject
    FrameworkTemplate

Derived:

    System.Windows.Controls.ControlTemplate
    System.Windows.Controls.ItemsPanelTemplate
    System.Windows.DataTemplate

Implements:
    INameScope
    IQueryAmbient

Properties:

    [System.Windows.Markup.Ambient]
    public System.Windows.ResourceDictionary Resources { get; set; }

    [System.Windows.Markup.Ambient]
    public System.Windows.TemplateContent Template { get; set; }

    public System.Windows.FrameworkElementFactory VisualTree { get; set; }

