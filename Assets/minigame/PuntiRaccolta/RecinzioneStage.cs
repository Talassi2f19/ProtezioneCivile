using UnityEngine;
using UnityEngine.Tilemaps;

namespace minigame.PuntiRaccolta
{
    public class RecinzioneStage : MonoBehaviour
    {
        private bool isDragging;
        private Vector2 betterPos;
        private Vector2 defPos;
        private SpriteRenderer spriteRecinzione;
        [SerializeField] private GameObject recinzione;
        [SerializeField] private Tilemap tilemap;
        [SerializeField]private int tileTot;

        [SerializeField] private int tileFatte;

        // Start is called before the first frame update
        private void Start()
        {
            isDragging = false;
            defPos = recinzione.transform.position;
            spriteRecinzione = recinzione.GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (isDragging)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                recinzione.transform.position = mousePos + betterPos;
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
                recinzione.transform.position = defPos;
                if (tileFatte == tileTot)
                { 
                    completatoCallback.Invoke();
                }
            }
        }

        // [SerializeField]private BoundsInt bounds;
        // [SerializeField]private TileBase[] allTiles;
        // [SerializeField] private int gg;
        private void Piazza()
        {
            Vector3Int pos = tilemap.WorldToCell(recinzione.transform.position);
            if (tilemap.GetTile(pos) == null)
                return;
            tilemap.SetTile(pos, null);
            tileFatte++;
            // bounds = tilemap.cellBounds;
            // allTiles = tilemap.GetTilesBlock(bounds);
            // gg = allTiles.Length;
        }

        public delegate void CompletatoCallback();
        private CompletatoCallback completatoCallback;

        public void Completato(CompletatoCallback tmp)
        {
            completatoCallback = tmp;
        }

    }
}
