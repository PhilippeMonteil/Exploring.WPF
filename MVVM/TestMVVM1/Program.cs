
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace TestMVVM1
{

    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Test1();
        }

        static void Test0()
        {
            User user = new User();
            ObservableRecipientUser observableRecipientUser = new ObservableRecipientUser(user);

            observableRecipientUser.PropertyChanging += ObservableRecipientUser_PropertyChanging;
            observableRecipientUser.PropertyChanged += ObservableRecipientUser_PropertyChanged;

            observableRecipientUser.Name = $"{DateTime.Now}";
        }

        static void Test1()
        {
            User user = new User();

            ObservableRecipientUser observableRecipientUser = new ObservableRecipientUser(user);
            observableRecipientUser.PropertyChanging += ObservableRecipientUser_PropertyChanging;
            observableRecipientUser.PropertyChanged += ObservableRecipientUser_PropertyChanged;

            observableRecipientUser.IsActive = true;

            var recipient = new Recipient0();

            WeakReferenceMessenger.Default.Register<ValueChangedMessage<User>>(recipient, (r, m) =>
            {
            });

            observableRecipientUser.Name = $"{DateTime.Now}";
        }

        private static void ObservableRecipientUser_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }

        private static void ObservableRecipientUser_PropertyChanging(object? sender, System.ComponentModel.PropertyChangingEventArgs e)
        {
        }

    }

}
