using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace minigame.Incidente
{
    public class Pt2C : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private GameObject scopa;
        [SerializeField] private GameObject detectPoint;
        private SpriteRenderer spriteRenderer;
        private bool isDragging;
        private Vector2 defPos;
        private int tileFatte;
        [SerializeField] private int tileTot;
        private Vector2 betterPos;

        void Start()
        {
            spriteRenderer = scopa.GetComponent<SpriteRenderer>();
            isDragging = false;
            defPos = scopa.transform.position;
        }

        void Update()
        {
            if (isDragging)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                scopa.transform.position = mousePos + betterPos;
                CambiaColore();
            }
        
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                betterPos = defPos - mousePos;
                if (spriteRenderer.bounds.Contains(mousePos))
                {
                    isDragging = true;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                scopa.transform.position = defPos;
                if (tileFatte == tileTot)
                {
                    completatoCallback.Invoke();
                }
            }
        }
    
        private void CambiaColore()
        {
            Vector3Int pos = tilemap.WorldToCell(detectPoint.transform.position);
            float alpha = tilemap.GetColor(pos).a - 0.05f;
            if(alpha < 0f) //già completata
                return;
            if (alpha < 0.3f && alpha > 0f)//completata
            {
                tileFatte++;
                alpha = 0f;
            }
            tilemap.SetColor(pos, new Color(1f, 1f, 1f, alpha));
        }
    
    
        public delegate void CompletatoCollback();
        private CompletatoCollback completatoCallback;
    
        public void Completato(CompletatoCollback tmp)
        {
            completatoCallback = tmp;
        }
    }
}
