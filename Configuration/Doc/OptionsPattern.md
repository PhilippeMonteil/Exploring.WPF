
# [Options pattern in .NET](https://docs.microsoft.com/en-us/dotnet/core/extensions/options)

## [IConfigurationRoot](https://docs.microsoft.com/fr-fr/dotnet/api/microsoft.extensions.configuration.iconfigurationroot?view=dotnet-plat-ext-6.0)

### Methods

    public void Reload ();

## [IConfiguration](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration?view=dotnet-plat-ext-6.0)

### Methods

	public IConfigurationSection GetSection (string key);

## [ConfigurationBinder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.configurationbinder?view=dotnet-plat-ext-6.0)

Classe exposant des extensions de l'interface IConfiguration

### Methods

    public static void Bind (this IConfiguration configuration, object instance);
    public static object Get (this IConfiguration configuration, Type type);
    public static object GetValue (this IConfiguration configuration, Type type, string key);

### Exemple : 'Bind'/'Get' d'une instance à une section de configuration

     using IHost host = Host.CreateDefaultBuilder(args)
         .ConfigureAppConfiguration((hostingContext, configuration) =>
         {
             configuration.Sources.Clear();
             configuration
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
         })
         .Build();

     // Bind
     {
         IConfiguration configuration = host.Services.GetService<IConfiguration>();

         TransientFaultHandlingOptions options = new();
         IConfigurationSection configurationSection = configuration.GetSection(nameof(TransientFaultHandlingOptions));
         configurationSection.Bind(options);

         Console.WriteLine($"TransientFaultHandlingOptions.Enabled={options.Enabled}");
         Console.WriteLine($"TransientFaultHandlingOptions.AutoRetryDelay={options.AutoRetryDelay}");
     }

     // Get<T>
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

### Exemple : ConfigureServices, Configure/GetSection

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

## [IOptions<TOptions>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.ioptions-1?view=dotnet-plat-ext-6.0) 

## [IOptionsSnapshot<TOptions>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.ioptionssnapshot-1?view=dotnet-plat-ext-6.0)

## [IOptionsMonitor<TOptions>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.ioptionsmonitor-1?view=dotnet-plat-ext-6.0)
