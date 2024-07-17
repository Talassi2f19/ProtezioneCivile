using Proyecto26;
using Script.User;
using Script.Utility;
using Unity.Mathematics;
using UnityEngine;

namespace minigame.Battito
{
    public class Battito : MonoBehaviour
    {
        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private PlayerLocal playerLocal;
        [SerializeField]private GameObject stage1, stage2;
        [SerializeField]private GameObject p1, p2;
        [SerializeField] private Vector2 posizione;

        public void Genera()
        {
            p1 = Instantiate(stage1, posizione,new quaternion(), transform);
            p1.GetComponent<Stage1>().Completato(P1Completato);
        }


        private void P1Completato()
        {
            mainCanvas.enabled = false;
            playerLocal.canMove = false;
            p2 = Instantiate(stage1, Info.localUser.coord,new quaternion(), transform);
            p2.GetComponent<Stage2>().Completato(P2Completato);
        }
        
        private void P2Completato()
        {
            Destroy(p1);
            Destroy(p2);
            mainCanvas.enabled = true;
            playerLocal.canMove = true;
            RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + ".json", "{\"Occupato\":false}"); 
            // RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":16001}").Catch(Debug.LogError);
        }
    }
}
