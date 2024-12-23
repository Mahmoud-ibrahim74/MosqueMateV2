using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Pages;
using MosqueMateV2.Resources;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Text;
using YoutubeExplode.Playlists;

namespace MosqueMateV2.Helpers
{
    public class FileHelper
    {
        public static async Task<string> WritePdfToTempAsync(byte[] file)
        {
            try
            {
                if (!Directory.Exists(SD.Localization.AppTempPath))
                    Directory.CreateDirectory(SD.Localization.AppTempPath);

                var filePath = Path.Combine(SD.Localization.AppTempPath, "output.pdf");
                await File.WriteAllBytesAsync(filePath, file);
                return filePath;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public static void ConvertPlayListToJsonFile(IReadOnlyList<PlaylistVideo> list)
        {
            var filePath = Path.Combine(SD.Localization.AppTempPath, "output.json");

            string jsonContent = Encoding.UTF8.GetString(FileResources.quran_link);
            var surahs = JsonConvert.DeserializeObject<List<DTOQuranLinks>>(jsonContent);

            foreach (var surah in surahs)
            {
                if (surah.name == "النّور")
                    surah.name = "النور";
                if (surah.name == "النّمل")
                    surah.name = "النمل";
                if (surah.name == "الرّوم")
                    surah.name = "الروم";
                if (surah.name == "فصّلت")
                    surah.name = "فصلت";
                if (surah.name == "الدّخان")
                    surah.name = "الدخان";





                var obj = list.FirstOrDefault(x => x.Title.Contains(surah.name) && !x.Title.Contains("سورة ال {61} للقارئ الشيخ ياسر الدوسري"));
                surah.url2 = obj?.Url;
            }
            string updatedJson = JsonConvert.SerializeObject(surahs, Formatting.Indented);
            File.WriteAllText("output.json", updatedJson);
        }
    }
}
