using System;
using System.Collections.Generic;
using System.Linq;
using Defective.JSON;
using Proyecto26;
using Script.Master.Prefabs;
using Script.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = Script.Utility.Scene;

// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable CommentTypo

namespace Script.Master
{
    public class PlayerLogin : MonoBehaviour
    {
        [SerializeField] private GameObject displaySessionCode;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject popUpPrefab;
        [SerializeField] private GameObject errore;
        
        private Listeners playerJoin;
        private Dictionary<string, GameObject> playerList = new Dictionary<string, GameObject>();
        private TextMeshProUGUI errorText;
        
        private void Start()
        {
            popUpPrefab.SetActive(false);
            errore.SetActive(false);
            errorText = errore.GetComponentInChildren<TextMeshProUGUI>();
            //mostra il codice di accesso
            displaySessionCode.GetComponent<TMP_Text>().text = Info.sessionCode;
            playerJoin = new Listeners(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + ".json");
            playerJoin.Start(PlayerAdd);
        }

        private void ErroriDisplay(int value)
        {
            if (value == 0)
            {
                errore.SetActive(false);
                return;
            }

            // stanza.GetComponent<Image>().color = Color.white;
            String messaggio = "";
            switch (value)
            {
                case 1:
                    messaggio = "Numero di giocatori insufficiente";
                    break;
                case 2:
                    messaggio = "Limite massimo di giocatori raggiunto";
                    break;
                default:
                    messaggio = "errore";
                    break;
            }

            errore.SetActive(true);
            errorText.text = "Errore: " + messaggio;
        }

        private void PlayerAdd(string str)
        {
            // player aggiutno
            // event: put
            // data: {"path":"/s","data":{"name":"s","role":"null"}}

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
                        Destroy(playerList[tmp.Key]);
                        // playerList[tmp.Key].SetActive(false);
                        playerList.Remove(tmp.Key);
                        Debug.Log("player left:" + tmp.Key);
                    }
                    else
                    {
                        //utente da aggiungere
                        if ((playerList.Count + 1) <= 30)
                        {
                            playerList.Add(tmp.Key, GameObject.Instantiate(playerPrefab, parent));
                            playerList[tmp.Key].GetComponent<PulsantePlayerRemove>().SetName(tmp.Key);
                            playerList[tmp.Key].SetActive(true);
                            Debug.Log("player join:" + tmp.Key);
                        }
                        else
                        {
                            ErroriDisplay(2);
                        }
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
            if (playerList.Count > Info.MinPlayer)
            {
                string str = "{\"" + Global.GameStatusCodeKey + "\":\"" + GameStatus.Candidatura + "\"}";
                RestClient.Patch(Info.DBUrl + Info.sessionCode + ".json", str);
                playerJoin.Stop();
                SceneManager.LoadScene(Scene.Master.Elezioni);
            }
            else
            {
                //popUpPrefab.SetActive(true);
                ErroriDisplay(1);
            }
        }
    }
}