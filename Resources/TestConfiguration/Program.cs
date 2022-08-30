
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace TestConfiguration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // Build a config object, using env vars and JSON providers.
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Console.WriteLine($"KeyOne = {config["KeyOne"]}");

            //// Get values from the config given their key and their target type.
            //Settings settings = config.GetRequiredSection("Settings").Get<Settings>();

            //// Write the values to the console.
            //Console.WriteLine($"KeyOne = {settings.KeyOne}");
            //Console.WriteLine($"KeyTwo = {settings.KeyTwo}");
            //Console.WriteLine($"KeyThree:Message = {settings.KeyThree.Message}");

        }
    }
}