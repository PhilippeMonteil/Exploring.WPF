
# Layout

## En résumé

- Measure

    - UIElement

        - public void Measure (Size availableSize);
    	- protected virtual Size MeasureCore (Size availableSize);
    	- public Size DesiredSize { get; }

        	Measure appelle MeasureCore puis met à jour DesiredSize

    	- public Media.Transform RenderTransform { get; set; }
    	- public Point RenderTransformOrigin { get; set; }

        	A render transform does not regenerate layout size or render size information.

    - FrameworkElement

    	- protected override sealed Size MeasureCore (Size availableSize);
    	- protected virtual Size MeasureOverride (Size availableSize);

    	- public Thickness Margin { get; set; }

            - surcharge 'sealed' de MeasureCore
    		- prise en compte de Margin
    		- appel de MeasureOverride

        - public double Width/Height { get; set; }
    	- public double Min/MaxWidth/Height { get; set; } ...

    	- public double ActualWidth/Height { get; }

        	> This property is a calculated value based on other width inputs, and the layout system. The value is set by the layout system itself, based on an actual rendering pass, and may therefore lag slightly behind the set value of properties such as Width that are the basis of the input change.

    - Control

        - public Thickness Padding { get; set; }

    	> The amount of space between the content of a Control and its Margin or Border. 

- Arrange

    - UIElement

    	public void Arrange (Rect finalRect);

        	- déclenche le positionnement de this, et de ses children ...
    		- The final size that the parent computes for the child element, 
    		  provided as a Rect instance.

    	protected virtual void ArrangeCore (Rect finalRect);

        	- appelée lors de Arrange
    		- The final area within the parent that element should use to arrange itself and its child elements.

    	public Size RenderSize { get; set; }

    		- Gets (or sets) the final render size of this element.
    		  set ne devrait pas être appelé ...

    - FrameworkElement

    	protected override sealed void ArrangeCore (Rect finalRect);
    	protected virtual Size ArrangeOverride (Size finalSize);

    	public HorizontalAlignment HorizontalAlignment { get; set; }
    	public VerticalAlignment VerticalAlignment { get; set; }

    - Control

    	protected override Size ArrangeOverride (Size arrangeBounds);

    	public Thickness Padding { get; set; }

    		- The amount of space between the content of a Control and its Margin or Border. 

        public Thickness BorderThickness { get; set; }

        public Horizontal/VerticalAlignment Horizontal/VerticalContentAlignment { get; set; }

- résumé des paramètres de positionnement, sizing, rendu

    - UIElement
        - public Media.Transform RenderTransform { get; set; }
    	- public Point RenderTransformOrigin { get; set; }

    - FrameworkElement
        - public double Width/Height { get; set; }
        - public double Min/MaxWidth/Height { get; set; } ...
        - public Thickness Margin { get; set; }
    	- public HorizontalAlignment HorizontalAlignment { get; set; }
    	- public VerticalAlignment VerticalAlignment { get; set; }

    - Control
        - public Thickness Padding { get; set; }
    	- public Thickness BorderThickness { get; set; }
    	- public Horizontal/VerticalAlignment Horizontal/VerticalContentAlignment { get; set; }

- résumé des propriétés affectées par les calculs de positionnement, par le rendu 

    - UIElement
    	- public Size DesiredSize { get; }
    	- public Size RenderSize { get; set; }

    - FrameworkElement
    	- public double ActualWidth/Height { get; }

    - Control

- divers

    	public Point TranslatePoint (Point point, UIElement relativeTo);

- UIElement methods : InvalidateMeasure, InvalidateArrange, UpdateLayout, InvalidateVisual 

    public void InvalidateMeasure ();
    public void InvalidateArrange ();
    public void UpdateLayout ();
    public void InvalidateVisual ();

- events : UIElement.LayoutUpdated, UIElement.LayoutUpdated

    public event EventHandler LayoutUpdated;
    public event System.Windows.SizeChangedEventHandler SizeChanged;


## Measure

### UIElement : .Measure, .MeasureCore, .DesiredSize, .RenderTransform 

	public void Measure (Size availableSize);
	public Size DesiredSize { get; }

	protected virtual Size MeasureCore (Size availableSize);

- Measure appelle MeasureCore et met à jour DesiredSize

