using System.Text.RegularExpressions;

namespace MosqueMateV2.Helpers
{
    public class NumberHelper
    {
        public  static bool IsTextNumeric(string text)
        {
            Regex regex = new("^[0-9]+$");
            return regex.IsMatch(text);
        }
    }
}
