using System.Collections;
using UnityEngine;

namespace minigame
{
    public class NuovaZonaMappa : MonoBehaviour
    {
        [SerializeField]private BoxCollider2D colliders;
        
        void Start()
        {
            StartCoroutine(ExecuteWithDelay());
        }
    
        private void OnTriggerStay2D(Collider2D other)
        {
            other.transform.position = new Vector3(0f, 0f, 0f);
            colliders.enabled = false;
        }
    
        IEnumerator ExecuteWithDelay() {
            yield return new WaitForSeconds(2f);
            colliders.enabled = false;
        }
    
    }
}
