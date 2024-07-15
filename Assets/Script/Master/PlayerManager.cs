using System;
using System.Collections.Generic;
using Defective.JSON;
using minigame.AllestimentoCRI;
using minigame.evacuaCittadini;
using minigame.incendio;
using minigame.Incidente;
using minigame.MaterialePericoloso;
using minigame.PuntiRaccolta;
using minigame.SalvaPersone;
using minigame.svuotaAcqua;
using minigame.TogliTane;
using minigame.TrovaDispersi;
using Proyecto26;
using Script.Utility;
using UnityEngine;
// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace Script.Master
{
    public class PlayerManager : MonoBehaviour
    {
        [Header("PLAYER")]
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject prefab;

        [Header("MINIGIOCHI")]
        // [SerializeField] private TrovaDispersi trovaDispersi;
        // [SerializeField] private TogliTane rimuoviTane;
        [SerializeField] private TogliAcqua togliAcqua;
        [SerializeField] private SpegniIncendio spegniIncendio;
        [SerializeField] private PuntiRaccolta puntiRaccolta;
        [SerializeField] private AllestimentoCRI allestimentoCri;
        [SerializeField] private TogliMonnezza togliMonnezza;
        // [SerializeField] private SalvaCose salvaPersoneAnimale;
        // [SerializeField] private EvacuaMain evacuaCittadini;
        [SerializeField] private Incidente incidente;
        
        
        
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

                //{"CodeTask":1, "":""}
                JSONObject json = new JSONObject(data.Split("data: ")[1]).GetField("data");
                Debug.Log(json);

                int codice = json.GetField("CodeTask").intValue;

                Assegna(codice);
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
            if(!userList)
                return;
            foreach (JSONObject json in userList.list)
            {
                if (!json.GetField("Virtual"))
                {
                    string n = json.GetField("Name").stringValue;
                    playerList.Add(n, Instantiate(prefab, parent));
                    playerList[n].name = n;
                    //playerList[n].GetComponent<PlayerOnline>().Set(json);
                }
                
            }
        }

        private void MuoviPlayer(JSONObject value)
        {
            String nome = value.GetField("path").stringValue.Split("/")[2];
            Vector2 newPos = value.GetField("data").ToVector2();

            newPos.x = newPos.x != 0 ? newPos.x : playerList[nome].transform.position.x;
            newPos.y = newPos.y != 0 ? newPos.y : playerList[nome].transform.position.y;
            
            playerList[nome].transform.position = newPos;
        }

        private void Assegna(int value)
        {
            if (value == 6)
            {
                NuovaNotifica("Informazioni da TLC");
                return;
            }
            if (value == 18000)
            {
                puntiRaccolta.Genera(false);
                return;
            }
            if (value == 30000)
            {
                allestimentoCri.Genera(false);
                return;
            }
            if (value == 16)
            {
                togliAcqua.Genera(false);
                return;
            }
            if (value == 16001)
            {
                togliAcqua.Rimuovi();
                return;
            }
            if (value == 57)
            {
                spegniIncendio.Genera(false);
                return;
            }
            if (value == 57001)
            {
                spegniIncendio.Rimuovi();
                return;
            }
            if (value == 27)
            {
                togliMonnezza.Genera(false);
                return;
            }
            if (value == 27001)
            {
                togliMonnezza.Rimuovi();
                return;
            }
            if (value == 47)
            {
                incidente.Genera(false);
                return;
            }
            if (value == 47001)
            {
                incidente.Rimuovi();
                return;
            }
        }
        private void NuovaNotifica(string testo)
        {
            // pallinoRosso.SetActive(true);
            // schede[^1].GetComponent<AddNotifica>().SetMessaggio(testo);
        }
    }
}