- RenderTransform

	public Media.Transform RenderTransform { get; set; }

    - A render transform does not regenerate layout size or render size information. 
	- Render transforms are typically intended for animating or applying 
	  a temporary effect to an element. 

#### [UIElement.Measure](https://learn.microsoft.com/en-us/dotnet/api/uielement.measure?view=windowsdesktop-7.0)

- Updates the DesiredSize of a UIElement. 
- Parent elements call this method from their own MeasureCore(Size) implementations to form a 
  recursive layout update. 
- Calling this method constitutes the first pass (the "Measure" pass) of a layout update.

#### [UIElement.MeasureCore](https://learn.microsoft.com/en-us/dotnet/api/uielement.measurecore?view=windowsdesktop-7.0#system-windows-uielement-measurecore(system-windows-size))

### FrameworkElement: .MeasureCore, .MeasureOverride, .Margin, .ActualWidth/Height, .Width/Height

	protected override sealed Size MeasureCore (Size availableSize);
	protected virtual Size MeasureOverride (Size availableSize);

	public Thickness Margin { get; set; }

> This property is a calculated value based on other width inputs, and the layout system. The value is set by the layout system itself, based on an actual rendering pass, and may therefore lag slightly behind the set value of properties such as Width that are the basis of the input change.

	public double ActualWidth { get; }
	public double ActualHeight { get; }

> Elements that have margins set will not typically constrain the size of the specified Margin if the allotted rectangle space is not large enough for the margin plus the element content area. The element content area will be constrained instead when layout is calculated. The only case where margins would be constrained also is if the content is already constrained all the way to zero.

	public double Width { get; set; }
	public double Height { get; set; }

> The return value of this property is always the same as any value that was set to it. In contrast, the value of the ActualWidth may vary. The layout may have rejected the suggested size for some reason. Also, the layout system itself works asynchronously relative to the property system set of Width and may not have processed that particular sizing property change yet.

	public double Min/MaxWidth { get; set; } ...
	public double Min/MaxHeight { get; set; } ...

#### [MeasureOverride](https://learn.microsoft.com/en-us/dotnet/api/frameworkelement.measureoverride?view=windowsdesktop-7.0)
#### [Margin](https://learn.microsoft.com/en-us/dotnet/api/frameworkelement.margin?view=windowsdesktop-7.0)
#### [ActualWidth](https://learn.microsoft.com/en-us/dotnet/api/frameworkelement.actualwidth?view=windowsdesktop-7.0)

- FrameworkElement surcharge MeasureCore qui appelle MeasureOverride

### Control: .Padding, .MeasureOverride

	public Thickness Padding { get; set; }

		The amount of space between the content of a Control and its Margin or Border. 
		The default is a thickness of 0 on all four sides.

	protected override Size MeasureOverride (Size constraint);

## Arrange

### UIElement : .Arrange, .ArrangeCore, .TranslatePoint

	public void Arrange (Rect finalRect);

		- The final size that the parent computes for the child element, provided as a Rect instance.

	protected virtual void ArrangeCore (Rect finalRect);

		- The final area within the parent that element should use to arrange itself and its child elements.

	public Size RenderSize { get; set; }

		- Gets (or sets) the final render size of this element.
		  set ne devrait pas être appelé ...

	public Media.Transform RenderTransform { get; set; }
	public Point RenderTransformOrigin { get; set; }

		- A render transform does not regenerate layout size or render size information. 
		- Render transforms are typically intended for animating or applying a 
		  temporary effect to an element. 

	public Point TranslatePoint (Point point, UIElement relativeTo);

		- Translates a point relative to this element to coordinates that are relative to 
		  the specified element.

#### Exemple :

````
protected override void OnRender(DrawingContext drawingContext)
{
  // Get a rectangle that represents the desired size of the rendered element
  // after the rendering pass.  This will be used to draw at the corners of the 
  // adorned element.
  Rect adornedElementRect = new Rect(this.AdornedElement.RenderSize);

  // Some arbitrary drawing implements.
  SolidColorBrush renderBrush = new SolidColorBrush(Colors.Green);
  renderBrush.Opacity = 0.2;
  Pen renderPen = new Pen(new SolidColorBrush(Colors.Navy), 1.5);
  double renderRadius = 5.0;

  // Just draw a circle at each corner.
  drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopLeft, renderRadius, renderRadius);
  drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopRight, renderRadius, renderRadius);
  drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomLeft, renderRadius, renderRadius);
  drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomRight, renderRadius, renderRadius);
}
````

