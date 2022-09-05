
# [Options pattern in .NET](https://docs.microsoft.com/en-us/dotnet/core/extensions/options)

En résumé:

- ConfigurationBinder expose des extensions à IConfiguration qui permettent à partir d'une section
de configuration (un appsettings.json enregistré au préalable) de :

    - 'remplir' une instance (Bind)
    - créer et initialiser une instance (Get)
    - créer et initialiser une instance à partir d'une sous section nommée (GetValue)

- OptionsConfigurationServiceCollectionExtensions expose des extensions à IServiceCollection
(Configure ...) qui permettent d'enregistrer dans un IServiceCollection des services 
IOptions/IOptionsSnapshot/IOptionsMonitor\<TOptions> se chargeant de retrouver dans la configuration
courante la section définissant l'état d'un TOptions.

- OptionsServiceCollectionExtensions expose des extensions à IServiceCollection
telle AddOptions qui retourne une instance de OptionsBuilder\<TOptions>.
Cette instance de OptionsBuilder se charge d'enregistrer pour son TOptions 
sous forme de services dans son IServiceCollection des actions de Build, PostBuild, Validation.
