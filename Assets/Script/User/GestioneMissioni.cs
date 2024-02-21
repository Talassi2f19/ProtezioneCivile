using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using Script.Utility;
using Unity.VisualScripting;
using UnityEngine;

public class GestioneMissioni : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    Ogni volta che viene lanciata una missione, viene creata una cartella nel database contenente tutte le informazioni
    al riguardo
    */
    public void LanciaMissione(MissionScriptableObject mso)
    {
        string missionURL = mso.GetMissionURL(); // URL/CODE/Missione/Code
        
        //Inserimento nella cartella della missione del suo nome
        string nomeMissione = "{\"NomeMissione\":\"" + mso.nomeMissione + "\"}";
        RestClient.Patch(missionURL + ".json", nomeMissione).Then(afterNome =>
        {
            //per ognuno degli step viene caricato il loro stato IsEnded e la lista di ruoli coinvolti per step
            for (int i = 0; i < mso.numeroStep; i++)
            {
                //stato dello step
                string statusFase = "{\"isCompleted\":false}";
                string[] ruoliStep = mso.ruoliPerStep[i].Split(",");
                RestClient.Patch(missionURL + "/fasi/" + i + ".json", statusFase).Then(afterStatus =>
                {
                    for (int j = 0; j < mso.numeroStep; j++)
                    {
                        string patchRuolo = "{\"" + ruoliStep[j] + "\":false}";
                        RestClient.Patch(missionURL + "/fasi/" + i + "/ruoli.json", patchRuolo);
                    }
                });
            }
        });
    }
}
