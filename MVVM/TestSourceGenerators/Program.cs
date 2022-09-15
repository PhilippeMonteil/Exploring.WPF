
namespace TestSourceGenerators
{

    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Test1();
        }

        static void Test1()
        {
            Class1 class1 = new Class1();
            class1.PropertyChanged += Class1_PropertyChanged;
            class1.Name0 = $"Name0 / Test1";
            class1.Name1 = $"Name1 / Test1";
        }

        private static void Class1_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Console.WriteLine($"{nameof(Class1_PropertyChanged)} e.PropertyName={e.PropertyName}");
        }
    }

}
