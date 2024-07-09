using UnityEngine;
using UnityEngine.UI;

namespace minigame.TogliTane
{
    public class Tana : MonoBehaviour
    {
        [SerializeField]private Image image;
        private bool trigger;
        [SerializeField]private float incrementSpeed = 0.1f;
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            trigger = true;
        }
    
        private void OnTriggerExit2D(Collider2D other)
        {
            trigger = false;
        }

        private void Start()
        {
            trigger = false;
        }

        void Update()
        {
            if (trigger)
            {
                image.fillAmount += incrementSpeed * Time.deltaTime;
                if (image.fillAmount >= 1.0f)
                {
                    Completato();
                }
            }
        }

        private void Completato()
        {
            trigger = false;
            onCompleteCallback.Invoke();
        }
    
    
        public delegate void ProgressBarCompleteDelegate();
        private ProgressBarCompleteDelegate onCompleteCallback;

        public void OnComplete(ProgressBarCompleteDelegate callback)
        {
            onCompleteCallback = callback;
        }
    }
}
