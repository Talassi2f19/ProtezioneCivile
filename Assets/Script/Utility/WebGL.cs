using System.Runtime.InteropServices;
using UnityEngine;
// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace Script.Utility
{
    //funzioni che si interfacciano con il browser tramite javascript (/plugin/function.jslib)
    public class WebGL : MonoBehaviour
    {
        public static bool isMobile = true;
        
        [DllImport("__Internal")]
        private static extern bool Mobile();
        [DllImport("__Internal")]
        public static extern void SetCookie(string cookie);
        [DllImport("__Internal")]
        public static extern string GetCookie();
        
        private void Start()
        {
 // #if !UNITY_EDITOR
 //        isMobile = Mobile();
 // #endif
            Debug.Log("è mobile: " + (isMobile ? "true" : "false"));
        }
    }
}
