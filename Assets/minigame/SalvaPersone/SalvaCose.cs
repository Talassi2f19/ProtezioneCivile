using System.Collections.Generic;
using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace minigame.SalvaPersone
{
    public class SalvaCose : MonoBehaviour
    {
        [SerializeField]private GameObject prefab;
        private TextMeshProUGUI text;
        [SerializeField] private GameObject ObjText;

        [SerializeField] private List<Vector2> posizioni;

        private void Start()
        {
            ObjText.SetActive(false);
            text = ObjText.GetComponent<TextMeshProUGUI>();
        }
        
        [ContextMenu("genera")]
        public void Genera()
        {
            // RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":"+ (val+2000) +"}");
            for (int i = 0; i < posizioni.Count; i++)
            {
                GameObject tmp = Instantiate(prefab, posizioni[i], new Quaternion(), transform);
                tmp.GetComponent<Button>().onClick.AddListener(()=> Click(tmp));
            }
            ObjText.SetActive(true);
            text.text = "Da salvare: " + transform.childCount;
        }
        
        public void Click(GameObject tmp)
        {
            DestroyImmediate(tmp);
            text.text = "Da salvare: " + transform.childCount;
            if (transform.childCount == 0)
            {
                ObjText.SetActive(false);
                //task done
                
                RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + ".json", "{\"Occupato\":false}");
                RestClient.Get(Info.DBUrl + Info.sessionCode + "/score.json").Then(e =>
                {
                    RestClient.Patch(Info.DBUrl + Info.sessionCode + ".json", "{\"score\":" + (int.Parse(e.Text == "null" ? "0" : e.Text ) + Info.PointForGame) + "}").Catch(Debug.Log);
                }).Catch(Debug.Log);
            }
        }
    }
}
