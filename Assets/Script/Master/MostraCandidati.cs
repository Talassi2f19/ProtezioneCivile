using System.Collections.Generic;
using Defective.JSON;
using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;
// ReSharper disable IdentifierTypo

namespace Script.Master
{
    public class MostraCandidati : MonoBehaviour
    {
        [SerializeField] private GameObject genericTextPrefab;
        [SerializeField] private Transform contenitore;
    
        [SerializeField] private GameObject terminaVotazioni;
        [SerializeField] private GameObject avviaVotazioni;
    
        private Listeners didSomeoneApplied;
        private List<GameObject> candidati = new List<GameObject>();
    
        void Start()
        {
            didSomeoneApplied = new Listeners(Info.DBUrl + Info.sessionCode + "/candidati.json");
            didSomeoneApplied.Start(AddCandidato);
        
            terminaVotazioni.SetActive(false);
            avviaVotazioni.SetActive(true);
        }
    
        private void AddCandidato(string str)
        {
            if (str.Contains("event: patch")) //Solo i messaggi di inserimento candidato vengono considerati
            {
                string nome;
            
                string dataJson = str.Split("\"data\":")[1].Split("}")[0] + "}"; //Oggetto contenente il candidato
            
                Debug.Log("AddCandidato: stringAfterSplit =" + dataJson);
            
                nome = new JSONObject(dataJson).keys[0]; //Nome candidato
            
                //Aggiunta prefab di testo alla lista
                candidati.Add(GameObject.Instantiate(genericTextPrefab, contenitore));
                //Inserisco il nome nel prefab del neocandidato
                candidati[candidati.Count - 1].GetComponent<GenericTextPrefab>().SetGenericText(nome);
            }
        }

        public void AvviaVotazioni()
        {
            List<string> players;
            string nomeUnicoCandidato;
            int posPlayerElettoForzatamente;
            
            
            didSomeoneApplied.Stop();
        
            if (candidati.Count > 1)
            {
                string str = "{\"gameStatusCode\":\"" + Info.GameStatus.Votazione + "\"}";
                RestClient.Patch(Info.DBUrl + Info.sessionCode + ".json", str);
            
                terminaVotazioni.SetActive(true);
                avviaVotazioni.SetActive(false);
            }
            else
            {
                RestClient.Get(Info.DBUrl + Info.sessionCode + "/players.json").Then(getPlayers =>
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
                    RestClient.Patch(Info.DBUrl + Info.sessionCode + "/candidati.json", futuroSindacoEletto).Then(
                        risultati =>
                        {
                            MostraRisultati();
                        });
                });
            }
        
        
        }
    
        public void MostraRisultati()
        {
            string changeStatusCode = "{\"gameStatusCode\":\"" + Info.GameStatus.RisultatiElezioni + "\"}";
            RestClient.Patch(Info.DBUrl + Info.sessionCode + ".json", changeStatusCode);
            SceneManager.LoadScene("_Scenes/Master/risultatiElezioni");
        }
    }
}