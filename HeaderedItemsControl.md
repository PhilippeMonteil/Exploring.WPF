
# [HeaderedItemsControl](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.headereditemscontrol?view=windowsdesktop-6.0)

## Class

	[System.Windows.Localizability(System.Windows.LocalizationCategory.Menu)]
	public class HeaderedItemsControl : System.Windows.Controls.ItemsControl

### Inheritance

     Object
     DispatcherObject
     DependencyObject
     Visual
     UIElement
     FrameworkElement
     Control
     ItemsControl
     HeaderedItemsControl

### Derived

    System.Windows.Controls.MenuItem
    System.Windows.Controls.Ribbon.RibbonGalleryCategory
    System.Windows.Controls.Ribbon.RibbonGroup
    System.Windows.Controls.Ribbon.RibbonTab
    System.Windows.Controls.ToolBar
    System.Windows.Controls.TreeViewItem 

### Properties

    [System.ComponentModel.Bindable(true)]
    public object Header { get; set; }

    [System.ComponentModel.Bindable(true)]
    public System.Windows.DataTemplate HeaderTemplate { get; set; }

    [System.ComponentModel.Bindable(true)]
    public System.Windows.Controls.DataTemplateSelector HeaderTemplateSelector { get; set; }

### Elaboration du VisualTree d'un HeaderedItemsControl

Ce processus est similaire à celui exécuté par un ItemsControl à ceci près:

- le .Template associé à un HeaderedItemsControl contient probablement:
    - un ItemsPresenter, comme dans le cas d'un ItemsControl, produisant un Panel,
      défini par .ItemsPanel puis les ItemUIs pour chaque Item contenu dans .ItemsSource / .Items
    - un ContentPresenter affichant le Header du contrôle à partir de .Header / .HeaderTemplate
      / .HeaderTemplateSelector

