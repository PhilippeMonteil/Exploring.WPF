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
        readonly IOptionsMonitor<TransientFaultHandlingOptions> optionsMonitor;

        public Client2(IOptionsMonitor<TransientFaultHandlingOptions> optionsSnapshot)
        {
            this.optionsMonitor = optionsSnapshot;
        }

        public override string ToString()
        {
            return $"{GetType().Name}[{GetHashCode()}] optionsMonitor[{optionsMonitor.GetHashCode()}] .CurrentValue.AutoRetryDelay={optionsMonitor.CurrentValue.AutoRetryDelay}";
        }

    }

}
