
# [ItemsControl](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.itemscontrol?view=windowsdesktop-6.0)

## En résumé

- classe :

    [System.Windows.Localizability(System.Windows.LocalizationCategory.None, Readability=System.Windows.Readability.Unreadable)]
    [System.Windows.Markup.ContentProperty("Items")]
    [System.Windows.StyleTypedProperty(Property="ItemContainerStyle", StyleTargetType=typeof(System.Windows.FrameworkElement))]
	public class ItemsControl : System.Windows.Controls.Control, 
                                System.Windows.Controls.Primitives.IContainItemStorage, 
                                System.Windows.Markup.IAddChild

- class System.Windows.Controls.ItemsControl
- 
- Inheritance : 
    Object / DispatcherObject / DependencyObject / Visual / UIElement / FrameworkElement 
    / Control / ItemsControl

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

- class Controls.ItemCollection Class : CollectionView

- class System.Windows.Data.CollectionView : DispatcherObject, 
            IEnumerable, 
            INotifyPropertyChanged, 
            INotifyCollectionChanged, 
            ICollectionView

# [HeaderedItemsControl](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.headereditemscontrol?view=windowsdesktop-7.0)

## En résumé

- class System.Windows.Controls.HeaderedItemsControl : ItemsControl

- Derived : MenuItem, ToolBar, TreeViewItem, ...

- Properties :
    - bool HasHeader { get; }
    - object Header { get; set; }
    - string HeaderStringFormat { get; set; }
    - DataTemplate HeaderTemplate { get; set; }
    - DataTemplateSelector HeaderTemplateSelector { get; set; }
    - IEnumerator LogicalChildren { get; }

## [Exemples](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.headereditemscontrol?view=windowsdesktop-7.0#examples)

