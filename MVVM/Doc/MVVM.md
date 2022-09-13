
# [Introduction to the MVVM Toolkit](https://docs.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/)

# [MVVM Community Toolkit](https://devblogs.microsoft.com/dotnet/announcing-the-dotnet-community-toolkit-800/)

## [Sample](https://github.com/CommunityToolkit/MVVM-Samples)

## ObservableObject

### En résumé

- Nuget CommunityToolkit.Mvvm
- using CommunityToolkit.Mvvm.ComponentModel;
- ObservableObject
    - SetProperty(ref name, value);
    - SetProperty(this.user.Name, value, user, (u, n) => u.Name = n);
    - private TaskNotifier<int>? requestTask;
      set => SetPropertyAndNotifyOnCompletion(ref requestTask, value);

### Examples

````
    internal class ObservableObject0 : ObservableObject
    {
        string name;

        public override string ToString()
        {
            return $"{GetType().Name}[{GetHashCode()}] Name='{Name}'";
        }

        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
            }
        }
    }
````

#### Wrapping a non-observable model

````
    internal class ObservableObjectUser : ObservableObject
    {
        readonly User user;

        public ObservableObjectUser(User user)
        {
            this.user = user;
        }

        public string Name
        {
            get => this.user.Name;
            set
            {
                SetProperty(this.user.Name, value, user, (u, n) => u.Name = n);
            }
        }

    }
````

#### Handling Task<T> properties

````
    public class MyModel : ObservableObject
    {
        private TaskNotifier<int>? requestTask;

        public Task<int>? RequestTask
        {
            get => requestTask;
            set => SetPropertyAndNotifyOnCompletion(ref requestTask, value);
        }

        public void RequestValue()
        {
            RequestTask = Task<int>.Run(() =>
            {
                Thread.Sleep(1000);
                return 777;
            });
        }

    }
````

#### [ObservableObject.TaskNotifier<T> Class](https://docs.microsoft.com/en-us/dotnet/api/microsoft.toolkit.mvvm.componentmodel.ObservableObject.TaskNotifier-1?view=win-comm-toolkit-dotnet-7.0)

##### Operators

    Implicit(ObservableObject.TaskNotifier to Task<T>) 

## [ObservableRecipient](https://docs.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/observablerecipient)

> The ObservableRecipient type is a base class for observable objects that also acts as recipients for messages. 
This class is an extension of ObservableObject which also provides built-in support to use the IMessenger type.

### [ObservableRecipient Class](https://docs.microsoft.com/en-us/dotnet/api/microsoft.toolkit.mvvm.componentmodel.ObservableRecipient?view=win-comm-toolkit-dotnet-7.0)

    public abstract class ObservableRecipient : ObservableObject

#### Constructors

    protected ObservableRecipient (IMessenger messenger);

    // utilise WeakReferenceMessenger.Default 
    protected ObservableRecipient ();

#### Properties

    public bool IsActive { get; set; }
    protected IMessenger Messenger { get; }

#### Methods

    protected virtual void Broadcast<T> (T oldValue, T newValue, string? propertyName);
    protected virtual void OnActivated ();
    protected virtual void OnDeactivated ();

    protected bool SetProperty<T> (ref T field, T newValue, bool broadcast, string? propertyName = default);
    ...
    protected bool SetProperty<TModel,T> (T oldValue, T newValue, 
                                            IEqualityComparer<T> comparer, 
                                            TModel model, 
                                            Action<TModel,T> callback, bool broadcast, string? propertyName = default) 
                                            where TModel : class;


