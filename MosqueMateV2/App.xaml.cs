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
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Load localization file
            LocalizationService = new JsonLocalizationService();

            // Set default culture (e.g., "en")
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(AppLocalization.Arabic);
        }
    }

}
