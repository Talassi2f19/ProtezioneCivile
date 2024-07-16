using System;
using UnityEngine;

namespace minigame.Incidente
{
    public class Pt1 : MonoBehaviour
    {
        public bool type = false;
        [SerializeField]private Danni danni;
        [SerializeField] private GameObject minimappaPoint;

        private void Start()
        {
            minimappaPoint.SetActive(type);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (type)
            {
                completeCallback.Invoke();
            }
            else
            {
                danni.Inizia();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            danni.Fine();
        }
        
        public delegate void Pt1CompleteCallback();
        private Pt1CompleteCallback completeCallback;

        public void Pt1Completato(Pt1CompleteCallback tmp)
        {
            completeCallback = tmp;
        }
    }
}
