
# [Routed Events](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/events/routed-events-overview?view=netdesktop-7.0)

## [System.Windows.RoutedEvent](https://learn.microsoft.com/en-us/dotnet/api/system.windows.routedevent?view=windowsdesktop-7.0)

## [System.Windows.EventManager](https://learn.microsoft.com/en-us/dotnet/api/system.windows.eventmanager?view=windowsdesktop-7.0)

### [RegisterRoutedEvent](https://learn.microsoft.com/en-us/dotnet/api/system.windows.eventmanager.registerroutedevent?view=windowsdesktop-7.0)

    public static System.Windows.RoutedEvent RegisterRoutedEvent (string name, 
                            System.Windows.RoutingStrategy routingStrategy, 
                            // The type of the event handler
                            Type handlerType,
                            Type ownerType);

### [RegisterClassHandler](https://learn.microsoft.com/en-us/dotnet/api/system.windows.eventmanager.registerclasshandler?view=windowsdesktop-7.0)

    public static void RegisterClassHandler (
                            // The type of the class that is declaring class handling.
                            Type classType, 
                            System.Windows.RoutedEvent routedEvent, 
                            Delegate handler, 
                            bool handledEventsToo);

### [GetRoutedEvents](https://learn.microsoft.com/en-us/dotnet/api/system.windows.eventmanager.getroutedevents?view=windowsdesktop-7.0)

    public static System.Windows.RoutedEvent[] GetRoutedEvents ();

### [GetRoutedEventsForOwner](https://learn.microsoft.com/en-us/dotnet/api/system.windows.eventmanager.getroutedeventsforowner?view=windowsdesktop-7.0)

    public static System.Windows.RoutedEvent[] GetRoutedEventsForOwner (Type ownerType);

#### [RoutingStrategy](https://learn.microsoft.com/en-us/dotnet/api/system.windows.routingstrategy?view=windowsdesktop-7.0)

#### [UIElement.RaiseEvent](https://learn.microsoft.com/fr-fr/dotnet/api/system.windows.uielement.raiseevent?view=windowsdesktop-7.0)

    public void RaiseEvent (System.Windows.RoutedEventArgs e);

##### Exemple

````
void RaiseTapEvent()
{
        RoutedEventArgs newEventArgs = new RoutedEventArgs(MyButtonSimple.TapEvent);
        RaiseEvent(newEventArgs);
}
````

#### UIElement.AddHandler, .RemoveHandler

##### [RoutedEventHandler](https://learn.microsoft.com/en-us/dotnet/api/system.windows.eventmanager.getroutedeventsforowner?view=windowsdesktop-7.0)

    public delegate void RoutedEventHandler(object sender, RoutedEventArgs e);

##### [RoutedEventArgs](https://learn.microsoft.com/en-us/dotnet/api/system.windows.routedeventargs?view=windowsdesktop-7.0)

- public class RoutedEventArgs : EventArgs

- constructeur

    public RoutedEventArgs (System.Windows.RoutedEvent routedEvent, object source);

        source : This pre-populates the Source property.

- properties

    - public bool Handled { get; set; }
    - public object OriginalSource { get; }
    - public System.Windows.RoutedEvent RoutedEvent { get; set; }
    - public object Source { get; set; }

##### [UIElement.AddHandler](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement.addhandler?view=windowsdesktop-7.0)

    public void AddHandler (System.Windows.RoutedEvent routedEvent, Delegate handler);

##### [UIElement.RemoveHandler](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement.removehandler?view=windowsdesktop-7.0)

    public void RemoveHandler (System.Windows.RoutedEvent routedEvent, Delegate handler);

#### Exemple

````
// Register a custom routed event using the Bubble routing strategy.
public static readonly RoutedEvent TapEvent = EventManager.RegisterRoutedEvent(
    name: "Tap",
    routingStrategy: RoutingStrategy.Bubble,
    handlerType: typeof(RoutedEventHandler),
    ownerType: typeof(CustomButton));

// Provide CLR accessors for adding and removing an event handler.
public event RoutedEventHandler Tap
{
    add { AddHandler(TapEvent, value); }
    remove { RemoveHandler(TapEvent, value); }
}
````

## Attached events

### Exemple

````
<StackPanel Name="outerStackPanel" VerticalAlignment="Center">
    <custom:ComponentWrapper
        x:Name="componentWrapper"
        TextBox.PreviewKeyDown="HandlerInstanceEventInfo"
        HorizontalAlignment="Center">
        <TextBox Name="componentTextBox" Width="200" />
    </custom:ComponentWrapper>
</StackPanel>
````

## [Static class event handlers](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/events/marking-routed-events-as-handled-and-class-handling?view=netdesktop-7.0#static-class-event-handlers)

- You can attach static class event handlers by calling the RegisterClassHandler method in the static constructor 
  of a class. 

- Each class in a class hierarchy can register its own static class handler for each routed event. 

- As a result, there can be multiple static class handlers invoked for the same event on any given node 
  in the event route. 

- When the event route for the event is constructed, all static class handlers for each node are added 
  to the event route. 

- The order of invocation of static class handlers on a node starts with the most-derived static class handler, 
  followed by static class handlers from each successive base class.

## [Override class event handlers](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/events/marking-routed-events-as-handled-and-class-handling?view=netdesktop-7.0#static-class-event-handlers)

- Some visual element base classes expose empty On<event name> and OnPreview<event name> virtual methods 
  for each of their public routed input events. 

  exemple : protected virtual void UIElement.OnKeyDown (System.Windows.Input.KeyEventArgs e);


## [Marking routed events as handled, and class handling](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/events/marking-routed-events-as-handled-and-class-handling?view=netdesktop-7.0#instance-and-class-routed-event-handlers)

- Although there's no absolute rule for when to mark a routed event as handled, 
  consider marking an event as handled if your code responds to the event in a significant way. 
  
- A routed event that's marked as handled will continue along its route, but only handlers that are configured 
  to respond to handled events are invoked. 
  
- Basically, marking a routed event as handled limits its visibility to listeners along the event route.

- Routed event handlers can be either instance handlers or class handlers. 

- Instance handlers handle routed events on objects or XAML elements. 

- Class handlers handle a routed event at a class level, and are invoked before any instance handler responding 
  to the same event on any instance of the class.

- When routed events are marked as handled, they're often marked as such within class handlers.

- To mark an event as handled, set the Handled property value in its event data to true.

## [How to create a custom routed event](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/events/how-to-create-a-custom-routed-event?view=netdesktop-7.0)

