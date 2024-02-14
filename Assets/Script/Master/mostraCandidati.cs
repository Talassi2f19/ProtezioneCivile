using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Defective.JSON;
using JetBrains.Annotations;
using Script.Utility;
using UnityEngine;
public class mostraCandidati : MonoBehaviour
{
    private Listeners DidSomeoneApplied;
    private List<GameObject> candidati = new List<GameObject>();

    [SerializeField] private GameObject genericTextPrefab;
    [SerializeField] private Transform contenitore;
    
    void Start()
    {
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
    
}
