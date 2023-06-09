
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

	public double ActualWidth { get; }
	public double ActualHeight { get; }

	public double Width { get; set; }
	public double Height { get; set; }

- FrameworkElement surcharge MeasureCore qui appelle MeasureOverride

### Control: .Padding, .MeasureOverride

	public System.Windows.Thickness Padding { get; set; }

	protected override System.Windows.Size MeasureOverride (System.Windows.Size constraint);

## Arrange

