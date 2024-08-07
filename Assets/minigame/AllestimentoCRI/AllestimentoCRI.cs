using _Scenes.User.telefono;
using Proyecto26;
using Script.User;
using Script.Utility;
using UnityEngine;

namespace minigame.AllestimentoCRI
{
    public class AllestimentoCRI : MonoBehaviour
    {
        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private PlayerLocal playerLocal;
        [SerializeField] private GameObject prefab1, prefab2, prefab3; 
        [SerializeField] private Transform mappa;
        private GameObject pt1, pt2, pt3 = null;

        private bool inProgress = false;
        

        private Vector2 posizione = new Vector2(-8f,11.5f);
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
                pt1 = Instantiate(prefab1, posizione, new Quaternion(), transform);
                pt1.GetComponent<P1>().Pt1Complete(Pt2Start);
                Destroy(pt3);
                inProgress = true;
            }
            else
            {
                if(pt3 == null && !inProgress)
                    pt3 = Instantiate(prefab3, mappa);
            }
            
        }

        private void Pt2Start()
        {
            mainCanvas.enabled = false;
            playerLocal.canMove = false;
            pt2 = Instantiate(prefab2, transform);
            pt2.GetComponent<P2>().Pt2Complete(Pt3Start);
        }
    
        private void Pt3Start()
        {
            pt3 = Instantiate(prefab3, mappa);
            mainCanvas.enabled = true;
            playerLocal.canMove = true;
            GameObject.FindWithTag("notifiche")?.GetComponent<TaskManager>()?.NuovaNotifica("Hai terminato la task");
            RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":30000}").Catch(Debug.Log);
            RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":202,\"Player\":\""+Info.localUser.name+"\"}").Catch(Debug.Log);
            RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + ".json", "{\"Occupato\":false}");
            Destroy(pt1);
            Destroy(pt2);
            inProgress = false;
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/score.json").Then(e =>
            {
                RestClient.Patch(Info.DBUrl + Info.sessionCode + ".json", "{\"score\":" + (int.Parse(e.Text == "null" ? "0" : e.Text ) + Info.PointForGame) + "}").Catch(Debug.Log);
            }).Catch(Debug.Log);
        }
    }
}