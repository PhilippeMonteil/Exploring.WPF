
using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMVVM0
{

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

}
