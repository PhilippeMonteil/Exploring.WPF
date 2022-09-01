
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace TestHosting0
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
                .ConfigureAppConfiguration((HostBuilderContext builderContext, 
                                            IConfigurationBuilder configurationBuild) =>
                {

                })
                .Build();
        }

    }

}
