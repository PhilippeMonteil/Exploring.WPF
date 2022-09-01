
# Configuration

## [Configuration in .NET](https://docs.microsoft.com/en-us/dotnet/core/extensions/configuration)

## Packages

    <ItemGroup>

        <PackageReference Include="Microsoft.Extensions.Configuration.Binder" Version="6.0.0" />
        <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="6.0.0" />
        <PackageReference Include="Microsoft.Extensions.Configuration.EnvironmentVariables" Version="6.0.0" />

        <PackageReference Include="Microsoft.Extensions.Hosting" Version="6.0.0" />

    </ItemGroup>

## [ConfigurationBuilder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.configurationbuilder?view=dotnet-plat-ext-6.0)

## Exemple : extraction d'une instance de classe d'un .json de configuration

- ajout d'un appsettings.json dans le projet, Copy To Output = yes

```
{
  "Settings": {
    "KeyOne": 1,
    "KeyTwo": true,
    "KeyThree": {
      "Message": "Oh, that's nice...",
      "SupportedVersions": {
        "v1": "1.0.0",
        "v3": "3.0.7"
      }
    },
    "IPAddressRange": [
      "46.36.198.121",
      "46.36.198.122",
      "46.36.198.123",
      "46.36.198.124",
      "46.36.198.125"
    ]
  }
}
```
- source

    // Build a config object, using env vars and JSON providers.
    IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json") // nécessite la présence du fichier dans le répertoire courant
        .AddEnvironmentVariables()
        .Build();

    // Get values from the config given their key and their target type.
    Settings settings = config.GetRequiredSection("Settings").Get<Settings>();

    // Write the values to the console.
    Console.WriteLine($"KeyOne = {settings.KeyOne}");
    Console.WriteLine($"KeyTwo = {settings.KeyTwo}");
    Console.WriteLine($"KeyThree:Message = {settings.KeyThree.Message}");

## Exemple : idem avec Hosting

     {
         using IHost host = Host.CreateDefaultBuilder(args).Build();

         // Ask the service provider for the configuration abstraction.
         IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

         // Get values from the config given their key and their target type.
         int keyOneValue = config.GetValue<int>("KeyOne");
         bool keyTwoValue = config.GetValue<bool>("KeyTwo");
         string keyThreeNestedValue = config.GetValue<string>("KeyThree:Message");

         // Write the values to the console.
         Console.WriteLine($"KeyOne = {keyOneValue}");
         Console.WriteLine($"KeyTwo = {keyTwoValue}");
         Console.WriteLine($"KeyThree:Message = {keyThreeNestedValue}");

     }

## [IConfiguration ](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration?view=dotnet-plat-ext-6.0)

## [Configuration providers in .NET](https://docs.microsoft.com/en-us/dotnet/core/extensions/configuration-providers?source=recommendations)
