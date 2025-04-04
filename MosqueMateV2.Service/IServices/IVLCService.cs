﻿using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosqueMateV2.Service.IServices
{
    public interface IVLCService:IDisposable
    {
        public MediaPlayer _mediaPlayer { get; set; }
        public void PlayMedia();
        public void PauseMedia();
        public void StopMedia();
    }
}
