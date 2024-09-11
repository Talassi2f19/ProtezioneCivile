using System;
using System.Collections.Generic;
using _Scenes.User.telefono;
using Proyecto26;
using Script.User;
using Script.Utility;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace minigame.Battito
{
    public class Battito : MonoBehaviour
    {
        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private PlayerLocal playerLocal;
        [SerializeField] private GameObject stage1, stage2;
         private GameObject p1, p2;
        [SerializeField] private List<Vector2> posizione;

        [ContextMenu("Genera")]
        public void Genera()
        {
            p1 = Instantiate(stage1,   posizione[Random.Range(0,posizione.Count)],new quaternion(), transform);
            p1.GetComponent<Stage1>().Completato(P1Completato);
        }
        
        private void P1Completato()
        {
            mainCanvas.enabled = false;
            playerLocal.canMove = false;
            p2 = Instantiate(stage2, Info.localUser.coord,new quaternion(), transform);
            p2.GetComponent<Stage2>().Completato(P2Completato);
        }
        
        private void P2Completato()
        {
            Destroy(p1);
            Destroy(p2);
            mainCanvas.enabled = true;
            playerLocal.canMove = true;
            GameObject.FindWithTag("notifiche")?.GetComponent<TaskManager>()?.NuovaNotifica("Hai terminato il task");
            RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + ".json", "{\"Occupato\":false}").Catch(Debug.LogError);
            RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":202,\"Player\":\""+Info.localUser.name+"\"}").Catch(Debug.LogError);
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/score.json").Then(e =>
            {
                RestClient.Patch(Info.DBUrl + Info.sessionCode + ".json", "{\"score\":" + (int.Parse(e.Text == "null" ? "0" : e.Text ) + Info.PointForGame) + "}").Catch(Debug.LogError);
            }).Catch(Debug.LogError);
        }
    }
}
