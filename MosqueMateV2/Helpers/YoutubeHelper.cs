using MosqueMateV2.Resources;
using System.IO;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos.Streams;

namespace MosqueMateV2.Helpers
{
    public class YoutubeHelper
    {

        public static async Task<IVideoStreamInfo> DownloadYouTubeVideoAsync(string videoUrl)
        {
            try
            {
                var youtube = new YoutubeClient();
                var streamManifestCollection = await youtube.Videos.Streams.GetManifestAsync(videoUrl);
                var streamInfo = streamManifestCollection.GetVideoStreams().FirstOrDefault();
                if (streamInfo is not null)
                {
                    await youtube.Videos.Streams.DownloadAsync(streamInfo, AppLocalization.VideoDirectoryDownload);
                    return streamInfo;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public static async Task<IReadOnlyList<PlaylistVideo>> GetPlayListAsync(string playlistUrl)
        {
            var youtube = new YoutubeClient();
            var playlist = await youtube.Playlists.GetAsync(playlistUrl);
            AppHelper.PlayListName = playlist.Title;
            var videos = await youtube.Playlists.GetVideosAsync(playlist.Id);
            return videos;
        }

    }
}
