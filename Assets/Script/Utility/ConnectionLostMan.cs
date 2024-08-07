using System;
using UnityEngine;

namespace Script.Utility
{
    public class ConnectionLostMan : MonoBehaviour
    {
        [SerializeField]private GameObject obj;

        private void Start()
        {
            obj.SetActive(false);
        }

        public void Enable()
        {
            obj.SetActive(true);
        }

        public void Disable()
        {
            obj.SetActive(false);
        }
    }
}
