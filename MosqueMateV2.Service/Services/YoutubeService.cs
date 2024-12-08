using MosqueMateV2.Service.IServices;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos.Streams;

namespace MosqueMateV2.Service.Services
{
    public class YoutubeService: IYoutubeService
    {
        public string _playListName { get; set; }
        YoutubeClient youtube;
        public YoutubeService()
        {
            _playListName = string.Empty;
            youtube = new YoutubeClient();
        }

        public async Task<IReadOnlyList<PlaylistVideo>> GetPlayListAsync(string playlistUrl)
        {
            var playlist = await youtube.Playlists.GetAsync(playlistUrl);
            _playListName = playlist.Title;
            var videos = await youtube.Playlists.GetVideosAsync(playlist.Id);
            return videos;
        }
        public async Task<IVideoStreamInfo> DownloadYouTubeVideoAsync(string videoUrl, string outputFile)
        {
            try
            {
                var streamManifestCollection = await youtube.Videos.Streams.GetManifestAsync(videoUrl);
                var muxedStreamInfo = streamManifestCollection.GetMuxedStreams().GetWithHighestVideoQuality();
                if (muxedStreamInfo is not null)
                {
                    await youtube.Videos.Streams.DownloadAsync(muxedStreamInfo, outputFile);
                    return muxedStreamInfo;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<IStreamInfo> DownloadYouTubeAudioAsync(string videoUrl, string outputFile)
        {
            try
            {
                var streamManifestCollection = await youtube.Videos.Streams.GetManifestAsync(videoUrl);
                var muxedStreamInfo = streamManifestCollection.GetAudioOnlyStreams().GetWithHighestBitrate();
                if (muxedStreamInfo is not null)
                {
                    await youtube.Videos.Streams.DownloadAsync(muxedStreamInfo, outputFile);
                    return muxedStreamInfo;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

    }
}
