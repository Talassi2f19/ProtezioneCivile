using System;
using Defective.JSON;
using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Scene = Script.Utility.Scene;

// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace Script.User
{
    //classe del login
    public class Login : MonoBehaviour
    {
        [SerializeField] private GameObject username;
        [SerializeField] private GameObject stanza;
        [SerializeField] private GameObject errore;
        [SerializeField] private Button button;
        private TextMeshProUGUI errorText;
        private TMP_InputField usernameText;
        private TMP_InputField stanzaText;
      
        private void Start()
        {
            errore.SetActive(false);
            errorText = errore.GetComponentInChildren<TextMeshProUGUI>();
            usernameText = username.GetComponent<TMP_InputField>();
            stanzaText = stanza.GetComponent<TMP_InputField>();
        }


        public void Join()
        {
            button.interactable = false;
            String codStanza = stanzaText.text.ToUpper().Trim();
            String nome = usernameText.text;
            if (codStanza == "")
            {
                ErroriDisplay(3);
                return;
            }
            
            if (codStanza.Length != Info.SessionCodeLength)
            {
                ErroriDisplay(1);
                return;
            }

            if (nome == "")
            {
                ErroriDisplay(4);
                return;
            }

            RestClient.Get(Info.DBUrl + ".json").Then(e =>
            {
                JSONObject json = new JSONObject(e.Text, 0, -1, 3, false);
                Debug.Log(json);
                if (!json.keys.Contains(codStanza.ToUpper().Trim()))
                {
                    ErroriDisplay(1);
                    return;
                }
                stanza.GetComponent<Image>().color = Color.green;
                ErroriDisplay(0);
                
                json = json.GetField(codStanza);

                if (json.GetField(Global.PlayerFolder).keys.Count > Info.MaxPlayer)
                {
                    ErroriDisplay(6);
                    return;
                }
                
                Info.sessionCode = codStanza;
                Info.localUser.name = nome;
                
                //controllo stato partita
                if (json.GetField(Global.GameStatusCodeKey).stringValue == GameStatus.WaitPlayer)
                {
                    //connessione assicurata
                    //controllo univocità nome
                    if (json.GetField(Global.PlayerFolder).keys.Contains(nome))
                    {
                        ErroriDisplay(2);
                        return;
                    }
                    //inserisci
                        
                    string toSend = "{\"Name\":\"" + Info.localUser.name + "\",\"Role\":\"" + Ruoli.Null + "\"}";
                    RestClient.Put(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + ".json", toSend).Then(e =>
                    {
                        SceneManager.LoadScene(Scene.User.Elezioni);
                    });
                    return;
                }
#if !UNITY_EDITOR                
                if(json.GetField(Global.GameStatusCodeKey).stringValue != GameStatus.End){
                    //partita già iniziata
                    //tenta riconnessione
                    string cookie = WebGL.GetCookie();
                    if (cookie.Contains(nome) && json.GetField(Global.PlayerFolder).keys.Contains(nome))
                    {
                        RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + nome + "/Role.json").Then(e =>
                        {
                            Debug.Log(e.Text);
                            Info.localUser.role = Enum.Parse<Ruoli>(e.Text);
                            SceneManager.LoadScene(Scene.User.Elezioni);
                        });
                    }
                    else
                    {
                        ErroriDisplay(7);
                    }
                }
#endif
            });
        }
        

        // private void ControlloUsername()
        // {
        //     
        // }
        
        private void ErroriDisplay(int value)
        {
            button.interactable = true;
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
                    messaggio = "Codice stanza non trovato";
                    break;
                case 2:
                    messaggio = "Username già in uso";
                    break;
                case 3:
                    messaggio = "Codice stanza mancante";
                    break;
                case 4:
                    messaggio = "Username mancante";
                    break;
                case 5:
                    messaggio = "Partita già iniziata";
                    break;
                case 6:
                    messaggio = "Stanza piena";
                    break;
                case 7:
                    messaggio = "Impossibile riconnettersi";
                    break;
                default:
                    messaggio = "errore";
                    break;
            }
            errore.SetActive(true);
            errorText.text = "Errore: " + messaggio;
        }

        //pulsante entra, scena login
