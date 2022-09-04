
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace TestOptionsPattern
{

    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Test1(args);
        }

        static void Test0(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, configuration) =>
                {
                    configuration.Sources.Clear();
                    configuration
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .Build();

            {
                IConfiguration configuration = host.Services.GetService<IConfiguration>();

                TransientFaultHandlingOptions options = new();
                IConfigurationSection configurationSection = configuration.GetSection(nameof(TransientFaultHandlingOptions));
                configurationSection.Bind(options);

                for (int i = 0; i < 1; i++)
                {
                    Console.WriteLine($"options.Enabled={options.Enabled}");
                    Console.WriteLine($"options.AutoRetryDelay={options.AutoRetryDelay}");
                    //                    Console.ReadLine();
                }
            }

            {
                IConfiguration configuration = host.Services.GetService<IConfiguration>();

                var options =
                    configuration.GetSection(nameof(TransientFaultHandlingOptions))
                                     .Get<TransientFaultHandlingOptions>();

                {
                    Console.WriteLine($"options.Enabled={options.Enabled}");
                    Console.WriteLine($"options.AutoRetryDelay={options.AutoRetryDelay}");
                    Console.ReadLine();
                }

            }

        }

        static void Test1(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, configuration) =>
                {
                    configuration.Sources.Clear();
                    configuration
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    var configuration = context.Configuration;
                    services.Configure<TransientFaultHandlingOptions>(
                        configuration.GetSection(nameof(TransientFaultHandlingOptions)));
                })
                .Build();

            {
                IOptions<TransientFaultHandlingOptions> options = host.Services.GetService<IOptions<TransientFaultHandlingOptions>>();
                Console.WriteLine($"options.Value.Enabled={options.Value.Enabled}");
                Console.WriteLine($"options.Value.AutoRetryDelay={options.Value.AutoRetryDelay}");
            }

        }

    }

}
