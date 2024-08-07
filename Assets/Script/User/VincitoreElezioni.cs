using System;
using System.Collections.Generic;
using Defective.JSON;
using FirebaseListener;
using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using Scene = Script.Utility.Scene;

namespace Script.User
{
    public class RisultatiElezioni : MonoBehaviour
    {
        /*
        private JSONObject risultatiJson;
        private List<string> candidati = new List<string>();
        private List<JSONObject> voti = new List<JSONObject>();
        */

        private JSONObject playersJson;
        private List<string> playersName = new List<string>();
        private List<JSONObject> playersData = new List<JSONObject>();

        [SerializeField] private GameObject vincitore;
        [SerializeField] private GameObject meMedesimo;
        [SerializeField] private GameObject vincitoreGenerico;
        private Listeners listener;


    
        void Start()
        {
            meMedesimo.SetActive(false);
            vincitoreGenerico.SetActive(false);
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + ".json").Then(onReceived =>
            {
                playersJson = new JSONObject(onReceived.Text);
                playersName = playersJson.keys;
                playersData = playersJson.list;

                string nomeVincitore;

                int i = 0;
                while (i < playersName.Count && playersData[i][Global.RuoloPlayerKey].stringValue != "Sindaco")
                    i++;
                if (i >= playersName.Count) {
                    // Debug.Log("è successo qualcosa di veramente storto");
                    nomeVincitore = "ERRORE";
                } else
                    nomeVincitore = playersName[i];
                
                if (nomeVincitore == Info.localUser.name)
                {
                    meMedesimo.SetActive(true);
                    vincitoreGenerico.SetActive(false);
                }
                else
                {
                    meMedesimo.SetActive(false);
                    vincitoreGenerico.SetActive(true);
                    vincitore.GetComponent<TMP_Text>().text = nomeVincitore;
                }
            }).Catch(Debug.LogError);
            listener = new Listeners(Info.DBUrl + Info.sessionCode + "/" + Global.GameStatusCodeKey + ".json");
            listener.Start(CambioScena);

        }
        private void CambioScena(string str)
        {
            if (str.Contains(GameStatus.GenRuoli))
            {
                listener.Stop();
                RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + "/" + Global.RuoloPlayerKey + ".json").Then(e =>
                {

                    string str1 = e.Text;
                    str1 = str1.Remove(0, 1).Split("\"")[0];
                    if(Enum.Parse<Ruoli>(str1) == Ruoli.Sindaco)
                    { 
                        Info.localUser.role = Ruoli.Sindaco;
                        SceneManager.LoadScene(Scene.User.SelezioneCoc);
                    }
                    else
                    {
                        SceneManager.LoadScene(Scene.User.AttesaRuoli);
                    }
                }).Catch(Debug.LogError);
            

            } 
        }

        private void OnApplicationQuit()
        {
            listener.Stop();
        }
    }
}
