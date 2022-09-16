
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNavigation
{

    public partial class ViewModel : ObservableObject
    {

        public ViewModel()
        {
            viewContent = new ViewModel0();
        }

        [ObservableProperty]
        Object viewContent;

        [RelayCommand]
        void SetView0()
        {
            this.ViewContent = new ViewModel0();
        }

        [RelayCommand]
        void SetView1()
        {
            this.ViewContent = new ViewModel1();
        }

    }

}
