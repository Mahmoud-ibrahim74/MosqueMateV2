using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Resources
{
    public class JsonLocalizationService
    {
        private readonly JObject _localizationData;

        public JsonLocalizationService()
        {
            string jsonFilePath = Path.Combine(Environment.CurrentDirectory, "AppResources", "SharedResource.json");
            if (!File.Exists(jsonFilePath))
                throw new FileNotFoundException($"Localization file not found: {jsonFilePath}");

            var jsonContent = File.ReadAllText(jsonFilePath);
            _localizationData = JObject.Parse(jsonContent);
        }
        public string this[string key]
        {
            get
            {
                var culture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
                var token = _localizationData.SelectToken($"{key}.{culture}");
                return token?.ToString() ?? $"[{key}]";
            }
        }

    }
}
