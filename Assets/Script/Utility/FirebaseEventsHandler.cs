using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

// ReSharper disable CommentTypo
//questa classe è la stessa del progetto fatto l'anno scorso
namespace Script.Utility
{
    public class FirebaseEventsHandler : DownloadHandlerScript
    {
        public delegate void DataReceivedEvents(string data);
        public event DataReceivedEvents DataReceived;
        // private byte[] bytes;

        // protected override byte[] GetData()
        // {
        //     return bytes;
        // }

        protected override bool ReceiveData(byte[] data, int dataLength)
        {
            if (data == null || data.Length < 1)
                return false;
            // bytes = data;
            if (DataReceived != null)
            {
                String encodedData = Encoding.UTF8.GetString(data);
                foreach (var tmp in encodedData.TrimEnd('\n').Split("\n\n"))
                {
                    DataReceived(tmp);
                    Print(tmp);
                }
            }
            return true;
        }
        
        
        
        private static int counter = 0;
        private void Print(String text)
        {
            String kk = "<color=brown>" + counter++ + "</color>";
            foreach (String tmp in text.Split("\n"))
            {
                kk += "<color=orange>" + tmp + "</color>\n";
            }
            Debug.LogWarning(kk);
        }
    }
}
