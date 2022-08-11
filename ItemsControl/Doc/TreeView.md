
# [TreeView](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.treeview?view=windowsdesktop-6.0)

	[System.Windows.StyleTypedProperty(Property="ItemContainerStyle", StyleTargetType=typeof(System.Windows.Controls.TreeViewItem))]
	public class TreeView : System.Windows.Controls.ItemsControl

Inheritance:

	Object
	DispatcherObject
	DependencyObject
	Visual
	UIElement
	FrameworkElement
	Control
	ItemsControl
	TreeView

TreeView dérive de ItemsControl.
Les items qu'il liste sont de type TreeViewItem.

# [TreeViewItem](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.treeviewitem?view=windowsdesktop-6.0)

	[System.Windows.StyleTypedProperty(Property="ItemContainerStyle", StyleTargetType=typeof(System.Windows.Controls.TreeViewItem))]
	[System.Windows.TemplatePart(Name="PART_Header", Type=typeof(System.Windows.FrameworkElement))]
	[System.Windows.TemplatePart(Name="ItemsHost", Type=typeof(System.Windows.Controls.ItemsPresenter))]
	public class TreeViewItem : System.Windows.Controls.HeaderedItemsControl, 
		System.Windows.Controls.Primitives.IHierarchicalVirtualizationAndScrollInfo

Inheritance: 

	Object
	DispatcherObject
	DependencyObject
	Visual
	UIElement
	FrameworkElement
	Control
	ItemsControl
	HeaderedItemsControl
	TreeViewItem

TreeViewItem dérive de ItemsControl/HeaderedItemsControl.
Les items qu'il liste sont de type TreeViewItem.

en résumé :

	TreeView : ItemsControl
		TreeViewItem[] Items

	TreeViewItem : HeaderedItemsControl
		TreeViewItem[] Items

# [HeaderedItemsControl](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.headereditemscontrol?view=windowsdesktop-6.0)

		[System.Windows.Localizability(System.Windows.LocalizationCategory.Menu)]
		public class HeaderedItemsControl : System.Windows.Controls.ItemsControl

Inheritance:

	Object
	DispatcherObject
	DependencyObject
	Visual
	UIElement
	FrameworkElement
	Control
	ItemsControl
	HeaderedItemsControl

Derived:

	System.Windows.Controls.MenuItem
	System.Windows.Controls.Ribbon.RibbonGalleryCategory
	System.Windows.Controls.Ribbon.RibbonGroup
	System.Windows.Controls.Ribbon.RibbonTab
	System.Windows.Controls.ToolBar
	__System.Windows.Controls.TreeViewItem__

Properties:

	[System.ComponentModel.Bindable(false)]
	[System.ComponentModel.Browsable(false)]
	public bool HasHeader { get; }

	[System.ComponentModel.Bindable(true)]
	public object Header { get; set; }

	// Gets or sets a composite string that specifies how to format the Header property if it is displayed as a string.
	[System.ComponentModel.Bindable(true)]
	public string HeaderStringFormat { get; set; }

	[System.ComponentModel.Bindable(true)]
	public System.Windows.DataTemplate HeaderTemplate { get; set; }

	[System.ComponentModel.Bindable(true)]
	public System.Windows.Controls.DataTemplateSelector HeaderTemplateSelector { get; set; }

# [HierarchicalDataTemplate](https://docs.microsoft.com/en-us/dotnet/api/system.windows.hierarchicaldatatemplate?view=windowsdesktop-6.0)

Represents a DataTemplate that supports HeaderedItemsControl, such as TreeViewItem or MenuItem.

	// Represents a DataTemplate that supports HeaderedItemsControl, such as TreeViewItem or MenuItem.
	public class HierarchicalDataTemplate : System.Windows.DataTemplate

Inheritance:

	Object
	DispatcherObject
	FrameworkTemplate
	DataTemplate
	HierarchicalDataTemplate

## Exemple

	<Window x:Class="TestTreeView0.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:TestTreeView0"
        xmlns:lib="clr-namespace:ItemsControlLib;assembly=ItemsControlLib"
        mc:Ignorable="d"
        Title="MainWindow 1.001" Height="450" Width="800"
        d:DataContext="{d:DesignInstance IsDesignTimeCreatable=True, Type=lib:ClassATest}">

    <Window.Resources>

        <HierarchicalDataTemplate DataType="{x:Type lib:ClassA}" ItemsSource="{Binding}">
            <TextBlock Text="{Binding Name}"/>
        </HierarchicalDataTemplate>

        <HierarchicalDataTemplate DataType="{x:Type lib:ClassB}" ItemsSource="{Binding}">
            <TextBlock Text="{Binding Name}"/>
        </HierarchicalDataTemplate>

        <DataTemplate DataType="{x:Type lib:ClassC}">
            <TextBlock Text="{Binding Name}"/>
        </DataTemplate>

    </Window.Resources>

    <Window.DataContext>
        <lib:ClassATest/>
    </Window.DataContext>
    
    <Grid Background="DarkGray">

        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="0.5*"/>
            <ColumnDefinition Width="0.5*"/>
        </Grid.ColumnDefinitions>

        <TreeView Grid.Column="0" ItemsSource="{Binding}" >
        </TreeView>

    </Grid>
    
	</Window>
