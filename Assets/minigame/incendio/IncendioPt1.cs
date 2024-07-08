using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace minigame.incendio
{
    public class IncendioPt1 : MonoBehaviour
    {
        [SerializeField]private Image image;
        [SerializeField] private GameObject minimappaPoint;
        private bool type;

        private Coroutine dannoPunti;
        private Coroutine dannoVisuale;
        
        private void Start()
        {
            image.color = new Color(0f, 0f, 0f, 0f);
            minimappaPoint.SetActive(type);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(dannoPunti != null)
                StopCoroutine(dannoPunti);
            if(dannoVisuale != null)
                StopCoroutine(dannoVisuale);
            image.color = new Color(0f, 0f, 0f, 0f);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (type)
            {
                Volontario();
            }
            else
            {
                AltroRuolo();
            }
        }
        
        private void Volontario()
        {
            //attiva gioco
            startCallback.Invoke();
        }

        private void AltroRuolo()
        {
            dannoPunti = StartCoroutine(DannoPunti());
            dannoVisuale = StartCoroutine(DannoVisuale());
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
                yield return new WaitForSeconds(5f);
                Debug.Log("punti");
            }
        }
        
        public void SetType(bool val)
        {
            type = val;
        }
        
        public delegate void StartPt2Callback();
        private StartPt2Callback startCallback;
        
        public void StartPt2(StartPt2Callback callback)
        {
            startCallback = callback;
        }
    }
}
