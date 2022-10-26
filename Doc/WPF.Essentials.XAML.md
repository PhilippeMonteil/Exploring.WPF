
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

- attribut TypeConverterAttribute appliqu� � une classe ou � une propri�t�
- pr�cise le nom d'une classe d�rivant de TypeConverter invoqu�e pour transformer une string 
  en valeur de la classe ou de la propri�t� marqu�e.

### exemples

	[System.ComponentModel.TypeConverter("System.Windows.NullableBoolConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
	public bool? IsSynchronizedWithCurrentItem { get; set; }

## Markup extensions

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

 // pour la g�n�ration de code
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

