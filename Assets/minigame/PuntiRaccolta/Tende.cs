using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace minigame.PuntiRaccolta
{
    [Serializable]
    class Tenda
    {
        [HideInInspector]public Vector2 defPos;
        public GameObject obj;
        [HideInInspector]public SpriteRenderer spriteObj;
        public Tilemap tilemap;
        public List<Vector3Int> tilePos = new List<Vector3Int>();
        public List<Tile> tile = new List<Tile>();
    }
    
    public class Tende : MonoBehaviour
    {
        private bool isDragging;
        private int tendaDragging = -1;
        [SerializeField]private List<Tenda> tenda;
        private Vector2 betterPos;
        [SerializeField]private int TendePiazzate;
        private bool errori;
        private Vector3Int lastTile = Vector3Int.zero;
    
        // Start is called before the first frame update
        private void Start()
        {
            isDragging = false;
            foreach (var tmp in tenda)
            {
                tmp.spriteObj = tmp.obj.GetComponent<SpriteRenderer>();
                tmp.defPos = tmp.obj.transform.position;
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (isDragging)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                tenda[tendaDragging].obj.transform.position = mousePos + betterPos;
                Piazza();
            }

            if (Input.GetMouseButtonDown(0))
            {
            
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                for (var i = 0; i < tenda.Count && tendaDragging == -1; i++)
                {
                    Debug.Log("dsa");
                    var tmp = tenda[i];
                    if (tmp.spriteObj.bounds.Contains(mousePos))
                    {
                        betterPos = tmp.defPos - mousePos;
                        isDragging = true;
                        tendaDragging = i;
                        tmp.spriteObj.enabled = false;
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (tendaDragging > -1)
                {
                    tenda[tendaDragging].spriteObj.enabled = true;
                    if (errori)
                    {
                        tenda[tendaDragging].tilemap.ClearAllTiles();
                    }
                    tenda[tendaDragging].obj.transform.position = tenda[tendaDragging].defPos;
                }
            
                isDragging = false;
                tendaDragging = -1;

                int num = 0; 
                foreach (var tmp in tenda)
                {
                    if (tmp.tilemap.GetTilesBlock(tmp.tilemap.cellBounds).Length != 0)
                        num++;
                }
                if (num == tenda.Count)
                {
                    completatoCallback.Invoke();
                }
            }
        }
    
        private void Piazza()
        {
            Tenda tt = tenda[tendaDragging]; 
            Vector3Int pos = tt.tilemap.WorldToCell(tt.obj.transform.position);
            if (pos == lastTile)
                return;
            lastTile = pos;
            errori = false;
            tt.tilemap.ClearAllTiles();
            for (int i = 0; i < tt.tilePos.Count; i++)
            {
                Vector3Int newTilePos = tt.tilePos[i] + pos;
            
                bool err = newTilePos.x > 6 || newTilePos.x < -9 || newTilePos.y > 3 || newTilePos.y < -4;

                for (int k = 0; k < tenda.Count; k++)
                {
                    if (tenda[k].tilemap.GetTile(newTilePos) != null)
                    {
                        err = true;
                    }
                }
                tt.tilemap.SetTile(newTilePos, tt.tile[i]);
                if (err)
                {
                    tt.tilemap.SetColor(newTilePos, new Color(1f, 0f, 0f, 1f));
                    errori = true;
                }
            }
        }

        public delegate void CompletatoCallback();
        private CompletatoCallback completatoCallback;

        public void Completato(CompletatoCallback tmp)
        {
            completatoCallback = tmp;
        }
    }
}