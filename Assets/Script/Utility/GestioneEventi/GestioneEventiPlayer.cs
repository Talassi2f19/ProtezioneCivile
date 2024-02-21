using System.Collections.Generic;
using Defective.JSON;
using UnityEngine;

namespace Script.Utility.GestioneEventi
{
    public class GestioneEventiPlayer : MonoBehaviour
    {
        private Listeners ascoltoEventi = new Listeners(Info.DBUrl + "AAAA" + "/Missioni.json");
        private Dictionary<string, Missione> missioni;
    
        void Start()
        {
            ascoltoEventi.Start(GestisciMissione);
        }


        private void GestisciMissione(string str)
        {   
            Debug.Log(str);
            if (!str.Contains("null"))
            {
                if (str.Contains("\"path\":\"/\""))
                {
                    //nuovo evento
                    // event: put
                    // data: {"path":"/","data":{"kkk":{"Fasi":{"fase1":{"Ruoli":{"Coc":false,"Sindaco":false},"Status":true},"fase2":{"Ruoli":{"sindaco":false},"Status":false}},"Nome":"nome"}}}
                    str = str.Split("\"data\":")[1];
                
                    JSONObject json = new JSONObject(str);
                    Missione miss = json.toMissione();
                    Debug.Log(miss.printData());
                
                    //TODO più missioni nello stesso momento
                }
                else
                {
                    //modifica di un qualche stato
                    // event: put
                    // data: {"path":"/kkk/Fasi/fase1/isCompleted","data":false}
                    // data: {"path":"/kkk/Fasi/fase1/Ruolo/Sindaco","data":false}
                    //           0 1    2    3     4     5
                    //Valore di "data"
                    string data = str.Split("\"data\":")[1].Split("}")[0];
                    string path = str.Split("\"path\":\"")[1].Split("\",\"")[0];
                    string[] arr = path.Split("/");

                    string codiceMissione = arr[1];
                    string codiceFase = arr[3];
                
                    Dictionary<string, Fase> elencoFasi = missioni[codiceMissione].getElencoFasi();
                
                    //if str.contains("isCompleted")" 
                    //missioni[n1].elencofasi[n3].iscomnpleterd = !
                    //else if (str.contains("Ruolo"))
                    //missioni[n1].elencofasi[n3].ruoli[n5] = !

                    if (path.Contains("Ruolo"))
                    {
                        string nomeRuolo = arr[arr.Length - 1];
                        elencoFasi[codiceFase].ruoloFinished(nomeRuolo);
                    }
                    else if (path.Contains("isCompleted"))
                    {
                        elencoFasi[codiceFase].taskFinished();
                    }
                    
                    //Distinguere diversi casi:
                    /*
                 * viene modificato isCompleted
                 * viene modificato lo stato di un ruolo
                 */

                    //Se è IsCompleted fa una cosa
                    //Se si trova dentro Codice/Fasi/FaseX/Ruoli fa un altra roba
                }
            
           

           
            }
        }

    
    
    
    }
}
