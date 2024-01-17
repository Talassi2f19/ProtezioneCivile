using System.Text;
using UnityEngine.Networking;

// ReSharper disable CommentTypo
//questa classe è la stessa del progetto fatto l'anno scorso
namespace Script.Utility
{
    public class FirebaseEventsHandler : DownloadHandlerScript
    {
        public delegate void DataReceivedEvents(string data);
        public event DataReceivedEvents DataReceived;
        private byte[] bytes;

        protected override byte[] GetData()
        {
            return bytes;
        }

        protected override bool ReceiveData(byte[] data, int dataLength)
        {
            if (data == null || data.Length < 1)
                return false;

            this.bytes = data;
            if (DataReceived != null)
                DataReceived(Encoding.UTF8.GetString(data));
            return true;
        }
    }
}
