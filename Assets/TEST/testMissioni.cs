using System;
using System.Collections;
using System.Collections.Generic;
using Defective.JSON;
using Proyecto26;
using Script.Utility;
using Script.Utility.GestioneEventi;
using UnityEngine;

public partial class NewBehaviourScript : MonoBehaviour
{
    private Dictionary<string, Missione> missioni = new Dictionary<string, Missione>();
    private void Start()
    {
        RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.MissioniFolder + ".json").Then(e =>
        { 
            Debug.Log(e.Text);
            JSONObject hh = new JSONObject(e.Text);
            Debug.Log(hh);
            printAll();
            missioni = hh.ToMissioneDictionary();
            printAll();
        });
    }
    private void printAll()
    {
        Debug.Log(missioni.Count);
        foreach (var VARIABLE in missioni)
        {
            Debug.Log(VARIABLE.Key + ":" + VARIABLE.Value.printData());
        }
    }
    // private void Start()
    // {
    //     
    //  
    //
    //     RestClient.Post(Info.DBUrl + ".json", ddas(jhgh)).Then(d =>
    //     {
    //         Debug.Log(d.Text); //codice missione
    //     });
    // }

 public MissioneObj jhgh;
    private string ddas(MissioneObj missioneObj)
    {
        JSONObject a = new JSONObject();
        a.AddField(Global.NomeMissioneKey , missioneObj.nomeMissione);
        a.AddField(Global.FasiFolder, b =>
        {
            int i = 0;
            foreach (var kk in missioneObj.fasi)
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
