using MosqueMateV2.Domain.Interfaces;
using NAudio.Wave;

namespace MosqueMateV2.Domain.Repositories
{
    public class MP3Player : IMP3Player
    {
        public bool IsPlaying { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        private IWavePlayer? waveOutDevice;
        private WaveStream? waveStream;
        public MP3Player()
        {
        }
        public void Pause()
        {
            waveOutDevice?.Pause();
        }

        public void Play(byte[] fileData)
        {
            waveOutDevice = new WaveOutEvent();
            waveStream = new Mp3FileReader(new MemoryStream(fileData));
            waveOutDevice.Init(waveStream);
            waveOutDevice.Play();
        }

        public void Stop()
        {
            waveOutDevice?.Stop();
            waveOutDevice?.Dispose();
            waveStream?.Dispose();
        }
    }
}
