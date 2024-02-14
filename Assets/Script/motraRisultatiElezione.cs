using System.Collections;
using System.Collections.Generic;
using Defective.JSON;
using Proyecto26;
using Script;
using Script.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MostraRisultatiElezione : MonoBehaviour
{
    private JSONObject risultatiJSON;
    private List<string> candidati = new List<string>();
    private List<JSONObject> voti = new List<JSONObject>();
    private List<GameObject> listaRisultati= new List<GameObject>();
    [SerializeField] private GameObject votoCandidatoPrefab;
    [SerializeField] private Transform contenitore;
    
    void Start()
    {
        RestClient.Get(Info.DBUrl + Info.SessionCode + "/candidati.json").Then(onReceived =>
        {
            risultatiJSON = new JSONObject(onReceived.Text);
            candidati = risultatiJSON.keys;
            voti = risultatiJSON.list;

            int posMaxVoti = maxVotiCandidato();
            
            for (int i = 0; i < candidati.Count; i++)
            {
                Debug.Log(candidati[i] +  " - " + voti[i].intValue);
                listaRisultati.Add(GameObject.Instantiate(votoCandidatoPrefab, contenitore));
                listaRisultati[i].GetComponent<votiCandidato>().setNomeCandidato(candidati[i]);
                listaRisultati[i].GetComponent<votiCandidato>().setNumeroVoti(voti[i].intValue);
            }
            
            listaRisultati[posMaxVoti].GetComponent<votiCandidato>().highlightBestCandidate();
        });
    }

    private int maxVotiCandidato()
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
        string str = "{\"gameStatusCode\":\"" + Info.GameStatus.Gioco + "\"}";
        RestClient.Patch(Info.DBUrl + Info.SessionCode + ".json", str);
        SceneManager.LoadScene("_Scenes/master/game");
    }
}