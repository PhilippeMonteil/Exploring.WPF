using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOptionsPattern
{

    public class TransientFaultHandlingOptions
    {
        public bool Enabled { get; set; }
        public TimeSpan AutoRetryDelay { get; set; }

        public override string ToString()
        {
            return $"{GetType().Name}[{GetHashCode()}] .Enabled={Enabled} .AutoRetryDelay={AutoRetryDelay}";
        }

    }

}
