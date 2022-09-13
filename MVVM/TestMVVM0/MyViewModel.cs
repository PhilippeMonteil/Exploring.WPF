
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMVVM0
{

    public class MyViewModel : ObservableRecipient, IRecipient<LoggedInUserRequestMessage>
    {
        public void Receive(LoggedInUserRequestMessage message)
        {
            // Handle the message here
        }

        protected override void OnActivated()
        {
            base.OnActivated();
        }

    }

    public class LoggedInUserRequestMessage
    {
    }

}
