using System;
using System.Collections.Generic;
using Proyecto26;
using Script.User;
using Script.Utility;
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
        private GameObject p1, p2;
        private int tipoDialogo; //0-positivo, 1-negativo, 3-sindaco
        
        
        [ContextMenu("Genera")]
        public void Genera()
        {
            for(int i = 0; i < posizioni1.Count; i++)
            {
                GameObject tmp = Instantiate(stage1, posizioni1[i], new Quaternion(), transform);
                var i1 = i;
                tmp.GetComponent<Evacua>().FineStage1(()=>IniziaPt2(i1, tmp, false));
            }
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
                if(tipoDialogo == 1)
                    RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":95,\"Player\":\""+i+"\"}").Catch(Debug.Log);
                            
                if (transform.childCount == 0)
                {
                    RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + ".json", "{\"Occupato\":false}");
                }
            }
            
        }
    }
}
