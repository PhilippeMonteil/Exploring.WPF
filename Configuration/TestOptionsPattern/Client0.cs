using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOptionsPattern
{

    internal class Client0
    {
        readonly IOptions<TransientFaultHandlingOptions> options;

        public Client0(IOptions<TransientFaultHandlingOptions> options)
        {
            this.options = options;
        }

        public override string ToString()
        {
            return $"{GetType().Name}[{GetHashCode()}] options[{options.GetHashCode()}] .Value.AutoRetryDelay={options.Value.AutoRetryDelay}";
        }

    }

}
