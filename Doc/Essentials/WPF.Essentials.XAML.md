
# XAML

## Elements and attributes

## Namespaces

### xmlns

        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:TestBinding0"
        xmlns:lib="clr-namespace:ItemsControlLib;assembly=ItemsControlLib"

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
            IProvideValueTarget provideValueTarget = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            DependencyObject dependencyObject = provideValueTarget.TargetObject as DependencyObject;
            DependencyProperty dependencyProperty = provideValueTarget.TargetProperty as DependencyProperty;
            ...
        }

    }

- ConstructorArgumentAttribute

  This attribute specifies that the associated property can be initialized 
  by a constructor parameter and should be ignored for XAML serialization 
  if the constructor is used to construct the instance.

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

#### Dictionaries

    <ResourceDictionary xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
        <Color x:Key="" ...>
        <Color x:Key="" ...>
    </ResourceDictionary>

## Loading XAML at runtime

    FileStream fs=null;
    Window window = (Window)XamlReader.Load(fs);
    Button ok=(Button)window.FindName("okButton");

## Code generation

### XAML keywords

 // pour la génération de code
 x:Class
 x:ClassModifier
 x:Name
 x:FieldModifier
 x:Subclass
 x:TypeArguments
 x:Key
 ...

 ### Markup Extensions

  x:Array
  x:Null
  x:Static
  x:Type

## Attached Properties

### Exemple

     <DockPanel>
       <TextBox DockPanel.Dock="Top">Enter text</TextBox>
     </DockPanel>

### Exemple

    <Grid >
      <VisualStateManager.VisualStateGroups>
        <VisualStateGroup x:Name="CommonStates">


