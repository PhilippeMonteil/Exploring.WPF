
# [UIElement](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement?view=windowsdesktop-6.0)

## Class

	[System.Windows.Markup.UidProperty("Uid")]
	public class UIElement : System.Windows.Media.Visual, 
							System.Windows.IInputElement, 
							System.Windows.Media.Animation.IAnimatable


## Inheritance

Object
DispatcherObject
DependencyObject
Visual
UIElement

## Derived

System.Windows.FrameworkElement

## Properties

	public System.Windows.Input.CommandBindingCollection CommandBindings { get; }
	public System.Windows.Input.InputBindingCollection InputBindings { get; }

	public string Uid { get; set; }

## Methods

	protected virtual void OnRender (System.Windows.Media.DrawingContext drawingContext);

	// On/OnPreview ... Key, Focus, Mouse, Touch, Drag
	protected virtual void OnKeyDown (System.Windows.Input.KeyEventArgs e);
	protected virtual void OnMouseDown (System.Windows.Input.MouseButtonEventArgs e);

	// Handler
	public void AddHandler (System.Windows.RoutedEvent routedEvent, Delegate handler);
	public void RemoveHandler (System.Windows.RoutedEvent routedEvent, Delegate handler);

	public void AddToEventRoute (System.Windows.EventRoute route, System.Windows.RoutedEventArgs e);


	// Focus
	public bool Focus ();
	protected virtual void OnGotFocus (System.Windows.RoutedEventArgs e);

## Events

	// On/Preview Key/Mouse/Stylus/Touch
	public event System.Windows.Input.KeyEventHandler PreviewKeyDown;
	public event System.Windows.Input.KeyEventHandler KeyDown;

	// Focus
	public event System.Windows.RoutedEventHandler GotFocus;
	public event System.Windows.RoutedEventHandler LostFocus;

	public event EventHandler LayoutUpdated;








