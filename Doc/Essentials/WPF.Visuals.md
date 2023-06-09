# Visuals

## En résumé

- Visual Class
    - class abstraite 
    - hierarchie de Visuals
    - chaque Visual possède des attributs de visualisation :
        - VisualOffset
        - VisualTransform
        - VisualOpacity
        - ...
 
 - ContainerVisual
    - dérive de Visual
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

