# WPF

## URLs

### [Optimizing performance: Controls](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/optimizing-performance-controls?view=netframeworkdesktop-4.8)

### [WPF Graphics Rendering Overview](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/graphics-multimedia/wpf-graphics-rendering-overview?view=netframeworkdesktop-4.8)
### [Using DrawingVisual Objects](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/graphics-multimedia/using-drawingvisual-objects?view=netframeworkdesktop-4.8)

### [Drawing Objects Overview](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/graphics-multimedia/drawing-objects-overview?view=netframeworkdesktop-4.8)

### [WPF: Data Virtualization](https://www.codeproject.com/Articles/34405/WPF-Data-Virtualization)

### [How to: Improve Rendering Performance by Caching an Element](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/graphics-multimedia/how-to-improve-rendering-performance-by-caching-an-element?view=netframeworkdesktop-4.8)

## UIElement / DrawingVisual

### [UIElement](https://docs.microsoft.com/en-us/dotnet/api/system.windows.uielement?view=windowsdesktop-6.0)

- Object
- DispatcherObject
- DependencyObject
- Visual
  - UIElement

### [DrawingVisual](https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.drawingvisual?view=windowsdesktop-6.0)

- Object
- DispatcherObject
- DependencyObject
- Visual
  - ContainerVisual
  - DrawingVisual

### Render

#### DrawingVisual

Opens the DrawingVisual object for rendering. The returned DrawingContext value can be used to render into the DrawingVisual.

	public System.Windows.Media.DrawingContext RenderOpen ();

#### UIElement

When overridden in a derived class, participates in rendering operations that are directed by the 
layout system. The rendering instructions for this element are not used directly when this method 
is invoked, and are instead preserved for later asynchronous use by layout and drawing.

	protected virtual void OnRender (System.Windows.Media.DrawingContext drawingContext);
