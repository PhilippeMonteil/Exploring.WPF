# WPF

## URLs

### [Optimizing performance: Controls](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/optimizing-performance-controls?view=netframeworkdesktop-4.8)

### [WPF Graphics Rendering Overview](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/graphics-multimedia/wpf-graphics-rendering-overview?view=netframeworkdesktop-4.8)
### [Using DrawingVisual Objects](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/graphics-multimedia/using-drawingvisual-objects?view=netframeworkdesktop-4.8)

### [Drawing Objects Overview](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/graphics-multimedia/drawing-objects-overview?view=netframeworkdesktop-4.8)

### [WPF: Data Virtualization](https://www.codeproject.com/Articles/34405/WPF-Data-Virtualization)

### [How to: Improve Rendering Performance by Caching an Element](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/graphics-multimedia/how-to-improve-rendering-performance-by-caching-an-element?view=netframeworkdesktop-4.8)

## Visual / UIElement / DrawingVisual

### [Visual](https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.visual?view=windowsdesktop-6.0)

#### Class

	public abstract class Visual : System.Windows.DependencyObject

#### Inheritance

- Object
- DispatcherObject
- DependencyObject
- Visual

#### Derived

- System.Windows.Media.ContainerVisual
- System.Windows.Media.Media3D.Viewport3DVisual
- System.Windows.UIElement

#### Properties

	protected System.Windows.DependencyObject VisualParent { get; }

	protected virtual int VisualChildrenCount { get; }

	public System.Windows.Media.Transform VisualTransform { protected internal get; protected set; }

	public System.Windows.Media.Geometry VisualClip { protected internal get; protected set; 
	public System.Windows.Media.Effects.Effect VisualEffect { protected internal get; protected set; }

#### Methods

	protected void AddVisualChild (System.Windows.Media.Visual child);
	protected void RemoveVisualChild (System.Windows.Media.Visual child);

	protected virtual System.Windows.Media.Visual GetVisualChild (int index);

	protected internal virtual void OnVisualChildrenChanged (System.Windows.DependencyObject visualAdded, System.Windows.DependencyObject visualRemoved);
	protected internal virtual void OnVisualParentChanged (System.Windows.DependencyObject oldParent);


### [UIElement](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement?view=windowsdesktop-6.0)

#### Inheritance

- Object
- DispatcherObject
- DependencyObject
- Visual
  - UIElement


### [ContainerVisual](https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.containervisual?view=windowsdesktop-6.0)

Manages a collection of Visual objects.

#### Inheritance

- Object
- DispatcherObject
- DependencyObject
- Visual
- ContainerVisual

#### Derived

- System.Windows.Media.DrawingVisual
- System.Windows.Media.HostVisual

#### Properties

	public System.Windows.Media.VisualCollection Children { get; }
	protected override sealed int VisualChildrenCount { get; }

#### Methods

	protected override sealed System.Windows.Media.Visual GetVisualChild (int index);


### [DrawingVisual](https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.drawingvisual?view=windowsdesktop-6.0)

#### Class

	public class DrawingVisual : System.Windows.Media.ContainerVisual

#### Inheritance

- Object
- DispatcherObject
- DependencyObject
- Visual
  - ContainerVisual
  - DrawingVisual

#### Properties

	public System.Windows.Media.DrawingGroup Drawing { get; }

#### Methods

	public System.Windows.Media.DrawingContext RenderOpen ();

#### Exemple

	DrawingGroup drawing = FindResource("MyDrawing") as DrawingGroup; 


### [HostVisual](https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.hostvisual?view=windowsdesktop-6.0)

Represents a Visual object that can be connected anywhere to a parent visual tree.

#### Class

	public class HostVisual : System.Windows.Media.ContainerVisual

#### Inheritance

- Object
- DispatcherObject
- DependencyObject
- Visual
- ContainerVisual
- HostVisual




### DrawingVisual / UIElement Rendering

#### DrawingVisual

Opens the DrawingVisual object for rendering. The returned DrawingContext value can be used to render into the DrawingVisual.

	public System.Windows.Media.DrawingContext RenderOpen ();

#### UIElement

When overridden in a derived class, participates in rendering operations that are directed by the 
layout system. The rendering instructions for this element are not used directly when this method 
is invoked, and are instead preserved for later asynchronous use by layout and drawing.

	protected virtual void OnRender (System.Windows.Media.DrawingContext drawingContext);
