
# Templates

## En r�sum�

- System.Windows.FrameworkTemplate
    - classe abstraite d�riv�e de  Object / DispatcherObject / FrameworkTemplate
    - classe m�re s�par�ment de ControlTemplate, DataTemplate, ItemsPanelTemplate
    - propri�t�s
        - VisualTree [Content]
        - Resources
    - m�thodes
        - FindName : retrouver un contr�le dans un templatedParent � partir de son nom

- FrameworkElement : propri�t�s et m�thodes li�es aux Templates
    - propri�t�s
        - TemplatedParent, si le FE a �t� cr�� par un Template
    - m�thodes
        - public bool ApplyTemplate ();
        - public virtual void OnApplyTemplate ();
        - protected internal System.Windows.DependencyObject GetTemplateChild (string childName);

- System.Windows.Control.ControlTemplate
    - classe d�riv�e de FrameworkTemplate
    - propri�t�s :
        - Type TargetType // DictionaryKey : attribut DictionaryKeyProperty
        - TriggerCollection Triggers

- Control : propri�t�s et m�thodes li�es aux Templates
    - propri�t�s
        - ControlTemplate Template { get; set; }
    - m�thodes
        - protected virtual void OnTemplateChanged (ControlTemplate oldTemplate, ControlTemplate newTemplate);

- Templated Parent properties : MarkupExtension TemplateBinding, Binding / RelativeSource = TemplatedParent
- Named elements in Templates : les retrouver gr�ce � : FrameworkElement.GetTemplateChild
- Attribut TemplatePart, (Name,Type), 'PART_', OnApplyTemplate, GetTemplateChild
- Attribut TemplateVisualState, (Name, GroupName), VisualStateManager.GoToState(this ...

- System.Windows.Control.DataTemplate
    - classe d�riv�e de FrameworkTemplate
    - propri�t�s :
        - object DataTemplateKey // DictionaryKey : attribut DictionaryKeyProperty
        - object DataType { get; set; }
        - TriggerCollection Triggers
    - usages : ContentControl.ContentTemplate, ContentPresenter.ContentTemplate, ItemsControl.ItemTemplate, ...

- ItemsPanelTemplate 

    - classe d�riv�e de FrameworkTemplate
    - usages : ItemsControl.ItemsPanel, ...

- ItemsControl

    - properties
        - public DataTemplate ItemTemplate { get; set; }
        - public DataTemplateSelector ItemTemplateSelector { get; set; }

    - methods
        - protected virtual void OnItemTemplateChanged(DataTemplate oldItemTemplate, DataTemplate newItemTemplate);
        - protected virtual void OnItemTemplateSelectorChanged(DataTemplateSelector oldItemTemplateSelector, DataTemplateSelector newItemTemplateSelector);

- DataTemplateSelector

    - methods
        - public virtual System.Windows.DataTemplate SelectTemplate (object item, 
                                                                    DependencyObject container);
            - item : The data object for which to select the template.
            - container : The data-bound object.

## [System.Windows.FrameworkTemplate Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworktemplate?view=windowsdesktop-7.0)

### Inheritance

- Object
- DispatcherObject
- FrameworkTemplate

### Derived

- classes d�riv�es directes :

    - ControlTemplate
    - System.Windows.DataTemplate
    - ItemsPanelTemplate

### Propri�t�s

    - VisualTree // Content : attribut ContentProperty
    - Template
    - Resources

### Class

    [System.Windows.Localizability(System.Windows.LocalizationCategory.NeverLocalize)]
    [System.Windows.Markup.ContentProperty("VisualTree")]
    public abstract class FrameworkTemplate : System.Windows.Threading.DispatcherObject, 
                    System.Windows.Markup.INameScope, 
                    System.Windows.Markup.IQueryAmbient
    {

        public System.Windows.FrameworkElementFactory VisualTree { get; set; }

        [System.Windows.Markup.Ambient]
        public System.Windows.TemplateContent Template { get; set; }

        [System.Windows.Markup.Ambient]
        public System.Windows.ResourceDictionary Resources { get; set; }

    }

### Methods

#### FindName

    public object FindName (string name, System.Windows.FrameworkElement templatedParent);

#### RegisterName, UnregisterName

    public void RegisterName (string name, object scopedElement);
    public void UnregisterName (string name);

#### LoadContent

    public System.Windows.DependencyObject LoadContent();

### FrameworkElement : propri�t�s et m�thodes li�es aux Templates

#### TemplatedParent

- ne s'applique que si le FrameworkElement a �t� cr�� par un Template

    public System.Windows.DependencyObject TemplatedParent { get; }

#### ApplyTemplate

    public bool ApplyTemplate ();

#### OnApplyTemplate

    public virtual void OnApplyTemplate ();

#### GetTemplateChild

    protected internal System.Windows.DependencyObject GetTemplateChild (string childName);

- Returns the named element in the visual tree of an instantiated ControlTemplate.

## [System.Windows.Control.ControlTemplate Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.controltemplate?view=windowsdesktop-7.0)

### Class

    [System.Windows.Localizability(System.Windows.LocalizationCategory.None, Readability=System.Windows.Readability.Unreadable)]
    [System.Windows.Markup.DictionaryKeyProperty("TargetType")]
    public class ControlTemplate : System.Windows.FrameworkTemplate
    {

        [System.Windows.Markup.Ambient]
        public Type TargetType { get; set; }

        [System.Windows.Markup.DependsOn("VisualTree")]
        [System.Windows.Markup.DependsOn("Template")]
        public System.Windows.TriggerCollection Triggers { get; }

    }

### ControlTemplate.TargetType

- r�le identique � la propri�t� de m�me nom pour Style : utilisation de cette propri�t�
  comme Key lors d'une insertion d'une instance de ControlTemplate dans un ResourceDictionary
  
  cf [System.Windows.Markup.DictionaryKeyProperty("TargetType")]

     <ControlTemplate TargetType="{x:Type Button}">
     </ControlTemplate>

### Control : propri�t�s et m�thodes li�es aux Templates

#### Template

    public ControlTemplate Template { get; set; }

#### OnTemplateChanged

    protected virtual void OnTemplateChanged (ControlTemplate oldTemplate, ControlTemplate newTemplate);

### Respecting the Templated Parent properties : MarkupExtension TemplateBinding, Binding / RelativeSource = TemplatedParent

     <ControlTemplate TargetType="{x:Type Button}">
       <TextBlock Text="{TemplateBinding Content}" />
       <TextBlock Text="{Binding RelativeSource={RelativeSource TemplatedParent} Path=Content}" />
     </ControlTemplate>

- TemplateBinding ne s'applique qu'� l'int�rieur d'un Template et pas aux propri�t�s expos�es par un Freezable

### Named elements in Templates

- utilisation interne au Template
- pas d'application aux �l�ments cr��s dans le TemplatedParent
- m�thode FrameworkElement.GetTemplateChild

### Attribut TemplatePart sur un Contr�le, Names : 'PART_', OnApplyTemplate

#### [TemplatePartAttribute Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.templatepartattribute?redirectedfrom=MSDN&view=windowsdesktop-7.0)

#### Exemples

	[System.Windows.Localizability(System.Windows.LocalizationCategory.Ignore)]
	[System.Windows.TemplatePart(Name="PART_ScrollContentPresenter", Type=typeof(ScrollContentPresenter))]
	[System.Windows.TemplatePart(Name="PART_HorizontalScrollBar", Type=typeof(Primitives.ScrollBar))]
	[System.Windows.TemplatePart(Name="PART_VerticalScrollBar", Type=typeof(Primitives.ScrollBar))]
	public class ScrollViewer : ContentControl
    {

    }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Button _button = base.GetTemplateChild("PART_Browse") as Button;
            if (_button != null)
            {
                _button.Click += _button_Click;
            }
        }

