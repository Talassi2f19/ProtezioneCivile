using System.Collections.Generic;
using UnityEngine;

namespace Script.Utility
{
    [CreateAssetMenu(fileName = "Missione", menuName = "ScriptableObjects/MissionScriptableObject", order = 1)]
    public class MissionScriptableObject : ScriptableObject
    {
        public string codiceMissione;
        public string nomeMissione = "Telecomunicazioni";
        public int numeroStep;
        public List<string> ruoliPerStep;

    

        public string GetMissionPositionURL()
        {
            return Info.DBUrl + Info.sessionCode + "/" + Global.MissioniFolder;
        }
    
    }
}