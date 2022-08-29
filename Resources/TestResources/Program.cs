
using TestResources.Properties;

namespace TestResources
{

    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Resource0.String1={Resource0.String1}");

            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Resource0.String1={Resource0.String1}");

            Task.Run(() =>
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr");
                Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Resource0.String1={Resource0.String1}");
            });

            Console.WriteLine("...");
            Console.ReadLine();
        }

    }

}
