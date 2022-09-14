
using CommunityToolkit.Mvvm.Messaging.Messages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMVVM1
{

    internal class UserChangedMessage : ValueChangedMessage<User>
    {

        public UserChangedMessage(User user) : base(user)
        {
        }

    }

}
