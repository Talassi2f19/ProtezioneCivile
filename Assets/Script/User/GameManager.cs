using System;
using System.Collections;
using System.Collections.Generic;
using _Scenes.User.telefono;
using Defective.JSON;
using FirebaseListener;
using Proyecto26;
using Script.User.Prefabs;
using Script.Utility;
using TEST;
using UnityEngine;
// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace Script.User
{
    public class GameManager : MonoBehaviour
    {
        
        [SerializeField] private GameObject onlinePlayer;
        [SerializeField] private Transform parent;

        private Listeners listeners;
        private Dictionary<string, GameObject> playerList = new Dictionary<string, GameObject>();

        private Dictionary<string, Task> taskList = new Dictionary<string, Task>();

        [SerializeField]private TaskManager taskManager;
        [SerializeField] private JoyStick joyStick;

        private List<string> taskRecived = new List<string>();
        
        private void Start()
        {
            
            joyStick.Enable(true);
            listeners = new Listeners(Info.DBUrl + Info.sessionCode + "/Game.json");
            listeners.Start(Parse);
            CaricaPlayer();
        }
        
        private void OnApplicationQuit()
        {
            if(listeners != null)
                listeners.Stop();
        }
        
        private void Parse(string data)
        {
            //da ignorare
             // event: put
             // data: {"path":"/","data":{"Posizione":{"a":{"Coord":{"x":1,"y":0}},"admin":{"Coord":{"x":0.6016445755958557,"y":0.6407574415206909}}},"Task":"{null}"}..........}
            
            // event: patch
            // data: {"path":"/Posizione/<nome Player locale>/Coord","data":{"x":1.3084839582443237,"y":1.1914124488830566}}
            
            // event: keep-alive
            // data: null
            
            //aggiorna pos
            // event: patch
            // data: {"path":"/Posizione/admin/Coord","data":{"x":0.6016445755958557,"y":0.6407574415206909}}
            
            //task
            // event: put
            // data: {"path":"/Task/-NxsMbI_dagLXyhk5LhS","data":{"CodTask":0,"Destinatario":"abscds","IdRisposta":""}}
            
            // event: put
            // data: {"path":"/Task/-NxsMbI_dagLXyhk5LhS","data":{"CodTask":0,"Destinatario":"","IdRisposta":"-NxsMbI_dagLXyhk5LhS"}}
            
            // event: put
            // data: {"path":"/Task/-NxsMbI_dagLXyhk5LhS","data":null}
            
            if (data.StartsWith("event: patch\ndata: {\"path\":\"/Posizione"))
            {
                if (!data.StartsWith("event: patch\ndata: {\"path\":\"/Posizione/" + Info.localUser.name))
                {
                    MuoviPlayer(new JSONObject(data.Split("data: ")[1]));
                    return;
                }
                return;
            }
            
            if (data.StartsWith("event: put\ndata: {\"path\":\"/Task"))
            {
                // event: put
                // data: {"path":"/Task/-NyblKDKNWqsCpdQe5Pq","data":{"CodeTask":1}}
                
                // Trova l'inizio del codice (dopo "/Task/")
                string startDelimiter = "/Task/";
                int startIndex = data.IndexOf(startDelimiter, StringComparison.Ordinal) + startDelimiter.Length;

                // Trova la fine del codice (prima di ","data)
                string endDelimiter = "\",\"data";
                int endIndex = data.IndexOf(endDelimiter, startIndex, StringComparison.Ordinal);
                
                string codice = data.Substring(startIndex, endIndex - startIndex);
                
                taskRecived.Add(codice);
                
                //{"CodeTask":1, "":""}
                JSONObject json = new JSONObject(data.Split("data: ")[1]).GetField("data");
                
                SetTask(json);
                return;
            }
            
            if (data.StartsWith("event: put\ndata: {\"path\":\"/\",\"data\":{"))
            {
                JSONObject json = new JSONObject(data.Split("data: ")[1]).GetField("data");
                if (!json.GetField("Task"))
                    return;
                json = json.GetField("Task");
                
                foreach (var key in json.keys)
                {
                    if (!taskRecived.Contains(key))
                    {
                        taskRecived.Add(key);
                        SetTask(json[key]);
                    }    
                }
            }
        }

        private void SetTask(JSONObject json)
        {
            int codice = json.GetField("CodeTask").intValue;
            
            if (codice == 0) 
                return;
                            
            if (json.GetField("Player"))
            {
                taskManager.Assegna(codice, json.GetField("Player").stringValue);
            }
            else
            {
                taskManager.Assegna(codice);
            }
        }

        private void CaricaPlayer()
        {
  
            JSONObject userList;
            RestClient.Get(Info.DBUrl+Info.sessionCode+"/" + Global.PlayerFolder + ".json").Then(a =>
            {
                userList = new JSONObject(a.Text);
                
                RestClient.Get(Info.DBUrl+Info.sessionCode+"/Game/Posizione.json").Then(b =>
                {
                    userList.Merge(new JSONObject(b.Text));
                    
                    CaricaPlayer2(userList);
                }).Catch(Debug.LogError);
            }).Catch(Debug.LogError);
        }

        private void CaricaPlayer2(JSONObject userList)
        {
            // Info.localUser.role = userList.GetField(Info.localUser.name).ToUser().role;
            
            userList.RemoveField(Info.localUser.name);
            
            if(!userList)
                return;
            foreach (JSONObject json in userList.list)
            {
                if (!json.GetField("Virtual"))
                {
                    string n = json.GetField("Name").stringValue;
                    Vector2 coord = json.GetField("Coord") ? json.GetField("Coord").ToVector2() : Vector2.zero;
                    playerList.Add(n, Instantiate(onlinePlayer ,coord, new Quaternion(), parent));
                    playerList[n].name = n;
                    playerList[n].GetComponent<PlayerOnline>().Set(json);
                }
            }
        }

        private void MuoviPlayer(JSONObject value)
        {
            String nome = value.GetField("path").stringValue.Split("/")[2];
            Vector2 newPos = value.GetField("data").ToVector2();
            playerList[nome].GetComponent<PlayerOnline>().Move(newPos);
        }
    }
}
