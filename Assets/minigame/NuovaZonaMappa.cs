using System.Collections;
using UnityEngine;

namespace minigame
{
    public class NuovaZonaMappa : MonoBehaviour
    {
        [SerializeField]private BoxCollider2D colliders;

        private bool flag;
        
        void Start()
        {
            flag = true;
            StartCoroutine(ExecuteWithDelay());
        }
    
        private void OnTriggerStay2D(Collider2D other)
        {
            if (flag)
            {
                other.transform.position = new Vector3(0f, 0f, 0f);
                colliders.enabled = false;
                flag = false;
            }
        }
    
        IEnumerator ExecuteWithDelay() {
            yield return new WaitForSeconds(2f);
            colliders.enabled = false;
            flag = false;
        }
    
    }
}
