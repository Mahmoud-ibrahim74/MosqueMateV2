using System.Text.RegularExpressions;

namespace MosqueMateV2.Domain.Extensions
{
    public static partial class IEnumerableExtensions
    {
        public static IEnumerable<string> AddSpacesBetweenWords(this IEnumerable<string> source)
        {
            var transformedList = source.Select(str =>
                            SpaceWordsRegx().Replace(str, " $1"));
            return transformedList ?? [];
        }
        public static IEnumerable<string> RemoveSpacesBetweenWords(this IEnumerable<string> source)
        {
            var transformedList = source.Select(str =>
                                                 string.Concat(str.Split(' ')));
            return transformedList ?? [];
        }

        [GeneratedRegex("(?<!^)([A-Z])")]
        private static partial Regex SpaceWordsRegx();
    }
}
