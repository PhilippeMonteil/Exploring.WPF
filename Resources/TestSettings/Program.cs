
using System.Configuration;
using System.Diagnostics;

namespace TestSettings
{

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
            LocalFileSettingsProvider _p = e.Provider as LocalFileSettingsProvider;
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

}
