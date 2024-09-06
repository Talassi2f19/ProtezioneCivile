using Script.Utility;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace minigame.svuotaAcqua
{
    public class PompaGioco : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemapWater;
        [SerializeField] private GameObject pompa;
        [SerializeField] private GameObject detectPoint;
        private SpriteRenderer spriteRendererPompa;
        private bool isDragging;
        private Vector2 defPos;
        [SerializeField]private int TileFatte;
        [SerializeField] private int num;
        [SerializeField ]private Vector2 betterPos;

        void Start()
        {
            transform.position = Info.localUser.coord;
            spriteRendererPompa = pompa.GetComponent<SpriteRenderer>();
            isDragging = false;
            defPos = pompa.transform.position;
        }

        void Update()
        {
            if (isDragging)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pompa.transform.position = mousePos + betterPos;
                CambiaColore();
            }
        
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                betterPos = defPos - mousePos;
                if (spriteRendererPompa.bounds.Contains(mousePos))
                {
                    isDragging = true;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                pompa.transform.position = defPos;
                if (TileFatte == num)
                {
                    completatoCallback.Invoke();
                }
            }
        }
    
        private void CambiaColore()
        {
            Vector3Int pos = tilemapWater.WorldToCell(detectPoint.transform.position);
            float alpha = tilemapWater.GetColor(pos).a - 0.4f;
            Debug.Log(pos + ", " + alpha);
            if(alpha < 0f) //già completata
                return;
            if (alpha < 0.3f && alpha > 0f)//completata
            {
                TileFatte++;
                Debug.Log(TileFatte);
                alpha = 0f;
            }
            tilemapWater.SetColor(pos, new Color(1f, 1f, 1f, alpha));
        }
    
    
        public delegate void Completato();
        private Completato completatoCallback;
    
        public void OnComplete(Completato tmp)
        {
            completatoCallback = tmp;
        }
    }
}
