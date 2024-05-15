using Script.Utility;
using UnityEngine;


namespace Script.User
{
    public class JoyStick : MonoBehaviour
    {
        public void Enable(bool value)
        {
            if(value)
                gameObject.SetActive(WebGL.isMobile);
            else
                gameObject.SetActive(false);
        }
    }
}
