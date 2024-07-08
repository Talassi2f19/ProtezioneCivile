using UnityEngine;

namespace minigame.PuntiRaccolta
{
    public class P1 : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            stage1Callback.Invoke();
        }
    
        public delegate void Stage1Callback();
        private Stage1Callback stage1Callback;

        public void Pt1Complete(Stage1Callback tmp)
        {
            stage1Callback = tmp;
        }
    }
}

