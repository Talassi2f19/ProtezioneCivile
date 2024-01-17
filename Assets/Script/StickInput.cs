using UnityEngine;

namespace Script
{
    public class StickInput : MonoBehaviour
    {
        private GameObject pl;
        private void Start()
        {
            pl = GameObject.Find("LocalPlayer");
        }

        private void OnMove(Vector2 moveValue)
        {
            pl.GetComponent<PlayerLocal>().OnMove(moveValue);
        }
    }
}
