using System;
using System.Collections;
using Script.Utility;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

namespace FirebaseListener
{
    public class Listeners
    {
        private UnityWebRequest webReq;
        private readonly string url;
        private DateTime lastDataReceivedTime;
        private Coroutine monitorCoroutine;
        private FirebaseEventsHandler.DataReceivedEvents callback;

        public Listeners(string url)
        {
            this.url = url;
            lastDataReceivedTime = DateTime.MinValue;
        }

        public bool IsStarted()
        {
            return webReq != null && !webReq.isDone;
        }

        public void Start(FirebaseEventsHandler.DataReceivedEvents method)
        {
            if (IsStarted())
                return;
            callback = method;
            Debug.Log("start");
            webReq = new UnityWebRequest(url);
            webReq.SetRequestHeader("Accept", "text/event-stream");

            FirebaseEventsHandler downloadHandler = new FirebaseEventsHandler();
            downloadHandler.DataReceived += (data) =>
            {
                method(data);
                lastDataReceivedTime = DateTime.Now; // Aggiorna l'ultimo tempo di ricezione dati
                
                GameObject.FindWithTag("ConnectionLost")?.GetComponent<ConnectionLostMan>()?.Disable();
            };
            webReq.downloadHandler = downloadHandler;

            webReq.SendWebRequest().completed += (asyncOperation) =>
            {
                downloadHandler.DataReceived -= method;
                if (webReq != null && (webReq.result == UnityWebRequest.Result.ProtocolError || webReq.result == UnityWebRequest.Result.ConnectionError))
                {
                    Debug.LogWarning($"Connection closed, restarting listener. Error: {webReq.result}");
                    Restart();
                }
            };

            lastDataReceivedTime = DateTime.Now; // Inizializza il tempo di ricezione dati
            monitorCoroutine = StaticCoroutine.StartCoroutine(MonitorDataReceived());
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

                if (monitorCoroutine != null)
                {
                    StaticCoroutine.StopCoroutine(monitorCoroutine);
                    monitorCoroutine = null;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error while stopping the request: {ex.Message}");
            }
        }

        private void Restart()
        {
            GameObject.FindWithTag("ConnectionLost")?.GetComponent<ConnectionLostMan>()?.Enable();
            StaticCoroutine.StartCoroutine(RestartTimer());
        }

        private IEnumerator RestartTimer()
        {
            yield return new WaitForSeconds(5);
            Stop();
            Start(callback);
        }

        private IEnumerator MonitorDataReceived()
        {
            while (true)
            {
                if ((DateTime.Now - lastDataReceivedTime).TotalSeconds > 35)
                {
                    Debug.LogWarning("No data received for a long time, restarting listener");
                    Restart();
                }
                yield return new WaitForSeconds(10); // Controlla ogni 10 secondi
            }
        }
    }
}