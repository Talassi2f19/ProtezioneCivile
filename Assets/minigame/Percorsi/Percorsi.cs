using _Scenes.User.telefono;
using Proyecto26;
using Script.User;
using Script.Utility;
using TMPro;
using UnityEngine;

namespace minigame.Percorsi
{
    public class Percorsi : MonoBehaviour
    {
        [SerializeField]private GameObject stage1, stage2;
        private GameObject p1;
        [SerializeField] private GameObject text; 
        private bool inProgress = false;

        [ContextMenu("GeneraTrue")]
        public void GeneraTrue()
        {
            Genera(1);
        }
        [ContextMenu("GeneraFalse")]
        public void GeneraFalse()
        {
            Genera(0);
        }
        [ContextMenu("GeneraCompleto")]
        public void GeneraCompleto()
        {
            Genera(2);
        }
        public void Genera(int type)
        {
            if (type == 1)
            {
                Destroy(p1);
                p1 = Instantiate(stage1, transform);
                p1.GetComponent<Acqua>().type = true;
                p1.GetComponent<Acqua>().text = text;
                p1.GetComponent<Acqua>().OnComplete(Fine);
                inProgress = true;
            }
            else if(type == 0)
            {
                if (!inProgress)
                {
                    Destroy(p1);
                    p1 = Instantiate(stage1, transform);
                    p1.GetComponent<Acqua>().text = text;
                    p1.GetComponent<Acqua>().type = false;
                }
            }
            else if(type == 2)
            {
                if (!inProgress)
                {
                    Destroy(p1);
                    p1 = Instantiate(stage2, transform);
                }
                    
            }
        }
        
        private void Fine()
        {
            text.SetActive(false);
            inProgress = false;
            Destroy(p1);
            p1 = Instantiate(stage2, transform);
            GameObject.FindWithTag("notifiche")?.GetComponent<TaskManager>()?.NuovaNotifica("Hai terminato il task");
            RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":203,\"Player\":\""+Info.localUser.name+"\"}").Catch(Debug.LogError);;
			RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + ".json", "{\"Occupato\":false}").Catch(Debug.LogError);;
            RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":46000}").Catch(Debug.LogError);
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/score.json").Then(e =>
            {
                RestClient.Patch(Info.DBUrl + Info.sessionCode + ".json", "{\"score\":" + (int.Parse(e.Text == "null" ? "0" : e.Text ) + Info.PointForGame) + "}").Catch(Debug.LogError);;
            }).Catch(Debug.LogError);;
        }
        
        
    }
}
