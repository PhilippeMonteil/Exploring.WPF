
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMVVM1
{

    internal class ObservableRecipientUser : ObservableRecipient
    {
        readonly User m_user;

        public ObservableRecipientUser(User user)
        {
            this.m_user = user;
        }

        public string Name
        {
            get => m_user.Name;
            set
            {
                SetProperty(oldValue : m_user.Name, newValue : value, model : m_user, 
                            callback : (r, m) =>
                            {
                                r.Name = m; 
                            });
            }
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            this.Messenger.Register<UserChangedMessage>(this, (r, m) =>
            {

            });
        }

        protected override void OnDeactivated()
        {
            this.Messenger.Unregister<UserChangedMessage>(this);
            base.OnDeactivated();
        }

    }

}
