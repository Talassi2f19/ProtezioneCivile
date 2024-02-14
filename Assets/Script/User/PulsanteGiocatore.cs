using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.User
{
    public class PulsanteGiocatore : MonoBehaviour
    {
        protected string nomeGiocatore;
        [SerializeField] protected GameObject pulsante;
    
        public void SetName(string str)
        {
            nomeGiocatore = str;
            pulsante.GetComponentInChildren<TMP_Text>().text = nomeGiocatore;
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(pulsante.GetComponentInChildren<TMP_Text>().preferredWidth + 20, 40);
        }

        public void ClickVotazione()
        {
            PlayerSelected();
            RestClient.Get(Info.DBUrl + Info.SessionCode + "/candidati/" + nomeGiocatore + ".json").Then(f =>
            {
                int value = int.Parse(f.Text) + 1;
                string send = "{\"" + nomeGiocatore + "\":" + value + "}";
                RestClient.Patch(Info.DBUrl + Info.SessionCode + "/candidati" + ".json", send);
            });
        }

        public void ClickSelezionaCoc()
        {
            string patchRequest = "{\"role\":\"coc\"}";
            PlayerSelected();

            RestClient.Patch(Info.DBUrl + Info.SessionCode + "/players/" + nomeGiocatore + ".json", patchRequest);
        }

        public void PlayerSelected()
        {
            SetOff();
            pulsante.GetComponent<Image>().color = Color.red;
            SendMessageUpwards("PlayerHasSelected", SendMessageOptions.DontRequireReceiver);
        }
    
        public void SetOff()
        {
            pulsante.GetComponent<Button>().interactable = false;
        }
    }
}
