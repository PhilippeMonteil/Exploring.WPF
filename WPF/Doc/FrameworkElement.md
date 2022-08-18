
# [FrameworkElement](https://docs.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement?view=windowsdesktop-6.0)

## Class

	[System.Windows.Markup.RuntimeNameProperty("Name")]
	[System.Windows.Markup.UsableDuringInitialization(true)]
	[System.Windows.Markup.XmlLangProperty("Language")]
	[System.Windows.StyleTypedProperty(Property="FocusVisualStyle", StyleTargetType=typeof(System.Windows.Controls.Control))]
	public class FrameworkElement : System.Windows.UIElement, 
					System.ComponentModel.ISupportInitialize, 
					System.Windows.IFrameworkInputElement, 
					System.Windows.Markup.IQueryAmbient

## Inheritance

- Object
- DispatcherObject
- DependencyObject
- Visual
- UIElement
- FrameworkElement

## Derived

- Microsoft.Windows.Themes.BulletChrome
- Microsoft.Windows.Themes.ScrollChrome
- System.Windows.Controls.AccessText
- System.Windows.Controls.AdornedElementPlaceholder
- System.Windows.Controls.ContentPresenter
- System.Windows.Controls.Control
- System.Windows.Controls.Decorator
- System.Windows.Controls.Image
- System.Windows.Controls.InkCanvas
- System.Windows.Controls.ItemsPresenter
- System.Windows.Controls.MediaElement
- System.Windows.Controls.Page
- System.Windows.Controls.Panel
- System.Windows.Controls.Primitives.DocumentPageView
- System.Windows.Controls.Primitives.GridViewRowPresenterBase
- System.Windows.Controls.Primitives.Popup
- System.Windows.Controls.Primitives.TickBar
- System.Windows.Controls.Primitives.Track
- System.Windows.Controls.TextBlock
- System.Windows.Controls.ToolBarTray
- System.Windows.Controls.Viewport3D
- System.Windows.Documents.Adorner
- System.Windows.Documents.AdornerLayer
- System.Windows.Documents.DocumentReference
- System.Windows.Documents.FixedPage
- System.Windows.Documents.Glyphs
- System.Windows.Documents.PageContent
- System.Windows.Interop.HwndHost
- System.Windows.Shapes.Shape 

## Properties

	[System.Windows.Localizability(System.Windows.LocalizationCategory.NeverLocalize)]
	public System.Windows.Data.BindingGroup BindingGroup { get; set; }

	public System.Windows.Controls.ContextMenu ContextMenu { get; set; }

	[System.Windows.Localizability(System.Windows.NeverLocalize)]
	public object DataContext { get; set; }

	protected internal object DefaultStyleKey { get; set; }

	public System.Windows.Style FocusVisualStyle { get; set; }

	public System.Windows.Input.InputScope InputScope { get; set; }

	[System.Windows.Markup.Ambient]
	public System.Windows.ResourceDictionary Resources { get; set; }

	public System.Windows.Style Style { get; set; }

	[System.Windows.Localizability(System.Windows.LocalizationCategory.NeverLocalize)]
	public object Tag { get; set; }

	public System.Windows.DependencyObject TemplatedParent { get; }

	[System.ComponentModel.Bindable(true)]
	[System.Windows.Localizability(System.Windows.LocalizationCategory.ToolTip)]
	public object ToolTip { get; set; }

	public System.Windows.TriggerCollection Triggers { get; }

## Methods

	public System.Windows.Data.BindingExpression GetBindingExpression (System.Windows.DependencyProperty dp);

	protected internal System.Windows.DependencyObject GetTemplateChild (string childName);

	public virtual void OnApplyTemplate ();

	protected internal virtual void OnStyleChanged (System.Windows.Style oldStyle, 
						System.Windows.Style newStyle);

	public System.Windows.Data.BindingExpression SetBinding (System.Windows.DependencyProperty dp, 
						string path);

	public void SetResourceReference (System.Windows.DependencyProperty dp, object name);

	public object TryFindResource (object resourceKey);

## Events

	public event System.Windows.DependencyPropertyChangedEventHandler DataContextChanged;

	public event EventHandler<System.Windows.Data.DataTransferEventArgs> SourceUpdated;
	public event EventHandler<System.Windows.Data.DataTransferEventArgs> TargetUpdated;

