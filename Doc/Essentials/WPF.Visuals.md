
# Visuals

## En résumé

- Object - DispatcherObject - DependencyObject - Visual - ContainerVisual - DrawingVisual
                                                        - UIElement - FrameworkElement - Control

- Visual Class
    - class abstraite 
    - hierarchie de Visuals
    - chaque Visual possède des attributs de visualisation :
        - VisualOffset
        - VisualTransform
        - VisualOpacity
        - ...
 
 - ContainerVisual
    - concrète, dérive de Visual
    - exposition d'une VisualCollection, Children
    - DependencyObject Parent
    - attributs de visualisation : 
        - Offset
        - Transform
        - Opacity
        - ...

- VisualCollection
    - Represents an ordered collection of Visual objects.
    - Add, Clear, Remove,
    - Count, Items
    - ...

- DrawingVisual
    - dérive de ContainerVisual
    - public System.Windows.Media.DrawingContext RenderOpen ();
        - créée un DrawingContext où dessiner ...
        - le DrawingContext doit être fermé : Close
    - public System.Windows.Media.DrawingGroup Drawing { get; }
        - retourne le DrawingGroup contenant les opérations de dessin effectuées
          dans le DrawingContext retourné par RenderOpen

- Using DrawingVisual Objects
    - par le biais d'une classe dérivant de FrameworkElement,
      créant une VisualCollection, surchargeant GetVisualChild, VisualChildrenCount
    - VisualTreeHelper.HitTest

## Hiérarchie des classes

- Object - DispatcherObject - DependencyObject - Visual - ContainerVisual - DrawingVisual
                                                        - UIElement - FrameworkElement - Control
                                                        
## [Visual Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.visual?view=windowsdesktop-7.0)

- hierarchie de Visuals
- chaque Visual possède des attributs de visualisation :
    - VisualOffset
    - VisualTransform
    - VisualOpacity
    - ...

### Classe

- public abstract class Visual : System.Windows.DependencyObject

### Properties

- protected virtual int VisualChildrenCount { get; }
- protected System.Windows.DependencyObject VisualParent { get; }
- public System.Windows.Vector VisualOffset { protected internal get; protected set; }
- public System.Windows.Media.Transform VisualTransform { protected internal get; protected set; }
- public double VisualOpacity { protected internal get; protected set; }
- ...

### Methodes

- protected void AddVisualChild (System.Windows.Media.Visual child);
- protected virtual System.Windows.Media.Visual GetVisualChild (int index);
- protected void RemoveVisualChild (System.Windows.Media.Visual child);
- ...

## [ContainerVisual Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.containervisual?view=windowsdesktop-7.0)

- exposition d'une VisualCollection, Children
- DependencyObject Parent
- attributs de visualisation : 
    - Offset
    - Transform
    - Opacity

### Classe

public class ContainerVisual : System.Windows.Media.Visual

### Properties

- public System.Windows.Media.VisualCollection Children { get; }
- protected override sealed int VisualChildrenCount { get; }
- public System.Windows.DependencyObject Parent { get; }
- public System.Windows.Media.Transform Transform { get; set; }
- public System.Windows.Vector Offset { get; set; }
- public double Opacity { get; set; }
-
### Methods

- protected override sealed System.Windows.Media.Visual GetVisualChild (int index);

## [VisualCollection Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.visualcollection?view=windowsdesktop-7.0)

Represents an ordered collection of Visual objects.

### Classe

    public sealed class VisualCollection : System.Collections.ICollection

### Properties

- public int Count { get; }
- public System.Windows.Media.Visual this[int index] { get; set; }

### Méthodes

- public int Add (System.Windows.Media.Visual visual);
- public void Clear ();
- public void Insert (int index, System.Windows.Media.Visual visual);
- public void Remove (System.Windows.Media.Visual visual);
- ...

## [DrawingVisual Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.drawingvisual?view=windowsdesktop-7.0)
 
 - DrawingVisual dérive de ContainerVisual
 - DrawingVisual is a visual object that can be used to render vector graphics on the screen. The content is persisted by the system.

 ### Classe

    public class DrawingVisual : System.Windows.Media.ContainerVisual

### Properties

    public System.Windows.Media.DrawingGroup Drawing { get; }

### Methods

    public System.Windows.Media.DrawingContext RenderOpen ();

## [UIElement.OnRender](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement.onrender?view=windowsdesktop-7.0)

UIElement dérive de Visual

    protected virtual void OnRender (System.Windows.Media.DrawingContext drawingContext);

> When overridden in a derived class, participates in rendering operations that are directed by the layout system. The rendering instructions for this element are not used directly when this method is invoked, and are instead preserved for later asynchronous use by layout and drawing.

    protected internal virtual void OnRenderSizeChanged (System.Windows.SizeChangedInfo info);

> When overridden in a derived class, participates in rendering operations that are directed by the layout system. This method is invoked after layout update, and before rendering, if the element's RenderSize has changed as a result of layout update.

> The FrameworkElement implementation invalidates the Width and Height properties and handles the basics of remaking the layout.

> info : The packaged parameters (SizeChangedInfo), which includes old and new sizes, and which dimension actually changes.

## [Using DrawingVisual Objects](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/graphics-multimedia/using-drawingvisual-objects?view=netframeworkdesktop-4.8)

- In order to use DrawingVisual objects, you need to create a host container for the objects. 

- The host container object must derive from the FrameworkElement class, 
  which provides the layout and event handling support that the DrawingVisual class lacks.

- FrameworkElement :
    - création d'un VisualCollection, par exemple 
    - protected override System.Windows.Media.Visual GetVisualChild (int index);
    - protected override int VisualChildrenCount { get; }

### Exemple

````
public class MyVisualHost : FrameworkElement
{
    // Create a collection of child visual objects.
    private VisualCollection _children;

    public MyVisualHost()
    {
        _children = new VisualCollection(this);
        _children.Add(CreateDrawingVisualRectangle());
        _children.Add(CreateDrawingVisualText());
        _children.Add(CreateDrawingVisualEllipses());

        // Add the event handler for MouseLeftButtonUp.
        this.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(MyVisualHost_MouseLeftButtonUp);
    }
````

### Providing Hit Testing Support

- [VisualTreeHelper.HitTest](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.visualtreehelper.hittest?view=windowsdesktop-7.0)

