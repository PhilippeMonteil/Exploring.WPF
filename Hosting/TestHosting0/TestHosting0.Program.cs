
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TestHosting0
{

    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            await Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<ConsoleHostedService>();
                })
                .RunConsoleAsync();

            // Test0(args);
        }

        static void Test0(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((HostBuilderContext builderContext, 
                                            IConfigurationBuilder configurationBuild) =>
                {

                })
                .Build();
        }

    }

}
