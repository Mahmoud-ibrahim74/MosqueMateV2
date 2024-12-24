using MosqueMateV2.Service.IServices;
using NAudio.Wave;
using System.Net;

namespace MosqueMateV2.Service.Services
{
    public class NAudioLiveStreamService : INAudioService
    {
        string _audioUrl {  get; set; }
        private IWavePlayer waveOut;
        private BufferedWaveProvider bufferedWaveProvider;
        public bool IsPlaying => throw new NotImplementedException();

        public NAudioLiveStreamService(string audioUrl)
        {
            this._audioUrl = audioUrl;
            try
            {
                // Create a buffer for the audio stream
                bufferedWaveProvider = new BufferedWaveProvider(new WaveFormat(44100, 16, 2));
                bufferedWaveProvider.BufferLength = 65536; // Adjust if needed
                bufferedWaveProvider.DiscardOnBufferOverflow = true;

                // Set up the WaveOut device for playback
                waveOut = new WaveOutEvent();
                waveOut.Init(bufferedWaveProvider);
                waveOut.Play();

                // Start streaming the audio
                using (var webClient = new WebClient())
                {
                    var stream = webClient.OpenRead(this._audioUrl);
                    var buffer = new byte[65536]; // 64KB buffer

                    int bytesRead;
                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        bufferedWaveProvider.AddSamples(buffer, 0, bytesRead);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing stream: {ex.Message}");
            }

        }


        public void PlayAudio()
        {
            waveOut?.Play();
        }
        public void PauseAudio()
        {
            waveOut?.Pause();
        }
        public void StopAudio()
        {
            waveOut?.Stop();
        }

        public void Dispose()
        {
            StopAudio();
            waveOut?.Dispose();
        }
    }
}
