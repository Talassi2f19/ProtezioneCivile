using System;
using System.Collections.Generic;
using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;

namespace minigame.MaterialePericoloso
{
    [Serializable]
    class TipoMonnezza
    {
        [SerializeField]public GameObject prefab;
        [SerializeField]public List<Vector3> posizione;
    }
    public class TogliMonnezza : MonoBehaviour
    {
        
        [SerializeField]private List<TipoMonnezza> monnezza;
        private TextMeshProUGUI text;
        [SerializeField]private GameObject ObjText;

        
        private bool inProgress = false;
        
        private void Start()
        {
            if (ObjText != null)
            {
                ObjText.SetActive(false);
                text = ObjText.GetComponent<TextMeshProUGUI>();
            }
        }
        
        [ContextMenu("GeneraTrue")]
        public void GeneraTrue()
        {
            Genera(true);
        }
        [ContextMenu("GeneraFalse")]
        public void GeneraFalse()
        {
            Genera(false);
        }
        public void Genera(bool type)
        {
            if (type)
            {
                
            }
            
            if (type)
            {
                ClearChild();
                Instanzia(true);
                inProgress = true;
                ObjText.SetActive(true);
                int num = 0;
                foreach (var t in monnezza)
                {
                    num += t.posizione.Count;
                }
                text.text = "Rifiuti da rimuovere: " + num;
            }
            else
            {
                if (transform.childCount == 0 && !inProgress)
                {
                    Instanzia(false);
                }
            }
        }

        [ContextMenu("Rimuovi")]
        public void Rimuovi()
        {
            if (!inProgress)
            {
                ClearChild();
            }
        }
        
        private void ClearChild()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void Instanzia(bool type)
        {
            foreach (var tipo in monnezza)
            {
                foreach (var t in tipo.posizione)
                {
                    GameObject tmp = Instantiate(tipo.prefab, t, new Quaternion(), transform);
                    tmp.GetComponent<Monnezza>().SetType(type);
                    tmp.GetComponent<Monnezza>().OnComplete(()=>Distruggi(tmp));
                }
            }
        }
        
        private void Distruggi(GameObject tmp)
        {
            DestroyImmediate(tmp);
            text.text = "Rifiuti da rimuovere: " + transform.childCount;
            if (transform.childCount == 0)
            {
                ObjText.SetActive(false);
                //task done
                RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":27001}").Catch(Debug.LogError);
                RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + ".json", "{\"Occupato\":false}");
                RestClient.Get(Info.DBUrl + Info.sessionCode + "/score.json").Then(e =>
                {
                    RestClient.Patch(Info.DBUrl + Info.sessionCode + ".json", "{\"score\":" + (int.Parse(e.Text == "null" ? "0" : e.Text ) + Info.PointForGame) + "}").Catch(Debug.Log);
                }).Catch(Debug.Log);
            }
        }
    }
}
