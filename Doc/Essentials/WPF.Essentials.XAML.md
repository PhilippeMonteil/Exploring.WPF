
# [XAML & WPF](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/xaml-in-wpf?view=netframeworkdesktop-4.8)

## en r�sum�

- mapping namespace XML / namespace
    - used for type resolution by a XAML object writer or XAML schema context.
    - xmlns:lib="clr-namespace:ItemsControlLib;assembly=ItemsControlLib"
    - attribut XmlnsDefinitionAttribute
        - dans AssemblyInfo.cs
        - ex: [assembly: XmlnsDefinitionAttribute("http://my.schemas.com/ResourceAssembly", "ResourceAssembly")]

    - xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        - x:Key
        - x:Class

- Property elements

- ContentProperty attribute

- TypeConverters

    - attribut TypeConverterAttribute appliqu� � une classe ou � une propri�t�,
      pr�cise la classe d�rivant de TypeConverter capable de faire des conversions
      vers et depuis la classe ou la propri�t� annot�e.
    - exemple :
    	[System.ComponentModel.TypeConverter("System.Windows.NullableBoolConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
    	public bool? IsSynchronizedWithCurrentItem { get; set; }

- Markup extensions

    - de la forme "{MarkupExtensionName [Named Parameter]',' [Positional Parameter]}"
    - MarkupExtensionName : nom d'une classe d�rivant de MarkupExtension
    - Named Parameter : 
        - Name = Value
        - Name correspond � une propri�t�
    - Positional Parameter : correspond � un param�tre de constructeur
    - exemples d'usage :

````
    	<object property="{DynamicResource key}" ... />  
	
    	<object>  
    		<object.property>  
    			<DynamicResource ResourceKey="key" ... />  
    		</object.property>  
    	</object>  
````

    - classe de MarkupExtension
        - d�rive de MarkupExtension
        - attribut MarkupExtensionReturnType
            ex : [MarkupExtensionReturnType(typeof(string))]
        - surcharge ProvideValue :
            public override object ProvideValue(IServiceProvider serviceProvider)
        - IServiceProvider.GetService
            ex :
                IProvideValueTarget provideValueTarget = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
                DependencyObject dependencyObject = provideValueTarget.TargetObject as DependencyObject;
                DependencyProperty dependencyProperty = provideValueTarget.TargetProperty as DependencyProperty;

    - attribut ConstructorArgument

    - x: Markup extensions
        - x:Type, x:Static, x:Null, x:Array

- Children of Object Elements

    - Content
    - Collection items
        - Lists
        - Dictionaries

- Loading XAML at runtime : XamlReader.Load

- Root window

ex :

    public class RootWindow : Window // non d�finie en XAML
    {
    }

    <local:RootWindow x:Class="WpfApp1.MainWindow" x:ClassModifier="public" ...

- Code generation

    - x:Class
    - x:ClassModifier
    - x:Name
    - x:FieldModifier
    - x:TypeArguments : support des types g�n�riques

    xmlns:sys="clr-namespace:System;assembly=mscorlib"
    xmlns:scg="clr-namespace:System.Collections.Generic;assembly=mscorlib"

    <scg:List x:TypeArguments="sys:String" ...> 
        instantiates a new List<T> with a String type argument.

    <scg:Dictionary x:TypeArguments="sys:String,sys:String" ...> 

    - x:Key

````
ex:

     <ResourceDictionary xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
        <Color x:Key="" ...>
        <Color x:Key="" ...>
    </ResourceDictionary>
````

- attached properties

    - ex :

     <DockPanel>
       <TextBox DockPanel.Dock="Top">Enter text</TextBox>
     </DockPanel>

    <Grid>
      <VisualStateManager.VisualStateGroups>
        <VisualStateGroup x:Name="CommonStates">



## Property elements
## ContentProperty attribute, ex: ContentControl

### Exemple

    <Button>
        <Button.Content>Click!</Button.Content>
    </Button>

## ContentProperty attribute, ex: ContentControl

````
namespace System.Windows.Controls
{
    [ContentProperty("Content")]
    [DefaultProperty("Content")]
    [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
    public class ContentControl : Control, IAddChild
    {
        public static readonly DependencyProperty ContentProperty;

        [Bindable(true)]
        public object Content { get; set; }
````


## Elements and attributes

## Namespaces

### xmlns

        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:TestBinding0"
        xmlns:lib="clr-namespace:ItemsControlLib;assembly=ItemsControlLib"

### [mapping namespace XML / namespace .Net: XmlnsDefinitionAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.windows.markup.xmlnsdefinitionattribute?view=windowsdesktop-7.0)

````
    [assembly: XmlnsDefinitionAttribute("http://my.schemas.com/ResourceAssembly", "ResourceAssembly")]
````

- Specifies a mapping on a per-assembly basis between a XAML namespace and a CLR namespace, 
  which is then used for type resolution by a XAML object writer or XAML schema context.

- Apply one or more XmlnsDefinitionAttribute attributes to assemblies in order to identify the types 
  within the assembly for XAML usage.

````
  public XmlnsDefinitionAttribute (string xmlNamespace, string clrNamespace);

  [assembly: XmlnsDefinition(
    xmlNamespace: "http://syncfusion.com/xforms",
    clrNamespace: "Syncfusion.SfNumericTextBox.XForms")]
````

### [The x: prefix](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/xaml-overview?view=netframeworkdesktop-4.8#the-x-prefix)

- the prefix x: was used to map the XAML namespace http://schemas.microsoft.com/winfx/2006/xaml, 
  which is the dedicated XAML namespace that supports XAML language constructs
  xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"

- x:Key: Sets a unique key for each resource in a ResourceDictionary (
- x:Class: Specifies the CLR namespace and class name for the class that provides code-behind for a XAML page.

## Property elements

### exemples : Content

    <Button Content="Click!" />

    <Button>
    Click!
    </Button>

    <Button>
        <Button.Content>Click!</Button.Content>
    </Button>

#### ContentProperty attribute

ex :

    [System.Windows.Markup.ContentProperty("Children")]
    public abstract class TimelineGroup : System.Windows.Media.Animation.Timeline, System.Windows.Markup.IAddChild

## Type Converters

- attribut TypeConverterAttribute appliqu� � une classe ou � une propri�t�
- pr�cise le nom d'une classe d�rivant de TypeConverter invoqu�e pour transformer une string 
  en valeur de la classe ou de la propri�t� marqu�e.

The per-property type converter technique is particularly useful if you choose to use a property type 
from Microsoft .NET Framework or from some other library where you cannot control the 
class definition and cannot apply a �TypeConverter� attribute there.

A XAML processor needs two pieces of information in order to process an attribute value. 
- The first piece of information is the value type of the property that is being set. 
- Any string that defines an attribute value and that is processed in XAML must ultimately 
  be converted or resolved to a value of that type. 
- If the value is a primitive that is understood by the XAML parser (such as a numeric value), 
  a direct conversion of the string is attempted. 
- If the value is an enumeration, the string is used to check for a name match to 
  a named constant in that enumeration. 
- If the value is neither a parser-understood primitive nor an enumeration, 
  then the type in question must be able to provide an instance of the type, 
  or a value, based on a converted string. 
  This is done by indicating a type converter class. 
The type converter is effectively a helper class for providing values of another class, 
both for the XAML scenario and also potentially for code calls in .NET code.

### https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/typeconverters-and-xaml?view=netframeworkdesktop-4.8

### https://learn.microsoft.com/en-us/previous-versions/ayybcxe5(v=vs.140)?redirectedfrom=MSDN

### exemples

	[System.ComponentModel.TypeConverter("System.Windows.NullableBoolConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
	public bool? IsSynchronizedWithCurrentItem { get; set; }

## Markup extensions

### [MarkupExtension class](https://learn.microsoft.com/en-us/uwp/api/windows.ui.xaml.markup.markupextension?view=winrt-22621)

````
namespace System.Windows.Markup
{
    public abstract class MarkupExtension
    {
        protected MarkupExtension();

        public abstract object ProvideValue(IServiceProvider serviceProvider);
    }
}
````

### [IServiceProvider](https://learn.microsoft.com/fr-fr/dotnet/api/system.iserviceprovider?view=net-7.0&viewFallbackFrom=windowsdesktop-7.0)

- m�thode :

    public object? GetService (Type serviceType);

- exemple d'appel dans MarkupExtension.ProvideValue

    IProvideValueTarget provideValueTarget = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;

    // TargetObject, TargetProperty
    DependencyObject dependencyObject = provideValueTarget.TargetObject as DependencyObject;
    DependencyProperty dependencyProperty = provideValueTarget.TargetProperty as DependencyProperty;


### MarkupExtensionReturnType

Une classe impl�mentant une MarkupExtension, d�rivant de MarkupExtension, devrait exposer
un attribut MarkupExtensionReturnType

### XAML

- de la forme "{MarkupExtensionName [Named Parameter]',' [Positional Parameter]}"

- MarkupExtensionName : nom d'une classe d�rivant de MarkupExtension

- Named Parameter : 
    - Name = Value
    - Name correspond � une propri�t�

- Positional Parameter : correspond � un param�tre de constructeur

- A markup extension can be implemented to provide values for properties in an attribute usage, 
  properties in a property element usage, or both.

	<object property="{DynamicResource key}" ... />  
	
	<object>  
		<object.property>  
			<DynamicResource ResourceKey="key" ... />  
		</object.property>  
	</object>  

- exemple : MarkupExtensionLocalization

    [MarkupExtensionReturnType(typeof(string))]
    public class MarkupExtensionLocalization : MarkupExtension
    {
        public MarkupExtensionLocalization(string id)
        {
            this.ID = id;
        }

        [ConstructorArgument("id")]
        public string ID { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            // obtention de l'objet et de la propri�t� cible de l'instance de MarkupExtension
            IProvideValueTarget provideValueTarget = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            DependencyObject dependencyObject = provideValueTarget.TargetObject as DependencyObject;
            DependencyProperty dependencyProperty = provideValueTarget.TargetProperty as DependencyProperty;
            ...

            // public class DependencyObject : DispatcherObject
            // {
            //     public void SetCurrentValue(DependencyProperty dp, object value);
            // ...

        }

        puis : DependencyObject.SetCurrentValue

        public void SetCurrentValue (System.Windows.DependencyProperty dp, object value);

    }

- ConstructorArgumentAttribute

  This attribute specifies that the associated property can be initialized 
  by a constructor parameter and should be ignored for XAML serialization 
  if the constructor is used to construct the instance.

### x: Markup extensions

#### x:Type
#### x:Static
#### x:Null
#### x:Array

### [WPF XAML Extensions](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/wpf-xaml-extensions?view=netframeworkdesktop-4.8)

## Children of Object Elements

### Content Property

#### Exemple

    [ContentProperty("Title")]
    public class Film
    {
        public Film()
        {
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _title;
    }

### Collection items

#### Lists

Example

    <ListBox.Items>
        <ListBoxItem Content="..." />
        <ListBoxItem Content="..." />
    </ListBox.Items>

    <StackPanel>
        <Button>First Button</Button>
        <Button>Second Button</Button>
    </StackPanel>

    <StackPanel>
        <StackPanel.Children>
            <!--<UIElementCollection>-->
            <Button>First Button</Button>
            <Button>Second Button</Button>
            <!--</UIElementCollection>-->
        </StackPanel.Children>
    </StackPanel>

#### Dictionaries

    <ResourceDictionary xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
        <Color x:Key="" ...>
        <Color x:Key="" ...>
    </ResourceDictionary>

## Loading XAML at runtime : XamlReader.Load

    FileStream fs=null;
    Window window = (Window)XamlReader.Load(fs);
    Button ok=(Button)window.FindName("okButton");

## Root window

ex :

    public class RootWindow : Window // non d�finie en XAML
    {
    }

    <local:RootWindow x:Class="WpfApp1.MainWindow" x:ClassModifier="public" ...

## Code generation

### XAML keywords

 // pour la g�n�ration de code

#### x:Class

ex :

    <Window x:Class="WpfApp1.MainWindow"

    public partial class MainWindow : Window

#### x:ClassModifier

#### x:Name

#### x:FieldModifier

#### x:Subclass

#### x:TypeArguments

 ex:

    xmlns:sys="clr-namespace:System;assembly=mscorlib"
    xmlns:scg="clr-namespace:System.Collections.Generic;assembly=mscorlib"

    <scg:List x:TypeArguments="sys:String" ...> 
        instantiates a new List<T> with a String type argument.

    <scg:Dictionary x:TypeArguments="sys:String,sys:String" ...> 
        instantiates a new Dictionary<TKey,TValue> with two String type arguments.

#### x:Key
 
 ex:

     <ResourceDictionary xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
        <Color x:Key="" ...>
        <Color x:Key="" ...>
    </ResourceDictionary>

## Attached Properties

### Exemple

     <DockPanel>
       <TextBox DockPanel.Dock="Top">Enter text</TextBox>
     </DockPanel>

### Exemple

    <Grid >
      <VisualStateManager.VisualStateGroups>
        <VisualStateGroup x:Name="CommonStates">

## [WPF XAML Namescopes](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/wpf-xaml-namescopes?view=netframeworkdesktop-4.8)



