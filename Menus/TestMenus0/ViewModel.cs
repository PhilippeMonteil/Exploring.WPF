
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
