
# [Introduction to the MVVM Toolkit](https://docs.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/)

# [MVVM Community Toolkit](https://devblogs.microsoft.com/dotnet/announcing-the-dotnet-community-toolkit-800/)

## [Mvvm.Messaging](https://docs.microsoft.com/en-us/dotnet/api/microsoft.toolkit.mvvm.messaging?view=win-comm-toolkit-dotnet-7.0)

### [IMessenger Interface](https://docs.microsoft.com/en-us/dotnet/api/microsoft.toolkit.mvvm.messaging.imessenger?view=win-comm-toolkit-dotnet-7.0)
### [IRecipient\<TMessage\> Interface](https://docs.microsoft.com/en-us/dotnet/api/microsoft.toolkit.mvvm.messaging.irecipient-1?view=win-comm-toolkit-dotnet-7.0)
### [IMessengerExtensions Class](https://docs.microsoft.com/en-us/dotnet/api/microsoft.toolkit.mvvm.messaging.imessengerextensions?view=win-comm-toolkit-dotnet-7.0)
### [StrongReferenceMessenger Class](https://docs.microsoft.com/en-us/dotnet/api/microsoft.toolkit.mvvm.messaging.strongreferencemessenger?view=win-comm-toolkit-dotnet-7.0)
### [WeakReferenceMessenger Class](https://docs.microsoft.com/en-us/dotnet/api/microsoft.toolkit.mvvm.messaging.weakreferencemessenger?view=win-comm-toolkit-dotnet-7.0)

### MessageHandler

    public delegate void MessageHandler<in TRecipient, in TMessage>(TRecipient recipient, TMessage message)
                                        where TRecipient : class
                                        where TMessage : class;

### Example

#### Register / Send / Unregister

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

#### Register / Send / Unregister

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
