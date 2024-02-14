using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class PulsanteVotazioni : MonoBehaviour
    {
        private string nomeGiocatore;
        [SerializeField] private GameObject pulsante;

        public void SetName(string str)
        {
            nomeGiocatore = str;
            pulsante.GetComponentInChildren<TMP_Text>().text = nomeGiocatore;
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(pulsante.GetComponentInChildren<TMP_Text>().preferredWidth + 20, 40);
        }
        public void ClickVotazione()
        {
            SetOff();
            pulsante.GetComponent<Image>().color = Color.red;
            SendMessageUpwards("PlayerHasVoted", SendMessageOptions.DontRequireReceiver);
            RestClient.Get(Info.DBUrl + Info.SessionCode + "/candidati/" + nomeGiocatore + ".json").Then(f =>
            {
                int value = int.Parse(f.Text) + 1;
                string send = "{\"" + nomeGiocatore + "\":" + value + "}";
                RestClient.Patch(Info.DBUrl + Info.SessionCode + "/candidati" + ".json", send);
            });
        }

        public void SetOff()
        {
            pulsante.GetComponent<Button>().interactable = false;
        }

        public Vector3 getCoords()
        {
            return new Vector3(pulsante.GetComponent<RectTransform>().sizeDelta.x, pulsante.GetComponent<RectTransform>().sizeDelta.y, 0);
        }
    }
}
