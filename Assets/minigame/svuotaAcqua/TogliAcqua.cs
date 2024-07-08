using minigame.svuotaAcqua;
using Proyecto26;
using Script.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.User
{
    public class TogliAcqua : MonoBehaviour
    {
        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private PlayerLocal playerLocal;
        [SerializeField]private GameObject prefabP1;
        [SerializeField]private GameObject prefabP2;
        private Transform parent;
        private GameObject pt1;
        private GameObject pt2;
        
    
        private Vector2[] posizioni = { 
            new Vector2(12f,12f), 
            new Vector2(5f,9.7f), 
            new Vector2(9.4f,-2.7f)
        };

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
            pt1 = Instantiate(prefabP1, posizioni[Random.Range(0, posizioni.Length)], new Quaternion(), parent);
            pt1.GetComponent<Pozza>().SetType(type);
            pt1.GetComponent<Pozza>().StartPt2(P1Completato);
        }
        
        private void P1Completato()
        {
            mainCanvas.enabled = false;
            playerLocal.PlayerCanMove(false);
            pt2 = Instantiate(prefabP2,Info.localUser.coord,new Quaternion(), parent);
            pt2.GetComponent<PompaGioco>().OnComplete(P2Completato);
        }
    
        private void P2Completato()
        {
            DestroyImmediate(pt1);
            DestroyImmediate(pt2);
            mainCanvas.enabled = true;
            playerLocal.PlayerCanMove(true);
            Debug.Log("Completato");
            RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + ".json", "{\"Occupato\":false}");
            //TODO
        }
    
    }
}
