using UnityEngine;

// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace Script.User
{
    //classe per la gestione dei player
    public class MostraRuoloManager : MonoBehaviour
    {
        [SerializeField] private GameObject mostraRuoloPrefab;
        [SerializeField] private GameObject infoRuolo;
        [SerializeField] private JoyStick joyStick;

        private void Start()
        {
            joyStick.Enable(true);
        }

        public void ShowMostraRuolo()
        {
            mostraRuoloPrefab.SetActive(true);
            infoRuolo.SetActive(false);
            joyStick.Enable(false);
        }

        public void CloseMostraRuolo()
        {
            mostraRuoloPrefab.SetActive(false);
            infoRuolo.SetActive(true);
            joyStick.Enable(true);
        }
    }
}