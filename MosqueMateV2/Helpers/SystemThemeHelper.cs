using Microsoft.Win32;

namespace MosqueMateV2.Helpers
{
    public static class SystemThemeHelper
    {
        public static bool IsDarkTheme()
        {
            const string registryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
            const string valueName = "AppsUseLightTheme";

            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryKeyPath))
                {
                    if (key != null)
                    {
                        object value = key.GetValue(valueName);
                        if (value is int intValue)
                        {
                            // 0 means Dark Theme, 1 means Light Theme
                            return intValue == 0;
                        }
                    }
                }
            }
            catch
            {
                // Handle exceptions (e.g., registry access denied) as needed
            }

            // Default to Light Theme if unable to determine
            return false;
        }
    }
}
