
# [Commanding](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/commanding-overview?view=netframeworkdesktop-4.8#Four_main_Concepts)

## En résumé

- ICommand

    - méthodes
        - public bool CanExecute (object? parameter);
        - public void Execute (object? parameter);
    - event
        - event EventHandler? CanExecuteChanged;

- RoutedCommand 

    - Inheritance : Object RoutedCommand
    - Derived : RoutedUICommand
    - Attributes : TypeConverterAttribute
    - Implements : ICommand
    - méthodes :
        public void Execute (object parameter, IInputElement target);
        public bool CanExecute (object parameter, IInputElement target);

- RoutedUICommand

    - dérive de RoutedCommand
    - property : 
        public string Text { get; set; }

- CommandBinding

    - constructeur

        public CommandBinding (ICommand command, 
                            ExecutedRoutedEventHandler executed, 
                            CanExecuteRoutedEventHandler canExecute);

    - events

        public event CanExecuteRoutedEventHandler PreviewCanExecute;
        public event CanExecuteRoutedEventHandler CanExecute;

        public event ExecutedRoutedEventHandler PreviewExecuted;
        public event ExecutedRoutedEventHandler Executed;
        
- ICommandSource

    - properties :
        - public ICommand Command { get; }
        - public object CommandParameter { get; }
        - public IInputElement CommandTarget { get; }

    - implements : ButtonBase 

- UIElement.CommandBindings

    public System.Windows.Input.CommandBindingCollection CommandBindings { get; }

- CommandManager

    - attached events :

        - public static readonly RoutedEvent PreviewCanExecuteEvent;
        - public static readonly RoutedEvent CanExecuteEvent;
        - public static readonly RoutedEvent PreviewExecutedEvent;
        - public static readonly RoutedEvent ExecutedEvent;

    - methods :

        - public static void AddCanExecuteHandler (UIElement element, CanExecuteRoutedEventHandler handler);
        - public static void AddPreviewCanExecuteHandler (UIElement element, CanExecuteRoutedEventHandler handler);
        - ... AddExecutedHandler / AddPreviewExecutedHandler
        - ... Remove ...

        - public static void InvalidateRequerySuggested();

        // ClassCommandBinding
        - public static void RegisterClassCommandBinding (Type type, CommandBinding commandBinding);
        // ClassInputBinding
        - public static void RegisterClassInputBinding (Type type, InputBinding inputBinding);

    - events :

        - public static event EventHandler RequerySuggested;

            exemple : implémentation de l'event ICommand.CanExecuteChanged

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

    - ClassCommandBinding, ClassInputBinding

        // ClassCommandBinding
        - public static void RegisterClassCommandBinding (Type type, CommandBinding commandBinding);
        // ClassInputBinding
        - public static void RegisterClassInputBinding (Type type, InputBinding inputBinding);

- IPreviewCommand, IPreviewCommandSource

    public interface IPreviewCommand : System.Windows.Input.ICommand

    public interface IPreviewCommandSource : System.Windows.Input.ICommandSource

## [ICommand](https://learn.microsoft.com/fr-fr/dotnet/api/icommand?view=net-8.0)

## [RoutedCommand](https://learn.microsoft.com/en-us/dotnet/api/routedcommand?view=windowsdesktop-7.0)

- Inheritance : Object RoutedCommand
- Derived : RoutedUICommand
- Attributes : TypeConverterAttribute
- Implements : ICommand

- methods :
```
    public void Execute (object parameter, IInputElement target);
    public bool CanExecute (object parameter, IInputElement target);

        target : Element at which to begin looking for command handlers.
```

L'appel de CanExecute / Execute se traduit par un déclenchement de la paire d'attached events
de CommandManager .PreviewCanExecute /.CanExecute et .PreviewExecuted (tunneling)/.Executed (bubbling) 
La propagation de ces events se fait le long de l'arbre visuel, à partir de la .CommandTarget
du ICommandSource ayant déclenché l'exécution de la commande, à la recherche d'un UIElement 
dont la collection .CommandBindings contient un CommandBinding traitant l'event en cours 
de propagation.

