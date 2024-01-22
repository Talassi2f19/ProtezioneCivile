using System.Runtime.InteropServices;
using UnityEngine;
// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace Script.Utility
{
    //funzioni che si interfacciano con il browser tramite javascript (/plugin/function.jslib)
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
