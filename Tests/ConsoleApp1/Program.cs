
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp1
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Test0(args);
        }

        static void Test0(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((HostBuilderContext context, IConfigurationBuilder configurationBuilder) =>
                {
                    configurationBuilder.AddJsonFile("appsettings.json");
                }).Build();

            IConfiguration configuration = host.Services.GetRequiredService<IConfiguration>();
            int value = configuration.GetSection("Settings").GetValue<int>("KeyOne");
        }

    }

}
