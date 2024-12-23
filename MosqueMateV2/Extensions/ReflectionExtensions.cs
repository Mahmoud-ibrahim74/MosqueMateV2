using System.Reflection;

namespace MosqueMateV2.Extensions
{
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Gets the names of all properties of the specified type based on provided binding flags.
        /// </summary>
        /// <typeparam name="T">The type to inspect.</typeparam>
        /// <param name="bindingFlags">The binding flags to use for filtering properties.</param>
        /// <returns>A list of property names.</returns>
        public static List<string> GetPropertyNames<T>(params BindingFlags[] bindingFlags)
        {
            // Combine all provided binding flags
            var combinedFlags = bindingFlags.Length > 0
                ? bindingFlags.Aggregate((current, next) => current | next)
                : BindingFlags.Default;

            return typeof(T)
                .GetProperties(combinedFlags)
                .Select(p => p.Name)
                .ToList();
        }
    }
}
