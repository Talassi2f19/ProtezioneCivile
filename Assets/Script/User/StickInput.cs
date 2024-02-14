using UnityEngine;

// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace Script.User
{
    //classe per inviare la posizione al player
    public class StickInput : MonoBehaviour
    {
        private GameObject pl;
        private void Start()
        {
            pl = GameObject.Find("LocalPlayer");
        }

        // OnMove viene chiamato da JoyStickLogic
        private void OnMove(Vector2 moveValue)
        {
            pl.GetComponent<PlayerLocal>().OnMove(moveValue);
        }
    }
}
