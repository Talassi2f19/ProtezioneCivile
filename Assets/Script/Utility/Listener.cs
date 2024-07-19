using System;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Networking;

// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
//classe per la gestione semplificata di un listener
namespace Script.Utility
{
    public class Listeners
    {
        private UnityWebRequest webReq;
        private readonly string url;

        public Listeners(string url)
        {
            this.url = url;
        }

        public bool IsStarted()
        {
            return webReq != null && !webReq.isDone;
        }
        
        public void Start(FirebaseEventsHandler.DataReceivedEvents method)
        {
            if (IsStarted())
                return;

            Debug.Log("start");
            webReq = new UnityWebRequest(url);
            webReq.SetRequestHeader("Accept", "text/event-stream");

            FirebaseEventsHandler downloadHandler = new FirebaseEventsHandler();
            downloadHandler.DataReceived += method;
            webReq.downloadHandler = downloadHandler;

            webReq.SendWebRequest().completed += (asyncOperation) =>
            {
                if (webReq != null && (webReq.result == UnityWebRequest.Result.ProtocolError || webReq.result == UnityWebRequest.Result.ConnectionError))
                {
                    Debug.LogError($"Request error: {webReq.error}" + "Il listener è morto");
                    downloadHandler.DataReceived -= method;
                    Stop();
                    Start(method);
                }
            };
        }

        public void Stop()
        {
            if (webReq == null)
            {
                Debug.Log("already stopped");
                return;
            }

            try
            {
                webReq.Abort();
                webReq.Dispose();
                webReq = null;
                Debug.Log("stop");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error while stopping the request: {ex.Message}");
            }
        }
    }
}


