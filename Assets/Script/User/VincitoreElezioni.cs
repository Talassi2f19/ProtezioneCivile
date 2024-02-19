using System.Collections.Generic;
using Defective.JSON;
using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.User
{
    public class RisultatiElezioni : MonoBehaviour
    {
        private JSONObject risultatiJson;
        private List<string> candidati = new List<string>();
        private List<JSONObject> voti = new List<JSONObject>();
        [SerializeField] private GameObject vincitore;
        private Listeners listener;


    
        void Start()
        {
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/candidati.json").Then(onReceived =>
            {
                risultatiJson = new JSONObject(onReceived.Text);
                candidati = risultatiJson.keys;
                voti = risultatiJson.list;

                int posMaxVoti = MaxVotiCandidato();

                //TODO se � il player stesso che vince le elezioni mostrare un altro messaggio

                vincitore.GetComponent<TMP_Text>().text = candidati[posMaxVoti];
            });

            listener = new Listeners(Info.DBUrl + Info.sessionCode + "/gameStatusCode.json");
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
            if (str.Contains(Info.GameStatus.AssegnazioneRuoli))
            {
                listener.Stop();
                RestClient.Get(Info.DBUrl + Info.sessionCode + "/players/" + Info.localUser.name + "/role.json").Then(e =>
                {
                    Debug.Log(e);
                    Debug.Log(e.Text);
                    
                    //TODO fix
                    if(e.Text == "null")
                    { 
                        Info.localUser.role = str;
                        SceneManager.LoadScene("_Scenes/user/selezioneCOC");
                    }
                    else
                    {
                        SceneManager.LoadScene("_Scenes/user/attesaRuoli");
                    }
                });
            

            } 
        }
    }
}
