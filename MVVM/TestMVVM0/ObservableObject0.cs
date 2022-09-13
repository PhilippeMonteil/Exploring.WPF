using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMVVM0
{

    internal class ObservableObject0 : ObservableObject
    {
        string name;

        public override string ToString()
        {
            return $"{GetType().Name}[{GetHashCode()}] Name='{Name}'";
        }

        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
            }
        }
    }

}
