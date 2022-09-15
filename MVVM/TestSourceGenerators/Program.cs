
using System.Windows.Input;

namespace TestSourceGenerators
{

    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Test2();
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

            {
                Task _t = class0.Method2Command.ExecuteAsync("par2");
                _t.Wait();
            }

        }

        private static void Class1_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Console.WriteLine($"{nameof(Class1_PropertyChanged)} e.PropertyName={e.PropertyName}");
        }
    }

}
