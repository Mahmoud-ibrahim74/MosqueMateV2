using System.Text.RegularExpressions;

namespace MosqueMateV2.Helpers
{
    public partial class NumberHelper
    {
        public static bool IsTextNumeric(string text)
        {
            return TextNumericRegx().IsMatch(text);
        }
        public static bool IsTextHasDigit(string text)
        {
            return IsTextHasDigitRegx().IsMatch(text);
        }
        public static int GetTextFromDigit(string text)
        {
            if (IsTextHasDigit(text))
            {
                var value = IsTextHasDigitRegx().Match(text);
                return int.Parse(value.Value);
            }
            return 0;
        }
        [GeneratedRegex(@"^[+-]?(\d+(\.\d*)?|\.\d+)$")]
        private static partial Regex TextNumericRegx();
        [GeneratedRegex(@"\d+")]
        private static partial Regex IsTextHasDigitRegx();
    }
}
