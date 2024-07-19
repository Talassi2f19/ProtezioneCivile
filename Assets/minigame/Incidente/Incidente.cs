using System;
using Proyecto26;
using Script.Master;
using Script.User;
using Script.Utility;
using UnityEngine;

namespace minigame.Incidente
{
    public class Incidente : MonoBehaviour
    {
        [SerializeField]private GameObject stage1, stage2;
        private GameObject p1, p2;
        [SerializeField]private Canvas mainCanva;
        [SerializeField]private PlayerLocal playerLocal;
        private Vector2 posizione = new Vector2(-0.9f,15.5f);
        private bool inProgress = false;

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
                Destroy(p1);
                p1 = Instantiate(stage1, posizione, new Quaternion(), transform);
                p1.GetComponent<Pt1>().type = true;
                p1.GetComponent<Pt1>().Pt1Completato(Pt1Fine);
                inProgress = true;
            }
            else
            {
                if (p1 == null && !inProgress)
                {
                    p1 = Instantiate(stage1, posizione, new Quaternion(), transform);
                    p1.GetComponent<Pt1>().type = false;
                }
            }
        }

        public void Rimuovi()
        {
            Destroy(p1);
            Destroy(p2);
        }

        private void Pt1Fine()
        {
            mainCanva.enabled = false;
            playerLocal.canMove = false;
            p2 = Instantiate(stage2, playerLocal.transform.position ,new Quaternion() , transform);
            p2.GetComponent<Pt2>().OnComplete(Pt2Fine);
            Destroy(p1);
        }
        
        private void Pt2Fine()
        {
            inProgress = false;
            mainCanva.enabled = true;
            playerLocal.canMove = true;
            Destroy(p1);
            Destroy(p2);
			RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + ".json", "{\"Occupato\":false}");
            RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":47001}").Catch(Debug.LogError);
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/score.json").Then(e =>
            {
                RestClient.Patch(Info.DBUrl + Info.sessionCode + ".json", "{\"score\":" + (int.Parse(e.Text == "null" ? "0" : e.Text ) + Info.PointForGame) + "}").Catch(Debug.Log);
            }).Catch(Debug.Log);
        }

    }
}
