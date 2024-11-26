using System.Text;

namespace MosqueMateV2.Helpers
{
    /// <summary>
    /// Provides utility methods for string operations such as appending and manipulating strings.
    /// </summary>
    public class StringHelper
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
    }
}
