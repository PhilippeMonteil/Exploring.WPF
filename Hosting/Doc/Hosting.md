
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