### Visual States

#### [TemplateVisualStateAttribute Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.templatevisualstateattribute?view=netframework-4.8)

Specifies that a control can be in a certain state and that a VisualState is expected in the control's 
ControlTemplate.

#### [VisualStateManager Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.visualstatemanager?view=windowsdesktop-7.0)

    public static bool GoToState (System.Windows.FrameworkElement control, string stateName, bool useTransitions);

 VisualStateManager.GoToState(this, "Positive", useTransitions);

#### Exemple

```
[TemplatePartAttribute(Name = "PART_EditableTextBox", Type = typeof(TextBox))]
[TemplatePartAttribute(Name = "PART_Popup", Type = typeof(Popup))]
[TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
[TemplateVisualState(Name = "MouseOver", GroupName = "CommonStates")]
[TemplateVisualState(Name = "Pressed", GroupName = "CommonStates")]
[TemplateVisualState(Name = "Disabled", GroupName = "CommonStates")]
[TemplateVisualState(Name = "Unfocused", GroupName = "FocusStates")]
[TemplateVisualState(Name = "Focused", GroupName = "FocusStates")]
class ...

<VisualStateManager.VisualStateGroups>

    <!-- Positive value are green, negative values are red -->
    <VisualStateGroup x:Name="ValueStates">
        <VisualState x:Name="Negative" >
            <Storyboard>
                <ColorAnimation To="Red" 
                    Storyboard.TargetName="PART_Text"
					Storyboard.TargetProperty="(Foreground).(Color)"/>
            </Storyboard>
        </VisualState>
 
        <!--Return the control to its initial state by return the TextBlock's Foreground to its original color.-->
        <VisualState Name="Positive"/>
 
    </VisualStateGroup>

    <VisualStateGroup x:Name="FocusStates">
 
        <!--Add a focus rectangle to highlight the entire control when it has focus.-->
        <VisualState Name="Focused">
            <Storyboard>
                <ObjectAnimationUsingKeyFrames
					Storyboard.TargetName="PART_FocusVisual" 
                    Storyboard.TargetProperty="Visibility" Duration="0">
                    <DiscreteObjectKeyFrame KeyTime="0">
                        <DiscreteObjectKeyFrame.Value>
                            <Visibility>Visible</Visibility>
                        </DiscreteObjectKeyFrame.Value>
                    </DiscreteObjectKeyFrame>
                </ObjectAnimationUsingKeyFrames>
            </Storyboard>
        </VisualState>
 
        <!-- Return the control to its initial state by hiding the focus rectangle -->
        <VisualState Name="Unfocused"/>

    </VisualStateGroup>

</VisualStateManager.VisualStateGroups>
```

