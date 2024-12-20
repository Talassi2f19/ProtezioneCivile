using _Scenes.User.telefono;
using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace minigame.TrovaDispersi
{
    public class TrovaDispersi : MonoBehaviour
    {
        [SerializeField]private GameObject prefab;
        private TextMeshProUGUI text;
        [SerializeField] private GameObject ObjText;
        
        private Vector2[,] posizioni = {
            {new Vector2(-2f, -3f), 
                new Vector2(-9f, 18f), 
                new Vector2(1f, -0.75f), 
                new Vector2(10.5f, 19f), 
                new Vector2(-1.75f, 0.5f)},

            {new Vector2(-13f, 3f), 
                new Vector2(1.75f, 0.25f), 
                new Vector2(7.25f, 13.25f), 
                new Vector2(5.75f, 0f), 
                new Vector2(19f, 19f)},

            {new Vector2(-13.5f, 11.5f), 
                new Vector2(20f, 7.5f), 
                new Vector2(-7.75f, 7.5f), 
                new Vector2(6.75f, -4.25f), 
                new Vector2(-5f, 3f)}
        };

        private void Start()
        {
            ObjText.SetActive(false);
            text = ObjText.GetComponent<TextMeshProUGUI>();
        }
        
        [ContextMenu("genera")]
        public void GeneraDaTrovare()
        {
            int val = Random.Range(0, posizioni.GetLength(0));
            // RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":"+ (val+2000) +"}");
            for (int i = 0; i < posizioni.GetLength(1); i++)
            {
                GameObject tmp = Instantiate(prefab, posizioni[val,i], new Quaternion(), transform);
                tmp.GetComponent<Button>().onClick.AddListener(()=> Click(tmp));
            }
            ObjText.SetActive(true);
            text.text = "Da trovare: " + transform.childCount + " dispersi";
        }
    
        public void Click(GameObject tmp)
        {
            DestroyImmediate(tmp);
            text.text = "Da trovare: " + transform.childCount + " dispersi";
            if (transform.childCount == 0)
            {
                ObjText.SetActive(false);
                //task done
                GameObject.FindWithTag("notifiche")?.GetComponent<TaskManager>()?.NuovaNotifica("Hai terminato il task");
                RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":204,\"Player\":\""+Info.localUser.name+"\"}").Catch(Debug.Log);
                RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + ".json", "{\"Occupato\":false}");
                RestClient.Get(Info.DBUrl + Info.sessionCode + "/score.json").Then(e =>
                {
                    RestClient.Patch(Info.DBUrl + Info.sessionCode + ".json", "{\"score\":" + (int.Parse(e.Text == "null" ? "0" : e.Text ) + Info.PointForGame) + "}").Catch(Debug.Log);
                }).Catch(Debug.Log);
            }
        }
    }
}
