
# Drawings

## [Drawing](https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.drawing?view=windowsdesktop-6.0)

### Class

	[System.Windows.Localizability(System.Windows.LocalizationCategory.None, Readability=System.Windows.Readability.Unreadable)]
	public abstract class Drawing : System.Windows.Media.Animation.Animatable

### Inheritance

- Object
- DispatcherObject
- DependencyObject
- Freezable
- Animatable
- Drawing

### Derived

- System.Windows.Media.DrawingGroup
- System.Windows.Media.GeometryDrawing
- System.Windows.Media.GlyphRunDrawing
- System.Windows.Media.ImageDrawing
- System.Windows.Media.VideoDrawing

### [DrawingGroup](https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.drawinggroup?view=windowsdesktop-6.0)

	[System.Windows.Markup.ContentProperty("Children")]
	public sealed class DrawingGroup : System.Windows.Media.Drawing
	{

		public System.Windows.Media.DrawingContext Open ();

### [GeometryDrawing](https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.geometrydrawing?view=windowsdesktop-6.0)

	public sealed class GeometryDrawing : System.Windows.Media.Drawing
	{

		public GeometryDrawing (System.Windows.Media.Brush brush, 
								System.Windows.Media.Pen pen, 
								System.Windows.Media.Geometry geometry);

	}

### [ImageDrawing](https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.imagedrawing?view=windowsdesktop-6.0)

	public sealed class ImageDrawing : System.Windows.Media.Drawing
	{
		public ImageDrawing (System.Windows.Media.ImageSource imageSource, 
							System.Windows.Rect rect);
	}


# [DrawingContext](https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.drawingcontext?view=windowsdesktop-6.0)

## Class

	public abstract class DrawingContext : System.Windows.Threading.DispatcherObject, IDisposable

## Inheritance

- Object
- DispatcherObject
- DrawingContext

## Implements

- IDisposable

## Methods

	public abstract void DrawDrawing (System.Windows.Media.Drawing drawing);

	public abstract void DrawGeometry (System.Windows.Media.Brush brush, 
							System.Windows.Media.Pen pen, 
							System.Windows.Media.Geometry geometry);

	public abstract void DrawImage (System.Windows.Media.ImageSource imageSource, 
							System.Windows.Rect rectangle);

	public abstract void Close ();


# [DrawingVisual](https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.drawingvisual?view=windowsdesktop-6.0)

## Class

	public class DrawingVisual : System.Windows.Media.ContainerVisual

## Inheritance

- Object
- DispatcherObject
- DependencyObject
- Visual
  - ContainerVisual
  - DrawingVisual

## Properties

	public System.Windows.Media.DrawingGroup Drawing { get; }

## Methods

	public System.Windows.Media.DrawingContext RenderOpen();

## Exemple

    DrawingGroup drawingGroup = new DrawingGroup();
    {
        using (DrawingContext drawingContext = drawingGroup.Open())
        {
            drawingContext.DrawRectangle(new SolidColorBrush(Colors.DarkGray),
										new Pen(new SolidColorBrush(Colors.Green), 1),
										new Rect(5, 5, 200, 100));
        }
    }

    drawingVisual = new DrawingVisual();

    using (DrawingContext drawingContext = drawingVisual.RenderOpen())
    {
        drawingContext.DrawRectangle(new SolidColorBrush(Colors.Blue),
									new Pen(new SolidColorBrush(Colors.Red), 1),
									new Rect(50, 100, 500, 300));

        drawingContext.DrawDrawing(drawingGroup);
    }

## Rendering d'un DrawingGroup, d'un DrawingVisual

- DrawingGroup

	public System.Windows.Media.DrawingContext Open ();

- DrawingVisual

	public System.Windows.Media.DrawingContext RenderOpen();

puis:

- DrawDrawing
- DrawGeometry
- DrawImage
- ...
- Close
	