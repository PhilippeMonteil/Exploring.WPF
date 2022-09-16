
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
        void SetView(string viewName)
        {
            if (string.Compare(viewName, "View0", true) == 0)
            {
                this.ViewContent = new ViewModel0();
                return;
            }
            if (string.Compare(viewName, "View1", true) == 0)
            {
                this.ViewContent = new ViewModel1();
                return;
            }
            this.ViewContent = null;
        }

    }

}
