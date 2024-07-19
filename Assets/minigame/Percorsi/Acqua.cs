using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace minigame.Percorsi
{
    public class Acqua : MonoBehaviour
    {
        public bool type;
        [SerializeField] private TilemapCollider2D tilemapCollider;
        [SerializeField] private Danni danni;
        [SerializeField] private GameObject piazza;
        [SerializeField] private GameObject minimappa;
        public GameObject text; 
        
        
        private void Start()
        {
            tilemapCollider.isTrigger = !type;
            minimappa.SetActive(type);
            piazza.SetActive(type);
            text.SetActive(type);
            text.GetComponent<TextMeshProUGUI>().text = "";
            if (type)
            {
                piazza.GetComponent<Piazza>().text = text.GetComponent<TextMeshProUGUI>();
                piazza.GetComponent<Piazza>().OnComplete(_completeCallback.Invoke);
            }
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            danni.Inizia();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            danni.Fine();
        }
        
        
        public delegate void CompleteCallback();
        private CompleteCallback _completeCallback;

        public void OnComplete(CompleteCallback tmp)
        {
            _completeCallback = tmp;
        }
    }
}
