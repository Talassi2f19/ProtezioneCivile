using System;
using System.Collections;
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
        [SerializeField] private GameObject waitText;
        private bool waitGen = true;
        private Coroutine coroutine;
        
        private void OnEnable()
        {
            if (parent.childCount == 0)
            {
                waitText.SetActive(true);
                coroutine = StartCoroutine(GetPlayer());
            }
            else
            {
                waitText.SetActive(false);
            }
        }

        private void OnDisable()
        {
            if(coroutine != null)
                StopCoroutine(coroutine);
        }

        private void Genera(string jsonText)
        {
            waitGen = false;
            waitText.SetActive(false);
            JSONObject json = new JSONObject(jsonText);
            for (int i = 0; i < json.count; i++)
            {
                if (!json.list[i].GetField("Virtual"))
                {
                    string testo = json.list[i].GetField("Name").stringValue + " - " + json.list[i].GetField("Role").stringValue;
                    Instantiate(prefab, parent).GetComponent<GenericTextPrefab>().SetGenericText(testo);
                }
            }
        }

        private IEnumerator GetPlayer()
        {
            while (waitGen)
            {
                RestClient.Get(Info.DBUrl+Info.sessionCode+"/"+Global.PlayerFolder+".json").Then(e =>
                {
                    if (!e.Text.ToLower().Contains("null"))
                    {
                        Genera(e.Text);
                    }
                });
                yield return new WaitForSeconds(3f);
            }
        }
    }
}
