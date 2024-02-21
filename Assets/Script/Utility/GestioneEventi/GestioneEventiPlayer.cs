using System.Collections;
using System.Collections.Generic;
using Script.Utility;
using UnityEngine;

public class GestioneEventiPlayer : MonoBehaviour
{
    private Listeners ascoltoEventi = new Listeners(Info.DBUrl + Info.sessionCode + "/Missioni.json");
     
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    
}
