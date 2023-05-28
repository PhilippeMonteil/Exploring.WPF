
# Triggers

- les classes FrameworkElement, Style, DataTemplate, ControlTemplate 

    public System.Windows.TriggerCollection Triggers { get; }

  avec cette restriction sur FrameworkElement.Triggers :

    The collection of triggers established on an element only supports EventTrigger, 
    not property triggers (Trigger). 
    If you require property triggers, you must place these within a style or template 
    and then assign that style or template to the element.

- de divers types : Trigger, MultiTrigger, DataTrigger, EventTrigger

## Property Triggers

Associer à une 'Value' d'une 'Property' d'un Contrôle un ensemble de Setters, 
qui assignent chacun une 'Value' à une 'Property', du même Contrôle ou d'un autre,
nommé ('TargetName').

     <Style.Triggers>

       <Trigger Property="IsMouseOver" Value="True">
         <Setter Property="" Value="" />
       </Trigger>

       <Trigger Property="Validation.HasErrors" Value="True">
         <Setter Property="Background" Value="Red" />
         <Setter Property="Tooltip" 
            Value="{Binding 
                    RelativeSource={RelativeSource Self}},
                    Path={Vaidation.Errors[0].ErrorContent} " />
       </Trigger>

     </Style.Triggers>

## Data Triggers

Associer à une 'Value', bindée relativement au Contrôle faisant l'objet du DataTrigger, 
un ensemble de Setters, qui assignent chacun une 'Value' à une 'Property', 
du même Contrôle ou d'un autre, nommé ('TargetName').

     <Style.Triggers>

       <DataTrigger 
            Binding="{Binding RelativeSource={RelativeSource Self}, Path=Text}" 
            Value="True">
         <Setter Property="IsEnabled" Value="False" />
       </DataTrigger>

     </Style.Triggers>

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

## Event Trigger

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
