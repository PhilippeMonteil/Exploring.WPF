
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
            log("Hello, World!");
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
                    log($"options.Enabled={options.Enabled}");
                    log($"options.AutoRetryDelay={options.AutoRetryDelay}");
                    //                    Console.ReadLine();
                }
            }

            {
                IConfiguration configuration = host.Services.GetService<IConfiguration>();

                var options =
                    configuration.GetSection(nameof(TransientFaultHandlingOptions))
                                     .Get<TransientFaultHandlingOptions>();

                {
                    log($"options.Enabled={options.Enabled}");
                    log($"options.AutoRetryDelay={options.AutoRetryDelay}");
                    Console.ReadLine();
                }

            }

        }

        static void log(string Txt)
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}]:'{Txt}'");
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

                    services.AddTransient<Client1>();
                    services.AddTransient<Client2>();
                    services.AddTransient<Client2>();
                })
                .Build();

            // IOptions
            for (int i = 0; i < 2; i++)
            {
                IOptions<TransientFaultHandlingOptions> options = host.Services.GetService<IOptions<TransientFaultHandlingOptions>>();
                log($"options={options.GetHashCode()}");
                log($"options.Value.Enabled={options.Value.Enabled}");
                log($"options.Value.AutoRetryDelay={options.Value.AutoRetryDelay}");
            }

            // IOptionsSnapshot
            for (int i = 0; i < 2; i++)
            {
                Client1 client = host.Services.GetService<Client1>();
                log($"client={client} ...");
                //Console.ReadLine();
            }

            // IOptionsMonitor
            for (int i = 0; i < 2; i++)
            {
                Client2 client = host.Services.GetService<Client2>();
                log($"client={client} ...");
                Console.ReadLine();
            }

        }

    }

}
