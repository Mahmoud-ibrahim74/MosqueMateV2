using MosqueMateV2.Service.IServices;
using NAudio.Wave;

namespace MosqueMateV2.Service.Services
{
    public class NAudioService : INAudioService
    {
        string _audioFile { get; set; }
        AudioFileReader audioFileReader { get; set; }
        WaveOutEvent waveOutEvent { get; set; }

        public bool IsPlaying { get; private set; }

        private readonly string[] _allowedExtension = [".mp3", ".wav"];
        public NAudioService(string audioFile)
        {
            this._audioFile = audioFile;
            if (!File.Exists(audioFile))
                throw new FileNotFoundException("file doesn't exist");

            var exe = Path.GetExtension(audioFile);
            if (!_allowedExtension.Contains(exe))
                throw new IOException("File extension should  be (.mp3, .wav)");
            audioFileReader = new AudioFileReader(audioFile);
            waveOutEvent = new();
            waveOutEvent.Init(audioFileReader);
        }

        public void PlayAudio()
        {
            waveOutEvent?.Play();
            IsPlaying = true;
        }
        public void PauseAudio()
        {
            waveOutEvent?.Pause();
        }
        public void StopAudio()
        {
            waveOutEvent?.Stop();
        }

        public void Dispose()
        {
            StopAudio();
            audioFileReader?.Dispose();
            waveOutEvent?.Dispose();
            IsPlaying = false;
        }
    }
}