### [Understanding Routed Events and Commands In WPF](https://learn.microsoft.com/en-us/archive/msdn-magazine/2008/september/advanced-wpf-understanding-routed-events-and-commands-in-wpf)

## [RoutedUICommand](https://learn.microsoft.com/en-us/dotnet/api/routeduicommand?view=windowsdesktop-7.0)

- property :

    public string Text { get; set; }

## [CommandBinding](https://learn.microsoft.com/en-us/dotnet/api/commandbinding?view=windowsdesktop-7.0)

- constructeur

    public CommandBinding (ICommand command, 
                            ExecutedRoutedEventHandler executed, 
                            CanExecuteRoutedEventHandler canExecute);

- events

    public event CanExecuteRoutedEventHandler PreviewCanExecute;
    public event CanExecuteRoutedEventHandler CanExecute;

    public event ExecutedRoutedEventHandler PreviewExecuted;
    public event ExecutedRoutedEventHandler Executed;

## [UIElement.CommandBindings Property](https://learn.microsoft.com/en-us/dotnet/api/uielement.commandbindings?view=windowsdesktop-7.0)

## [ICommandSource](https://learn.microsoft.com/en-us/dotnet/api/icommandsource?view=windowsdesktop-7.0)

> The command source defines how a command is invoked by that particular object. For example, if a Button is associated with a command, the command is invoked when the Button is clicked.

- exposer ICommandSource n'a de sens que si le .Command fourni est de type RoutedCommand.
  Les autres propriétés exposées sont utilisées pour appeller RoutedCommand.CanExecute et .Execute :

    public void Execute (object parameter, IInputElement target);
    public bool CanExecute (object parameter, IInputElement target);

- properties

    - public ICommand Command { get; }
    - public object CommandParameter { get; }
    - public IInputElement CommandTarget { get; }

### Exemple

````
<Window.CommandBindings>
  <CommandBinding Command="ApplicationCommands.Open"
                  Executed="OpenCmdExecuted"
                  CanExecute="OpenCmdCanExecute"/>
</Window.CommandBindings>
````

## [CommandManager](https://learn.microsoft.com/en-us/dotnet/api/commandmanager?view=windowsdesktop-7.0)

> The CommandManager is responsible for managing routed commands. For more information about commanding, see Commanding Overview.

- public sealed class CommandManager

- attached events :

    - public static readonly RoutedEvent PreviewCanExecuteEvent;
    - public static readonly RoutedEvent CanExecuteEvent;
    - public static readonly RoutedEvent PreviewExecutedEvent;
    - public static readonly RoutedEvent ExecutedEvent;

- methods :

    - public static void AddCanExecuteHandler (UIElement element, CanExecuteRoutedEventHandler handler);
    - public static void AddPreviewCanExecuteHandler (UIElement element, CanExecuteRoutedEventHandler handler);
    - ... AddExecutedHandler / AddPreviewExecutedHandler
    - ... Remove ...

    - public static void InvalidateRequerySuggested();

    // ClassCommandBinding
    - public static void RegisterClassCommandBinding (Type type, CommandBinding commandBinding);
    // ClassInputBinding
    - public static void RegisterClassInputBinding (Type type, InputBinding inputBinding);

- events :

    - public static event EventHandler RequerySuggested;

    exemple : implémentation de l'event ICommand.CanExecuteChanged

````
public class RelayCommand : ICommand
{
    private Action<object> execute;
    private Func<object, bool> canExecute;

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }
````

## [IPreviewCommand](https://learn.microsoft.com/en-us/dotnet/api/microsoft.windows.input.ipreviewcommand?view=windowsdesktop-7.0)

    Provides methods for previewing commands without actually committing the action.

- public interface IPreviewCommand : ICommand

- methods :
    - public void CancelPreview();
    - public void Preview (object parameter);
        
## [IPreviewCommandSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.windows.input.ipreviewcommandsource?view=windowsdesktop-7.0)

Defines an object that knows how to preview a command.

- public interface IPreviewCommandSource : ICommandSource

- properties :
    - public object PreviewCommandParameter { get; }
