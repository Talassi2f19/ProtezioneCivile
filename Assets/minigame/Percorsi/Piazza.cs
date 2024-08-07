using System;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace minigame.Percorsi
{
    public class Piazza : MonoBehaviour
    {
        private int tileFatte;
        [SerializeField] private int tileTot;
        [SerializeField] private Tilemap tileMapNascondi;
        public TextMeshProUGUI text;
        private bool isDragging;

        private void Start()
        {
            text.text = "Coni piazzati: " + tileFatte + " di " + tileTot;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                if(tileFatte == tileTot) 
                    _completeCallback.Invoke();
            }

            if (isDragging)
            {
                PiazzaCono();
            }
        }

        private void PiazzaCono()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int pos = tileMapNascondi.WorldToCell(mousePos);
            if(tileMapNascondi.GetTile(pos) == null)
                return;
            tileMapNascondi.SetTile(pos, null);
            tileFatte++;
            text.text = "Coni piazzati: " + tileFatte + " di " + tileTot;
        }


        public delegate void OnCompleteCallback();
        private OnCompleteCallback _completeCallback;

        public void OnComplete(OnCompleteCallback tmp)
        {
            _completeCallback = tmp;
        }
    }
}
