
using System.Runtime.CompilerServices;

namespace TestMVVM0
{

    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Test1();

            Console.WriteLine("...");
            Console.ReadLine();
        }

        static void log(string Txt, [CallerMemberName] string member = null)
        {
            Console.WriteLine($"{member}:{Txt}");
        }

        static void Test0()
        {
            ObservableObject0 _instance = new ObservableObject0();
            _instance.PropertyChanging += _instance_PropertyChanging;
            _instance.PropertyChanged += _instance_PropertyChanged;
            _instance.Name = $"Name={DateTime.Now}";
        }

        static void Test1()
        {
            log("(-)");
            MyModel myModel = new MyModel();
            myModel.PropertyChanging += _instance_PropertyChanging;
            myModel.PropertyChanged += _instance_PropertyChanged;
            myModel.RequestValue();
            log("(+)");
        }

        private static void _instance_PropertyChanging(object? sender, System.ComponentModel.PropertyChangingEventArgs e)
        {
            log($"sender={sender} e.PropertyName={e.PropertyName}");
        }

        private static void _instance_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            log($"sender={sender} e.PropertyName={e.PropertyName}");
        }

    }

}