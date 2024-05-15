using System;
using UnityEngine;
using UnityEngine.Networking;

// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
//classe per la gestione semplificata di un listener
namespace Script.Utility
{
    public class Listeners
    {
        private UnityWebRequest webReq;
        private string url;
    
        public Listeners(string url) {
            this.url = url;
        }

        public bool IsStarted()
        {
            return webReq != null;
        }

        public void Start(FirebaseEventsHandler.DataReceivedEvents method)
        {
            if(IsStarted())
                return;
            
            Debug.Log("start");
            webReq = new UnityWebRequest(url);
            webReq.SetRequestHeader("Accept", "text/event-stream");
            FirebaseEventsHandler downloadHandler = new FirebaseEventsHandler();
            downloadHandler.DataReceived += method;
            webReq.downloadHandler = downloadHandler;
            webReq.SendWebRequest();
        }
        public void Stop()
        {
            try
            {
                webReq.Abort();
                webReq.Dispose();
                webReq = null;
                Debug.Log("stop");
            }
            catch (Exception)
            {
                Debug.Log("already stopped");
            }
        }
    }
}


