
# [ItemsControl](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.itemscontrol?view=windowsdesktop-6.0)


## Class
 
    [System.Windows.Localizability(System.Windows.LocalizationCategory.None, Readability=System.Windows.Readability.Unreadable)]
    [System.Windows.Markup.ContentProperty("Items")]
    [System.Windows.StyleTypedProperty(Property="ItemContainerStyle", StyleTargetType=typeof(System.Windows.FrameworkElement))]
	public class ItemsControl : System.Windows.Controls.Control, 
                                System.Windows.Controls.Primitives.IContainItemStorage, 
                                System.Windows.Markup.IAddChild
    {


### Inheritance

    Object
    DispatcherObject
    DependencyObject
    Visual
    UIElement
    FrameworkElement
    Control
    ItemsControl

### Derived

    System.Windows.Controls.HeaderedItemsControl
    System.Windows.Controls.Primitives.DataGridCellsPresenter
    System.Windows.Controls.Primitives.DataGridColumnHeadersPresenter
    System.Windows.Controls.Primitives.MenuBase
    System.Windows.Controls.Primitives.Selector
    System.Windows.Controls.Primitives.StatusBar
    System.Windows.Controls.Ribbon.RibbonContextualTabGroupItemsControl
    System.Windows.Controls.Ribbon.RibbonControlGroup
    System.Windows.Controls.Ribbon.RibbonGallery
    System.Windows.Controls.Ribbon.RibbonQuickAccessToolBar
    System.Windows.Controls.Ribbon.RibbonTabHeaderItemsControl
    System.Windows.Controls.TreeView 

### Notes

 - ItemsControl derives from Control
 - its .Content property is 'Items' 
 - the TargetType of the Style assigned to .ItemContainerStyle should be 'FrameworkElement'

### Default template : cf [WPF source code](https://github.com/dotnet/wpf)

    <Style x:Key="{x:Type ItemsControl}"
           TargetType="{x:Type ItemsControl}">
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

### Fields

    public static readonly System.Windows.DependencyProperty ItemsSourceProperty;
    ...

### Properties

Elaboration du VisualTree d'un ItemsControl

    public System.Windows.Media.VisualCollection Children { get; }
 
  associ� � un ItemsControl d�pend de ses propri�t�s:

    public System.Windows.Controls.ControlTemplate Template { get; set; }

    public System.Windows.Controls.ItemCollection Items { get; }

    public System.Collections.IEnumerable ItemsSource { get; set; }

    public System.Windows.DataTemplate ItemTemplate { get; set; }

    public System.Windows.Style ItemContainerStyle { get; set; }

    public System.Windows.Controls.ItemsPanelTemplate ItemsPanel { get; set; }

    public System.Windows.Controls.StyleSelector ItemContainerStyleSelector { get; set; }

    public System.Windows.Controls.DataTemplateSelector ItemTemplateSelector { get; set; }

  son �laboration s'effectue, peut-�tre, comme ceci:

    - r�initialisation du VisualTree .Children
    - production du VisualTree � partir de .Template 
    - inspection de .Template � la recherche d'un ItemsPresenter
    - .ItemsPanel d�termine le Panel utilis� pour acceuillir les ItemUIs de chaque Item trouv� dans .ItemsSource / .Items
    - pour chaque Item de .ItemsSource / .Items
    - production d'un ItemUI, � partir de la classe pr�cis�e par l'attribut de classe StyleTypedProperty,
      ContentPresenter par d�faut, LitBoxItem pour une ListBox ...
    - assignation de ItemUI.DataContext = Item
    - d�termination et application du Style de cet ItemUI :
        - valeur de .ItemContainerStyle ou valeur retourn�e par .ItemContainerStyleSelector
          pour l'Item courant
    - d�termination du Template de cet ItemUI : valeur par d�faut, retourn�e par .ItemTemplateSelector,
      assign�e par son Style ...
    - le template de cet ItemUI contient sans doute un ContentPresenter: 
      d�termination de son VisualTree: Content + DataTemplate
        - le Content � prendre en compte est l'Item courant
        - d�termination du DataTemplate � prendre en compte :
        - .ItemTemplate
        - si null, le ContentPresenter applique ses r�gles : recherche dans les ressources
          du VisualTree d'un DataTemplate associ� au type de l'Item ...
    - insertion de l'ItemUI dans le Panel du ItemsPresenter

Exemple : type de l'Item UI pour une ListBox : ListBoxItem

    [System.Windows.Localizability(System.Windows.LocalizationCategory.ListBox)]
    [System.Windows.StyleTypedProperty(Property="ItemContainerStyle", StyleTargetType=typeof(System.Windows.Controls.ListBoxItem))]
    public class ListBox : System.Windows.Controls.Primitives.Selector

## [ItemCollection](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.itemcollection?view=windowsdesktop-6.0)

## [ContentPropertyAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.windows.markup.contentpropertyattribute?view=windowsdesktop-6.0)

Indicates which property of a type is the XAML content property. 
A XAML processor uses this information when processing XAML child elements 
of XAML representations of the attributed type.

## [StyleSelector](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.styleselector?view=windowsdesktop-6.0)

    public virtual System.Windows.Style SelectStyle (object item, System.Windows.DependencyObject container);

## [DataTemplateSelector](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.datatemplateselector?view=windowsdesktop-6.0)

    public virtual System.Windows.DataTemplate SelectTemplate (object item, System.Windows.DependencyObject container);

## [ItemsPresenter](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.itemspresenter?view=windowsdesktop-6.0)

> Used __within the template of an item control__ to specify the place in the control's visual tree where 
the __ItemsPanel__ defined by the ItemsControl is to be added.

## .ItemsPanel

### Exemple: __ItemsPanel__

    <ItemsControl x:Name="ItemsControl1" 
              ItemsSource="{Binding}"
        Margin="4" Background="DarkBlue">
        
        <ItemsControl.ItemsPanel>
            <ItemsPanelTemplate>
                <UniformGrid Columns="2" />
            </ItemsPanelTemplate>
        </ItemsControl.ItemsPanel>

In this case, the ItemsPanelTemplate .ItemsPanel property indicates to the ItemPresenter present in the
ItemsControl ControlTemplate (see Default Template above) that it should use a UniformGrid to contain
the UI Items produced from the .ItemsSource items using the .ItemTemplate DataTemplate.

## .ItemContainerStyle

The __.ItemContainerStyle__ Style is applied to each UI Item produced from the .ItemsSource content using the .ItemTemplate DataTemplate
so as to assign them Triggers among others.

Dans le cas de __ItemsControl__, le TargetType du Style .ItemContainerStyle doit �tre ContentPresenter.

Dans le cas de __ListBox__, le TargetType du Style .ItemContainerStyle doit �tre [ListBoxItem](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.listboxitem?view=windowsdesktop-6.0):
le Style peut assigner un ControlTemplate aux ItemContainer de ce type ListBoxItem ...  
Pour un Item donn�, l'UI produit par l'__ItemTemplate__ appara�t dans le __ContentPresenter__ 
du __ControlTemplate__ (assign� via le Style __.ItemContainerStyle__ de la ListBox) de son ListBoxItem h�te. 

### Exemple : ItemsControl

    <ItemsControl ItemsSource="{Binding}">
        
        <ItemsControl.ItemTemplate>
        <DataTemplate>
            <Grid Background="Red" TextBlock.Foreground="White">
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="50*" />
                <ColumnDefinition Width="50*" />
            </Grid.ColumnDefinitions>
            <TextBlock Grid.Column="0" Text="{Binding Title}" Margin="4"/>
            <ProgressBar Grid.Column="1"  Minimum="0" Maximum="100" Value="{Binding Completion}" Margin="4"/>
            </Grid>
        </DataTemplate>
        </ItemsControl.ItemTemplate>

        <ItemsControl.ItemContainerStyle>
        <Style TargetType="ContentPresenter">
            <Setter Property="Margin" Value="2" />
        </Style>
        </ItemsControl.ItemContainerStyle>
        
    </ItemsControl>

### Exemple : ListBox

    <ListBox.ItemContainerStyle>
        
        <Style TargetType="ListBoxItem">
            
            <Setter Property="Opacity" Value="0.5" />
            <Setter Property="MaxHeight" Value="75" />

            <Setter Property="Template">
                <Setter.Value>
                    <ControlTemplate TargetType="ListBoxItem">
                        <Border BorderThickness="4" BorderBrush="Yellow">
                            <ContentPresenter/>
                        </Border>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
            
            <Style.Triggers>
                <Trigger Property="IsSelected" Value="True">
                    <Trigger.Setters>
                        <Setter Property="Opacity" Value="1.0" />
                    </Trigger.Setters>
                </Trigger>
            </Style.Triggers>
            
        </Style>

    </ListBox.ItemContainerStyle>



    
