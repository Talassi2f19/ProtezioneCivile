using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Defective.JSON;
using JetBrains.Annotations;
using Script.Utility;
using UnityEngine;
using Proyecto26;
using TMPro;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class mostraCandidati : MonoBehaviour
{
    private Listeners DidSomeoneApplied;
    private List<GameObject> candidati = new List<GameObject>();

    [SerializeField] private GameObject genericTextPrefab;
    [SerializeField] private Transform contenitore;
    
    [SerializeField] private GameObject terminaVotazioni;
    [SerializeField] private GameObject avviaVotazioni;

    private Random generatore = new Random();
    
    void Start()
    {
        terminaVotazioni.SetActive(false);
        avviaVotazioni.SetActive(true);
        DidSomeoneApplied = new Listeners(Info.DBUrl + Info.SessionCode + "/candidati.json");
        DidSomeoneApplied.Start(AddCandidato);
    }
    
    private void AddCandidato(string str)
    {
        //gestisco il messaggio di keep alive periodico che arriva
        //così quel messaggio non passa per quello che viene dopo l'if
        if (str.Contains("event: patch"))
        {
            string nome = "";
            
            //Contine l'oggetto contenente il nome del candidato
            string dataJSON = str.Split("\"data\":")[1].Split("}")[0] + "}";
            
            Debug.Log("AddCandidato: stringAfterSplit =" + dataJSON);
            
            //Trasformo il contenuto in un oggetto JSON in modo da ottenerne le chiavi
            JSONObject listaJSON = new JSONObject(dataJSON);
            
            //Nome del player che si è candidato
            nome = listaJSON.keys[0];
            
            //Aggiunta prefab di testo alla lista
            candidati.Add(GameObject.Instantiate(genericTextPrefab, contenitore));
            //Inserisco il nome nel prefab del neocandidato
            candidati[candidati.Count - 1].GetComponent<genericTextPrefab>().setGenericText(nome);
        }
    }

    public void AvviaVotazioni()
    {
        List<string> players = new List<string>();
        string nomeUnicoCandidato;
        int posPlayerElettoForzatamente = 0;

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
                    posPlayerElettoForzatamente = generatore.Next(0, players.Count);
                    nomeUnicoCandidato = players[posPlayerElettoForzatamente];
                }
                else
                    nomeUnicoCandidato = candidati[0].GetComponentInChildren<TMP_Text>().text;
                
                string futuroSindacoEletto = "{\"" + nomeUnicoCandidato + "\":" + players.Count + "}";
                RestClient.Patch(Info.DBUrl + Info.SessionCode + "/candidati.json", futuroSindacoEletto).Then(e =>
                {
                    MostraRisultati();
                });
            });
        }
        
        DidSomeoneApplied.Stop();
    }
    
    public void MostraRisultati()
    {
        string changeStatusCode = "{\"gameStatusCode\":\"" + Info.GameStatus.RisultatiElezioni + "\"}";
        RestClient.Patch(Info.DBUrl + Info.SessionCode + ".json", changeStatusCode);
        SceneManager.LoadScene("_Scenes/master/risultatiElezioni");
    }
    
}
