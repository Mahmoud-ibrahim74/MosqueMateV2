using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Service.IServices;
using MosqueMateV2.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosqueMateV2.Helpers
{
    class YoutubeHelper
    {
        private readonly IYoutubeService _youtubeService;
        ILinkRepository linkRepository { get; set; }
        public YoutubeHelper()
        {

            _youtubeService = new YoutubeService(); 
            linkRepository = new LinkRepository();
        }
        public async Task GetPlayListAsync(string url)
        {
            var res =  await _youtubeService.GetPlayListAsync(url);
            var json = linkRepository.ModifyOneLink([.. res.Select(x => x.Url)]);
            Console.WriteLine(json);    
        }
    }
}
