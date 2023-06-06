
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

#### [UIElement.AddHandler](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement.addhandler?view=windowsdesktop-7.0)

    public void AddHandler (System.Windows.RoutedEvent routedEvent, Delegate handler);

#### [UIElement.RemoveHandler](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement.removehandler?view=windowsdesktop-7.0)

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

## [Class handling](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/events/marking-routed-events-as-handled-and-class-handling?view=netdesktop-7.0#instance-and-class-routed-event-handlers)

