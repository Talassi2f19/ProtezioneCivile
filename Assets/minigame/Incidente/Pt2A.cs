using UnityEngine;
using UnityEngine.Tilemaps;

namespace minigame.Incidente
{
    public class Pt2A : MonoBehaviour
    {
        private bool isDragging;
        private Vector2 betterPos;
        private Vector2 defPos;
        private SpriteRenderer spriteRecinzione;
        [SerializeField] private GameObject spriteObj;
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private int tileTot;
        private int tileFatte;

        // Start is called before the first frame update
        private void Start()
        {
            isDragging = false;
            defPos = spriteObj.transform.position;
            spriteRecinzione = spriteObj.GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (isDragging)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                spriteObj.transform.position = mousePos + betterPos;
                Piazza();
            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                betterPos = defPos - mousePos;
                if (spriteRecinzione.bounds.Contains(mousePos))
                {
                    isDragging = true;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                spriteObj.transform.position = defPos;
                if (tileFatte == tileTot)
                { 
                    completatoCallback.Invoke();
                }
            }
        }
        
        private void Piazza()
        {
            Vector3Int pos = tilemap.WorldToCell(spriteObj.transform.position);
            if (tilemap.GetTile(pos) == null)
                return;
            tilemap.SetTile(pos, null);
            tileFatte++;
        }

        public delegate void CompletatoCallback();
        private CompletatoCallback completatoCallback;

        public void Completato(CompletatoCallback tmp)
        {
            completatoCallback = tmp;
        }
    }
}
