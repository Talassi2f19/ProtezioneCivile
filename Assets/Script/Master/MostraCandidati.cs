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
            didSomeoneApplied = new Listeners(Info.DBUrl + Info.SessionCode + "/candidati.json");
            didSomeoneApplied.Start(AddCandidato);
        
            terminaVotazioni.SetActive(false);
            avviaVotazioni.SetActive(true);
        }
    
        private void AddCandidato(string str)
        {
            if (str.Contains("event: patch")) //Solo i messaggi di inserimento candidato vengono considerati
            {
                string nome = "";
            
                string dataJSON = str.Split("\"data\":")[1].Split("}")[0] + "}"; //Oggetto contenente il candidato
            
                Debug.Log("AddCandidato: stringAfterSplit =" + dataJSON);
            
                nome = new JSONObject(dataJSON).keys[0]; //Nome candidato
            
                //Aggiunta prefab di testo alla lista
                candidati.Add(GameObject.Instantiate(genericTextPrefab, contenitore));
                //Inserisco il nome nel prefab del neocandidato
                candidati[candidati.Count - 1].GetComponent<GenericTextPrefab>().SetGenericText(nome);
            }
        }

        public void AvviaVotazioni()
        {
            List<string> players = new List<string>();
            string nomeUnicoCandidato;
            int posPlayerElettoForzatamente = 0;

            didSomeoneApplied.Stop();
        
            if (candidati.Count > 1)
            {
                string str = "{\"gameStatusCode\":\"" + Info.GameStatus.Votazione + "\"}";
                RestClient.Patch(Info.DBUrl + Info.SessionCode + ".json", str);
            
                terminaVotazioni.SetActive(true);
                avviaVotazioni.SetActive(false);
            }
            else
            {
                RestClient.Get(Info.DBUrl + Info.SessionCode + "/players.json").Then(getPlayers =>
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
                    RestClient.Patch(Info.DBUrl + Info.SessionCode + "/candidati.json", futuroSindacoEletto).Then(
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
            RestClient.Patch(Info.DBUrl + Info.SessionCode + ".json", changeStatusCode);
            SceneManager.LoadScene("_Scenes/master/risultatiElezioni");
        }
    }
}