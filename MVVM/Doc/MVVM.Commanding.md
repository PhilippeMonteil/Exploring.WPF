
# [Commanding](https://docs.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/relaycommand)

## [ICommand Interface](https://docs.microsoft.com/en-us/dotnet/api/system.windows.input.icommand?view=net-6.0)

	public bool CanExecute (object? parameter);
	public void Execute (object? parameter);
	event EventHandler? CanExecuteChanged;

## [IRelayCommand Interface](https://docs.microsoft.com/en-us/dotnet/api/microsoft.toolkit.mvvm.input.irelaycommand?view=win-comm-toolkit-dotnet-7.0)

	public interface IRelayCommand : System.Windows.Input.ICommand

	// Notifies that the CanExecute(Object) property has changed.
	public void NotifyCanExecuteChanged ();

## [IRelayCommand<T> Interface](https://docs.microsoft.com/en-us/dotnet/api/microsoft.toolkit.mvvm.input.irelaycommand-1?view=win-comm-toolkit-dotnet-7.0)

	public interface IRelayCommand<in T> : Microsoft.Toolkit.Mvvm.Input.IRelayCommand, 
											System.Windows.Input.ICommand

	public bool CanExecute (T? parameter);
	public void Execute (T? parameter);

## [RelayCommand and RelayCommand<T>](https://docs.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/relaycommand)

The RelayCommand and RelayCommand\<T\> are ICommand implementations that can expose a method or delegate to the view. These types act as a way to bind commands between the viewmodel and UI elements.

## [RelayCommand\<T> Class](https://docs.microsoft.com/en-us/dotnet/api/microsoft.toolkit.mvvm.input.RelayCommand-1?view=win-comm-toolkit-dotnet-7.0)

A generic command whose sole purpose is to relay its functionality to other objects by invoking delegates. 
The default return value for the CanExecute method is true. 
This class allows you to accept command parameters in the Execute(T) and CanExecute(T) callback methods.
