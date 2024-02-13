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
        Debug.Log("AddCandidato: stringValue=" + str);

        //gestisco il messaggio di keep alive periodico che arriva
        //così quel messaggio non passa per quello che viene dopo l'if
        if (str.Contains("put"))
        {
            List<string> nomi = new List<string>();
            
            //Contine il valore del campo "data" che viene ricevuto con il Listener
            string dataJSON = str.Split("\"data\":")[1].Split("}")[0];
            
            //Trasformo il contenuto in un oggetto JSON in modo da ottenerne le chiavi
            JSONObject listaJSON = new JSONObject(dataJSON);

            //Lista dei player che si sono candidati
            nomi = listaJSON.keys;

            foreach (string nome in nomi)
            {
                //Aggiunta prefab di testo alla lista
                candidati.Add(GameObject.Instantiate(genericTextPrefab, contenitore));
                //Inserisco il nome nel prefab del neocandidato
                candidati[candidati.Count - 1].GetComponent<genericTextPrefab>().setGenericText(nome);
                //Posizionamento oggetti
                candidati[candidati.Count - 1].transform.position = new Vector3(100, 100 * (candidati.Count - 1), 0);
            }
        }
        
    }
    
}
