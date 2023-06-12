
# [Routed Events](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/events/routed-events-overview?view=netdesktop-7.0)

## En résumé

- RoutedEvent

    - TypeConverter : RoutedEventConverter

    - properties :
        - public string Name { get; }
        - public Type OwnerType { get; }
        - public Type HandlerType { get; }
        - public RoutingStrategy RoutingStrategy { get; }

- EventManager

    - public static class EventManager

    - methods :

        - public static RoutedEvent RegisterRoutedEvent (string name, 
                                                    RoutingStrategy routingStrategy, 
                                                    Type handlerType, 
                                                    Type ownerType);

        - public static void RegisterClassHandler (Type classType, 
                                                    RoutedEvent routedEvent, 
                                                    Delegate handler, 
                                                    bool handledEventsToo);

        - public static RoutedEvent[] GetRoutedEvents ();

        - public static RoutedEvent[] GetRoutedEventsForOwner (Type ownerType);

- RoutingStrategy : Tunnel, Bubble, Direct 
 
- RoutedEventArgs
    - public RoutedEventArgs (RoutedEvent routedEvent, object source);
    - public bool Handled { get; set; }
    - public object OriginalSource { get; }
    - public RoutedEvent RoutedEvent { get; set; }
    - public object Source { get; set; }

- RoutedEventHandler
    - public delegate void RoutedEventHandler(object sender, RoutedEventArgs e);

- UIElement.RaiseEvent
    - public void RaiseEvent (RoutedEventArgs e);

- UIElement.AddHandler, .RemoveHandler
    - public void AddHandler (RoutedEvent routedEvent, Delegate handler);
    - exemple: exposition d'un RoutedEvent comme un event

- attached events
    - exemple : TextBox.PreviewKeyDown="HandlerInstanceEventInfo"
  
- Static class event handlers
- Override class event handlers 
- Marking routed events as handled

## [RoutedEvent](https://learn.microsoft.com/en-us/dotnet/api/routedevent?view=windowsdesktop-7.0)

- class

    [TypeConverter("Markup.RoutedEventConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
    public sealed class RoutedEvent

- properties

    - public Type HandlerType { get; }
    - public string Name { get; }
    - public Type OwnerType { get; }
    - public RoutingStrategy RoutingStrategy { get; }

## [EventManager](https://learn.microsoft.com/en-us/dotnet/api/eventmanager?view=windowsdesktop-7.0)

- public static class EventManager

- méthodes :
    
    public static RoutedEvent RegisterRoutedEvent (string name, 
                            RoutingStrategy routingStrategy, 
                            // The type of the event handler
                            Type handlerType,
                            Type ownerType);

    public static void RegisterClassHandler (
                            // The type of the class that is declaring class handling.
                            Type classType, 
                            RoutedEvent routedEvent, 
                            Delegate handler, 
                            bool handledEventsToo);

    public static RoutedEvent[] GetRoutedEvents ();

    public static RoutedEvent[] GetRoutedEventsForOwner (Type ownerType);

## [RoutingStrategy](https://learn.microsoft.com/en-us/dotnet/api/routingstrategy?view=windowsdesktop-7.0)

- RoutingStrategy : Tunnel, Bubble, Direct 

## [RoutedEventHandler](https://learn.microsoft.com/en-us/dotnet/api/eventmanager.getroutedeventsforowner?view=windowsdesktop-7.0)

    public delegate void RoutedEventHandler(object sender, RoutedEventArgs e);

## [RoutedEventArgs](https://learn.microsoft.com/en-us/dotnet/api/routedeventargs?view=windowsdesktop-7.0)

- public class RoutedEventArgs : EventArgs

- constructeur

    public RoutedEventArgs (RoutedEvent routedEvent, object source);

        source : This pre-populates the Source property.

- properties

    - public bool Handled { get; set; }
    - public object OriginalSource { get; }
    - public RoutedEvent RoutedEvent { get; set; }
    - public object Source { get; set; }

## [UIElement.RaiseEvent](https://learn.microsoft.com/fr-fr/dotnet/api/uielement.raiseevent?view=windowsdesktop-7.0)

    public void RaiseEvent (RoutedEventArgs e);

### Exemple

````
void RaiseTapEvent()
{
        RoutedEventArgs newEventArgs = new RoutedEventArgs(MyButtonSimple.TapEvent);
        RaiseEvent(newEventArgs);
}
````

## UIElement.AddHandler, .RemoveHandler

### [UIElement.AddHandler](https://learn.microsoft.com/en-us/dotnet/api/uielement.addhandler?view=windowsdesktop-7.0)

    public void AddHandler (RoutedEvent routedEvent, Delegate handler);

### [UIElement.RemoveHandler](https://learn.microsoft.com/en-us/dotnet/api/uielement.removehandler?view=windowsdesktop-7.0)

    public void RemoveHandler (RoutedEvent routedEvent, Delegate handler);

### Exemple

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

  exemple : protected virtual void UIElement.OnKeyDown (Input.KeyEventArgs e);


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

## [WeakEventManager](https://learn.microsoft.com/en-us/dotnet/api/system.windows.weakeventmanager?view=windowsdesktop-7.0)

### WeakEventManager

- classe : public abstract class WeakEventManager : System.Windows.Threading.DispatcherObject
 
- Inheritance : Object -> DispatcherObject -> WeakEventManager

- Derived :
    System.Collections.Specialized.CollectionChangedEventManager
    System.ComponentModel.CurrentChangedEventManager
    System.ComponentModel.CurrentChangingEventManager
    System.ComponentModel.ErrorsChangedEventManager
    System.ComponentModel.PropertyChangedEventManager
    System.Windows.Data.DataChangedEventManager
    System.Windows.Input.CanExecuteChangedEventManager
    System.Windows.LostFocusEventManager
    System.Windows.WeakEventManager<TEventSource,TEventArgs>

### [Weak event patterns](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/events/weak-event-patterns?view=netdesktop-7.0)

