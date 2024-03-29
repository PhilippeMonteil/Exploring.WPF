﻿using Microsoft.Extensions.Options;
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
        readonly IOptionsSnapshot<TransientFaultHandlingOptions> options;

        public Client1(IOptionsSnapshot<TransientFaultHandlingOptions> options)
        {
            this.options = options;
        }

        public override string ToString()
        {
            return $"{GetType().Name}[{GetHashCode()}] options[{options.GetHashCode()}] .CurrentValue.AutoRetryDelay={options.Value.AutoRetryDelay}";
        }

    }

}
