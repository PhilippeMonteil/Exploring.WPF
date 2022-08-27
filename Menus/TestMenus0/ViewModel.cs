using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace TestMenus0
{

    internal class ViewModel : ObservableObject
    {
        public ViewModel()
        {
        }

        RelayCommand<object> _relayCommand = new RelayCommand<object>(param =>
        {
            Debug.WriteLine($"_relayCommand param={param}");
        },
        param => true);

        public RelayCommand<object> Command => _relayCommand;

    }

}
