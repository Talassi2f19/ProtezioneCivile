using System;
using System.Collections.Generic;
using Defective.JSON;
using Script.Utility;
using UnityEngine;

// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace Script.User
{
    //classe per la gestione dei player
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private GameObject onlinePlayer;
        [SerializeField] private Transform parent;
        
        private Listeners pl = new Listeners(Info.DBUrl + Info.SessionCode +"/players/.json");
        private Dictionary<string, GameObject> playerList = new Dictionary<string, GameObject>();
        
        private void Start()
        {
            pl.Start(Read);
        }

        private void OnApplicationQuit()
        {
            pl.Stop();
        }

        private void Read(string str)
        {
            Debug.Log(str);
            if (str.Split("\n")[0].Contains("put"))
            {
                //crea un dizionario con i player 
                //Dictionary<string, User> kh = DeserializeUser(str.Split("\"data\":")[1]);
               Dictionary<string, GenericUser> kh = new JSONObject(str.Split("\"data\":")[1]).ToUserDictionary();
                   
                
                //rimuove il player locale
                kh.Remove(Info.localGenericUser.name);
                
                //si istanziano i gameObject degli altri player
                foreach (var pl in kh)
                {
                    playerList.Add(pl.Key, GameObject.Instantiate(onlinePlayer, parent));
                    playerList[pl.Key].GetComponent<PlayerOnline>().SetUser(pl.Value);
                }
            }
            else if (str.Split("\n")[0].Contains("patch") && str.Contains("cord"))
            {
                str = str.Split("\"path\":\"/")[1];
                string n = str.Split("/", 2)[0];
                try
                {
                    var t = playerList[n]; //se il player non viene trovato esce direttamente senza eseguire il resto
                    str = str.Remove(str.Length - 1);
                    str = str.Split("data\":")[1];
                    Vector2 move = new JSONObject(str).ToVector2();
                    
                    playerList[n].GetComponent<PlayerOnline>().Move(move);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
    }
}