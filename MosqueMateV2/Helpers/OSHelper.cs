using Microsoft.Win32;
using System.Windows;

namespace MosqueMateV2.Helpers
{
    class OSHelper
    {
       public static ThemeMode GetWindowsTheme()
        {
            try
            {
                using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
                if (key != null)
                {
                    object value = key.GetValue("AppsUseLightTheme");
                    if (value is int intValue)
                    {
                        return intValue == 1 ? ThemeMode.Light : ThemeMode.Dark;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading registry: {ex.Message}");
            }

            return ThemeMode.Light;
        }
    }
}
