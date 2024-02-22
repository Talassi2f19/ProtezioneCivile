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
        
     

        RestClient.Post(Info.DBUrl + ".json", ddas(jhgh)).Then(d =>
        {
            Debug.Log(d.Text); //codice missione
        });
    }

 public MissioneDef jhgh;
    private string ddas(MissioneDef missioneDef)
    {
        JSONObject a = new JSONObject();
        a.AddField(Global.NomeMissioneKey , missioneDef.nomeMissione);
        a.AddField(Global.FasiFolder, b =>
        {
            int i = 0;
            foreach (var kk in missioneDef.fasi)
            {
                b.AddField("F"+ i++, c =>
                {
                    c.AddField(Global.IsCompletedKey, false);
                    c.AddField(Global.RuoliFolder, d =>
                    {
                        foreach (var rr in kk.Ruoli)
                        {
                            d.AddField(rr.ToString(), false);
                        }
                    });
                });
            }
        });
        return a.ToString();
    }
}
