using Script.Utility;
using UnityEngine;


namespace Script.User
{
    public class JoyStickDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject joyStick;

        private void Start()
        {
            //attiva il joystick
            joyStick.SetActive(WebGL.isMobile);
        }
    }
}
