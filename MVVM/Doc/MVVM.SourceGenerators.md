
# [MVVM source generators](https://docs.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/generators/overview)

## [ObservableProperty](https://docs.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/generators/observableproperty)

### En résumé

- la classe doit dériver de ObservableObject
- attribut ObservableProperty / property \<Name>
- on peut implémenter On\<Name>Changing et On\<Name>Changed

### NotifyPropertyChangedFor : Notifying dependent properties 
 
#### Example

    internal partial class Class1 : ObservableObject
    {

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullName))]
        private string? name0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullName))]
        private string? name1;

        partial void OnName0Changing(string? value)
        {
            Console.WriteLine($"Name0 is about to change to {value}");
        }

        partial void OnName1Changing(string? value)
        {
            Console.WriteLine($"Name1 is about to change to {value}");
        }

        partial void OnName0Changed(string? value)
        {
            Console.WriteLine($"Name0 has changed to {value}");
        }

        partial void OnName1Changed(string? value)
        {
            Console.WriteLine($"Name1 has changed to {value}");
        }

    }

## [RelayCommand attribute](https://docs.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/generators/relaycommand)

### En résumé

- partial class
- attribut RelayCommand / méthode privée \<MethodName> -> ICommand \<MethodName>Command
- attribut RelayCommand / méthode privée 'Task \<MethodName>' -> IAsyncRelayCommand \<MethodName>Command
- MethodName : doit débuter par une majuscule ... 
- CanExecute : [RelayCommand(CanExecute = nameof(CanGreetUser))
- NotifyCanExecuteChangedFor : ObservableProperty + NotifyCanExecuteChangedFor(nameof(GreetUserCommand))
- AllowConcurrentExecutions : seulement pour une méthode asynchrone
  
### Example

    public partial class Class0
    {

        [RelayCommand] // -> IRelayCommand GreetUserCommand
        private void Method0()
        {
            Console.WriteLine($"{nameof(Method0)}");
        }

        [RelayCommand]
        private void Method1(string par0)
        {
            Console.WriteLine($"{nameof(Method1)} par0={par0}");
        }

    }

### Asynchronous commands

### Example

     [RelayCommand] // IAsyncRelayCommand Method2Command
     private async Task Method2(string par0)
     {
         Console.WriteLine($"{nameof(Method1)}(-) par0={par0}");
         await Task.Delay(1000);
         Console.WriteLine($"{nameof(Method1)}(+) par0={par0}");
     }

     // IAsyncRelayCommand Method2Command
     {
         bool _canBeCanceled0 = class0.Method2Command.CanBeCanceled;
         bool _isCancellationRequested0 = class0.Method2Command.IsCancellationRequested;
         bool _isRunning0 = class0.Method2Command.IsRunning;

         Task _t = class0.Method2Command.ExecuteAsync("par2");

         bool _isCancellationRequested1 = class0.Method2Command.IsCancellationRequested;
         bool _isRunning1 = class0.Method2Command.IsRunning;

         _t.Wait();

         bool _isCancellationRequested2 = class0.Method2Command.IsCancellationRequested;
         bool _isRunning2 = class0.Method2Command.IsRunning;
     }

### Enabling and disabling commands

     [RelayCommand(CanExecute = nameof(CanExecuteMethod3))]
     private void Method3(string? par0)
     {
         Console.WriteLine($"{nameof(Method3)} par0={par0}!");
     }

     private bool CanExecuteMethod3(string? par0)
     {
         return par0 is not null;
     }

### NotifyCanExecuteChangedFor

La modification de la 'ObservableProperty' marquée NotifyCanExecuteChangedFor(nameof(GreetUserCommand))
entraine l'appel de la méthode NotifyCanExecuteChanged de la Command dont le nom est passé en paramètre
à l'attribut.

        [RelayCommand]
        private void GreetUser(string? user)
        {
        }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GreetUserCommand))]
        private string? selectedUser;

### [Handling concurrent executions](https://docs.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/generators/relaycommand#handling-concurrent-executions)

 When using the RelayCommand attribute, this can be set via the AllowConcurrentExecutions property. 

### [Handling asynchronous exceptions](https://docs.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/generators/relaycommand#handling-asynchronous-exceptions)

### [Cancel commands for asynchronous operations](https://docs.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/generators/relaycommand#handling-concurrent-executions)
