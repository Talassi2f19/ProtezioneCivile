using System;
using UnityEngine;
using UnityEngine.UI;

namespace minigame.MaterialePericoloso
{
    public class Monnezza : MonoBehaviour
    {
        [SerializeField] private Image image;
        private bool trigger;
        [SerializeField] private float incrementSpeed = 0.1f;
        private bool type;
        [SerializeField] private Danni danni;
        [SerializeField]private GameObject minimappaPoint;

        public void SetType(bool flag)
        {
            type = flag;
        }

        private void OnEnable()
        {
            image.fillAmount = 0f;
            minimappaPoint.SetActive(type);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (type)
            {
                 trigger = true;
            }
            else
            {
                danni.Inizia();
            }
           
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (type)
            {
                trigger = false;
            }
            else
            {
                danni.Fine();
            }
        }

        private void Start()
        {
            trigger = false;
        }

        void Update()
        {
            if (type && trigger)
            {
                image.fillAmount += incrementSpeed * Time.deltaTime;
                if (image.fillAmount >= 1.0f)
                {
                    trigger = false;
                    onCompleteCallback.Invoke();
                }
            }
        }

        
        public delegate void ProgressBarCompleteDelegate();

        private ProgressBarCompleteDelegate onCompleteCallback;

        public void OnComplete(ProgressBarCompleteDelegate callback)
        {
            onCompleteCallback = callback;
        }
    }
}