
# [Options pattern in .NET](https://docs.microsoft.com/en-us/dotnet/core/extensions/options)

En r�sum�:

- [ConfigurationBinder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.configurationbinder?view=dotnet-plat-ext-6.0) 
expose des extensions � IConfiguration qui permettent � partir d'une section
de configuration (un appsettings.json enregistr� au pr�alable) de :

    - 'remplir' une instance (Bind)
    - cr�er et initialiser une instance (Get)
    - cr�er et initialiser une instance � partir d'une sous section nomm�e (GetValue)

- [OptionsConfigurationServiceCollectionExtensions](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.optionsconfigurationservicecollectionextensions?view=dotnet-plat-ext-6.0) 
expose des extensions � IServiceCollection (Configure ...) qui permettent d'enregistrer dans un IServiceCollection des services 
IOptions/IOptionsSnapshot/IOptionsMonitor\<TOptions> se chargeant de retrouver dans la configuration
courante la section d�finissant l'�tat d'un TOptions.

- [OptionsServiceCollectionExtensions](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.optionsservicecollectionextensions?view=dotnet-plat-ext-6.0) expose des extensions � IServiceCollection
telle AddOptions qui retourne une instance de [OptionsBuilder\<TOptions>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.optionsbuilder-1?view=dotnet-plat-ext-6.0).
Cette instance de OptionsBuilder se charge d'enregistrer pour son TOptions 
sous forme de services dans son IServiceCollection des actions de Build, PostBuild, Validation.


