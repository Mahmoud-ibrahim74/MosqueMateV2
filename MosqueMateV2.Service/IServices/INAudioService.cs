using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosqueMateV2.Service.IServices
{
    public interface INAudioService:IDisposable
    {
        public void PlayAudio();
        public void PauseAudio();
        public void StopAudio();
    }
}
