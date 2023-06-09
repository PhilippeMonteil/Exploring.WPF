
# Layout

## En résumé


## Measure

### UIElement : .Measure, .MeasureCore, .DesiredSize, .RenderTransform 

	public void Measure (System.Windows.Size availableSize);
	public System.Windows.Size DesiredSize { get; }

	protected virtual System.Windows.Size MeasureCore (System.Windows.Size availableSize);

- Measure appelle MeasureCore et met à jour DesiredSize

- RenderTransform

	public System.Windows.Media.Transform RenderTransform { get; set; }

    - A render transform does not regenerate layout size or render size information. 
	- Render transforms are typically intended for animating or applying 
	  a temporary effect to an element. 

#### [UIElement.Measure](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement.measure?view=windowsdesktop-7.0)

- Updates the DesiredSize of a UIElement. 
- Parent elements call this method from their own MeasureCore(Size) implementations to form a 
  recursive layout update. 
- Calling this method constitutes the first pass (the "Measure" pass) of a layout update.

#### [UIElement.MeasureCore](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement.measurecore?view=windowsdesktop-7.0#system-windows-uielement-measurecore(system-windows-size))

### FrameworkElement: .MeasureCore, .MeasureOverride, .Margin, .ActualWidth/Height, .Width/Height

	protected override sealed System.Windows.Size MeasureCore (System.Windows.Size availableSize);
	protected virtual System.Windows.Size MeasureOverride (System.Windows.Size availableSize);

	public System.Windows.Thickness Margin { get; set; }

> Elements that have margins set will not typically constrain the size of the specified Margin if the allotted rectangle space is not large enough for the margin plus the element content area. The element content area will be constrained instead when layout is calculated. The only case where margins would be constrained also is if the content is already constrained all the way to zero.

	public double ActualWidth { get; }
	public double ActualHeight { get; }

	public double Width { get; set; }
	public double Height { get; set; }

> The return value of this property is always the same as any value that was set to it. In contrast, the value of the ActualWidth may vary. The layout may have rejected the suggested size for some reason. Also, the layout system itself works asynchronously relative to the property system set of Width and may not have processed that particular sizing property change yet.

	public double Min/MaxWidth { get; set; } ...
	public double Min/MaxHeight { get; set; } ...

#### [MeasureOverride](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.measureoverride?view=windowsdesktop-7.0)
#### [Margin](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.margin?view=windowsdesktop-7.0)
#### [ActualWidth](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.actualwidth?view=windowsdesktop-7.0)

- FrameworkElement surcharge MeasureCore qui appelle MeasureOverride

### Control: .Padding, .MeasureOverride

	public System.Windows.Thickness Padding { get; set; }

		The amount of space between the content of a Control and its Margin or Border. 
		The default is a thickness of 0 on all four sides.

	protected override System.Windows.Size MeasureOverride (System.Windows.Size constraint);

## Arrange

### UIElement: .Arrange, .ArrangeCore

	public void Arrange (System.Windows.Rect finalRect);
	protected virtual void ArrangeCore (System.Windows.Rect finalRect);

	public System.Windows.HorizontalAlignment HorizontalAlignment { get; set; }
	public System.Windows.VerticalAlignment VerticalAlignment { get; set; }

