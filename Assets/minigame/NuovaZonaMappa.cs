using System.Collections;
using UnityEngine;

namespace minigame
{
    public class NuovaZonaMappa : MonoBehaviour
    {
        private BoxCollider2D collider;
        // Start is called before the first frame update
        void Start()
        {
            collider = GetComponent<BoxCollider2D>();
            StartCoroutine(ExecuteWithDelay());
        }
    
        private void OnTriggerStay2D(Collider2D other)
        {
            other.transform.position = new Vector3(-6.9f, 13f, 0);
            collider.enabled = false;
        }
    
        IEnumerator ExecuteWithDelay() {
            yield return new WaitForSeconds(1f);
            collider.enabled = false;
        }
    
    }
}
