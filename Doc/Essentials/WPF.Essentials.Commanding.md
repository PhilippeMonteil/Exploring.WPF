
# [Commanding](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/commanding-overview?view=netframeworkdesktop-4.8#Four_main_Concepts)

## En résumé

- System.Windows.Input.ICommand

    - méthodes
        - public bool CanExecute (object? parameter);
        - public void Execute (object? parameter);
    - event
        - event EventHandler? CanExecuteChanged;

- System.Windows.Input.RoutedCommand 

    - Inheritance : Object RoutedCommand
    - Derived : RoutedUICommand
    - Attributes : TypeConverterAttribute
    - Implements : ICommand
    - méthodes :
        public void Execute (object parameter, System.Windows.IInputElement target);
        public bool CanExecute (object parameter, System.Windows.IInputElement target);

- ICommandSource

    - properties :
        - public System.Windows.Input.ICommand Command { get; }
        - public object CommandParameter { get; }
        - public System.Windows.IInputElement CommandTarget { get; }

- [UIElement.CommandBindings Property](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement.commandbindings?view=windowsdesktop-7.0)

- CommandBinding


## [System.Windows.Input.ICommand](https://learn.microsoft.com/fr-fr/dotnet/api/system.windows.input.icommand?view=net-8.0)

## [RoutedCommand](https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.routedcommand?view=windowsdesktop-7.0)

- Inheritance : Object RoutedCommand
- Derived : RoutedUICommand
- Attributes : TypeConverterAttribute
- Implements : ICommand

- methods :

    public void Execute (object parameter, System.Windows.IInputElement target);
    public bool CanExecute (object parameter, System.Windows.IInputElement target);

        target : Element at which to begin looking for command handlers.

        L'appel de CanExecute / Execute se traduit par un déclenchement de la paire d'attached events
        de CommandManager .PreviewCanExecute /.CanExecute et .PreviewExecuted (tunneling)/.Executed (bubbling) 
        La propagation de ces events se fait le long de l'arbre visuel, à partir de la .CommandTarget
        du ICommandSource ayant déclenché l'exécution de la commande, à la recherche d'un UIElement 
        dont la collection .CommandBindings contient un CommandBinding traitant l'event en cours 
        de propagation.

## [RoutedUICommand](https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.routeduicommand?view=windowsdesktop-7.0)

- property :

    public string Text { get; set; }

## [CommandBinding](https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.commandbinding?view=windowsdesktop-7.0)

- constructeur

    public CommandBinding (System.Windows.Input.ICommand command, 
                            System.Windows.Input.ExecutedRoutedEventHandler executed, 
                            System.Windows.Input.CanExecuteRoutedEventHandler canExecute);

- events

    public event System.Windows.Input.CanExecuteRoutedEventHandler PreviewCanExecute;
    public event System.Windows.Input.CanExecuteRoutedEventHandler CanExecute;

    public event System.Windows.Input.ExecutedRoutedEventHandler PreviewExecuted;
    public event System.Windows.Input.ExecutedRoutedEventHandler Executed;

## [UIElement.CommandBindings Property](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement.commandbindings?view=windowsdesktop-7.0)

## [System.Windows.Input.ICommandSource](https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.icommandsource?view=windowsdesktop-7.0)

> The command source defines how a command is invoked by that particular object. For example, if a Button is associated with a command, the command is invoked when the Button is clicked.

- exposer ICommandSource n'a de sens que si le .Command fourni est de type RoutedCommand.
  Les autres propriétés exposées sont utilisées pour appeller RoutedCommand.CanExecute et .Execute :

    public void Execute (object parameter, System.Windows.IInputElement target);
    public bool CanExecute (object parameter, System.Windows.IInputElement target);

- properties

    - public System.Windows.Input.ICommand Command { get; }
    - public object CommandParameter { get; }
    - public System.Windows.IInputElement CommandTarget { get; }

### Exemple

````
<Window.CommandBindings>
  <CommandBinding Command="ApplicationCommands.Open"
                  Executed="OpenCmdExecuted"
                  CanExecute="OpenCmdCanExecute"/>
</Window.CommandBindings>
````

## [CommandManager](https://learn.microsoft.com/en-us/dotnet/api/system.windows.input.commandmanager?view=windowsdesktop-7.0)

