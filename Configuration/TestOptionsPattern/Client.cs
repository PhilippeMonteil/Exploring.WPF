using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOptionsPattern
{

    internal class Client1
    {
        readonly IOptionsSnapshot<TransientFaultHandlingOptions> optionsSnapshot;

        public Client1(IOptionsSnapshot<TransientFaultHandlingOptions> optionsSnapshot)
        {
            this.optionsSnapshot = optionsSnapshot;
        }

        public override string ToString()
        {
            return $"{GetType().Name}[{GetHashCode()}] optionsSnapshot[{optionsSnapshot.GetHashCode()}] .Value.AutoRetryDelay={optionsSnapshot.Value.AutoRetryDelay}";
        }

    }

}
