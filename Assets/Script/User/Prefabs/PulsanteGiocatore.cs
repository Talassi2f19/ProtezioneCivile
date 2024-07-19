using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Scene = Script.Utility.Scene;

namespace Script.User.Prefabs
{
    public class PulsanteGiocatore : MonoBehaviour
    {
        protected string nomeGiocatore;
        [SerializeField] private GameObject pulsante;
    
        public void SetName(string str)
        {
            nomeGiocatore = str;
            pulsante.GetComponentInChildren<TMP_Text>().text = nomeGiocatore;
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(pulsante.GetComponentInChildren<TMP_Text>().preferredWidth + 20 + 40, 40 + 40);
        }

        //al click vota una persona (votazione)
        public void ClickVotazione()
        {
            PlayerSelected();
            HideAll();
            /*
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.CandidatiFolder + "/" + nomeGiocatore + ".json").Then(f =>
            {
                int value = int.Parse(f.Text) + 1;
                string send = "{\"" + nomeGiocatore + "\":" + value + "}";
                RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.CandidatiFolder + ".json", send);
            });
            */
            string toSend = "{\"Voto\":\"" + nomeGiocatore + "\"}";
            RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + ".json", toSend).Catch(Debug.LogError);
        }
        
        public void ClickSelezionaCoc()
        {
            PlayerSelected();
            HideAll();
            string patchRequest = "{\"" + Global.RuoloPlayerKey + "\":\"" + Ruoli.Coc + "\"}";
            //va direttamente alla scena successiva
            RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + nomeGiocatore + ".json", patchRequest).Then(
                e =>
                {
                    //genera gli altri ruoli
                    // GeneraRuoli generaRuoli = new GeneraRuoli();
                    // generaRuoli.Genera();
                    
                    //cambia scena
                    SceneManager.LoadScene(Scene.User.AttesaRuoli);
                }).Catch(Debug.LogError);
        }
        
        public void PlayerSelected()
        {
            SetOff();
            pulsante.GetComponent<Image>().color = Color.red;
      //      gameObject.GetComponentInParent<RectTransform>().gameObject.SendMessage("PlayerHasSelected", SendMessageOptions.DontRequireReceiver);
       //     SendMessageUpwards("PlayerHasSelected", SendMessageOptions.DontRequireReceiver);
        }
        
        //disattiva se stesso
        public void SetOff()
        {
            pulsante.GetComponent<Button>().interactable = false;
        }

        //disattiva tutti i pulsanti
        private void HideAll()
        {
            foreach (Transform child in gameObject.transform.parent.transform)
            {
                child.gameObject.GetComponent<PulsanteGiocatore>().SetOff();
            }
        }
    }
}
