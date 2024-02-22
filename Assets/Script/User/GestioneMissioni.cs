using Proyecto26;
using Script.Utility;
using Script.Utility.GestioneEventi;
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
        
        public void LanciaMissione(MissioneDef mso)
        {
            string missionURL = mso.GetMissionPositionURL();
            Debug.Log("Missione lanciata");

            string patchRequest = GetJSONCurrentMission(mso);
            Debug.Log(patchRequest);
            
            Debug.Log(missionURL);
            
            RestClient.Patch(missionURL + ".json", patchRequest);
        }
        
        private string GetJSONCurrentMission(MissioneDef mso)
        {
            string patchRequest = "{\"";
            patchRequest += mso.codiceMissione + "\":";
            patchRequest += "{\"";
            patchRequest += Global.FasiFolder + "\":";
            patchRequest += "{";
            
            for (int i = 0; i < mso.fasi.Count; i++)
            {
                patchRequest += "\"" + "F" + i + "\":";
                patchRequest += "{";
                patchRequest += "\"" + Global.IsCompletedKey + "\":false, \"" + Global.RuoliFolder + "\":";
                patchRequest += "{";

                for (int j = 0; j < mso.fasi[i].Ruoli.Count; j++)
                    patchRequest += "\"" + mso.fasi[i].Ruoli[j] + "\": false";

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
