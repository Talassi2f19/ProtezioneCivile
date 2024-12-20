using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Defective.JSON;
using Proyecto26;
using Script.User;
using Script.User.Prefabs;
using Script.Utility;
using TMPro;
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
        [SerializeField] private GameObject vincitore;
        
        /*
        private JSONObject risultatiJson;
        private List<string> candidati = new List<string>();
        private List<JSONObject> voti = new List<JSONObject>();
        private List<GameObject> listaRisultati= new List<GameObject>();
        */

    
        // TODO: contare i voti secondo la proprietà Voto negli giocatori
        // INCOMPLETO
        private JSONObject playersJson;
        private List<string> playersName = new List<string>();
        private List<JSONObject> playersData = new List<JSONObject>();

        private void Start()
        {
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + ".json").Then(onReceived =>
            {

                playersJson = new JSONObject(onReceived.Text);
                playersName = playersJson.keys;
                playersData = playersJson.list;
                
                int[] votes = new int[playersName.Count];

                for (int i = 0; i < playersName.Count; i++) {
                    if (playersData[i].HasField("Voto"))
                        votes[playersName.IndexOf(playersData[i]["Voto"].stringValue)]++;
                    /*
                    if (playersJson.HasField(playersData[i]["Voto"].stringValue)) {
                        if (!playersJson[playersData[i]["Voto"].stringValue].HasField("NrVoti"))
                            playersJson[playersData[i]["Voto"].stringValue].AddField("NrVoti", 0);
                        playersJson[playersData[i]["Voto"].stringValue].SetField("NrVoti", playersJson[playersData[i]["Voto"].stringValue]["NrVoti"].intValue + 1);
                    }
                    */
                }
                
                // int posMaxVoti = MaxVotiCandidato();
                int posMaxVoti = votes.ToList().IndexOf(votes.Max());
            
                // for (int i = 0; i < candidati.Count; i++)
                // {
                //     Debug.Log(candidati[i] +  " - " + voti[i].intValue);
                //     //listaRisultati.Add(GameObject.Instantiate(votoCandidatoPrefab, contenitore));
                //     listaRisultati[i].GetComponent<VotiCandidato>().SetNomeCandidato(candidati[i]);
                //     listaRisultati[i].GetComponent<VotiCandidato>().SetNumeroVoti(voti[i].intValue);
                // }

                //TODO da aggiustare perché non visualizza il nome
                vincitore.GetComponent<TMP_Text>().text = playersName[posMaxVoti];
                //listaRisultati[posMaxVoti].GetComponent<VotiCandidato>().HighlightBestCandidate();
                //aggiorna il ruolo del player
                string str = "{\"" + Global.RuoloPlayerKey + "\":\"" + Ruoli.Sindaco + "\"}";
                RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + playersName[posMaxVoti] + ".json", str).Then(e => {
                    string changeStatusCode = "{\"" + Global.GameStatusCodeKey + "\":\"" + GameStatus.RisultatiElezioni + "\"}";
                    RestClient.Patch(Info.DBUrl + Info.sessionCode + ".json", changeStatusCode).Catch(Debug.LogError);
                }).Catch(Debug.LogError);
            }).Catch(Debug.LogError);

            
        }
        
        public void AvviaPartita()
        {
            string str = "{\"" + Global.GameStatusCodeKey + "\":\"" + GameStatus.GenRuoli + "\"}";
            RestClient.Patch(Info.DBUrl + Info.sessionCode + ".json", str).Catch(Debug.LogError);
            SceneManager.LoadScene(Scene.Master.GenRuoli);
        }
        
    }
}