using System.Collections;
using System.Collections.Generic;
using Defective.JSON;
using Script.Utility;
using UnityEngine;

public class GestioneEventiPlayer : MonoBehaviour
{
    private Listeners ascoltoEventi = new Listeners(Info.DBUrl + "AAAA" + "/Missioni.json");

    
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
            }
            else
            {
                //modifica di un qualche stato
                // event: put
                // data: {"path":"/kkk/Fasi/fase1/Status","data":false}
                
                
                
            }
            
           

           
        }
    }

    
    
    
}
