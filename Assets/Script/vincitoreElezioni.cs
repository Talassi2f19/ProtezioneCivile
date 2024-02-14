using System.Collections;
using System.Collections.Generic;
using Defective.JSON;
using Proyecto26;
using Script;
using Script.Utility;
using TMPro;
using UnityEngine;

public class RisultatiElezioni : MonoBehaviour
{
    private JSONObject risultatiJSON;
    private List<string> candidati = new List<string>();
    private List<JSONObject> voti = new List<JSONObject>();
    [SerializeField] private GameObject vincitore;

    
    void Start()
    {
        RestClient.Get(Info.DBUrl + Info.SessionCode + "/candidati.json").Then(onReceived =>
        {
        risultatiJSON = new JSONObject(onReceived.Text);
        candidati = risultatiJSON.keys;
        voti = risultatiJSON.list;

        int posMaxVoti = maxVotiCandidato();
        vincitore.GetComponent<TMP_Text>().text = candidati[posMaxVoti];
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
    
}
