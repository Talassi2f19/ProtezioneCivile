using Defective.JSON;
using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scenes.User.telefono
{
    public class ListaVolontari : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform parent;

        private Ruoli roleType;
        private void Start()
        {
            switch (Info.localUser.role)
            {
                case Ruoli.RefPC:
                    roleType = Ruoli.VolPC;
                    break;
                case Ruoli.RefCri:
                    roleType = Ruoli.VolCri;
                    break;
                case Ruoli.RefFuoco:
                    roleType = Ruoli.VolFuoco;
                    break;
                case Ruoli.RefGgev:
                    roleType = Ruoli.VolGgev;
                    break;
                case Ruoli.RefPolizia:
                    roleType = Ruoli.VolPolizia;
                    break;
            }
        }

        private void OnEnable()
        {
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + ".json").Then(e =>
            {
                foreach (Transform figlio in parent)
                {
                    figlio.gameObject.SetActive(false);
                    // Destroy(figlio.gameObject);
                }
                Mostra(new JSONObject(e.Text));    
            }).Catch(Debug.Log);
        }

        private void Mostra(JSONObject json)
        {
            foreach (var var in json.list)
            {
                if (var.GetField("Role").stringValue == roleType.ToString())
                {
                    GameObject tmp = Instantiate(prefab, parent);
                    tmp.GetComponentInChildren<TextMeshProUGUI>().text = var.GetField("Name").stringValue;
                    if (var.GetField("Occupato").boolValue)
                    {
                        tmp.GetComponentInChildren<Image>().color = Color.red;
                    }
                }
            }
        }
    }
}
