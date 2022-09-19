
# [Panel Class] (https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.panel?view=windowsdesktop-6.0)

# TestCustomPanel

## En résumé

### FrameworkElement : Width/Height, MinWidth/Height, Margin, MeasureOverride, ArrangeOverride ...

#### [FrameworkElement.WidthProperty Field](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.widthproperty?view=windowsdesktop-6.0)

#### [FrameworkElement.Margin Property](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.margin?view=windowsdesktop-6.0)

#### [Control.Padding Property](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.control.padding?view=windowsdesktop-6.0)

#### [FrameworkElement.MeasureOverride(Size)](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.measureoverride?view=windowsdesktop-6.0) 

- protected override Size MeasureOverride(Size availableSize)

    - si le Panel est contenu dans une fenêtre ou dans une Grid.Cell, availableSize correspond
      à la totalité de l'espace disponible : zone client, cellule.
    - si le Panel est contenu dans un ScrollViewer : la taille est infinie : pas de contrainte.
    - si le Panel indique une MinWidth/Height, cette valeur est appliquée au paramètre availableSize
      avant l'appel de MeasureOverride
    - la valeur retournée représente le minimum pour l'intérieur : contenu et padding (Control), plus la marge

#### [FrameworkElement.ArrangeOverride(Size)](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.arrangeoverride?view=windowsdesktop-6.0)

    protected virtual System.Windows.Size ArrangeOverride (System.Windows.Size finalSize);

Parameters

    finalSize    Size 

The final area within the parent that this element should use to arrange itself and its children.

Returns

    Size

The actual size used.
