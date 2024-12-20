using _Scenes.User.telefono;
using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;

namespace minigame.TogliTane
{
    public class TogliTane : MonoBehaviour
    {
        [SerializeField]private GameObject prefab;
        private TextMeshProUGUI text;
        [SerializeField]private GameObject ObjText;
        [SerializeField] private Argine argine;

        
        private Vector2[,] posizioni = {
            {new Vector2(6.670286f, -4.150032f), 
                new Vector2(6.311871f, 2.047081f), 
                new Vector2(9.204801f, 6.339566f), 
                new Vector2(7.923988f, 16.12234f), 
                new Vector2(9.85625f, 13.08592f)}, 

            {new Vector2(10.17645f, 16.034f), 
                new Vector2(5.212645f, -1.143522f), 
                new Vector2(9.193848f, 2.393102f), 
                new Vector2(6.77567f, 9.497435f), 
                new Vector2(8.398771f, 19.32437f)}

        };
        
        private void Start()
        {
            ObjText.SetActive(false);
            text = ObjText.GetComponent<TextMeshProUGUI>();
        }
        
        [ContextMenu("genera")]
        public void GeneraTane()
        {
            int val = Random.Range(0, posizioni.GetLength(0));
            //RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":"+ (val+2000) +"}");
            for (int i = 0; i < posizioni.GetLength(1); i++)
            {
                GameObject tmp = Instantiate(prefab, posizioni[val,i], new Quaternion(), transform);
                tmp.GetComponent<Tana>().OnComplete( () => TanaRimossa(tmp) );
            }
            ObjText.SetActive(true);
            text.text = "Tane da chiudere: " + transform.childCount;
            argine.ArgineAccessibile(true);
        }
        
        private void TanaRimossa(GameObject tmp)
        {
            DestroyImmediate(tmp);
            text.text = "Tane da chiudere: " + transform.childCount;
            if (transform.childCount == 0)
            {
                ObjText.SetActive(false);
                //task done
                argine.ArgineAccessibile(false);
                GameObject.FindWithTag("notifiche")?.GetComponent<TaskManager>()?.NuovaNotifica("Hai terminato il task");
                RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":201,\"Player\":\""+Info.localUser.name+"\"}").Catch(Debug.Log);
                RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + ".json", "{\"Occupato\":false}");
                RestClient.Get(Info.DBUrl + Info.sessionCode + "/score.json").Then(e =>
                {
                    RestClient.Patch(Info.DBUrl + Info.sessionCode + ".json", "{\"score\":" + (int.Parse(e.Text == "null" ? "0" : e.Text ) + Info.PointForGame) + "}").Catch(Debug.Log);
                }).Catch(Debug.Log);
            }
        }
    }
}
