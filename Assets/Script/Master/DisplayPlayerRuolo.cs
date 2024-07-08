using Defective.JSON;
using Proyecto26;
using Script.Master.Prefabs;
using Script.Utility;
using UnityEngine;
// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace Script.Master
{
    public class DisplayPlayerRuolo : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject prefab;
        void Start()
        {
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + ".json").Then(e =>
            {
                JSONObject json = new JSONObject(e.Text);
                for (int i = 0; i < json.count; i++)
                {
                    string testo = json.list[i].GetField("Name").stringValue + " - " + json.list[i].GetField("Role").stringValue;
                    Instantiate(prefab, parent).GetComponent<GenericTextPrefab>().SetGenericText(testo);
                }
            }).Catch(Debug.LogError);
        }
    
    }
}