//         public void Join()
//         {
//             string nomeUtente = nome.GetComponent<TMP_InputField>().text;
//             Debug.Log("JOIN");
//             if (nomeUtente != "" && nomeUtente.Length <= 16 && Info.sessionCode != "")
//             { // se il nome e la sessione(già validata) sono presenti
//                 RestClient.Get(Info.DBUrl + Info.sessionCode + ".json").Then(response =>
//                 {
//                     //richiedo la sessione
//                     JSONObject jj = new JSONObject(response.Text);
//
//                     string code = jj.GetField(Global.GameStatusCodeKey).stringValue;
//                     JSONObject playerList = jj.GetField(Global.PlayerFolder);
//                     
//                     //controllo se la partita è già iniziata e il numero di player
//                     if (code == GameStatus.WaitPlayer && (playerList == null || playerList.list.Count < Info.MaxPlayer))
//                     {
//                         if(playerList == null)
//                             AddPlayer();
//                         else if (playerList.GetField(nomeUtente) != null)
//                              plInGame.SetActive(true);
//                         else
//                             AddPlayer();
//                     }
//                     else
//                     {
//                         
//                         //la stanza non accetta nuovi tentativo di riconnessione
//                         Debug.Log("Il gioco è già iniziato");
// #if !UNITY_EDITOR
//                         string cookie = WebGL.GetCookie();
//                         if (cookie.Contains(nomeUtente) && playerList.keys.Contains(nomeUtente))
//                         {
//                             playerList.GetField(nomeUtente, jsonObject =>
//                             {
//                                 Info.localUser = jsonObject.ToUser();
//                                 Debug.Log("riconnesso");
//                                                     
//                         SceneManager.LoadScene(Scene.User.Elezioni);
//                             });
//                         }
//                         else
//                         {
//                             Debug.Log("non puoi riconnetterti");
//                         }
// #endif                        
//                     }
//                 });
//             }
//             else
//             {
//                 //evidenziare il campo obbligatorio mancante o errato
//                 if(Info.sessionCode == "")
//                     sessione.GetComponent<Image>().color = UnityEngine.Color.red;
//                 if(nomeUtente == "" || nomeUtente.Length > 16)
//                     nome.GetComponent<Image>().color = UnityEngine.Color.red;
//             }
//         }
//         private void AddPlayer()
//         {
//             Debug.Log("AddPlayer");
//             plInGame.SetActive(false);
//             Info.localUser.name = nome.GetComponent<TMP_InputField>().text;
//             //string toSend = JsonConvert.SerializeObject(Info.LocalUser);
//             string toSend = "{\"Name\":\"" + Info.localUser.name + "\",\"Role\":\""+Ruoli.Null+"\"}";
//             Debug.Log(toSend);
//             RestClient.Put(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + ".json", toSend).Then(e =>
//             {
//                 Debug.Log("Caricamento elezioni");
//                 SceneManager.LoadScene(Scene.User.Elezioni);
//             });
//         }
//         
//         public void SessionAutoCheck()
//         {
//             string code = sessione.GetComponent<TMP_InputField>().text.ToUpper();
//            //Debug.Log("Typed code: " + code + "; Code Length: " + code.Length);
//             
//             if (code.Length == Info.SessionCodeLength)
//                 SessionExist(code);
//             else if (code.Length > Info.SessionCodeLength)
//                 NotF();
//             else
//             {
//                 notFound.SetActive(false);
//                 found.SetActive(false);
//                 Info.sessionCode = "";
//             }
//         }
//         
//         private void SessionExist(string str)
//         {
//      
//             RestClient.Get(Info.DBUrl + ".json").Then(onResponse =>
//             {
//                 JSONObject jj = new JSONObject(onResponse.Text);
//                 
//                 jj.GetField(str, jsonObject1 =>
//                 {
//                     jsonObject1.GetField(Global.GameStatusCodeKey, jsonObject2 =>
//                     {
//                         if (jsonObject2.ToString() == GameStatus.End)
//                         {
//                             NotF();
//                         }
//                         else
//                         {
//                             notFound.SetActive(false);
//                             found.SetActive(true);
//                             Info.sessionCode = str;
//                         }
//                     }, fail => NotF());
//                 }, fail => NotF());
//             }).Catch(exception => NotF());
//         }
//
//         private void NotF()
//         {
//             notFound.SetActive(true);
//             found.SetActive(false);
//             Info.sessionCode = "";
//         }
//
//         public void Color(GameObject kk)
//         {
//             kk.GetComponent<Image>().color = UnityEngine.Color.white;
//         }
    }
}