### Templates and Style

Si un style contient un ControlTemplate, une m�me propri�t� peut �tre affect�e de divers mani�res:
- par un setter dans le Style
- par un Trigger dans le Style
- par un Trigger dans le Template
La valeur effective de la propri�t� est d�termin�e par un ordre de pr�c�dence:
- les Style Triggers priment sur les Template Triggers
- les Triggers priment sur les Setters
- ...

## [DataTemplate](https://learn.microsoft.com/en-us/dotnet/api/system.windows.datatemplate?view=windowsdesktop-7.0)

### [DataTemplate Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.datatemplate?view=windowsdesktop-7.0)

[System.Windows.Markup.DictionaryKeyProperty("DataTemplateKey")]
public class DataTemplate : System.Windows.FrameworkTemplate
{
    public object DataTemplateKey { get; }

    [System.Windows.Markup.Ambient]
    public object DataType { get; set; }

    [System.Windows.Markup.DependsOn("VisualTree")]
    [System.Windows.Markup.DependsOn("Template")]
    public System.Windows.TriggerCollection Triggers { get; }

    protected override void ValidateTemplatedParent (System.Windows.FrameworkElement templatedParent);

}

#### DataTemplateKey

If you do not set the x:Key Directive on a DataTemplate that is in a ResourceDictionary, 
the DataTemplateKey is used as the key.

#### DataType

Gets or sets the type for which this DataTemplate is intended.

#### ValidateTemplatedParent

The method uses the following rules:

    The templatedParent must be a non-null FrameworkElement.

    The DataTemplate must be applied to a ContentPresenter.

### Exemples d'usage

#### ContentPresenter .ContentTemplate, .ContentTemplateSelector  

#### ItemsControl .ItemTemplate, .ItemTemplateSelector

[System.Windows.Localizability(System.Windows.LocalizationCategory.None, Readability=System.Windows.Readability.Unreadable)]
[System.Windows.Markup.ContentProperty("Items")]
[System.Windows.StyleTypedProperty(Property="ItemContainerStyle", StyleTargetType=typeof(System.Windows.FrameworkElement))]
public class ItemsControl : Control, 
        Primitives.IContainItemStorage, 
        System.Windows.Markup.IAddChild

[System.ComponentModel.Bindable(true)]
public System.Windows.DataTemplate ItemTemplate { get; set; }

[System.ComponentModel.Bindable(true)]
public DataTemplateSelector ItemTemplateSelector { get; set; }

Inheritance :

Control
ItemsControl

Derived :

HeaderedItemsControl
Primitives.DataGridCellsPresenter
Primitives.DataGridColumnHeadersPresenter
Primitives.MenuBase
Primitives.Selector
Primitives.StatusBar
Ribbon.RibbonContextualTabGroupItemsControl
Ribbon.RibbonControlGroup
Ribbon.RibbonGallery
Ribbon.RibbonQuickAccessToolBar
Ribbon.RibbonTabHeaderItemsControl
TreeView

## ItemsPanelTemplate 

### [ItemsPanelTemplate class] (https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.itemspaneltemplate?view=windowsdesktop-7.0)

### Inheritance

- FrameworkTemplate
- ItemsPanelTemplate

### Usage : ItemsControl.ItemsPanel

## DataTemplateSelector

### [DataTemplateSelector Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.datatemplateselector?view=windowsdesktop-7.0)

