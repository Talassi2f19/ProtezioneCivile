using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class PulsanteVotazioni : MonoBehaviour
    {
        private string name;
        public GameObject pulsante;
        void Start()
        {
            
        }

        public void SetName(string str)
        {
            name = str;
            pulsante.GetComponentInChildren<TMP_Text>().text = name;
            pulsante.GetComponent<RectTransform>().sizeDelta =
                new Vector2(pulsante.GetComponentInChildren<TMP_Text>().preferredWidth + 20,
                    pulsante.GetComponentInChildren<TMP_Text>().preferredHeight + 5);
        }
        public void ClickVotazione()
        {
            RestClient.Get(Info.DBUrl + Info.SessionCode + "/candidati/" + name + ".json").Then(f =>
            {
                int value = int.Parse(f.Text) + 1;
                string send = "{\"" + name + "\":" + value + "}";
                RestClient.Patch(Info.DBUrl + Info.SessionCode + "/candidati" + ".json", send);
            });
            pulsante.GetComponent<Image>().color = Color.yellow;
            SendMessageUpwards("HasVoted", SendMessageOptions.DontRequireReceiver);
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
