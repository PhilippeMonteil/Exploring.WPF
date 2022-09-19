
# [Panel Class] (https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.panel?view=windowsdesktop-6.0)

# TestCustomPanel

## [Control.Padding Property](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.control.padding?view=windowsdesktop-6.0)

## [FrameworkElement.Margin Property](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.margin?view=windowsdesktop-6.0)

## [FrameworkElement.MeasureOverride(Size)](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.measureoverride?view=windowsdesktop-6.0) 

````
     <!-- First Child -->
     <Rectangle Fill="Red" Width="50" MinWidth="100"/>
     <!-- ... -->
     <Button >Button0</Button>
     <Button Width="200">Button1</Button>
     <Button MinWidth="200">Button1</Button>
     <Button Width="100" MinWidth="200">Button1</Button>
     <Rectangle Fill="Blue" />
     <Rectangle Fill="Green" Width="100" />
     <Rectangle Fill="DarkRed" MinWidth="100" />
     <Rectangle Fill="DarkBlue" Width="50" MinWidth="100"/>
     <Rectangle Fill="DarkGreen" />
````

- protected override Size MeasureOverride(Size availableSize)

MeasureOverride(-) availableSize=700;300

  ui.DesiredSize=108;8
  ui.DesiredSize=68,09333333333333;41,96
  ui.DesiredSize=216;41,96
  ui.DesiredSize=216;41,96
  ui.DesiredSize=216;41,96
  ui.DesiredSize=8;8
  ui.DesiredSize=108;8
  ui.DesiredSize=108;8
  ui.DesiredSize=108;8
  ui.DesiredSize=8;8

MeasureOverride(+) vret=756;8

- Min/Max Width/Height : appliqués à availableSize à l'appel de MeasureOverride

- détermination de la taille requise par un UI ELement

    ui.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

  - Measure retourne :
    - si Width assigné
        - vret = Width + Margin
    - sinon
        - vret = calcul de la valeur minimale requise par le contenu de l'UIElement + Margin
    - si MinWidth assigné : vret = Max(vret, MinWidth)
    - si MaxWidth assigné : vret = Min(vret, MaxWidth)
 
- MeasureOverride ne doit pas prendre en compte Margin, valeur appliquée par Measure
 