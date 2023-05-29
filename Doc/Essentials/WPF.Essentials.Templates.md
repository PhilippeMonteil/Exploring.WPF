
# Templates

## En résumé

- System.Windows.FrameworkTemplate
    - classe abstraite dérivée de DispatcherObject
    - classe mère séparément de ControlTemplate, DataTemplate, ItemsPanelTemplate
    - propriétés
        - VisualTree [Content]
        - Resources
    - méthodes
        - FindName : retrouvé un contrôle dans un templatedParent à partir de son nom

- FrameworkElement : propriétés et méthodes liées aux Templates
    - propriétés
        - TemplatedParent, si le FE a été créé par un Template
    - méthodes
        - public bool ApplyTemplate ();
        - public virtual void OnApplyTemplate ();
        - protected internal System.Windows.DependencyObject GetTemplateChild (string childName);

- System.Windows.Control.ControlTemplate
    - classe dérivée de FrameworkTemplate
    - propriétés :
        - TargetType // DictionaryKey : attribut DictionaryKeyProperty
        - TriggerCollection Triggers

- Control : propriétés et méthodes liées aux Templates
    - propriétés
        - ControlTemplate Template { get; set; }
    - méthodes
        - protected virtual void OnTemplateChanged (System.Windows.Controls.ControlTemplate oldTemplate, System.Windows.Controls.ControlTemplate newTemplate);

- Templated Parent properties : MarkupExtension TemplateBinding, Binding / RelativeSource = TemplatedParent
- Named elements in Templates : les retrouver grâce à : FrameworkElement.GetTemplateChild
- Attribut TemplatePart sur un Contrôle, Names : 'PART_', OnApplyTemplate

## [System.Windows.FrameworkTemplate Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworktemplate?view=windowsdesktop-7.0)

### Inheritance

- Object
- DispatcherObject
- FrameworkTemplate

### Derived

- classes dérivées directes :

    - System.Windows.Controls.ControlTemplate
    - System.Windows.DataTemplate
    - System.Windows.Controls.ItemsPanelTemplate

- propriétés :

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

### FrameworkElement : propriétés et méthodes liées aux Templates

#### TemplatedParent

- ne s'applique que si le FrameworkElement a été créé par un Template

    public System.Windows.DependencyObject TemplatedParent { get; }

#### ApplyTemplate

    public bool ApplyTemplate ();

#### OnApplyTemplate

    public virtual void OnApplyTemplate ();

#### GetTemplateChild

    protected internal System.Windows.DependencyObject GetTemplateChild (string childName);

- Returns the named element in the visual tree of an instantiated ControlTemplate.

## [System.Windows.Control.ControlTemplate Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.controltemplate?view=windowsdesktop-7.0)

### Inheritance

- Object
- DispatcherObject
- FrameworkTemplate
- ControlTemplate

- propriétés :

    - TargetType // DictionaryKey : attribut DictionaryKeyProperty
    - Triggers

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

- rôle identique à la propriété de même nom pour Style : utilisation de cette propriété
  comme Key lors d'une insertion d'une instance de ControlTemplate dans un ResourceDictionary
  
  cf [System.Windows.Markup.DictionaryKeyProperty("TargetType")]

     <ControlTemplate TargetType="{x:Type Button}">
     </ControlTemplate>

### Control : propriétés et méthodes liées aux Templates

#### Template

    public System.Windows.Controls.ControlTemplate Template { get; set; }

#### OnTemplateChanged

    protected virtual void OnTemplateChanged (System.Windows.Controls.ControlTemplate oldTemplate, System.Windows.Controls.ControlTemplate newTemplate);

## Respecting the Templated Parent properties : MarkupExtension TemplateBinding, Binding / RelativeSource = TemplatedParent

     <ControlTemplate TargetType="{x:Type Button}">
       <TextBlock Text="{TemplateBinding Content}" />
       <TextBlock Text="{Binding RelativeSource={RelativeSource TemplatedParent} Path=Content}" />
     </ControlTemplate>

- TemplateBinding ne s'applique qu'à l'intérieur d'un Template et pas aux propriétés exposées par un Freezable

## Named elements in Templates

- utilisation interne au Template
- pas d'application aux éléments créés dans le TemplatedParent
- méthode FrameworkElement.GetTemplateChild

## Attribut TemplatePart sur un Contrôle, Names : 'PART_', OnApplyTemplate

	[System.Windows.Localizability(System.Windows.LocalizationCategory.Ignore)]
	[System.Windows.TemplatePart(Name="PART_ScrollContentPresenter", Type=typeof(System.Windows.Controls.ScrollContentPresenter))]
	[System.Windows.TemplatePart(Name="PART_HorizontalScrollBar", Type=typeof(System.Windows.Controls.Primitives.ScrollBar))]
	[System.Windows.TemplatePart(Name="PART_VerticalScrollBar", Type=typeof(System.Windows.Controls.Primitives.ScrollBar))]
	public class ScrollViewer : System.Windows.Controls.ContentControl
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

## Visual States

Triggers du Template.

## Templates and Style

Si un style contient un ControlTemplate, une même propriété peut être affectée de divers manières:
- par un setter dans le Style
- par un Trigger dans le Style
- par un Trigger dans le Template
La valeur effective de la propriété est déterminée par un ordre de précédence:
- les Style Triggers priment sur les Template Triggers
- les Triggers priment sur les Setters
- ...

