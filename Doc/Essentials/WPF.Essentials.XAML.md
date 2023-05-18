
# XAML & WPF 

https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/xaml-in-wpf?view=netframeworkdesktop-4.8

## Elements and attributes

## Namespaces

### xmlns

        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:TestBinding0"
        xmlns:lib="clr-namespace:ItemsControlLib;assembly=ItemsControlLib"

### The x: prefix

https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/xaml-overview?view=netframeworkdesktop-4.8#the-x-prefix

- x:Key: Sets a unique key for each resource in a ResourceDictionary (
- x:Class: Specifies the CLR namespace and class name for the class that provides code-behind for a XAML page.


### mapping namespace XML / namespace .Net: XmlnsDefinitionAttribute

    [assembly: XmlnsDefinitionAttribute("http://my.schemas.com/ResourceAssembly", "ResourceAssembly")]

## Property elements

### exemples : Content

    <Button Content="Click!" />

    <Button>
    Click!
    </Button>

    <Button>
        <Button.Content>Click!</Button.Content>
    </Button>

## Type Converters

- attribut TypeConverterAttribute appliqué à une classe ou à une propriété
- précise le nom d'une classe dérivant de TypeConverter invoquée pour transformer une string 
  en valeur de la classe ou de la propriété marquée.

The per-property type converter technique is particularly useful if you choose to use a property type 
from Microsoft .NET Framework or from some other library where you cannot control the 
class definition and cannot apply a “TypeConverter” attribute there.

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

- de la forme "{MarkupExtensionName [Named Parameter]',' [Positional Parameter]}"
- MarkupExtensionName : nom d'une classe dérivant de MarkupExtension
- Named Parameter : 
    - Name = Value
    - Name correspond à une propriété
- Positional Parameter : correspond à un paramètre de constructeur

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
            // obtention de l'objet et de la propriété cible de l'instance de MarkupExtension
            IProvideValueTarget provideValueTarget = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            DependencyObject dependencyObject = provideValueTarget.TargetObject as DependencyObject;
            DependencyProperty dependencyProperty = provideValueTarget.TargetProperty as DependencyProperty;
            ...

            // public class DependencyObject : DispatcherObject
            // {
            //     public void SetCurrentValue(DependencyProperty dp, object value);
            // ...

        }

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

## Code generation

### XAML keywords

 // pour la génération de code

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


