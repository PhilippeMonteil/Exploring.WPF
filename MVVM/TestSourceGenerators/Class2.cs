
using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSourceGenerators
{

    internal partial class Class2
    {

        [RelayCommand(AllowConcurrentExecutions = true)]
        async Task Method0()
        {
            await Task.Delay(1000);
        }

    }

}
