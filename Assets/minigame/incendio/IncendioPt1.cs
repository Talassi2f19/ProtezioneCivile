using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace minigame.incendio
{
    public class IncendioPt1 : MonoBehaviour
    {
        [SerializeField] private Danni danni;
        [SerializeField] private GameObject minimappaPoint;
        private bool type;
        
        private void Start()
        {
            minimappaPoint.SetActive(type);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            danni.Fine();
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
            danni.Inizia();
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
