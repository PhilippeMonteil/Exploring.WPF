
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace TestSourceGenerators
{

    internal class Program
    {

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            await Test5();

            return;

            if (false)
            {
                try
                {
                    await Test4(true);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"e={e.Message}");
                }
            }
            else
            {
                await Test4(true);
            }

            Console.WriteLine("...");
            Console.ReadLine();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine($"{nameof(CurrentDomain_UnhandledException)} sender={sender} e.IsTerminating={e.IsTerminating}");
        }

        private static void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            Console.WriteLine($"{nameof(TaskScheduler_UnobservedTaskException)} sender={sender} e.Observed={e.Observed}");
        }

        static void Test1()
        {
            ObservableObject0 observableObject = new ObservableObject0();
            observableObject.PropertyChanged += Class1_PropertyChanged;
            observableObject.Name0 = $"Name0 / Test1";
            observableObject.Name1 = $"Name1 / Test1";
        }

        static void Test2()
        {
            Class0 class0 = new Class0();

            {
                bool _ok = class0.Method0Command.CanExecute(null);
                class0.Method0Command.Execute(null);
            }
            {
                bool _ok = class0.Method1Command.CanExecute("par0");
                class0.Method1Command.Execute("par1");
            }

            // ExecuteAsync : IAsyncRelayCommand Method2Command
            {
                bool _canBeCanceled0 = class0.Method2Command.CanBeCanceled;
                bool _isCancellationRequested0 = class0.Method2Command.IsCancellationRequested;
                bool _isRunning0 = class0.Method2Command.IsRunning;

                Task _t = class0.Method2Command.ExecuteAsync("par2");

                bool _isCancellationRequested1 = class0.Method2Command.IsCancellationRequested;
                bool _isRunning1 = class0.Method2Command.IsRunning;

                _t.Wait();

                bool _isCancellationRequested2 = class0.Method2Command.IsCancellationRequested;
                bool _isRunning2 = class0.Method2Command.IsRunning;
            }

            // CanExecute
            {
                bool _canExecute = class0.Method3Command.CanExecute(null);
                _canExecute = class0.Method3Command.CanExecute("par0");
                class0.Method3Command.Execute("par0");
            }

        }

        private static void Class1_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Console.WriteLine($"{nameof(Class1_PropertyChanged)} e.PropertyName={e.PropertyName}");
        }

        #region --- Test3

        static void Test3()
        {
            Class1 class1 = new Class1();

            class1.GreetUserCommand.CanExecuteChanged += GreetUserCommand_CanExecuteChanged;
            class1.SelectedUser = "user";
            class1.SelectedUser = null;
        }

        private static void GreetUserCommand_CanExecuteChanged(object? sender, EventArgs e)
        {
        }

        #endregion

        #region --- Test4

        static async Task Test4(bool Method0)
        {
            Class3 class3 = new Class3();

            if (Method0)
            {
                try
                {
                    await class3.Method0Command.ExecuteAsync(null);
                }
                //catch (Exception E)
                //{
                //}
                finally
                {
                }
            }
            else
            {
                try
                {
                    Task _t = class3.Method1Command.ExecuteAsync(null);
                    _t.Wait();
                }
                //catch (Exception E)
                //{
                //}
                finally
                {
                }
            }

        }

        #endregion

        #region --- Test5

        static async Task Test5()
        {
            Class3 class3 = new Class3();

            Task _t = class3.Method2Command.ExecuteAsync(null);

            Thread.Sleep(100);

            {
                ICommand command = class3.Method2Command.CreateCancelCommand();
                command.Execute(null);
            }

            {
                bool _canBeCanceled = class3.Method2Command.CanBeCanceled;
                bool _isCancellationRequested = class3.Method2Command.IsCancellationRequested;
                bool _isRunning = class3.Method2Command.IsRunning;
            }

            await _t;

            {
                bool _canBeCanceled = class3.Method2Command.CanBeCanceled;
                bool _isCancellationRequested = class3.Method2Command.IsCancellationRequested;
                bool _isRunning = class3.Method2Command.IsRunning;
            }

        }

        #endregion

    }

}
