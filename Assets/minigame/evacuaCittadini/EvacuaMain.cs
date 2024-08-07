using System;
using System.Collections.Generic;
using _Scenes.User.telefono;
using Proyecto26;
using Script.User;
using Script.Utility;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace minigame.evacuaCittadini
{

    [Serializable]
    class SpriteEder
    {
        [SerializeField]public List<Sprite> sprite;
    }
    public class EvacuaMain : MonoBehaviour
    {
        [SerializeField] private List<Vector3> posizioni1;
        [SerializeField] private List<Vector3> posizioni2;
        [SerializeField] private List<int> tipoPorta;
        [SerializeField] private List<SpriteEder> sprite;
        [SerializeField] private GameObject stage1;
        [SerializeField] private GameObject stage2;
        [SerializeField] private PlayerLocal playerLocal;
        [SerializeField] private Canvas mainCanvas;
        private TextMeshProUGUI text;
        [SerializeField] private GameObject ObjText;
        private GameObject p1, p2;
        private int tipoDialogo; //0-positivo, 1-negativo, 3-sindaco
        
        private List<int> posCase = new List<int>();
        [SerializeField] private int numCase = 8;
        
        private void Start()
        {
            ObjText.SetActive(false);
            text = ObjText.GetComponent<TextMeshProUGUI>();
        }
        
        private void GeneraPosizioni()
        {
            List<int> numbers = new List<int>();
            for (int i = 0; i <= posizioni1.Count; i++)
                numbers.Add(i);
            
            for (int i = 0; i < numCase; i++)
            {
                int index =  Random.Range(0,numbers.Count);
                posCase.Add(numbers[index]);
                numbers.RemoveAt(index);
            }
        }
        
        
        [ContextMenu("Genera")]
        public void Genera()
        {
            GeneraPosizioni();
            for(int i = 0; i < posCase.Count; i++)
            {
                GameObject tmp = Instantiate(stage1, posizioni1[posCase[i]], new Quaternion(), transform);
                var i1 = posCase[i];
                tmp.GetComponent<Evacua>().FineStage1(()=>IniziaPt2(i1, tmp, false));
            }
            ObjText.SetActive(true);
            text.text = "Case da visitare: " + numCase;
        }
        public void Genera(string val)
        {
            int num = int.Parse(val);
            GameObject tmp = Instantiate(stage1, posizioni1[int.Parse(val)], new Quaternion(), transform);
            tmp.GetComponent<Evacua>().FineStage1(()=>IniziaPt2(num, tmp, true));
        }

        private void IniziaPt2(int i, GameObject tmp, bool flag)
        {
            if (flag)
               tipoDialogo = 2;// dialogo Sindaco
            else{
               // genera il tipo di dialogo: 70% positivo - 30% negativo
               tipoDialogo = Random.Range(0,100) < 70 ? 0 : 1;
            }
            p1 = tmp;
            p2 = Instantiate(stage2, posizioni2[i], new Quaternion(), transform);
            p2.GetComponent<stage2>().Set(sprite[tipoPorta[i]].sprite,tipoDialogo);
            p2.GetComponent<stage2>().Stage2Fine(()=>FinePt2(i, flag));
            mainCanvas.enabled = false;
            playerLocal.canMove = false;
            playerLocal.gameObject.transform.position = posizioni2[i] - new Vector3(0f,0.3f,0f);
        }
        
        private void FinePt2(int i, bool flag)
        {
            Destroy(p1);
            Destroy(p2);
            
            mainCanvas.enabled = true;
            playerLocal.canMove = true;
            if (!flag) //se non è il sindaco
            {
                text.text = "Case da visitare: " + transform.childCount;
                if(tipoDialogo == 1)
                    RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":95,\"Player\":\""+i+"\"}").Catch(Debug.LogError);
                            
                if (transform.childCount == 0)
                {
                    ObjText.SetActive(false);
                    GameObject.FindWithTag("notifiche")?.GetComponent<TaskManager>()?.NuovaNotifica("Hai terminato la task");
                    RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":200,\"Player\":\""+Info.localUser.name+"\"}").Catch(Debug.Log);
                    RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + ".json", "{\"Occupato\":false}");
                    RestClient.Get(Info.DBUrl + Info.sessionCode + "/score.json").Then(e =>
                    {
                        RestClient.Patch(Info.DBUrl + Info.sessionCode + ".json", "{\"score\":" + (int.Parse(e.Text == "null" ? "0" : e.Text ) + Info.PointForGame) + "}").Catch(Debug.Log);
                    }).Catch(Debug.Log);
                }
            }
            
        }
    }
}
