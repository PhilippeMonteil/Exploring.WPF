
# [MVVM source generators](https://docs.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/generators/overview)

## ObservableProperty

### En r�sum�

- la classe doit d�river de ObservableObject
- attribut ObservableProperty / property \<Name>
- on peut impl�menter On\<Name>Changing et On\<Name>Changed

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

## RelayCommand attribute
