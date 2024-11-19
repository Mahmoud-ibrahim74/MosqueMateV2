using Resources;
using System.Globalization;
using System.Windows;
namespace MosqueMateV2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static JsonLocalizationService LocalizationService { get; private set; }
        public static string AppLanguage { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {

            // Load localization file
            LocalizationService = new JsonLocalizationService();

            // Set default culture (e.g., "en")
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(AppLocalization.English);
            AppLanguage = Thread.CurrentThread.CurrentUICulture.Name;
            base.OnStartup(e);

        }
    }

}
