using UnityEngine;

namespace Script.User
{
    public class JoyStickDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject joyStick;

        void Start()
        {
            //attiva il joystick
            //joyStick.SetActive(WebGL.IsMobile);
        }
    }
}
