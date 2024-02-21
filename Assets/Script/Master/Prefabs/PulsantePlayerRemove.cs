using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;

// ReSharper disable CommentTypo

namespace Script.Master.Prefabs
{
    public class PulsantePlayerRemove : MonoBehaviour
    {
        private string nomeGiocatore;
        [SerializeField] private GameObject pulsante;

        public void SetName(string str)
        {
            nomeGiocatore = str;
            pulsante.GetComponentInChildren<TMP_Text>().text = nomeGiocatore;
          
            //Vector2 dim = new Vector2(pulsante.GetComponentInChildren<TMP_Text>().preferredWidth + 20, pulsante.GetComponentInChildren<TMP_Text>().preferredHeight + 10);
            Vector2 dim = new Vector2(pulsante.GetComponentInChildren<TMP_Text>().preferredWidth + 20, 40);
            gameObject.GetComponent<RectTransform>().sizeDelta = dim;
        }
        public void OnClick()
        {
            gameObject.SetActive(false);
            RestClient.Delete(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + nomeGiocatore + ".json");
        }
    }
}
