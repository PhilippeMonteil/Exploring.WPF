
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

    internal partial class Class1 : ObservableObject
    {

        [ObservableProperty]
        private string? name0;

        [ObservableProperty]
        private string? name1;

        partial void OnNameChanging(string? value)
        {
            Console.WriteLine($"Name is about to change to {value}");
        }

        partial void OnNameChanged(string? value)
        {
            Console.WriteLine($"Name has changed to {value}");
        }

    }

}
