using UnityEngine;
using System.Collections.Generic;
using Script.Utility;

[CreateAssetMenu(fileName = "Missione", menuName = "ScriptableObjects/MissionScriptableObject", order = 1)]
public class MissionScriptableObject : ScriptableObject
{
    public int codiceMissione;
    public string nomeMissione = "Telecomunicazioni";
    public int numeroStep;
    public List<string> ruoliPerStep;

    

    public string GetMissionURL()
    {
        return Info.DBUrl + Info.sessionCode + "/" + Global.MissioniFolder + "/" + codiceMissione;
    }
    
}