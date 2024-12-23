using MosqueMateV2.Resources;
using System.Reflection;
using System.Text;
using System.Windows;

namespace MosqueMateV2.Extensions
{
    /// <summary>
    /// Provides utility methods for string operations such as appending and manipulating strings.
    /// </summary>
    public static class StringExtenstion
    {
        /// <summary>
        /// Concatenates the specified string values into a single string without line breaks.
        /// </summary>
        /// <param name="values">An array of strings to concatenate.</param>
        /// <returns>A single concatenated string.</returns>
        public static string AppendString(params string[] values)
        {
            StringBuilder stringBuilder = new();
            foreach (string value in values)
            {
                stringBuilder.Append(value);
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// Concatenates the specified string values into a single string, with each value followed by a line break.
        /// </summary>
        /// <param name="values">An array of strings to concatenate.</param>
        /// <returns>A single concatenated string with line breaks.</returns>
        public static string AppendLineString(params string[] values)
        {
            StringBuilder stringBuilder = new();
            foreach (string value in values)
            {
                stringBuilder.AppendLine(value);
            }
            return stringBuilder.ToString();
        }
        public static string AppendDuration(this string input, TimeSpan duration)
        {
            string formattedDuration = $"{App.LocalizationService[SD.Localization.Duration]} : {duration.Hours:D2}:{duration.Minutes:D2}:{duration.Seconds:D2}";
            return $"{input} {formattedDuration}".Trim();
        }

        /// <summary>
        /// Converts a string to a ThemeMode struct.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <returns>The corresponding ThemeMode struct.</returns>
        public static ThemeMode ToThemeMode(this string value)
        {
            // Get the type of ThemeMode
            Type themeModeType = typeof(ThemeMode);

            // Search for a static property matching the string value
            var staticProperty = themeModeType
                .GetProperty(value, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);

            if (staticProperty != null && staticProperty.PropertyType == themeModeType)
            {
                // Return the matching static instance
                return (ThemeMode)staticProperty.GetValue(null);
            }

            // If no predefined static instance matches, create a new ThemeMode with the string value
            return (ThemeMode)Activator.CreateInstance(themeModeType, value);
        }
    }
}

