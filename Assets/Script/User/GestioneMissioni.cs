using Defective.JSON;
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
            Debug.Log("Missione lanciata");
            
            RestClient.Post(Info.DBUrl + Info.sessionCode + "/" + Global.MissioniFolder + ".json",GeneraJson(mso) ).Then(d =>
            {
                Debug.Log(d.Text); //codice missione
            }).Catch(g =>
            {
                Debug.Log(g);
            });

        }
        
        private string GeneraJson(MissioneDef missioneDef)
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
}
