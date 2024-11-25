using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosqueMateV2.Services.IServices
{
    public interface IToastNotificationService
    {
        public void SendInfo(string message);   
        public void SendSuccess(string message);   
        public void SendWarning(string message);   
        public void SendError(string message);   
    }
}
