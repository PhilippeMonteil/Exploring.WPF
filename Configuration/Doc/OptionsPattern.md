
# [Options pattern in .NET](https://docs.microsoft.com/en-us/dotnet/core/extensions/options)

## En résumé:

- [ConfigurationBinder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.configurationbinder?view=dotnet-plat-ext-6.0) 
expose des extensions à IConfiguration qui permettent à partir d'une section
de configuration (un appsettings.json enregistré au préalable) de :

    - 'remplir' une instance (Bind)
    - créer et initialiser une instance (Get)
    - créer et initialiser une instance à partir d'une sous section nommée (GetValue)

- [OptionsConfigurationServiceCollectionExtensions](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.optionsconfigurationservicecollectionextensions?view=dotnet-plat-ext-6.0) 
expose des extensions à IServiceCollection (Configure ...) qui permettent d'enregistrer dans un IServiceCollection des services 
IOptions/IOptionsSnapshot/IOptionsMonitor\<TOptions> se chargeant de retrouver dans la configuration
courante la section définissant l'état d'un TOptions.

- [OptionsServiceCollectionExtensions](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.optionsservicecollectionextensions?view=dotnet-plat-ext-6.0) expose des extensions à IServiceCollection
telle AddOptions qui retourne une instance de [OptionsBuilder\<TOptions>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.optionsbuilder-1?view=dotnet-plat-ext-6.0).
Cette instance de OptionsBuilder se charge d'enregistrer pour son TOptions 
sous forme de services dans son IServiceCollection des actions de Build, PostBuild, Validation.

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
             configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
         })
         .Build();

     // Bind / Section
     {
         IConfiguration configuration = host.Services.GetService<IConfiguration>();

         TransientFaultHandlingOptions options = new();
         IConfigurationSection configurationSection = configuration.GetSection(nameof(TransientFaultHandlingOptions));
         configurationSection.Bind(options);

         Console.WriteLine($"TransientFaultHandlingOptions.Enabled={options.Enabled}");
         Console.WriteLine($"TransientFaultHandlingOptions.AutoRetryDelay={options.AutoRetryDelay}");
     }

     // Get<T> / Section
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

## [OptionsConfigurationServiceCollectionExtensions](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.optionsconfigurationservicecollectionextensions?view=dotnet-plat-ext-6.0)

Extensions de l'interface IServiceCollection permettant de configurer 
les Options portant sur un type \<TOptions> donné'.

Extension methods for adding configuration related options services to the DI container.

### Methods

    public static IServiceCollection Configure<TOptions> (this IServiceCollection services, 
                                                        IConfiguration config) 
                                                        where TOptions : class;

Registers a configuration instance that TOptions will bind against, 
and updates the options when the configuration changes.

### Exemple : ConfigureServices, Configure/GetSection

#### appsettins.json :

```
{
  "SecretKey": "Secret key value",
  "TransientFaultHandlingOptions": {
    "Enabled": true,
    "AutoRetryDelay": "00:00:07"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```

