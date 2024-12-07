using LibVLCSharp.Shared;
using LibVLCSharp.WPF;
using MosqueMateV2.Resources;
using System.IO;
using System.Reflection;
using MediaPlayer = LibVLCSharp.Shared.MediaPlayer;

namespace MosqueMateV2.Helpers
{
    public static class VLCHelper
    {
        private static LibVLC _libVLC;
        private static MediaPlayer _mediaPlayer;
        public static bool IsLibVLCInstalled()
        {
            // Check if LibVLC is present in a known location, e.g., in the current directory of the application
            string appDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string libVLCPath = Path.Combine(appDirectory, "libvlc.dll");

            // Check if libvlc.dll exists in the application's folder
            if (File.Exists(libVLCPath))
            {
                return true;
            }

            // Optionally, check the system directories or common LibVLC installation paths
            string[] commonLibVLCPaths =
            [
                        @"C:\Program Files\VideoLAN\VLC\libvlc.dll",  // Default install path for VLC
                        @"C:\Program Files (x86)\VideoLAN\VLC\libvlc.dll"  // For 32-bit systems
            ];

            if (commonLibVLCPaths.Any(File.Exists))
            {
                return true;
            }

            return false;
        }

        public static void PlayVideoFromYoutube(this VideoView videoView)
        {
            if (!File.Exists(AppLocalization.VideoDirectoryDownload))
                return;


            Core.Initialize();
            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);
            videoView.MediaPlayer = _mediaPlayer;
            _mediaPlayer.Play(new Media(_libVLC, AppLocalization.VideoDirectoryDownload, FromType.FromPath));

        }
    }
}
