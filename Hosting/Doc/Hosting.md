
# [.NET Generic Host](https://docs.microsoft.com/en-us/dotnet/core/extensions/generic-host)

## Microsoft.Extensions.Hosting

## [Host](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.host?view=dotnet-plat-ext-6.0)

### Class

	public static class Host

### Methods

	public static IHostBuilder CreateDefaultBuilder (string[] args);

The following defaults are applied to the returned HostBuilder:

- Set the ContentRootPath to the result of GetCurrentDirectory().
- Load host IConfiguration from "DOTNET_" prefixed environment variables.
- Load host IConfiguration from supplied command line arguments.
- Load app IConfiguration from 'appsettings.json' and 'appsettings.[EnvironmentName].json'.
- Load app IConfiguration from User Secrets when EnvironmentName is 'Development' using the entry assembly.
- Load app IConfiguration from environment variables.
- Load app IConfiguration from supplied command line arguments.
- Configure the ILoggerFactory to log to the console, debug, and event source output.
- Enable scope validation on the dependency injection container when EnvironmentName is 'Development'.

## [IHostBuilder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.ihostbuilder?view=dotnet-plat-ext-6.0)

### Methods

	public IHost Build ();

	public IHostBuilder ConfigureAppConfiguration (Action<HostBuilderContext,IConfigurationBuilder> configureDelegate);

	public Microsoft.Extensions.Hosting.IHostBuilder ConfigureServices (Action<Microsoft.Extensions.Hosting.HostBuilderContext,Microsoft.Extensions.DependencyInjection.IServiceCollection> configureDelegate);

## [HostBuilderContext](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.hostbuildercontext?view=dotnet-plat-ext-6.0)

## [IConfigurationBuilder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfigurationbuilder?view=dotnet-plat-ext-6.0)

## [IHost Interface](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.ihost?view=dotnet-plat-ext-6.0)

### Parameters

	public IServiceProvider Services { get; }

### Methods

	public Task StartAsync (CancellationToken cancellationToken = default);
	public Task StopAsync (CancellationToken cancellationToken = default);

## [IHostedService](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.ihostedservice?view=dotnet-plat-ext-6.0)

### Methods

	public Task StartAsync (System.Threading.CancellationToken cancellationToken);
	public Task StopAsync (System.Threading.CancellationToken cancellationToken);

## [Building a Console App with .NET Generic Host](https://dfederm.com/building-a-console-app-with-.net-generic-host/)

- créer une classe ConsoleHostedService exposant IHostedService

- enregistrer une instance de cette classe comme 'Hosted Service'

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

- les méthodes IHostedService exposées par ConsoleHostedService sont appelées :

	public Task StartAsync (System.Threading.CancellationToken cancellationToken);

	public Task StopAsync (System.Threading.CancellationToken cancellationToken);

- StartAsync enregistre des handlers auprès des CancellationToken exposés par IHostApplicationLifetime :

```
    CancellationToken ApplicationStarted { get; }

    CancellationToken ApplicationStopping { get; }

    CancellationToken ApplicationStopped { get; }
```

comme :

```
     _appLifetime.ApplicationStarted.Register(() =>
     {
         Task.Run(async () =>
         {
             try
             {
                _logger.LogInformation("ConsoleHostedService : Hello World!");

                // Simulate real work is being done
                await Task.Delay(1000);
             }
             catch (Exception ex)
             {
                _logger.LogError(ex, "Unhandled exception!");
             }
             finally
             {
                // Stop the application once the work is done
                _appLifetime.StopApplication();
             }
         });
     });
```

- le code enregistré auprès de .ApplicationStarted doit déclencher la fin de l'appication :

    _appLifetime.StopApplication();
