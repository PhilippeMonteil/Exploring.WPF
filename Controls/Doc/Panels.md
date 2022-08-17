
# [Panel](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.panel?view=windowsdesktop-6.0)

## [Panels Overview](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/controls/panels-overview?view=netframeworkdesktop-4.8)

## Class

	[System.Windows.Localizability(System.Windows.LocalizationCategory.Ignore)]
	[System.Windows.Markup.ContentProperty("Children")]
	public abstract class Panel : System.Windows.FrameworkElement, 
									System.Windows.Markup.IAddChild

## Inheritance

Object
DispatcherObject
DependencyObject
Visual
UIElement
FrameworkElement
Panel

## Derived

System.Windows.Controls.Canvas
System.Windows.Controls.DockPanel
System.Windows.Controls.Grid
System.Windows.Controls.Primitives.TabPanel
System.Windows.Controls.Primitives.ToolBarOverflowPanel
System.Windows.Controls.Primitives.UniformGrid
System.Windows.Controls.Ribbon.Primitives.RibbonContextualTabGroupsPanel
System.Windows.Controls.Ribbon.Primitives.RibbonGalleryCategoriesPanel
System.Windows.Controls.Ribbon.Primitives.RibbonGalleryItemsPanel
System.Windows.Controls.Ribbon.Primitives.RibbonGroupItemsPanel
System.Windows.Controls.Ribbon.Primitives.RibbonQuickAccessToolBarOverflowPanel
System.Windows.Controls.Ribbon.Primitives.RibbonTabHeadersPanel
System.Windows.Controls.Ribbon.Primitives.RibbonTabsPanel
System.Windows.Controls.Ribbon.Primitives.RibbonTitlePanel
System.Windows.Controls.StackPanel
System.Windows.Controls.VirtualizingPanel
System.Windows.Controls.WrapPanel

## Implements

IAddChild

## Methods

	protected override void OnRender (System.Windows.Media.DrawingContext dc);

### [How to: Override the Panel OnRender Method](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/controls/how-to-override-the-panel-onrender-method?view=netframeworkdesktop-4.8)

# [VirtualizingPanel](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.virtualizingpanel?view=windowsdesktop-6.0)


## Class

	public abstract class VirtualizingPanel : System.Windows.Controls.Panel

## Inheritance

Object
DispatcherObject
DependencyObject
Visual
UIElement
FrameworkElement
Panel
VirtualizingPanel

## Derived

System.Windows.Controls.DataGridCellsPanel
System.Windows.Controls.Ribbon.Primitives.RibbonQuickAccessToolBarPanel
System.Windows.Controls.VirtualizingStackPanel


# [VirtualizingStackPanel](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.virtualizingstackpanel?view=windowsdesktop-6.0)

## [Overview](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/controls/panels-overview?view=netframeworkdesktop-4.8#virtualizingstackpanel)

## Class

	public class VirtualizingStackPanel : System.Windows.Controls.VirtualizingPanel, 
										System.Windows.Controls.Primitives.IScrollInfo

## Inheritance

Object
DispatcherObject
DependencyObject
Visual
UIElement
FrameworkElement
Panel
VirtualizingPanel
VirtualizingStackPanel

## Derived

System.Windows.Controls.Primitives.DataGridRowsPresenter
System.Windows.Controls.Ribbon.Primitives.RibbonMenuItemsPanel

## Implements

IScrollInfo
