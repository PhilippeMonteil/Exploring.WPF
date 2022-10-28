
# Templates

## VisualTree property : Content

     <ControlTemplate>
       <Grid>
       ...
       </Grid>
     </ControlTemplate>

## Triggers

     <ControlTemplate>
       <Grid>
       ...
       </Grid>
       <ControlTemplate.Triggers>
         <Setter TargetName="" Property="" Value="">
       </ControlTemplate.Triggers>
     </ControlTemplate>

## TargetType

     <ControlTemplate TargetType="{x:Type Button}">
     </ControlTemplate>

## Respecting the Templated Parent properties

     <ControlTemplate TargetType="{x:Type Button}">
       <TextBlock Text="{TemplateBinding Content}" />
       <TextBlock Text="{Binding RelativeSource={RelativeSource TemplatedParent} Path=Content}" />
     </ControlTemplate>

## PART_, OnApplyTemplate

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

