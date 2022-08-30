
# [Manage application settings](https://docs.microsoft.com/en-us/visualstudio/ide/managing-application-settings-dotnet?view=vs-2022)

# [Configuration in .NET](https://docs.microsoft.com/en-us/dotnet/core/extensions/configuration)
# [Configuration providers in .NET](https://docs.microsoft.com/en-us/dotnet/core/extensions/configuration-providers)

## Settings

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

## [ApplicationSettingsBase](https://docs.microsoft.com/en-us/dotnet/api/system.configuration.applicationsettingsbase?view=dotnet-plat-ext-6.0)

### Class

	public abstract class ApplicationSettingsBase : System.Configuration.SettingsBase, System.ComponentModel.INotifyPropertyChanged

## [SettingsProperty](https://docs.microsoft.com/en-us/dotnet/api/system.configuration.settingsproperty?view=dotnet-plat-ext-6.0)

### Class

	public class SettingsProperty

###  Inheritance

- Object
- SettingsProperty

### Events

	public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
	public event System.Configuration.SettingChangingEventHandler SettingChanging;
	public event System.Configuration.SettingsLoadedEventHandler SettingsLoaded;
	public event System.Configuration.SettingsSavingEventHandler SettingsSaving;

## [SettingsProperty](https://docs.microsoft.com/en-us/dotnet/api/system.configuration.settingsproperty?view=dotnet-plat-ext-6.0)

