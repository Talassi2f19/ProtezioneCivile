using System;
using System.Collections;
using System.Collections.Generic;
using Defective.JSON;
using Proyecto26;
using Script.Utility;
using Script.Utility.GestioneEventi;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("dsa");
        RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + "/Role.json").Then(e =>
        {
            Debug.Log(e.Text);
            Debug.Log( Info.localUser.role);
            Info.localUser.role = (Ruoli)Enum.Parse(typeof(Ruoli), e.Text);
            Debug.Log( Info.localUser.role);
            
            Debug.Log(Info.localUser.name);
        });
    }

  
}


