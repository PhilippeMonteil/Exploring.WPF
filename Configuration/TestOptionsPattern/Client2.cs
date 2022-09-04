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

        public Client2(IOptionsMonitor<TransientFaultHandlingOptions> optionsSnapshot)
        {
            this.options = optionsSnapshot;
        }

        public override string ToString()
        {
            return $"{GetType().Name}[{GetHashCode()}] options[{options.GetHashCode()}] .CurrentValue.AutoRetryDelay={options.CurrentValue.AutoRetryDelay}";
        }

    }

}
