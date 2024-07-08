using System;
using System.Collections.Generic;
using _Scenes.User.telefono;
using Defective.JSON;
using Proyecto26;
using Script.test;
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
        
        private void Start()
        {
            joyStick.Enable(true);
            listeners = new Listeners(Info.DBUrl + Info.sessionCode + "/Game.json");
            listeners.Start(Parse);
            CaricaPlayer();
            
            // FirstLoadTask();
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
                
                //{"CodeTask":1, "":""}
                JSONObject json = new JSONObject(data.Split("data: ")[1]).GetField("data");
                Debug.Log(json);
                
                int codice = json.GetField("CodeTask").intValue;
                
                if (json.GetField("Player"))
                {
                    taskManager.Assegna(codice, json.GetField("Player").stringValue);
                }
                else
                {
                    taskManager.Assegna(codice);
                }

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
            userList.RemoveField(Info.localUser.name);
            if(!userList)
                return;
            foreach (JSONObject json in userList.list)
            {
                if(json.GetField("Virtual"))
                    return;
                string n = json.GetField("Name").stringValue;
                playerList.Add(n, Instantiate(onlinePlayer ,json.GetField("Coord").ToVector2(),new Quaternion(), parent));
                playerList[n].name = n;
                playerList[n].GetComponent<PlayerOnline>().Set(json);
            }
        }

        private void MuoviPlayer(JSONObject value)
        {
            String nome = value.GetField("path").stringValue.Split("/")[2];
            Vector2 newPos = value.GetField("data").ToVector2();
            playerList[nome].GetComponent<PlayerOnline>().Move(newPos);
        }

        // private Task GetTask(JSONObject value)
        // {
        //     if (value.GetField("data").isNull)
        //         return null;
        //     
        //     String id = value.GetField("path").stringValue.Split("/")[2];
        //     return value.GetField("data").ToTask(id);
        // }

        // private void GestisciTask(Task value)
        // {
        //     if (value.idRisposta == "")
        //     { //TODO aggiungere il controllo sull'utente e sul tipo si task
        //         taskList.Add(value.id,value);   
        //         // TODO esegui cose della task
        //         return;
        //     }
        //
        //     if (taskList.ContainsKey(value.idRisposta))
        //     {
        //         //task completata
        //         EliminaTask(value.idRisposta);
        //     }
        // }

        // private void FirstLoadTask()
        // {
        //     RestClient.Get(Info.DBUrl + Info.sessionCode + "/Game/Task.json").Then(e =>
        //     {
        //         JSONObject value = new JSONObject(e.Text);
        //         for(int i = 0; i < value.count; i++)
        //         {
        //             GestisciTask(value.list[i].ToTask(value.keys[i]));
        //         }
        //     });
        // }

        // private void EliminaTask(String value)
        // {
        //     taskList.Remove(value);
        //     RestClient.Delete(Info.DBUrl + Info.sessionCode + "/Game/Task/" + value + ".json");
        // }
        
        
        
    }
}
