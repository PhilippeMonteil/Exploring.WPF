
# Triggers

## En résumé

- les classes FrameworkElement, Style, DataTemplate, ControlTemplate exposent une propriété .Triggers

    public System.Windows.TriggerCollection Triggers { get; }

  avec cette restriction sur [FrameworkElement.Triggers](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement.triggers?view=windowsdesktop-7.0) :

    The collection of triggers established on an element only supports EventTrigger, 
    not property triggers (Trigger). 
    If you require property triggers, you must place these within a style or template 
    and then assign that style or template to the element.

- de divers types : (Property)Trigger, DataTrigger, EventTrigger, MultiTrigger

- (Property)Trigger : 
    - le trigger porte sur une property ou AttachedProperty du FrameworkElement parent.

- DataTrigger :
    - Un DataBinding est défini par un Binding, une Value et un ensemble de Setters.
      Ses Setters sont appliqués lorsque sa Value est égale à la valeur produite par son Binding.

- Event Trigger :
    - public System.Windows.RoutedEvent RoutedEvent { get; set; }
    - public string SourceName { get; set; }
    - public System.Windows.TriggerActionCollection Actions { get; }

- MultiTrigger :
    Similaire à Property Trigger, permettant de définir une condition 'And' sur plusieurs propriété
    du Contrôle faisant l'objet du MultiTrigger.

## Property Triggers

- Associer à une 'Value' d'une 'Property' d'un Contrôle un ensemble de Setters, 
  qui assignent chacun une 'Value' à une 'Property'.

- la .Property d'un Trigger peut être une de celles exposées par le FrameworkElement auquel il s'applique
  ou une de ses AttachedProperty, ex: Validation.HasErrors

- exemple :

     <Style.Triggers>

       <Trigger Property="IsMouseOver" Value="True">
         <Setter Property="" Value="" />
       </Trigger>

       <Trigger Property="Validation.HasErrors" Value="True">
         <Setter Property="Background" Value="Red" />
         <Setter Property="Tooltip" 
            Value="{Binding 
                    RelativeSource={RelativeSource Self}},
                    Path={Validation.Errors[0].ErrorContent} " />
       </Trigger>

     </Style.Triggers>

Remarque :

- SourceName (and TargetName, for that matter) are not intended for use within the Triggers 
  collection of a Style. 
  A style does not have a namescope, so it does not make sense to refer to elements by name there.  
  But a template (either DataTemplate or ControlTemplate) does have a namescope.

## Data Triggers

### [DataTrigger Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.datatrigger?view=windowsdesktop-7.0)

[System.Windows.Markup.ContentProperty("Setters")]
[System.Windows.Markup.XamlSetMarkupExtension("ReceiveMarkupExtension")]
public class DataTrigger : System.Windows.TriggerBase, System.Windows.Markup.IAddChild

- properties

    public System.Windows.Data.BindingBase Binding { get; set; }
    public object Value { get; set; }
    public System.Windows.SetterBaseCollection Setters { get; }

### Notes

Un DataBinding est défini par un Binding, une Value et un ensemble de Setters.
Ses Setters sont appliqués lorsque sa Value est égale à la valeur produite par son Binding.

     <Style.Triggers>

       <DataTrigger 
            Binding="{Binding RelativeSource={RelativeSource Self}, Path=Text}" 
            Value="True">
         <Setter Property="IsEnabled" Value="False" />
       </DataTrigger>

     </Style.Triggers>

### Exemple

````
<DataTemplate.Triggers>
  <DataTrigger Binding="{Binding Path=TaskType}">
    <DataTrigger.Value>
      <local:TaskType>Home</local:TaskType>
    </DataTrigger.Value>
    <Setter TargetName="border" Property="BorderBrush" Value="Yellow"/>
  </DataTrigger>
</DataTemplate.Triggers>
````

## Event Trigger

### [EventTrigger Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.eventtrigger?view=windowsdesktop-7.0)

- constructors :
    - public EventTrigger ();
    - public EventTrigger (System.Windows.RoutedEvent routedEvent);

- properties :
    - public System.Windows.RoutedEvent RoutedEvent { get; set; }
    - public string SourceName { get; set; }
        Gets or sets the name of the object with the event that activates this trigger. 
        This is only used by element triggers or template triggers.
        The default value is null. If this property value is null, then the element being monitored 
        for the raising of the event is the templated parent or the logical tree root.
    - public System.Windows.TriggerActionCollection Actions { get; }

### Exemple

Associer à un Event du Contrôle faisant l'objet du EventTrigger le déclenchement d'une série d'Actions,
de type BeginStoryBoard par exemple.

     <Style.Triggers>
       <EventTrigger RoutedEvent="Button.Click">
         <EventTrigger.Actions>
           <BeginStoryBoard>
             <StoryBoard>

             </StoryBoard>
           </BeginStoryBoard>
         </EventTrigger.Actions>
       </EventTrigger>
     </Button.Triggers>

## MultiTrigger

Similaire à Property Trigger, permettant de définir une condition 'And' sur plusieurs propriété
du Contrôle faisant l'objet du MultiTrigger.

     <Style.Triggers>

       <MultiTrigger>

         <MultiTrigger.Conditions>
           <Condition Property="IsMouseOver" Value="True"/>
           <Condition Property="IsFocused" Value="True"/>
         </MultiTrigger.Conditions>

         <Setter Property="IsEnabled" Value="False" />

       </MultiTrigger>

     </Style.Triggers>
