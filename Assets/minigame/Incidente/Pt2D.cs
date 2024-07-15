using UnityEngine;
using UnityEngine.Tilemaps;

namespace minigame.Incidente
{
    public class Pt2D : MonoBehaviour
    {
        private bool isDragging = false;
        [SerializeField]private Tilemap tilemap;
        private int rimosse;
        [SerializeField]private int tileTot;
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                Controlla();
            }
            
            if (isDragging)
            {
                RimuoviCose();
            }
        }

        private void RimuoviCose()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int pos = tilemap.WorldToCell(mousePos);
            if (tilemap.HasTile(pos))
            {
                tilemap.SetTile(pos,null);
                rimosse++;
            }
                
        }

        private void Controlla()
        {
            if (tileTot == rimosse)
            {
                completeCallBack.Invoke();
            }
        }

        public delegate void CompleteCallBack();
        private CompleteCallBack completeCallBack;

        public void Completato(CompleteCallBack tmp)
        {
            completeCallBack = tmp;
        }
    }
}
