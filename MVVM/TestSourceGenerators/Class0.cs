
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

        [RelayCommand]
        private async Task Method2(string par0)
        {
            Console.WriteLine($"{nameof(Method1)}(-) par0={par0}");
            await Task.Delay(1000);
            Console.WriteLine($"{nameof(Method1)}(+) par0={par0}");
        }

    }

}