#### source code :

     public class TransientFaultHandlingOptions
     {
        ...
     }

     using IHost host = Host.CreateDefaultBuilder(args)
         .ConfigureAppConfiguration((context, configuration) =>
         {
             configuration.Sources.Clear();
             configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
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

## Named options support using IConfigureNamedOptions

Association d'un Type (Features), d'un nom (Features.Personalize) 
et d'une section de configuration ("Features:Personalize")

### Exemple

     public class Features
     {
        public const string Personalize = nameof(Personalize);
        ...
     }

     services.Configure<Features>(
         Features.Personalize,
         Configuration.GetSection("Features:Personalize"));

puis :

     readonly Features _personalizeFeature;

     public Service(IOptionsSnapshot<Features> namedOptionsAccessor)
     {
         _personalizeFeature = namedOptionsAccessor.Get(Features.Personalize);

     public class Features
     {
        public const string Personalize = nameof(Personalize);
        public const string WeatherStation = nameof(WeatherStation);

        public bool Enabled { get; set; }
        public string ApiKey { get; set; }
     }

     public class Service
     {
        private readonly Features _personalizeFeature;
        private readonly Features _weatherStationFeature;

        public Service(IOptionsSnapshot<Features> namedOptionsAccessor)
        {
            _personalizeFeature = namedOptionsAccessor.Get(Features.Personalize);
            _weatherStationFeature = namedOptionsAccessor.Get(Features.WeatherStation);
        }

        public override string ToString()
        {
            return $"{GetType().Name}[{GetHashCode()}] _personalizeFeature={_personalizeFeature} _weatherStationFeature={_weatherStationFeature}";
        }

     }

     ConfigureServices(services =>
     {
        services.Configure<Features>(
            Features.Personalize,
            Configuration.GetSection("Features:Personalize"));

        services.Configure<Features>(
            Features.WeatherStation,
            Configuration.GetSection("Features:WeatherStation"));

        services.AddTransient<Service>();
     });

     {
        IServiceProvider serviceProvider = host.Services;
        Service service = serviceProvider.GetService<Service>();
     }

## [OptionsServiceCollectionExtensions Class](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.optionsservicecollectionextensions?view=dotnet-plat-ext-6.0)

Extension methods for adding options services to the DI container.

### [OptionsServiceCollectionExtensions.AddOptions<TOptions>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.optionsservicecollectionextensions.addoptions?view=dotnet-plat-ext-6.0#microsoft-extensions-dependencyinjection-optionsservicecollectionextensions-addoptions(microsoft-extensions-dependencyinjection-iservicecollection)) 

    public static OptionsBuilder<TOptions> AddOptions<TOptions> (this IServiceCollection services) where TOptions : class;

Gets an options builder that forwards Configure calls for the same named TOptions to the underlying service collection.

## [OptionsBuilder<TOptions> Class](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.optionsbuilder-1?view=dotnet-plat-ext-6.0)

Classe ajoutant des Options à une IServiceCollection.

### Constructeur

    public OptionsBuilder (IServiceCollection services, string name);

- services : The IServiceCollection for the options being configured.

- name : The default name of the TOptions instance, if null DefaultName is used.

### Methods

     public virtual OptionsBuilder<TOptions> Configure (Action<TOptions> configureOptions);

Registers an action used to configure a particular type of options. 
These are run before all PostConfigure(Action\<TOptions>).

    public virtual OptionsBuilder<TOptions> PostConfigure (Action<TOptions> configureOptions);

Registers an action used to configure a particular type of options. 
These are run after all Configure(Action\<TOptions>).

    public virtual OptionsBuilder<TOptions> Validate (Func<TOptions,bool> validation);

Register a validation action for an options type using a default failure message.

## [Options validation](https://docs.microsoft.com/en-us/dotnet/core/extensions/options#options-validation)

### AddOptions / Bind / ValidateDataAnnotations

#### Example

appsettings.json:

    {
        "MyCustomSettingsSection": 
        {
            "SiteTitle": "Amazing docs from Awesome people!",
            "Scale": 10,
            "VerbosityLevel": 32
        }
    }

source:

    using System.ComponentModel.DataAnnotations;

    namespace ConsoleJson.Example;

    public class SettingsOptions
    {
        public const string ConfigurationSectionName = "MyCustomSettingsSection";

        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$")]
        public string SiteTitle { get; set; } = null!;

        [Range(0, 1000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Scale { get; set; }

        public int VerbosityLevel { get; set; }
    }

    services.AddOptions<SettingsOptions>()
        .Bind(Configuration.GetSection(SettingsOptions.ConfigurationSectionName))
        .ValidateDataAnnotations();

### [IValidateOptions for complex validation](https://docs.microsoft.com/en-us/dotnet/core/extensions/options#ivalidateoptions-for-complex-validation)

- création d'une classe ValidateSettingsOptions exposant IValidateOptions

- mise en oeuvre :

    // connexion de la classe SettingsOptions avec la section nommée 
    // SettingsOptions.ConfigurationSectionName de Configuration 
    services.Configure<SettingsOptions>(Configuration.GetSection(SettingsOptions.ConfigurationSectionName));

    // ajout à services d'un ServiceDescriptor :
    // - Singleton
    // - interface IValidateOptions<SettingsOptions>
    // - type l'implémentant : ValidateSettingsOptions
    services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<SettingsOptions>,ValidateSettingsOptions>());

On suppose que les interfaces IValidateOptions<TOptions> enregistrées dans l'injection de dépendance 
sont parcourues et invoquées lors de chaque production d'une instance de TOptions 

#### [ServiceCollectionDescriptorExtensions.TryAddEnumerable](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.extensions.servicecollectiondescriptorextensions.tryaddenumerable?view=dotnet-plat-ext-6.0)

### [Options post-configuration](https://docs.microsoft.com/en-us/dotnet/core/extensions/options#options-post-configuration)

Associer à un type TOptions une Action<TOptions> qui sera appliquée à chaque instance de TOptions produite, éventuellement nommé,
voire à toutes.

#### [OptionsServiceCollectionExtensions.PostConfigure](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.optionsservicecollectionextensions.postconfigure?view=dotnet-plat-ext-6.0)

    public static IServiceCollection PostConfigure<TOptions> (this IServiceCollection services, Action<TOptions> configureOptions) where TOptions : class;

#### Examples

    services.PostConfigure<CustomOptions>(customOptions =>
    {
        customOptions.Option1 = "post_configured_option1_value";
    });

    services.PostConfigure<CustomOptions>("named_options_1", customOptions =>
    {
        customOptions.Option1 = "post_configured_option1_value";
    });

    services.PostConfigureAll<CustomOptions>(customOptions =>
    {   
        customOptions.Option1 = "post_configured_option1_value";
    });

## [IOptions<TOptions>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.ioptions-1?view=dotnet-plat-ext-6.0) 

## [IOptionsSnapshot<TOptions>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.ioptionssnapshot-1?view=dotnet-plat-ext-6.0)

## [IOptionsMonitor<TOptions>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.ioptionsmonitor-1?view=dotnet-plat-ext-6.0)

