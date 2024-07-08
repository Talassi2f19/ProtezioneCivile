using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Script.User
{
    public class TrovaDispersi : MonoBehaviour
    {
        [SerializeField]private GameObject prefab;
        private Transform parent;
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
                new Vector2(-11.5f, -4.25f)}
        };

        private void Start()
        {
            ObjText.SetActive(false);
            text = ObjText.GetComponent<TextMeshProUGUI>();
            parent = gameObject.transform;
        }
        
        [ContextMenu("genera")]
        public void GeneraDaTrovare()
        {
            int val = Random.Range(0, posizioni.GetLength(0));
            // RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":"+ (val+2000) +"}");
            for (int i = 0; i < posizioni.GetLength(1); i++)
            {
                GameObject tmp = Instantiate(prefab, posizioni[val,i], new Quaternion(), parent);
                tmp.GetComponent<Button>().onClick.AddListener(()=> Click(tmp));
            }
            ObjText.SetActive(true);
            text.text = "Da trovare: " + parent.childCount + " dispersi";
        }

        // public void GeneraOverlay(int val)
        // {
        //     val -= 2000;
        //     for (int i = 0; i < posizioni.GetLength(1); i++)
        //     {
        //         GameObject tmp = Instantiate(prefab, posizioni[val,i], new Quaternion(), parent);
        //         tmp.GetComponent<Button>().onClick.AddListener(()=> Click(tmp));
        //     }
        // }
        //
        // public void RimuoviOverlay()
        // {
        //     for (int i = parent.childCount - 1; i >= 0; i--)
        //     {
        //         DestroyImmediate(parent.GetChild(i).gameObject);
        //     }
        // }
    
        public void Click(GameObject tmp)
        {
            DestroyImmediate(tmp);
            text.text = "Da trovare: " + parent.childCount + " dispersi";
            if (parent.childCount == 0)
            {
                ObjText.SetActive(false);
                //task done
                
                RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + ".json", "{\"Occupato\":false}");
            }
        }
    }
}
