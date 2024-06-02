using System;
using System.Collections.Generic;
using Defective.JSON;
using Proyecto26;

using Script.Utility;
using UnityEngine;
// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace Script.Master
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject prefab;

        private Listeners listeners;
        private Dictionary<string, GameObject> playerList = new Dictionary<string, GameObject>();
        private void Start()
        {
            listeners = new Listeners(Info.DBUrl + Info.sessionCode + "/Game.json");
            listeners.Start(Parse);
            CaricaPlayer();
        }

        private void OnApplicationQuit()
        {
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
                
                // Debug.Log("hey");
                // string kk = data.Split("\"CodeTask\":")[1].Split("}")[0];
                //
                // Debug.Log("è passato");
                // int codice = Convert.ToInt32(kk);
                // taskManager.Assegna(codice);

                // data = data.Split("data: ")[1];
                // Task tmp = GetTask(new JSONObject(data));
                // if (tmp == null)
                // {
                //     taskList.Remove(new JSONObject(data).GetField("path").stringValue.Split("/")[2]);
                //     return;
                // }
                // GestisciTask(tmp);
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
                });
            });
        }

        private void CaricaPlayer2(JSONObject userList)
        {
            Dictionary<String, GenericUser> userDictionary = userList.ToUserDictionary();
            foreach (var tmp in userDictionary)
            {
                Debug.Log(tmp.Key + "--"+tmp.Value);
                playerList.Add(tmp.Key, Instantiate(prefab ,tmp.Value.coord,new Quaternion(), parent));
                playerList[tmp.Key].name = tmp.Key;
                // playerList[tmp.Key].GetComponent<>().Set();
            }
        }

        private void MuoviPlayer(JSONObject value)
        {
            String nome = value.GetField("path").stringValue.Split("/")[2];
            Vector2 newPos = value.GetField("data").ToVector2();
            Debug.Log(newPos);

            newPos.x = newPos.x != 0 ? newPos.x : playerList[nome].transform.position.x;
            newPos.y = newPos.y != 0 ? newPos.y : playerList[nome].transform.position.y;
            
            playerList[nome].transform.position = newPos;
        }
    }
}
