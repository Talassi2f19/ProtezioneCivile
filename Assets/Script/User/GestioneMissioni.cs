using Proyecto26;
using Script.Utility;
using UnityEngine;
// ReSharper disable CommentTypo

namespace Script.User
{
    public class GestioneMissioni : MonoBehaviour
    {
        /*
    Ogni volta che viene lanciata una missione, viene creata una cartella nel database contenente tutte le informazioni
    al riguardo
    */
        
        public void LanciaMissione(MissionScriptableObject mso)
        {
            string missionURL = mso.GetMissionPositionURL();
            Debug.Log("Missione lanciata");

            string patchRequest = getJSONCurrentMission(mso);
            
            RestClient.Patch(missionURL + ".json", patchRequest);
        }
        
        private string getJSONCurrentMission(MissionScriptableObject mso)
        {
            string patchRequest = "{\"";
            patchRequest += mso.codiceMissione + "\":";
            patchRequest += "{\"";
            patchRequest += Global.FasiFolder + "\":";
            patchRequest += "{";
            
            for (int i = 0; i < mso.numeroStep; i++)
            {
                string[] ruoliStep = mso.ruoliPerStep[i].Split(",");

                patchRequest += "\"" + "F" + i + "\":";
                patchRequest += "{";
                patchRequest += "\"" + Global.IsCompletedKey + "\":false, \"" + Global.RuoliFolder + "\":";
                patchRequest += "{";

                for (int j = 0; j < ruoliStep.Length; j++)
                    patchRequest += "\"" + ruoliStep[j] + "\": false";

                patchRequest += "}";
                patchRequest += "}";
            }
            patchRequest += "}";
            patchRequest += ",\"" + Global.NomeMissioneKey + "\":\"" + mso.nomeMissione + "\"";

            patchRequest += "}";
            patchRequest += "}";

            return patchRequest;
        }
    }
}
