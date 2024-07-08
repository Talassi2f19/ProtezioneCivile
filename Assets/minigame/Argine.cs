using UnityEngine;
using UnityEngine.Tilemaps;

namespace minigame
{
    public class Argine : MonoBehaviour
    {

        private bool chiudi;
        private TilemapCollider2D tilemapCollider2D;

        private void Start()
        {
            chiudi = false;
            tilemapCollider2D = GetComponent<TilemapCollider2D>();
        }
    

        private void OnTriggerExit2D(Collider2D other)
        {
            if(chiudi)
                tilemapCollider2D.isTrigger = false;
        }
    
        public void ArgineAccessibile(bool flag)
        {
        
            if (flag)
            {
                tilemapCollider2D.isTrigger = true;
                chiudi = false;
            }
            else
            {
                chiudi = true;
            }
        }
    }
}
