
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSourceGenerators
{

    public partial class Class1 : ObservableObject
    {

        [RelayCommand(CanExecute = nameof(CanGreetUser))]
        private void GreetUser(string? user)
        {
        }

        private bool CanGreetUser(string? user)
        {
            return user is not null;
        }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GreetUserCommand))]
        private string? selectedUser;

    }

}
