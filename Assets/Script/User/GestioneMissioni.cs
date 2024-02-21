using Proyecto26;
using Script.Utility;
using UnityEngine;

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
            string missionURL = mso.GetMissionURL(); // URL/CODE/Missione/Code
            Debug.Log("Missione lanciata");
            //Inserimento nella cartella della missione del suo nome
            string nomeMissione = "{\"" + Global.NomeMissioneKey + "\":\"" + mso.nomeMissione + "\"}";
            RestClient.Patch(missionURL + ".json", nomeMissione).Then(afterNome =>
            {
                //per ognuno degli step viene caricato il loro stato IsEnded e la lista di ruoli coinvolti per step
                for (int i = 0; i < mso.numeroStep; i++)
                {
                    //stato dello step
                    string statusFase = "{\"" + Global.IsCompletedKey + "\":false}";
                    string[] ruoliStep = mso.ruoliPerStep[i].Split(",");
                    RestClient.Patch(missionURL + "/" + Global.FasiFolder + "/" + i + ".json", statusFase).Then(afterStatus =>
                    {
                        for (int j = 0; j < mso.numeroStep; j++)
                        {
                            //BUG possibile bux di i
                            string patchRuolo = "{\"" + ruoliStep[j] + "\":false}";
                            RestClient.Patch(missionURL + "/" + Global.FasiFolder + "/" + i + "/" + Global.RuoliFolder + ".json", patchRuolo);
                        }
                    });
                }
            });
        }
    }
}
