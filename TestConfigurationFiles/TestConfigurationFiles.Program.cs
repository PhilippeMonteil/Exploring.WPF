
using System.Configuration;

namespace TestConfigurationFiles
{

    internal class Program
    {

        static void Main(string[] args)
        {
            ReadAllSettings();

            foreach (ConfigurationUserLevel configurationUserLevel in Enum.GetValues(typeof(ConfigurationUserLevel)))
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(configurationUserLevel);
                Console.WriteLine($"configurationUserLevel={configurationUserLevel} configuration.FilePath={configuration.FilePath}");
            }

            foreach (ConfigurationUserLevel configurationUserLevel in Enum.GetValues(typeof(ConfigurationUserLevel)))
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(configurationUserLevel);
                Console.WriteLine($"configurationUserLevel={configurationUserLevel} configuration.FilePath={configuration.FilePath}");

                AddUpdateAppSettings(configurationUserLevel, "NewSetting", "May 7, 2014");
                AddUpdateAppSettings(configurationUserLevel, "Setting1", "May 8, 2014");

                ReadAllSettings();
            }

        }


        static void ReadAllSettings()
        {
            try
            {

                if (ConfigurationManager.AppSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                }
                else
                {
                    foreach (string? key in ConfigurationManager.AppSettings.AllKeys)
                    {
                        Console.WriteLine("Key: {0} Value: {1}", key, ConfigurationManager.AppSettings[key]);
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        static void ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                Console.WriteLine(result);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        static void AddUpdateAppSettings(ConfigurationUserLevel configurationUserLevel, string key, string value)
        {
            try
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(configurationUserLevel);

                Console.WriteLine($"configurationUserLevel={configurationUserLevel} configuration.FilePath={configuration.FilePath}");

                KeyValueConfigurationCollection settings = configuration.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }

                configuration.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection(configuration.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException e)
            {
                Console.WriteLine($"configurationUserLevel={configurationUserLevel} Exception={e}");
            }
        }

    }

}
