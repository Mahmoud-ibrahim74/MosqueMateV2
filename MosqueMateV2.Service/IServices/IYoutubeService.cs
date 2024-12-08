using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode;

namespace MosqueMateV2.Service.IServices
{
    public interface IYoutubeService
    {
        public string _playListName { get; set; }
        public Task<IReadOnlyList<PlaylistVideo>> GetPlayListAsync(string playlistUrl);
        public Task<IVideoStreamInfo> DownloadYouTubeVideoAsync(string videoUrl, string outputFile);
        public Task<IStreamInfo> DownloadYouTubeAudioAsync(string videoUrl, string outputFile);
    }
}
