using System.Text.RegularExpressions;

namespace MosqueMateV2.Helpers
{
    public partial class EnumHelper<T> where T : Enum
    {
        public static List<string> ConvertEnumToFormattedList()
        {
            var enumNames = Enum.GetNames(typeof(T));
            List<string> formattedList = [];

            foreach (var name in enumNames)
            {
                string formattedName = SpaceRegx().Replace(name, " $1").Trim();
                formattedList.Add(formattedName);
            }
            return formattedList;
        }
        [GeneratedRegex("(?<!^)([A-Z])")]
        private static partial Regex SpaceRegx();

        public static T GetEnumValue(string formattedValue)
        {
            // Convert formatted value back to PascalCase
            string pascalCaseValue = formattedValue.Replace(" ", "");

            // Check if a matching enum name exists
            if (Enum.GetNames(typeof(T)).FirstOrDefault(name => name.Equals(pascalCaseValue, StringComparison.OrdinalIgnoreCase)) is string match)
            {
                return (T)Enum.Parse(typeof(T), match);
            }

            return default;
        }


    }
}
