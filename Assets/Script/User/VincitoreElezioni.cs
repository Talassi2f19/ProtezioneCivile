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
        private JSONObject risultatiJSON;
        private List<string> candidati = new List<string>();
        private List<JSONObject> voti = new List<JSONObject>();
        [SerializeField] private GameObject vincitore;
        private Listeners listener;


    
        void Start()
        {
            RestClient.Get(Info.DBUrl + Info.SessionCode + "/candidati.json").Then(onReceived =>
            {
                risultatiJSON = new JSONObject(onReceived.Text);
                candidati = risultatiJSON.keys;
                voti = risultatiJSON.list;

                int posMaxVoti = MaxVotiCandidato();
                vincitore.GetComponent<TMP_Text>().text = candidati[posMaxVoti];
            });

            listener = new Listeners(Info.DBUrl + Info.SessionCode + "/gameStatusCode.json");
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
            if (str.Contains(Info.GameStatus.Gioco))
            {
                listener.Stop();
                RestClient.Get(Info.DBUrl + Info.SessionCode + "/players/" + Info.localGenericUser.name + "/role.json").Then(e =>
                {
                    if(e.Text == "null")
                    {
                        SceneManager.LoadScene("_Scenes/user/attesaRuoli");
                    }
                    else
                    {
                        Info.localGenericUser.role = str;
                        SceneManager.LoadScene("_Scenes/user/selezioneCOC");
                    }
                });
            

            } 
        }
    }
}
