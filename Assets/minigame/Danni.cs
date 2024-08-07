using System;
using System.Collections;
using _Scenes.User.telefono;
using Proyecto26;
using Script.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace minigame
{
    public class Danni : MonoBehaviour
    {
        [SerializeField]private Image image;
        private Coroutine dannoPunti;
        private Coroutine dannoVisuale;

        private void Start()
        {
            image.color = new Color(0f, 0f, 0f, 0f);
        }

        public void Inizia()
        {
            dannoPunti = StartCoroutine(DannoPunti());
            dannoVisuale = StartCoroutine(DannoVisuale());
        }

        public void Fine()
        {
            if(dannoPunti != null)
                StopCoroutine(dannoPunti);
            if(dannoVisuale != null)
                StopCoroutine(dannoVisuale);
            image.color = new Color(0f, 0f, 0f, 0f);
        }

        private IEnumerator DannoVisuale()
        {
            while (true)
            {
                image.color = new Color(1f, 0f, 0f, 0.4f);
                yield return new WaitForSeconds(0.2f);
                image.color = new Color(0f, 0f, 0f, 0f);
                yield return new WaitForSeconds(1f);
            }
        }
        private IEnumerator DannoPunti()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                
                GameObject.FindWithTag("notifiche")?.GetComponent<TaskManager>()?.NuovaNotifica("Allontanati da questa zona! Stai facendo perdere punti alla classe!");
                RestClient.Get(Info.DBUrl + Info.sessionCode + "/score.json").Then(e =>
                {
                    RestClient.Patch(Info.DBUrl + Info.sessionCode + ".json", "{\"score\":" + (int.Parse(e.Text == "null" ? "0" : e.Text ) - 1 ) + "}").Catch(Debug.LogError);
                }).Catch(Debug.LogError);
                
                yield return new WaitForSeconds(4f);
            }
        }
    }
}
