
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TestConfiguration
{

    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            //Test0();
            Test1(args);
        }

        static void Test0()
        {
            // Build a config object, using env vars and JSON providers.
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json") // nécessite la présence du fichier dans le répertoire courant
                .AddEnvironmentVariables()
                .Build();

            // Get values from the config given their key and their target type.
            Settings settings = config.GetRequiredSection("Settings").Get<Settings>();

            // Write the values to the console.
            Console.WriteLine($"KeyOne = {settings.KeyOne}");
            Console.WriteLine($"KeyTwo = {settings.KeyTwo}");
            Console.WriteLine($"KeyThree:Message = {settings.KeyThree.Message}");

        }

        static void Test1(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args).Build();

            // Ask the service provider for the configuration abstraction.
            IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

            // Get values from the config given their key and their target type.
            int keyOneValue = config.GetValue<int>("KeyOne");
            bool keyTwoValue = config.GetValue<bool>("KeyTwo");
            string keyThreeNestedValue = config.GetValue<string>("KeyThree:Message");

            // Write the values to the console.
            Console.WriteLine($"KeyOne = {keyOneValue}");
            Console.WriteLine($"KeyTwo = {keyTwoValue}");
            Console.WriteLine($"KeyThree:Message = {keyThreeNestedValue}");

        }

    }

    public sealed class Settings
    {
        public int KeyOne { get; set; }
        public bool KeyTwo { get; set; }
        public NestedSettings KeyThree { get; set; } = null!;
    }
    
    public sealed class NestedSettings
    {
        public string Message { get; set; } = null!;
    }

}
