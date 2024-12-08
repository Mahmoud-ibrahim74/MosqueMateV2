using LibVLCSharp.Shared;
using MosqueMateV2.Service.IServices;

namespace MosqueMateV2.Service.Services
{
    public class VLCService : IVLCService
    {
        LibVLC _libVLC;
        public MediaPlayer _mediaPlayer { get; set; }
        public string mediaPath { get; set; }
        public VLCService(string mediaPath)
        {
            Core.Initialize();
            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);
            if (!File.Exists(mediaPath))
                throw new Exception("File Path not found");

            this.mediaPath = mediaPath;
        }
        public void PlayMedia()
        {
            _mediaPlayer?.Play(new Media(_libVLC, mediaPath, FromType.FromPath));
        }
        public void Audio()
        {

        }
        public void PauseMedia()
        {
            _mediaPlayer?.Pause();
        }
        public void StopMedia()
        {
            _mediaPlayer?.Stop();
        }

        public void Dispose()
        {
            _libVLC?.Dispose();
            _mediaPlayer?.Dispose();
        }
    }
}
