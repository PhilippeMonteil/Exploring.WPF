
using CommunityToolkit.Mvvm.Messaging;

namespace TestMessaging0
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            //TestSend0();
            TestReceive0();

            Console.WriteLine("...");
            Console.ReadLine();
        }

        static void TestSend0()
        {
            Recipient0 recipient0 = new Recipient0();

            WeakReferenceMessenger.Default.Register<UserChangedMessage>(recipient0, (r, m) =>
            {
                Console.WriteLine($"r={r} m={m}");
            });

            User user = new User();

            // Send a message from some other module
            WeakReferenceMessenger.Default.Send(new UserChangedMessage(user));

            // Unregisters the recipient from a message type
            WeakReferenceMessenger.Default.Unregister<UserChangedMessage>(recipient0);
        }

        static void TestReceive0()
        {
            Recipient0 recipient0 = new Recipient0();

            User user0 = new User();

            WeakReferenceMessenger.Default.Register<UserRequestMessage>(recipient0, (r, m) =>
            {
                m.Reply(user0);
            });

            {
                User user1 = WeakReferenceMessenger.Default.Send<UserRequestMessage>();
            }

            WeakReferenceMessenger.Default.Unregister<UserRequestMessage>(recipient0);
        }

    }

}
