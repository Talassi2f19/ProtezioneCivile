using System.Collections;
using System.Collections.Generic;
using Script.Utility;
using UnityEngine;

public class GestioneEventiPlayer : MonoBehaviour
{
    private Listeners ascoltoEventi = new Listeners(Info.DBUrl + "AAAA" + "/Missioni.json");
     
    
    // Start is called before the first frame update
    void Start()
    {
        ascoltoEventi.Start(GestisciMissione);
    }


    private void GestisciMissione(string str)
    {   
        Debug.Log(str);
        if (!str.Contains("null"))
        {
            
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    
}
