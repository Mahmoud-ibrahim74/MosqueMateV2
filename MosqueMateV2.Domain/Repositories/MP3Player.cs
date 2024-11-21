using MosqueMateV2.Domain.Interfaces;
using NAudio.Wave;

namespace MosqueMateV2.Domain.Repositories
{
    using NAudio.Wave;
    using System.IO;

    public class MP3Player : IMP3Player
    {
        private IWavePlayer? waveOutDevice;
        private WaveStream? waveStream;
        private bool isPaused;

        public bool IsPlaying { get; set; }

        public void Play(byte[]? fileData = null)
        {
            if (isPaused && waveOutDevice != null && waveStream != null)
            {
                // Resume playback from the current position
                waveOutDevice.Play();
                IsPlaying = true;
                isPaused = false;
            }
            else
            {
                if (fileData == null)
                    return;

                Stop(); // Stop any existing playback

                waveOutDevice = new WaveOutEvent();
                waveStream = new Mp3FileReader(new MemoryStream(fileData));
                waveOutDevice.Init(waveStream);
                waveOutDevice.Play();

                IsPlaying = true;
                isPaused = false;
            }
        }

        public void Pause()
        {
            if (waveOutDevice != null && waveStream != null && IsPlaying)
            {
                waveOutDevice.Pause();
                IsPlaying = false;
                isPaused = true;
            }
        }

        public void Stop()
        {
            waveOutDevice?.Stop();
            waveOutDevice?.Dispose();
            waveStream?.Dispose();

            waveOutDevice = null;
            waveStream = null;

            IsPlaying = false;
            isPaused = false;
        }
    }

}
