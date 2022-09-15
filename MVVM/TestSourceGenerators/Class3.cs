using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSourceGenerators
{

    internal partial class Class3
    {

        [RelayCommand(FlowExceptionsToTaskScheduler = true)]
        async Task Method0()
        {
            throw new Exception($"from {Method0}");

            await Task.Run(() =>
            {
                throw new Exception($"from {Method0}");
            });
        }

        [RelayCommand(FlowExceptionsToTaskScheduler = false)]
        async Task Method1()
        {
            await Task.Run(() =>
            {
                throw new Exception($"from {Method1}");
            });
        }

        [RelayCommand(IncludeCancelCommand = true)]
        private async Task Method2(CancellationToken token)
        {
            await Task.Delay(1000);
        }

    }

}
