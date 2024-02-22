using System.Collections.Generic;
using Defective.JSON;
using Proyecto26;
using Script.User;
using Script.User.Prefabs;
using Script.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = Script.Utility.Scene;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace Script.Master
{
    public class MostraRisultatiElezione : MonoBehaviour
    {
        [SerializeField] private GameObject votoCandidatoPrefab;
        [SerializeField] private Transform contenitore;
    
        private JSONObject risultatiJson;
        private List<string> candidati = new List<string>();
        private List<JSONObject> voti = new List<JSONObject>();
        private List<GameObject> listaRisultati= new List<GameObject>();

        private void Start()
        {
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.CandidatiFolder + ".json").Then(onReceived =>
            {
                risultatiJson = new JSONObject(onReceived.Text);
                candidati = risultatiJson.keys;
                voti = risultatiJson.list;

                int posMaxVoti = MaxVotiCandidato();
            
                for (int i = 0; i < candidati.Count; i++)
                {
                    Debug.Log(candidati[i] +  " - " + voti[i].intValue);
                    listaRisultati.Add(GameObject.Instantiate(votoCandidatoPrefab, contenitore));
                    listaRisultati[i].GetComponent<VotiCandidato>().SetNomeCandidato(candidati[i]);
                    listaRisultati[i].GetComponent<VotiCandidato>().SetNumeroVoti(voti[i].intValue);
                }
            
                listaRisultati[posMaxVoti].GetComponent<VotiCandidato>().HighlightBestCandidate();
                //aggiorna il ruolo del player
                string str = "{\"" + Global.RuoloPlayerKey + "\":\"" + Ruoli.Sindaco + "\"}";
                RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + candidati[posMaxVoti] + ".json", str);
            });
        }

        private int MaxVotiCandidato()
        {
            int pos = 0;
            for (int i = 1; i < candidati.Count; i++)
            {
                if (voti[pos].intValue < voti[i].intValue)
                    pos = i;
            }
            return pos;
        }

        public void AvviaPartita()
        {
            string str = "{\"" + Global.GameStatusCodeKey + "\":\"" + GameStatus.Gioco + "\"}";
            RestClient.Patch(Info.DBUrl + Info.sessionCode + ".json", str);
            SceneManager.LoadScene(Scene.Master.Game);
        }
    }
}