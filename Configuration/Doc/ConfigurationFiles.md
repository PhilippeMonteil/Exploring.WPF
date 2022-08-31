
# [Configure apps by using configuration files](https://docs.microsoft.com/en-us/dotnet/framework/configure-apps/) 

## Résumé

- Ajouter le nuget System.Configuration.ConfigurationManager
- ajouter un item 'Application Configuration File', 'App.config' par défaut

> To create the output configuration file that's deployed with the app, Visual Studio copies the source configuration file to the directory where the compiled assembly is placed. This file is named <yourappname>.exe.config. For example, an app named myApp.exe will have an output configuration file named myApp.exe.config.

Exemple : \bin\Debug\net6.0\TestConfig.dll.config

## [How to: Read application settings](https://docs.microsoft.com/en-us/dotnet/framework/configure-apps/read-app-settings)

## [ConfigurationManager Class](https://docs.microsoft.com/en-us/dotnet/api/system.configuration.configurationmanager?view=dotnet-plat-ext-6.0)

### Properties

	// contenu des sections appsettings et 
	public static System.Collections.Specialized.NameValueCollection AppSettings { get; }
	public static System.Configuration.ConnectionStringSettingsCollection ConnectionStrings { get; }

### Lecture des AppSettings d'un App.config

#### Exemple

     foreach (string? key in ConfigurationManager.AppSettings.AllKeys)
     {
         Console.WriteLine("Key: {0} Value: {1}", key, ConfigurationManager.AppSettings[key]);
     }

```
<configuration>

	<appSettings>
		<add key="Setting1" value="May 5, 2014"/>
		<add key="Setting2" value="May 6, 2014"/>
	</appSettings>

</configuration>
```

Output :

	Key: Setting1 Value: May 5, 2014
	Key: Setting2 Value: May 6, 2014

### Emplacement des fichiers .config lus au runtime : ConfigurationManager.OpenExeConfiguration, Configuration.FilePath

#### Exemple

     foreach (ConfigurationUserLevel configurationUserLevel in Enum.GetValues(typeof(ConfigurationUserLevel)))
     {
         Configuration configuration = ConfigurationManager.OpenExeConfiguration(configurationUserLevel);
         Console.WriteLine($"configurationUserLevel={configurationUserLevel} configuration.FilePath={configuration.FilePath}");
     }

Output :

```
configurationUserLevel=None 
configuration.FilePath=C:\Solal\Solal.Experiments\Exploring.WPF\TestConfigurationFiles\bin\Debug\net6.0\TestConfigurationFiles.dll.config

configurationUserLevel=PerUserRoaming 
configuration.FilePath=C:\Users\Philippe\AppData\Roaming\TestConfigurationFiles\TestConfigurationFiles_Url_rpwlvac4grq4t2vftnfbgar5uh1sia5l\1.0.0.0\user.config

configurationUserLevel=PerUserRoamingAndLocal 
configuration.FilePath=C:\Users\Philippe\AppData\Local\TestConfigurationFiles\TestConfigurationFiles_Url_rpwlvac4grq4t2vftnfbgar5uh1sia5l\1.0.0.0\user.config
```

## ConfigurationUserLevel 

| Valeur      | Emplacement |
| ----------- | ----------- |
| configurationUserLevel | répertoire d'exécution |
| PerUserRoaming | C:\Users\Philippe\AppData\Roaming\TestConfigurationFiles\... |
| PerUserRoamingAndLocal | C:\Users\Philippe\AppData\Local\TestConfigurationFiles\... |
