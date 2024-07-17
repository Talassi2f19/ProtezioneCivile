using System;
using UnityEngine;

namespace minigame.Battito
{
    public class Stage1 : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            _st1Callback.Invoke();
        }


        
        
        public delegate void St1callback();
        private St1callback _st1Callback;
        public void Completato(St1callback tmp)
        {
            _st1Callback = tmp;
        }

    }
}
