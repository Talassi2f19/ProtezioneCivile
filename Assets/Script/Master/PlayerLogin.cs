using System.Collections.Generic;
using System.Linq;
using Defective.JSON;
using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable CommentTypo

namespace Script.Master
{
    public class PlayerLogin : MonoBehaviour
    {
        [SerializeField] private GameObject displaySessionCode;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform parent;
    
        private Listeners playerJoin;
        private Dictionary<string, GameObject> playerList = new Dictionary<string, GameObject>();

        private void Start()
        {
            //mostra il codice di accesso
            displaySessionCode.GetComponent<TMP_Text>().text = Info.sessionCode;
            playerJoin = new Listeners(Info.DBUrl + Info.sessionCode + "/players.json");
            playerJoin.Start(PlayerAdd);
        }

        private void PlayerAdd(string str)
        {
            // player aggiutno
            // event: put
            // data: {"path":"/s","data":{"cord":{"x":0.0,"y":0.0},"name":"s","role":"null"}}

            // player rimosso
            // event: put
            // data: {"path":"/s","data":null}

            // nessun player presente
            // event: put
            // data: {"path":"/","data":1}
            if (str.Contains("event: put"))
            {
                if (!str.Contains("\"path\":\"/\""))
                {
                    str = Normalize(str.Split("data: ", 2)[1]);
                    KeyValuePair<string, GenericUser> tmp = (new JSONObject(str).ToUserDictionary()).First();

                    Debug.Log(str);

                    if (tmp.Value == null)
                    {
                        //utente da rimuovere
                        playerList[tmp.Key].SetActive(false);
                        playerList.Remove(tmp.Key);
                        Debug.Log("player left:" + tmp.Key);
                    }
                    else
                    {
                        //utente da aggiungere
                        playerList.Add(tmp.Key, GameObject.Instantiate(playerPrefab, parent));
                        playerList[tmp.Key].GetComponent<PulsantePlayerRemove>().SetName(tmp.Key);
                        playerList[tmp.Key].SetActive(true);
                        Debug.Log("player join:" + tmp.Key);
                    }
                }

            }
            Debug.Log(str);
        }

        private string Normalize(string str)
        {
            return "{\"" + str.Split("/", 2)[1].Split("\"", 2)[0] + "\"" + str.Split("\"data\"")[1];
        }

        public void ProssimaScena()
        {
            //TODO aggiungere limite minimo
            if (playerList.Count > 0)
            {
                string str = "{\"gameStatusCode\":\"" + Info.GameStatus.Candidatura + "\"}";
                RestClient.Patch(Info.DBUrl + Info.sessionCode + ".json", str);
                playerJoin.Stop();
                SceneManager.LoadScene("_Scenes/master/elezioni");
            }
            else
            {
                //TODO Messaggio di errore
            }
        }
    }
}