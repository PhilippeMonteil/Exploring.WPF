
using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMVVM0
{

    internal class ObservableObjectUser : ObservableObject
    {
        readonly User user;

        public ObservableObjectUser(User user)
        {
            this.user = user;
        }

        public string Name
        {
            get => this.user.Name;
            set
            {
                SetProperty(this.user.Name, value, user, (u, n) => u.Name = n);
            }
        }

    }

}
