using System;
using System.Collections;
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

        protected override bool ReceiveData(byte[] data, int dataLength)
        {
            if (data == null || data.Length < 1)
                return false;
            
            if (DataReceived == null) 
                return true;
            
            string encodedData = Encoding.UTF8.GetString(data);
            foreach (var tmp in encodedData.TrimEnd('\n').Split("\n\n"))
            {
                DataReceived(tmp.Trim());
              //  Print(tmp.Trim());
            }
            return true;
        }
     
        
        private static int counter = 0;
        private void Print(String text)
        {
            String str = "<color=brown>" + counter++ + "</color>";
            foreach (String tmp in text.Split("\n"))
            {
                str += "<color=orange>" + tmp + "</color>\n";
            }
            Debug.Log(str);
        }
    }
}
