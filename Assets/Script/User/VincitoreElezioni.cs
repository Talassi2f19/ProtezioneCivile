using System;
using System.Collections.Generic;
using Defective.JSON;
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
        private JSONObject risultatiJson;
        private List<string> candidati = new List<string>();
        private List<JSONObject> voti = new List<JSONObject>();
        [SerializeField] private GameObject vincitore;
        [SerializeField] private GameObject meMedesimo;
        [SerializeField] private GameObject vincitoreGenerico;
        private Listeners listener;


    
        void Start()
        {
            meMedesimo.SetActive(false);
            vincitoreGenerico.SetActive(false);
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.CandidatiFolder + ".json").Then(onReceived =>
            {
                risultatiJson = new JSONObject(onReceived.Text);
                candidati = risultatiJson.keys;
                voti = risultatiJson.list;

                
                string nomeVincitore = candidati[MaxVotiCandidato()];
                
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
            });

            listener = new Listeners(Info.DBUrl + Info.sessionCode + "/" + Global.GameStatusCodeKey + ".json");
            listener.Start(CambioScena);

        }

        private int MaxVotiCandidato()
        {
            int pos = 0;
            for (int i = 1; i < candidati.Count; i++)
            {
                if (voti[pos].intValue < voti[i].intValue)
                    pos = i;
            }
            return pos;
        }
    
        private void CambioScena(string str)
        {
            if (str.Contains(GameStatus.Gioco))
            {
                listener.Stop();
                RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + "/" + Global.RuoloPlayerKey + ".json").Then(e =>
                {
                    Debug.Log(e);
                    Debug.Log(e.Text);

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
                });
            

            } 
        }
    }
}
