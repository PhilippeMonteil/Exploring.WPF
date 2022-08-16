
# Content

## [ContentControl](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.contentcontrol?view=windowsdesktop-6.0)

    [System.Windows.Localizability(System.Windows.LocalizationCategory.None, Readability=System.Windows.Readability.Unreadable)]
    [System.Windows.Markup.ContentProperty("Content")]
    public class ContentControl : System.Windows.Controls.Control, System.Windows.Markup.IAddChild

### Inheritance

    Object
    DispatcherObject
    DependencyObject
    Visual
    UIElement
    FrameworkElement
    Control
    ContentControl

### Derived

    System.Activities.Presentation.View.ExpressionTextBox
    System.Activities.Presentation.View.TypePresenter
    System.Activities.Presentation.WorkflowElementDialog
    System.Activities.Presentation.WorkflowItemPresenter
    System.Activities.Presentation.WorkflowItemsPresenter
    System.Activities.Presentation.WorkflowViewElement
    System.Windows.Controls.DataGridCell
    System.Windows.Controls.Frame
    System.Windows.Controls.GroupItem
    System.Windows.Controls.HeaderedContentControl
    System.Windows.Controls.Label
    System.Windows.Controls.ListBoxItem
    System.Windows.Controls.Primitives.ButtonBase
    System.Windows.Controls.Primitives.StatusBarItem
    System.Windows.Controls.Ribbon.RibbonControl
    System.Windows.Controls.Ribbon.RibbonGalleryItem
    System.Windows.Controls.Ribbon.RibbonTabHeader
    System.Windows.Controls.ScrollViewer
    System.Windows.Controls.ToolTip
    System.Windows.Controls.UserControl
    System.Windows.Window 

### Properties

    // Gets or sets the content of a ContentControl. 
    public object Content { get; set; }

    // Gets or sets the data template used to display the content of the ContentControl.
    // A DataTemplate that defines the visualization of the content. The default is null.
    public System.Windows.DataTemplate ContentTemplate { get; set; }

    [System.ComponentModel.Bindable(true)]
    public System.Windows.Controls.DataTemplateSelector ContentTemplateSelector { get; set; }

## [DataTemplateSelector](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.datatemplateselector?view=windowsdesktop-6.0)

    public virtual System.Windows.DataTemplate SelectTemplate (object item, System.Windows.DependencyObject container);

## [ContentPresenter](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.contentpresenter?view=windowsdesktop-6.0)

    [System.Windows.Localizability(System.Windows.LocalizationCategory.None, Readability=System.Windows.Readability.Unreadable)]
    public class ContentPresenter : System.Windows.FrameworkElement

Inheritance:

    Object
    DispatcherObject
    DependencyObject
    Visual
    UIElement
    FrameworkElement
    ContentPresenter

Derived:

    System.Windows.Controls.Primitives.DataGridDetailsPresenter
    System.Windows.Controls.Ribbon.RibbonContentPresenter
    System.Windows.Controls.ScrollContentPresenter

Properties:

    // Gets or sets the data used to generate the child elements of a ContentPresenter. 
    public object Content { get; set; }

    // Gets or sets the base name to use during automatic aliasing.
    // The base name to use during automatic aliasing. The default is "Content".
    public string ContentSource { get; set; }

    // Gets or sets the template used to display the content of the control.
    // A DataTemplate that defines the visualization of the content. The default is null.
    public System.Windows.DataTemplate ContentTemplate { get; set; }

Notes:

You typically use the __ContentPresenter__ in the __ControlTemplate__ of a __ContentControl__ to specify where the content is to be added. Every ContentControl type has a ContentPresenter in its default ControlTemplate.

When a ContentPresenter object is in a ControlTemplate of a ContentControl, the Content, ContentTemplate, and ContentTemplateSelector properties get their values from the properties of the same names of the ContentControl. You can have the ContentPresenter property get the values of these properties from other properties of the templated parent by setting the ContentSource property or binding to them.

The ContentPresenter uses the following logic to display the Content:

- If the ContentTemplate property on the ContentPresenter is set, the ContentPresenter applies that DataTemplate to the Content property and the resulting UIElement and its child elements, if any, are displayed. For more information about DataTemplate objects, see Data Templating Overview.

- If the ContentTemplateSelector property on the ContentPresenter is set, the ContentPresenter applies the appropriate DataTemplate to the Content property and the resulting UIElement and its child elements, if any, are displayed.

- If there is a DataTemplate associated with the type of Content, the ContentPresenter applies that DataTemplate to the Content property and the resulting UIElement and its child elements, if any, are displayed.

- If Content is a UIElement object, the UIElement is displayed. If the UIElement already has a parent, an exception occurs.

- If there is a TypeConverter that converts the type of Content to a UIElement, the ContentPresenter uses that TypeConverter and the resulting UIElement is displayed.

- If there is a TypeConverter that converts the type of Content to a string, the ContentPresenter uses that TypeConverter and creates a TextBlock to contain that string. The TextBlock is displayed.

- If the content is an XmlElement, the value of the InnerText property is displayed in a TextBlock.

- The ContentPresenter calls the ToString method on the Content and creates a TextBlock to contain the string returned by ToString. The TextBlock is displayed.

# [HeaderedContentControl](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.headeredcontentcontrol?view=windowsdesktop-6.0)

## Class

	[System.Windows.Localizability(System.Windows.LocalizationCategory.Text)]
	public class HeaderedContentControl : System.Windows.Controls.ContentControl

## Inheritance

Object
DispatcherObject
DependencyObject
Visual
UIElement
FrameworkElement
Control
ContentControl
HeaderedContentControl

## Derived

System.Windows.Controls.Expander
System.Windows.Controls.GroupBox
System.Windows.Controls.TabItem

## Properties

	[System.ComponentModel.Bindable(true)]
	[System.Windows.Localizability(System.Windows.LocalizationCategory.Label)]
	public object Header { get; set; }

	[System.ComponentModel.Bindable(true)]
	public System.Windows.DataTemplate HeaderTemplate { get; set; }

	[System.ComponentModel.Bindable(true)]
	public System.Windows.Controls.DataTemplateSelector HeaderTemplateSelector { get; set; }

