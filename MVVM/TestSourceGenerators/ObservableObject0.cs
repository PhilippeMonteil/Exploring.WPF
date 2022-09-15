
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TestSourceGenerators
{

    internal partial class ObservableObject0 : ObservableObject
    {

        #region --- ObservableProperty, NotifyPropertyChangedFor, ...

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullName))]
        private string? name0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullName))]
        private string? name1;

        public string FullName => $"{name0}:{name1}";

        partial void OnName0Changing(string? value)
        {
            Console.WriteLine($"Name0 is about to change to {value}");
        }
        partial void OnName1Changing(string? value)
        {
            Console.WriteLine($"Name1 is about to change to {value}");
        }

        partial void OnName0Changed(string? value)
        {
            Console.WriteLine($"Name0 has changed to {value}");
        }

        partial void OnName1Changed(string? value)
        {
            Console.WriteLine($"Name1 has changed to {value}");
        }

        #endregion

    }

}
