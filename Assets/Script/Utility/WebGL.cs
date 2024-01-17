using System.Runtime.InteropServices;
using UnityEngine;

namespace Script.Utility
{
    public class WebGL : MonoBehaviour
    {
        [SerializeField]
        public static bool IsMobile = false;
        
        [DllImport("__Internal")]
        private static extern bool Mobile();
        [DllImport("__Internal")]
        public static extern void SetCookie(string cookie);
        [DllImport("__Internal")]
        public static extern string GetCookie();
        
        private void Start()
        {
#if !UNITY_EDITOR
        IsMobile = Mobile();
#endif
            Debug.Log("è mobile: " + (IsMobile ? "true" : "false"));
        }
    }
}
