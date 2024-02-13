using System.Collections.Generic;
using Defective.JSON;
using Proyecto26;
using Script.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace Script
{
    //classe del login
    public class Login : MonoBehaviour
    {
        [SerializeField] private GameObject nome;
        [SerializeField] private GameObject sessione;
        [SerializeField] private GameObject notFound;
        [SerializeField] private GameObject found;
        [SerializeField] private GameObject plInGame;
        
        //pulsante entra, scena login
        public void Join()
        {
            string nomeUtente = nome.GetComponent<InputField>().text;
            if (nomeUtente != "" && nomeUtente.Length <= 16 && Info.SessionCode != "")
                
            { // se il nome e la sessione(già validata) sono presenti

                RestClient.Get(Info.DBUrl + Info.SessionCode + ".json").Then(r =>
                {
                    //richiedo la sessione
                    JSONObject jj = new JSONObject(r.Text);

                    string code = jj.GetField("gameStatusCode").stringValue;
                    JSONObject playerList = jj.GetField("players");
                    
                    //controllo se la partita è già iniziata e il numero di player
                    if (code == Info.GameStatus.WaitPlayer && (playerList == null || playerList.list.Count < Info.MaxPlayer))
                    {
                        if(playerList == null)
                        {
                            AddPlayer();
                        }
                        else if (playerList.GetField(nomeUtente) != null)
                        {
                            plInGame.SetActive(true);
                        }
                        else
                        {
                            AddPlayer();
                        }
                    }
                    else
                    {
                        //la stanza non accetta nuovi tentativo di riconnessione
                        Debug.Log("il gioco è già iniziato");
#if !UNITY_EDITOR
                        string cookie = WebGL.GetCookie();
                        if (cookie.Contains(nomeUtente) && playerList.keys.Contains(nomeUtente))
                        {
                            playerList.GetField(nomeUtente, jsonObject =>
                            {
                                Info.LocalUser = jsonObject.ToUser();
                                Debug.Log("riconnesso");
                        //TODO selezione scena rispettiva                             
                        SceneManager.LoadScene("game");
                            });
                        }
                        else
                        {
                            Debug.Log("non puoi riconnetterti");
                        }
#endif                        
                    }
                });
            }
            else
            {
                //evidenziare il campo obbligatorio mancante o errato
                if(Info.SessionCode == "")
                    sessione.GetComponent<Image>().color = UnityEngine.Color.red;
                if(nomeUtente == "" || nomeUtente.Length > 16)
                    nome.GetComponent<Image>().color = UnityEngine.Color.red;
            }
        }
        private void AddPlayer()
        {
            plInGame.SetActive(false);
            Info.LocalUser.name = nome.GetComponent<InputField>().text;
            //string toSend = JsonConvert.SerializeObject(Info.LocalUser);
            string toSend = JsonUtility.ToJson(Info.LocalUser);
            // Debug.Log(toSend);
            RestClient.Put(Info.DBUrl + Info.SessionCode + "/players/" + Info.LocalUser.name + ".json", toSend).Then( 
                loadElezioni =>
            {
                Debug.Log("Caricamento elezioni");
                 SceneManager.LoadScene("elezioni");
            });
        }
        
        public void SessionAutoCheck()
        {
            string code = sessione.GetComponent<InputField>().text.ToUpper();
            // Debug.Log(session.text);
            if (code.Length == Info.SessionCodeLength)
            {
                SessionExist(code);
            }
            else if (code.Length > Info.SessionCodeLength)
            {
                NotF();
            }
            else
            {
                notFound.SetActive(false);
                found.SetActive(false);
                Info.SessionCode = "";
            }
        }
        
        private void SessionExist(string str)
        {
     
            RestClient.Get(Info.DBUrl + ".json").Then(onResponse =>
            {
                JSONObject jj = new JSONObject(onResponse.Text);
                List<string> li = jj.keys;
                
                jj.GetField(str, jsonObject1 =>
                {
                    jsonObject1.GetField("gameStatusCode", jsonObject2 =>
                    {
                        if (jsonObject2.ToString() == Info.GameStatus.End)
                        {
                            NotF();
                        }
                        else
                        {
                            notFound.SetActive(false);
                            found.SetActive(true);
                            Info.SessionCode = str;
                        }
                    }, fail => NotF());
                }, fail => NotF());
            }).Catch(exception => NotF());
        }

        private void NotF()
        {
            notFound.SetActive(true);
            found.SetActive(false);
            Info.SessionCode = "";
        }

        public void Color(GameObject kk)
        {
            kk.GetComponent<Image>().color = UnityEngine.Color.white;
        }
    }
}