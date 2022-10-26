
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
    </Button>

    <Button>
        <Button.Content>Click</Button.Content>
    </Button>

## Type Converters

## Markup extensions

## Children of Object Eelements

### Content

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
