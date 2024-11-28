using MosqueMateV2.Resources;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Text;

namespace Resources
{
    public class JsonLocalizationService
    {
        private readonly JObject _localizationData;

        public JsonLocalizationService()
        {
            if (FileResources.SharedResource.Length == 0)
                return;

            string jsonContent = Encoding.UTF8.GetString(FileResources.SharedResource);
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
