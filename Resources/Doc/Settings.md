
# Settings

## [Manage application settings](https://docs.microsoft.com/en-us/visualstudio/ide/managing-application-settings-dotnet?view=vs-2022)

### En résumé

- l'ajout d'un ou plusieurs .settings crée automatiquement un fichier App.config
- les settings peuvent être de scope 'User' ou 'Application'
- ils se retrouvent dans des tags userSettings et applicationSettings dans App.config
- à chaque fichier .settings correspond une classe 
    public sealed partial class Settings1 : global::System.Configuration.ApplicationSettingsBase

- à chaque setting correspond une propriété :
 
    - get / set pour les scope 'User' 

        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Value0")]
        public string Setting0 {
            get {
                return ((string)(this["Setting0"]));
            }
            set {
                this["Setting0"] = value;
            }
        }
 
    - get pour les scope 'Application' 

        public string Setting2 {
            get {
                return ((string)(this["Setting2"]));
            }
        }

- inspection d'un Settings

     foreach (SettingsProperty property in settings1.Properties)
     {
         Debug.WriteLine($"    property={property}");
         Debug.WriteLine($"    .Provider={property.Provider}");
         Debug.WriteLine($"    .PropertyType={property.PropertyType}");
         Debug.WriteLine($"    '{settings1[property.Name]}'");
     }

- modification et sauvegarde d'un Settings de scope 'User'

    settings1.Setting0 = $"Setting0 {DateTime.Now}";
    settings1.Save();

- fichier de sauvegarde d'un Settings de scope 'User'

Elles sont enregistrées dans un fichier user.config dans un répertoire 
C:\Users\[Philippe]\AppData\Local\[TestSettings]\... comme : 

    C:\Users\Philippe\AppData\Local\TestSettings\TestSettings_Url_bfuncae2vcw4t22bfmcatpz2ll2ijfdq\1.0.0.0

### Tests

- ajouter le Nuget 'System.Configuration.ConfigurationManager'
- ajouter un 'New Item' / 'Settings Files' : Settings.settings
- modifier sa visibilité : 'internal' -> 'public'
- effets de l'ajout:
 
    - un fichier __App.config__ est créé avec une section __userSettings__ et une section __applicationSettings__

```
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <configSections>
        <sectionGroup name="userSettings" type="System.Configuration.UserSettingsGroup, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" >
            <section name="TestSettings.Settings1" type="System.Configuration.ClientSettingsSection, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" allowExeDefinition="MachineToLocalUser" requirePermission="false" />
        </sectionGroup>
        <sectionGroup name="applicationSettings" type="System.Configuration.ApplicationSettingsGroup, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" >
            <section name="TestSettings.Settings1" type="System.Configuration.ClientSettingsSection, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" requirePermission="false" />
        </sectionGroup>
    </configSections>
    <userSettings>
        <TestSettings.Settings1>
            <setting name="Setting0" serializeAs="String">
                <value>Value0</value>
            </setting>
        </TestSettings.Settings1>
    </userSettings>
    <applicationSettings>
        <TestSettings.Settings1>
            <setting name="Setting1" serializeAs="String">
                <value>Value1</value>
            </setting>
        </TestSettings.Settings1>
    </applicationSettings>
</configuration>
```

    - une classe __Settings1__ est générée :

    public sealed partial class Settings1 : global::System.Configuration.ApplicationSettingsBase

    - les valeurs de scope 'User' sont modifiables, pas celles de scope 'Application',
      elles sont enregistrées dans un fichier user.config dans un répertoire 
      C:\Users\[Philippe]\AppData\Local\[TestSettings]\... comme : 

    	C:\Users\Philippe\AppData\Local\TestSettings\TestSettings_Url_bfuncae2vcw4t22bfmcatpz2ll2ijfdq\1.0.0.0

```
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <userSettings>
        <TestSettings.Settings1>
            <setting name="Setting0" serializeAs="String">
                <value>Setting0 30/08/2022 16:50:35</value>
            </setting>
        </TestSettings.Settings1>
    </userSettings>
</configuration>
```

Code:

    internal class Program
    {
        static void Main(string[] args)
        {
            Settings1 settings1 = Settings1.Default;

            settings1.PropertyChanged += Settings1_PropertyChanged;
            settings1.SettingChanging += Settings1_SettingChanging;
            settings1.SettingsLoaded += Settings1_SettingsLoaded;
            settings1.SettingsSaving += Settings1_SettingsSaving;

            Test0(settings1);
        }

        static void Test0(Settings1 settings1)
        {
            try
            {
                Debug.WriteLine($"{nameof(Test0)}(-) settings1={settings1}");

                {
                    SettingsContext _context = settings1.Context;
                    Debug.WriteLine($"_context={_context}");
                }

                foreach (SettingsProperty property in settings1.Properties)
                {
                    Debug.WriteLine($"    property={property}");
                    Debug.WriteLine($"    .Provider={property.Provider}");
                    Debug.WriteLine($"    .PropertyType={property.PropertyType}");
                    Debug.WriteLine($"    '{settings1[property.Name]}'");
                }

                Debug.WriteLine($".Setting0={settings1.Setting0}");

                {
                    Debug.WriteLine($"settings1.Setting0(-)");
                    settings1.Setting0 = $"Setting0 {DateTime.Now}";
                    Debug.WriteLine($"settings1.Setting0(+)");
                }

                {
                    Debug.WriteLine($"settings1.Save(-)");
                    settings1.Save();
                    Debug.WriteLine($"settings1.Save(+)");
                }

            }
            finally
            {
                Debug.WriteLine($"{nameof(Test0)}(+)");
            }
        }

        private static void Settings1_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Debug.WriteLine($">> {nameof(Settings1_PropertyChanged)}");
        }

        private static void Settings1_SettingsLoaded(object sender, SettingsLoadedEventArgs e)
        {
            Debug.WriteLine($">> {nameof(Settings1_SettingsLoaded)} e.Provider=<<{e.Provider}>>");
        }

        private static void Settings1_SettingsSaving(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Debug.WriteLine($">> {nameof(Settings1_SettingsSaving)}");
        }

        private static void Settings1_SettingChanging(object sender, SettingChangingEventArgs e)
        {
            Debug.WriteLine($">> {nameof(Settings1_SettingChanging)}");
        }

    }

## [ApplicationSettingsBase](https://docs.microsoft.com/en-us/dotnet/api/system.configuration.applicationsettingsbase?view=dotnet-plat-ext-6.0)

### Class

	public abstract class ApplicationSettingsBase : System.Configuration.SettingsBase, System.ComponentModel.INotifyPropertyChanged

