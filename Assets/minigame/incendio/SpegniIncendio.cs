using Proyecto26;
using Script.User;
using Script.Utility;
using UnityEngine;

namespace minigame.incendio
{
    public class SpegniIncendio : MonoBehaviour
    {
        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private PlayerLocal playerLocal;
        [SerializeField]private GameObject prefabP1;
        [SerializeField]private GameObject prefabP2;
        private Transform parent;
        private GameObject pt1;
        private GameObject pt2;
        private bool inProgress = false;
        
        //BUG non gestita l'apparizione e la sparizione della zona in fiamme per gli altri player(non usata)
        private Vector2 posizione = new Vector2(-4.6f,5.65f);
    
        private void Start()
        {
            parent = gameObject.transform;
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
                Destroy(pt1);
                pt1 = Instantiate(prefabP1, posizione, new Quaternion(), parent);
                pt1.GetComponent<IncendioPt1>().SetType(true);
                pt1.GetComponent<IncendioPt1>().StartPt2(P1Completato);
                inProgress = true;
            }
            else
            {
                if (pt1 == null && !inProgress)
                {
                    pt1 = Instantiate(prefabP1, posizione, new Quaternion(), parent);
                    pt1.GetComponent<IncendioPt1>().SetType(false);
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
            playerLocal.PlayerCanMove(false);
            pt2 = Instantiate(prefabP2,Info.localUser.coord,new Quaternion(), parent);
            pt2.GetComponent<IncendioPt2>().OnComplete(P2Completato);
        }
        
        private void P2Completato()
        {
            DestroyImmediate(pt1);
            DestroyImmediate(pt2);
            mainCanvas.enabled = true;
            playerLocal.PlayerCanMove(true);
            inProgress = false;
            Debug.Log("Completato");
            RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + ".json", "{\"Occupato\":false}");
            RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":57001}").Catch(Debug.LogError);
            //TODO
        }
    }
}
