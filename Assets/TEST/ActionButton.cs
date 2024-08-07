using System;
using Proyecto26;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Script.test
{
    public class ActionButton : MonoBehaviour
    {
        [SerializeField]private BoundsInt bounds;
        [SerializeField]private TileBase[] allTiles;
        [SerializeField] private int ff;
        [SerializeField] private Tile hh;
        private Tilemap tilemap;
        private void Start()
        {
            RestClient.Put("https://prtcv-de7d0-default-rtdb.europe-west1.firebasedatabase.app/aa.json",
                "{\".sv\": {\"increment\":10}}}").Catch(Debug.LogError);
            // tilemap = GetComponent<Tilemap>();
            //  
            //  bounds = tilemap.cellBounds;
            //  allTiles = tilemap.GetTilesBlock(bounds);
            //  ff = allTiles.Length;
        }

        [SerializeField] private BoundsInt boundssss = new BoundsInt();
    
        private void Update()
        {
            if (boundssss != tilemap.cellBounds)
            {
                boundssss = tilemap.cellBounds;
                // Debug.Log(tilemap.GetTilesBlock(tilemap.cellBounds).Length);
            }
        }

        // [ContextMenu("dsadas")]
        // public void gg()
        // {
        //     Debug.Log(allTiles.Length);
        // }
        //
        // [ContextMenu("ss")]
        // public void gsg()
        // {
        //     tilemap.SetTile(new Vector3Int(50,50,0),hh);
        // }
        // [ContextMenu("sss")]
        // public void gsgs()
        // {
        //     tilemap.ClearAllTiles();
        // }

    }
}
