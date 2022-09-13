
# [Introduction to the MVVM Toolkit](https://docs.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/)

# [MVVM Community Toolkit](https://devblogs.microsoft.com/dotnet/announcing-the-dotnet-community-toolkit-800/)

## [Sample](https://github.com/CommunityToolkit/MVVM-Samples)

## ObservableObject

### En résumé

- Nuget CommunityToolkit.Mvvm
- using CommunityToolkit.Mvvm.ComponentModel;
- ObservableObject
    - SetProperty(ref name, value);

### Example

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

