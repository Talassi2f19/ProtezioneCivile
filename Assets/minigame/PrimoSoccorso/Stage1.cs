using UnityEngine;

namespace minigame.PrimoSoccorso
{
    public class Stage1 : MonoBehaviour
    {
        [SerializeField] private Danni danni;
        private bool type;
        [SerializeField]private GameObject minimappaPoint;
        
        
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
            //attiva i danni
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
