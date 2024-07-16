using System;
using System.Collections;
using System.Collections.Generic;
using Defective.JSON;
using Proyecto26;
using Script.Master.Prefabs;
using Script.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;
using Scene = Script.Utility.Scene;


// ReSharper disable IdentifierTypo

namespace Script.Master
{
    public class MostraCandidati : MonoBehaviour
    {
        [SerializeField] private GameObject genericTextPrefab;
        [SerializeField] private Transform contenitore;
        
        [SerializeField] private GameObject votazioni;
        [SerializeField] private GameObject candidatura;
        [SerializeField] private TextMeshProUGUI testoContatore;
    
        private Listeners didSomeoneApplied;
        private List<GameObject> candidati = new List<GameObject>();
        private int numeroPlayer = 30;
    
        void Start()
        {
            didSomeoneApplied = new Listeners(Info.DBUrl + Info.sessionCode + "/" + Global.CandidatiFolder + ".json");
            didSomeoneApplied.Start(AddCandidato);
        
            votazioni.SetActive(false);
            candidatura.SetActive(true);
            
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + ".json").Then(e =>
            {
                numeroPlayer = new JSONObject(e.Text, 0, -1, 1).keys.Count;
            }).Catch(Debug.LogError);
        }
    
        private void AddCandidato(string str)
        {
            if (str.Contains("event: patch")) //Solo i messaggi di inserimento candidato vengono considerati
            {
                string nome;
            
                string dataJson = str.Split("\"data\":")[1].Split("}")[0] + "}"; //Oggetto contenente il candidato
            
                Debug.Log("AddCandidato: stringAfterSplit =" + dataJson);
            
                nome = new JSONObject(dataJson).keys[0]; //Nome candidato
                //TODO inserire script per visualizzare quanti candidati mancano nella schermata con scritta n / 30
                
                //Aggiunta prefab di testo alla lista
                candidati.Add(GameObject.Instantiate(genericTextPrefab, contenitore));
                //Inserisco il nome nel prefab del neocandidato
                candidati[candidati.Count - 1].GetComponent<GenericTextPrefab>().SetGenericText(nome);
            }
        }

        private void OnApplicationQuit()
        {
            didSomeoneApplied.Stop();
        }

        public void AvviaVotazioni()
        {
            List<string> players;
            string nomeUnicoCandidato;
            int posPlayerElettoForzatamente;
            
            
            didSomeoneApplied.Stop();
        
            if (candidati.Count > 1)
            {
                string str = "{\"" + Global.GameStatusCodeKey + "\":\"" + GameStatus.Votazione + "\"}";
                RestClient.Patch(Info.DBUrl + Info.sessionCode + ".json", str).Catch(Debug.LogError);
            
                votazioni.SetActive(true);
                candidatura.SetActive(false);
                AvviaAggiornamento();
            }
            else
            {
                RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + ".json").Then(getPlayers =>
                {
                    players = new JSONObject(getPlayers.Text).keys;

                    if (candidati.Count == 0)
                    {
                        posPlayerElettoForzatamente = new Random().Next(0, players.Count);
                        nomeUnicoCandidato = players[posPlayerElettoForzatamente];
                    }
                    else
                        nomeUnicoCandidato = candidati[0].GetComponentInChildren<TMP_Text>().text;
                
                    string futuroSindacoEletto = "{\"" + nomeUnicoCandidato + "\":" + players.Count + "}";
                    RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.CandidatiFolder + ".json", futuroSindacoEletto).Then(
                        risultati =>
                        {
                            MostraRisultati();
                        }).Catch(Debug.LogError);
                }).Catch(Debug.LogError);
            }
        }
    
        public void MostraRisultati()
        {
            InterrompiAggiornamento();
            SceneManager.LoadScene(Scene.Master.RisultatiElezioni);
        }
        
        private void AvviaAggiornamento()
        {
            Debug.Log("avvia aggiornamento");
            StartCoroutine(CallFunctionEverySecond());
        }
        
        private void InterrompiAggiornamento()
        {
            Debug.Log("interrompi aggiornamento");
            isRunning = false;
        }

        private bool isRunning = true;
        private IEnumerator CallFunctionEverySecond()
        {
            while (isRunning)
            {
                AggiornaContatoreVotanti();
                // Aspetta un secondo prima di ripetere
                yield return new WaitForSeconds(1);
            }
        }

        private void AggiornaContatoreVotanti()
        {
            Debug.Log("exe");
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + ".json").Then(e =>
            {
                int nVoti = 0;
                foreach (JSONObject p in new JSONObject(e.Text).list)
                {
                    if (p.HasField("Voto"))
                        nVoti++;
                }
                testoContatore.text = nVoti + " / " + numeroPlayer;
                if (nVoti == numeroPlayer)
                {
                    StartCoroutine(StanzaPiena());
                }
            }).Catch(Debug.LogError);
        }

        private IEnumerator StanzaPiena()
        {
            yield return new WaitForSeconds(3f);
            MostraRisultati();
            yield return null;
        }
    }
}