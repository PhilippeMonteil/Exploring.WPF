
# [Configuration in .NET](https://docs.microsoft.com/en-us/dotnet/core/extensions/configuration)

> Configuration in .NET is performed using one or more configuration providers. Configuration providers read configuration data from key-value pairs using a variety of configuration sources:
- Settings files, such as appsettings.json
- Environment variables
- Azure Key Vault
- Azure App Configuration
- Command-line arguments
- Custom providers, installed or created
- Directory files
- In-memory .NET objects
- Third-party providers

## [ConfigurationManager](https://docs.microsoft.com/en-us/dotnet/api/system.configuration.configurationmanager?view=dotnet-plat-ext-6.0)

### Class

	public static class ConfigurationManager

### Properties

	public static NameValueCollection AppSettings { get; }
	public static ConnectionStringSettingsCollection ConnectionStrings { get; }

### ConfigurationUserLevel

	public enum ConfigurationUserLevel

| Value | Description |
| ----------- | ----------- |
| None | Gets the Configuration that applies to all users. |
| PerUserRoaming | Gets the roaming Configuration that applies to the current user.|
| PerUserRoamingAndLocal | Gets the local Configuration that applies to the current user.|

### Methods

	public static Configuration OpenExeConfiguration (System.Configuration.ConfigurationUserLevel userLevel);
	public static Configuration OpenExeConfiguration (string exePath);

