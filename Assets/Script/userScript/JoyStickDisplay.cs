using Script.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoyStickDisplay : MonoBehaviour
{
    [SerializeField] private GameObject joyStick;

    void Start()
    {
        //attiva il joystick
        //joyStick.SetActive(WebGL.IsMobile);
    }
}
