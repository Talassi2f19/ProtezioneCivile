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
        private InputField usernameText;
        private InputField stanzaText;
      
        private void Start()
        {
            errore.SetActive(false);
            errorText = errore.GetComponentInChildren<TextMeshProUGUI>();
            usernameText = username.GetComponent<InputField>();
            stanzaText = stanza.GetComponent<InputField>();
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

            if (nome.ToUpper() == "NULL" || nome.ToUpper().Contains("COMPUTER"))
            {
                ErroriDisplay(8);
                return;
            }

            RestClient.Get(Info.DBUrl + ".json").Then(e =>
            {
                JSONObject json = new JSONObject(e.Text, 0, -1, 3, false);
                if (!json.keys.Contains(codStanza.ToUpper().Trim()))
                {
                    ErroriDisplay(1);
                    return;
                }
             
                ErroriDisplay(0);
                json = json.GetField(codStanza);
                JSONObject playerList = json.GetField(Global.PlayerFolder);
                
                if (playerList != null && playerList.keys.Count >= Info.MaxPlayer)
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
                    if (playerList != null && playerList.keys.Contains(nome))
                    {
                        ErroriDisplay(2);
                        return;
                    }
                    //inserisci

                    string toSend = "{\"Name\":\"" + Info.localUser.name + "\",\"Role\":\"" + Ruoli.Null + "\"}";
                    RestClient.Put(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + ".json", toSend).Then(e =>
                    {
                        SceneManager.LoadScene(Scene.User.Elezioni);
                    }).Catch(Debug.LogError);
                    return;
                }
// #if !UNITY_EDITOR                
//                 if(json.GetField(Global.GameStatusCodeKey).stringValue != GameStatus.End){
//                     //partita già iniziata
//                     //tenta riconnessione
//                     string cookie = WebGL.GetCookie();
//                     if (cookie.Contains(nome) && json.GetField(Global.PlayerFolder).keys.Contains(nome))
//                     {
//                         RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + nome + "/Role.json").Then(e =>
//                         {
//                             Debug.Log(e.Text);
//                             Info.localUser.role = Enum.Parse<Ruoli>(e.Text);
//                             SceneManager.LoadScene(Scene.User.Elezioni);
//                         }).Catch(Debug.LogError);
//                     }
//                     else
//                     {
//                         ErroriDisplay(7);
//                     }
//                 }
// #endif
                if (json.GetField(Global.GameStatusCodeKey).stringValue != GameStatus.WaitPlayer)
                {
                    ErroriDisplay(5);
                    return;
                }
                ErroriDisplay(-1);
            }).Catch(Debug.LogError);
        }
        
        private void ErroriDisplay(int value)
        {
            button.interactable = true;
            if (value == 0)
            {
                errore.SetActive(false);
                return;
            }
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
                case 8:
                    messaggio = "Username non accettabile";
                    break;
                default:
                    messaggio = "errore";
                    break;
            }
            errore.SetActive(true);
            errorText.text = "Errore: " + messaggio;
        }
    }
}