
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSourceGenerators
{

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

        [RelayCommand] // IAsyncRelayCommand Method2Command
        private async Task Method2(string par0)
        {
            Console.WriteLine($"{nameof(Method1)}(-) par0={par0}");
            await Task.Delay(1000);
            Console.WriteLine($"{nameof(Method1)}(+) par0={par0}");
        }

        [RelayCommand(CanExecute = nameof(CanExecuteMethod3))]
        private void Method3(string? par0)
        {
            Console.WriteLine($"{nameof(Method3)} par0={par0}!");
        }

        private bool CanExecuteMethod3(string? par0)
        {
            return par0 is not null;
        }

    }

}
