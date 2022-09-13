using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMessaging0
{

    internal class User
    {
        string m_Name;

        public override string ToString()
        {
            return $"{GetType().Name}[{GetHashCode()}] Name='{Name}'";
        }

        public string Name
        {
            get => m_Name;
            set => m_Name = value;
        }

    }

}
