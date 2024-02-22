using System;
using System.Collections.Generic;
using Defective.JSON;
using Script.User.Prefabs;
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
        [SerializeField] private GameObject mostraRuoloPrefab;
        [SerializeField] private GameObject infoRuolo;
        [SerializeField] private GameObject joyStick;
        
        private Listeners pl = new Listeners(Info.DBUrl + Info.sessionCode +"/" + Global.PlayerFolder + ".json");
        private Dictionary<string, GameObject> playerList = new Dictionary<string, GameObject>();
        
        private void Start()
        {
            ShowMostraRuolo();
            pl.Start(Read);
        }

        private void OnApplicationQuit()
        {
            pl.Stop();
        }

        private void Read(string str)
        {
            //Debug.Log(str);
            if (str.Split("\n")[0].Contains("put"))
            {
                //crea un dizionario con i player 
                //Dictionary<string, User> kh = DeserializeUser(str.Split("\"data\":")[1]);
               Dictionary<string, GenericUser> players = new JSONObject(str.Split("\"data\":")[1]).ToUserDictionary();
                   
                
                //rimuove il player locale
                players.Remove(Info.localUser.name);
                
                //si istanziano i gameObject degli altri player
                foreach (var player in players)
                {
                    playerList.Add(player.Key, GameObject.Instantiate(onlinePlayer, parent));
                    playerList[player.Key].GetComponent<PlayerOnline>().SetUser(player.Value);
                }
            }
            else if (str.Split("\n")[0].Contains("patch") && str.Contains(Global.CoordPlayerKey))
            {
                str = str.Split("\"path\":\"/")[1];
                string n = str.Split("/", 2)[0];
                try
                {
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

        //TODO non mostrare il controller se non si è da mobile
        
        public void ShowMostraRuolo()
        {
            mostraRuoloPrefab.SetActive(true);
            infoRuolo.SetActive(false);
            joyStick.SetActive(false);
        }

        public void CloseMostraRuolo()
        {
            infoRuolo.SetActive(true);
            joyStick.SetActive(true);
        }
    }
}