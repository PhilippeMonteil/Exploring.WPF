# [ItemsControl](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.itemscontrol?view=windowsdesktop-6.0)

## En résumé

- class System.Windows.Controls.ItemsControl
- Inheritance : Object / DispatcherObject / DependencyObject / Visual / UIElement / FrameworkElement / Control / ItemsControl
- Derived : HeaderedItemsControl, Primitives.MenuBase, Primitives.Selector, TreeView ...
- Properties :
    - IEnumerable ItemsSource { get; set; }
    - Controls.ItemCollection Items { get; } (ItemCollection Class : CollectionView)
    - string DisplayMemberPath
    - string ItemStringFormat
    - DataTemplate ItemTemplate
    - DataTemplateSelector ItemTemplateSelector
    - ItemsPanelTemplate ItemsPanel
    - Style ItemContainerStyle
    - StyleSelector ItemContainerStyleSelector
    - ItemContainerGenerator ItemContainerGenerator

- class System.Windows.Data.CollectionView : DispatcherObject, IEnumerable, INotifyPropertyChanged, INotifyCollectionChanged, ICollectionView

## Class
 
    [System.Windows.Localizability(System.Windows.LocalizationCategory.None, Readability=System.Windows.Readability.Unreadable)]
    [System.Windows.Markup.ContentProperty("Items")]
    [System.Windows.StyleTypedProperty(Property="ItemContainerStyle", StyleTargetType=typeof(System.Windows.FrameworkElement))]
	public class ItemsControl : System.Windows.Controls.Control, 
                                System.Windows.Controls.Primitives.IContainItemStorage, 
                                System.Windows.Markup.IAddChild
    {

### Inheritance

Object / DispatcherObject / DependencyObject / Visual / UIElement / FrameworkElement / Control / ItemsControl

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

### Fields

    public static readonly System.Windows.DependencyProperty ItemsSourceProperty;
    ...

### Properties

    [System.ComponentModel.Bindable(true)]
    public System.Collections.IEnumerable ItemsSource { get; set; }

    [System.ComponentModel.Bindable(true)]
    public System.Windows.Controls.ItemCollection Items { get; }

    [System.ComponentModel.Bindable(true)]
    public string DisplayMemberPath { get; set; }

    [System.ComponentModel.Bindable(true)]
    public string ItemStringFormat { get; set; }

    [System.ComponentModel.Bindable(true)]
    public System.Windows.DataTemplate ItemTemplate { get; set; }

    [System.ComponentModel.Bindable(true)]
    public System.Windows.Controls.DataTemplateSelector ItemTemplateSelector { get; set; }

    [System.ComponentModel.Bindable(false)]
    public System.Windows.Controls.ItemsPanelTemplate ItemsPanel_ { get; set; }

    [System.ComponentModel.Bindable(true)]
    public System.Windows.Style ItemContainerStyle { get; set; }

    [System.ComponentModel.Bindable(true)]
    public System.Windows.Controls.StyleSelector ItemContainerStyleSelector { get; set; }

    [System.ComponentModel.Bindable(false)]
    [System.ComponentModel.Browsable(false)]
    public System.Windows.Controls.ItemContainerGenerator ItemContainerGenerator { get; }
