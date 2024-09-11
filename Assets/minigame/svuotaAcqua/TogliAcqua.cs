using _Scenes.User.telefono;
using Proyecto26;
using Script.User;
using Script.Utility;
using UnityEngine;

namespace minigame.svuotaAcqua
{
    public class TogliAcqua : MonoBehaviour
    {
        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private PlayerLocal playerLocal;
        [SerializeField]private GameObject prefabP1;
        [SerializeField]private GameObject prefabP2;
        private GameObject pt1;
        private GameObject pt2;
        private bool inProgress = false;
        
        private Vector2 posizione = new Vector2(4f,2f);
        
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
                Destroy(pt1);
                pt1 = Instantiate(prefabP1, posizione, new Quaternion(), transform);
                pt1.GetComponent<Pozza>().SetType(true);
                pt1.GetComponent<Pozza>().StartPt2(P1Completato);
                inProgress = true;
            }
            else
            {
                if (pt1 == null && !inProgress)
                {
                    pt1 = Instantiate(prefabP1, posizione, new Quaternion(), transform);
                    pt1.GetComponent<Pozza>().SetType(false);
                }
            }
        }

        [ContextMenu("Rimuovi")]
        public void Rimuovi()
        {
            if (!inProgress)
            {
                Destroy(pt1);
            }
        }
        
        private void P1Completato()
        {
            mainCanvas.enabled = false;
            playerLocal.canMove = false;
            pt2 = Instantiate(prefabP2,Info.localUser.coord,new Quaternion(), transform);
            pt2.GetComponent<PompaGioco>().OnComplete(P2Completato);
        }
    
        private void P2Completato()
        {
            DestroyImmediate(pt1);
            DestroyImmediate(pt2);
            mainCanvas.enabled = true;
            playerLocal.canMove = true;
            inProgress = false;
            GameObject.FindWithTag("notifiche")?.GetComponent<TaskManager>()?.NuovaNotifica("Hai terminato il task");
            RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":200,\"Player\":\""+Info.localUser.name+"\"}").Catch(Debug.Log);
            RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + ".json", "{\"Occupato\":false}");
            RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":16001}").Catch(Debug.LogError);
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/score.json").Then(e =>
            {
                RestClient.Patch(Info.DBUrl + Info.sessionCode + ".json", "{\"score\":" + (int.Parse(e.Text == "null" ? "0" : e.Text ) + Info.PointForGame) + "}").Catch(Debug.Log);
            }).Catch(Debug.Log);
        }
    
    }
}
