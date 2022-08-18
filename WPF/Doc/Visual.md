
# [Visual](https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.visual?view=windowsdesktop-6.0)

## Class

	public abstract class Visual : System.Windows.DependencyObject

## Inheritance

- Object
- DispatcherObject
- DependencyObject
- Visual

## Derived

- System.Windows.Media.ContainerVisual
- System.Windows.Media.Media3D.Viewport3DVisual
- System.Windows.UIElement

## Properties

	protected System.Windows.DependencyObject VisualParent { get; }

	protected virtual int VisualChildrenCount { get; }

	public System.Windows.Media.Transform VisualTransform { protected internal get; protected set; }

	public System.Windows.Media.Geometry VisualClip { protected internal get; protected set; 
	public System.Windows.Media.Effects.Effect VisualEffect { protected internal get; protected set; }

## Methods

	protected void AddVisualChild (System.Windows.Media.Visual child);
	protected void RemoveVisualChild (System.Windows.Media.Visual child);

	protected virtual System.Windows.Media.Visual GetVisualChild (int index);

	protected internal virtual void OnVisualChildrenChanged (System.Windows.DependencyObject visualAdded, System.Windows.DependencyObject visualRemoved);
	protected internal virtual void OnVisualParentChanged (System.Windows.DependencyObject oldParent);

