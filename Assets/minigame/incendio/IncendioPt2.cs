using Script.Utility;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace minigame.incendio
{
    public class IncendioPt2 : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemapFire;
        [SerializeField] private GameObject estintore;
        [SerializeField] private GameObject detectPoint;
        [SerializeField] private GameObject estintoreFumo;
        private SpriteRenderer spriteRendererEstintore;
        private bool isDragging;
        private Vector2 defPos;
        private int TileFatte;
        [SerializeField] private int num;
        [SerializeField ]private Vector2 betterPos;

        void Start()
        {
            transform.position = Info.localUser.coord;
            spriteRendererEstintore = estintore.GetComponent<SpriteRenderer>();
            isDragging = false;
            defPos = estintore.transform.position;
            estintoreFumo.SetActive(false);
        }

        void Update()
        {
            if (isDragging)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                estintore.transform.position = mousePos + betterPos;
                CambiaColore();
            
            }
    
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                betterPos = defPos - mousePos;
                if (spriteRendererEstintore.bounds.Contains(mousePos))
                {
                    isDragging = true;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                estintore.transform.position = defPos;
                if (TileFatte == num)
                {
                    completatoCallback.Invoke();
                }
            }
        }

        private void CambiaColore()
        {
            Vector3Int pos = tilemapFire.WorldToCell(detectPoint.transform.position);
        
            if (tilemapFire.GetTile(pos) == null)
            {
                estintoreFumo.SetActive(false);
                return;
            }
        
            float alpha = tilemapFire.GetColor(pos).a - 0.05f;
            // if(alpha < 0f) //già completata
            // {
            //     estintoreFumo.SetActive(false);
            //     return;
            // }
            if (alpha < 0.3f && alpha > 0f)//completata
            {
                estintoreFumo.SetActive(false);
                TileFatte++;
                tilemapFire.SetTile(pos, null);
                //alpha = 0f;
            }
            estintoreFumo.SetActive(true);
            tilemapFire.SetColor(pos, new Color(1f, 1f, 1f, alpha));
        }


        public delegate void Completato();
        private Completato completatoCallback;

        public void OnComplete(Completato tmp)
        {
            completatoCallback = tmp;
        }
    }
}
