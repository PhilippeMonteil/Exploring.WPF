
# [Options pattern in .NET](https://docs.microsoft.com/en-us/dotnet/core/extensions/options)

En r�sum�:

- ConfigurationBinder expose des extensions � IConfiguration qui permettent � partir d'une section
de configuration (un appsettings.json enregistr� au pr�alable) de :

    - 'remplir' une instance (Bind)
    - cr�er et initialiser une instance (Get)
    - cr�er et initialiser une instance � partir d'une sous section nomm�e (GetValue)

- OptionsConfigurationServiceCollectionExtensions expose des extensions � IServiceCollection
(Configure ...) qui permettent d'enregistrer dans un IServiceCollection des services 
IOptions/IOptionsSnapshot/IOptionsMonitor\<TOptions> se chargeant de retrouver dans la configuration
courante la section d�finissant l'�tat d'un TOptions.

- OptionsServiceCollectionExtensions expose des extensions � IServiceCollection
telle AddOptions qui retourne une instance de OptionsBuilder\<TOptions>.
Cette instance de OptionsBuilder se charge d'enregistrer pour son TOptions 
sous forme de services dans son IServiceCollection des actions de Build, PostBuild, Validation.
