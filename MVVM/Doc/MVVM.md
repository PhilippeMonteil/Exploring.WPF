
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