### FrameworkElement : .ArrangeCore, .ArrangeOverride, .HorizontalAlignment/...

	protected override sealed void ArrangeCore (Rect finalRect);

	protected virtual Size ArrangeOverride (Size finalSize);

		- The final area within the parent that this element should use to arrange itself and its children.
		- returns the actual size used.

		> Control authors who want to customize the arrange pass of layout processing should override this method. The implementation pattern should call Arrange(Rect) on each visible child element, and pass the final desired size for each child element as the finalRect parameter. Parent elements should call Arrange(Rect) on each child, otherwise the child elements will not be rendered.

	public HorizontalAlignment HorizontalAlignment { get; set; }
	public VerticalAlignment VerticalAlignment { get; set; }

### Control

	protected override Size ArrangeOverride (Size arrangeBounds);

	public Thickness Padding { get; set; }

		- The amount of space between the content of a Control and its Margin or Border. 

	public Thickness BorderThickness { get; set; }

        - espaces entre l'extérieur du Contrôle et sa zone de Padding

	public HorizontalAlignment HorizontalContentAlignment { get; set; }
	public VerticalAlignment VerticalContentAlignment { get; set; }

		- Gets or sets the horizontal alignment of the control's content.

## UIElement methods : InvalidateMeasure, InvalidateArrange, UpdateLayout, InvalidateVisual 

### [InvalidateMeasure](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement.invalidatemeasure?view=windowsdesktop-7.0)

    public void InvalidateMeasure ();

- Invalidates the measurement state (layout) for the element.
- Calling this method also calls InvalidateArrange internally, there is no need to call 
  InvalidateMeasure and InvalidateArrange in succession. 
- After the invalidation, the element will have its layout updated, which will occur asynchronously, 
  unless UpdateLayout is called to force a synchronous layout change.

### [InvalidateArrange](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement.invalidatearrange?view=windowsdesktop-7.0)

    public void InvalidateArrange ();

- Invalidates the arrange state (layout) for the element. After the invalidation, 
  the element will have its layout updated, which will occur asynchronously 
  unless subsequently forced by UpdateLayout().

### [UIElement.UpdateLayout Method](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement.updatelayout?view=windowsdesktop-7.0)

    public void UpdateLayout ();

- Ensures that all visual child elements of this element are properly updated for layout.
- When you call this method, elements with IsMeasureValid false or IsArrangeValid false 
  will call element-specific MeasureCore and ArrangeCore methods, which forces layout update, 
  and all computed sizes will be validated.

### [InvalidateVisual](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement.invalidatevisual?view=windowsdesktop-7.0)

    public void InvalidateVisual ();

- Invalidates the rendering of the element, and forces a complete new layout pass. 
  OnRender(DrawingContext) is called after the layout cycle is completed.
- This method calls InvalidateArrange internally.
- This method is not generally called from your application code. 

## Events

### [UIElement.LayoutUpdated](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.control.borderthickness?view=windowsdesktop-7.0)

- public event EventHandler LayoutUpdated;
- This member is a CLR event, not a routed event.

### [FrameworkElement.SizeChanged](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.sizechanged?view=windowsdesktop-7.0)

- public event System.Windows.SizeChangedEventHandler SizeChanged;
- Occurs when either the ActualHeight or the ActualWidth properties change value on this element.

- public delegate void SizeChangedEventHandler(object sender, SizeChangedEventArgs e);
- public class SizeChangedEventArgs : System.Windows.RoutedEventArgs

#### [SizeChangedEventArgs](https://learn.microsoft.com/en-us/dotnet/api/system.windows.sizechangedeventargs?view=windowsdesktop-7.0)

### Direct vs Routed events

- Direct routed events do not follow a route, they are only handled within the same element on which 
  they are raised. 
- Direct routed events do support other routed event behavior: 
    - they support an accessible handlers collection,
    - they can be used as an EventTrigger in a style.

## [Adorner Class](https://learn.microsoft.com/en-us/dotnet/api/documents.adorner?view=windowsdesktop-7.0)

