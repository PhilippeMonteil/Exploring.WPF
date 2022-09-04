using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOptionsPattern
{

    internal class Client2
    {
        readonly IOptionsMonitor<TransientFaultHandlingOptions> options;
        IDisposable onChange;

        public Client2(IOptionsMonitor<TransientFaultHandlingOptions> optionsSnapshot)
        {
            this.options = optionsSnapshot;
            onChange = this.options.OnChange(onChangeAction);
        }

        void onChangeAction(TransientFaultHandlingOptions options, string name)
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}]:{GetType().Name}.{nameof(onChangeAction)} options={options} name={name}");
        }

        public override string ToString()
        {
            return $"{GetType().Name}[{GetHashCode()}] options[{options.GetHashCode()}] .CurrentValue.AutoRetryDelay={options.CurrentValue.AutoRetryDelay}";
        }

    }

}
