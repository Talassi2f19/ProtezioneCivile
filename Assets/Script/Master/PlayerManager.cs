using System;
using System.Collections;
using System.Collections.Generic;
using Defective.JSON;
using FirebaseListener;
using minigame.AllestimentoCRI;
using minigame.Battito;
using minigame.evacuaCittadini;
using minigame.incendio;
using minigame.Incidente;
using minigame.MaterialePericoloso;
using minigame.MonitoraArgini;
using minigame.Percorsi;
using minigame.PrimoSoccorso;
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
        [SerializeField]private NotificaManager notificaManager;
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
        [SerializeField] private PrimoSoccorso primoSoccorso;
        // [SerializeField] private Battito battito;
        [SerializeField] private Percorsi percorsi;
        // [SerializeField] private MonitoraArgini monitoraArgini;
        
        
        
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
            // data: {"path":"/Task/-NxsMbI_dagLXyhk5LhS","data":{"CodTask":0,}}
            
            // event: put
            // data: {"path":"/Task/-NxsMbI_dagLXyhk5LhS","data":{"CodTask":0,"Player":""}}
            
            // event: put
            // data: {"path":"/Task/-NxsMbI_dagLXyhk5LhS","data":null}
            
            
            if (data.StartsWith("event: patch\ndata: {\"path\":\"/Posizione"))
            {
                MuoviPlayer(new JSONObject(data.Split("data: ")[1]));
                return;
            }

            if (data.StartsWith("event: put\ndata: {\"path\":\"/Task"))
            {
                // event: put
                // data: {"path":"/Task/-NyblKDKNWqsCpdQe5Pq","data":{"CodeTask":1}}

                //{"CodeTask":1, "":""}
                JSONObject json = new JSONObject(data.Split("data: ")[1]).GetField("data");
                // Debug.Log(json);

                int codice = json.GetField("CodeTask").intValue;

                
                if (json.GetField("Player"))
                {
                    if (json.GetField("Player").stringValue.Contains("Computer"))
                    {
                        StartCoroutine(NPCtimer(json.GetField("Player").stringValue, codice));
                    }
                    else
                    {
                        Assegna(codice, json.GetField("Player").stringValue);
                    }
                }
                else
                {
                    Assegna(codice);
                }
            }
        }

         private IEnumerator NPCtimer(string name, int codice)
         {
             yield return new WaitForSeconds(120f);
             codice = ConvertiCodice(codice);
             RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":" + codice + "}").Catch(Debug.LogError);
             RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + name + ".json", "{\"Occupato\":false}"); 
             RestClient.Get(Info.DBUrl + Info.sessionCode + "/score.json").Then(e =>
             {
                 RestClient.Patch(Info.DBUrl + Info.sessionCode + ".json", "{\"score\":" + (int.Parse(e.Text == "null" ? "0" : e.Text ) + Info.PointForGame/2) + "}").Catch(Debug.LogError);
             }).Catch(Debug.LogError);
         }

         private int ConvertiCodice(int code)
         {
             switch (code)
             {
                case 16:
                    return 16001;
                case 57:
                    return 57001;
                case 18:
                    return 18000;
                case 35:
                    return 30000;
                case 27:
                    return 27001;
                case 47:
                    return 47001;
                case 36:
                    return 36001;
                case 46:
                    return 46001;
             }
             return 0;
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
                    Vector2 coord = json.GetField("Coord") ? json.GetField("Coord").ToVector2() : Vector2.zero;
                    GameObject tmp = Instantiate(prefab, coord, new Quaternion(), parent);
                    string ruolo = json.GetField("Role").stringValue;
                    tmp.GetComponent<SpriteRenderer>().color = GetColor(Enum.Parse<Ruoli>(ruolo));
                    playerList.Add(n, tmp);
                    playerList[n].name = n;
                 }
             }
         }

         private Color GetColor(Ruoli ruolo)
         {
             Color tmp;
             switch (ruolo)
             {
                 case Ruoli.Coc:
                     tmp = new Color(254/ 255f, 235/ 255f, 52/ 255f);
                     break;
                 case Ruoli.RefCri:
                 case Ruoli.VolCri:
                     tmp = new Color(102/ 255f, 11/ 255f, 28/ 255f);
                     break;
                 case Ruoli.RefGgev:
                 case Ruoli.VolGgev:
                     tmp = new Color(0/ 255f, 150/ 255f, 17/ 255f);
                     break;
                 case Ruoli.RefPC:
                 case Ruoli.VolPC:
                     tmp = new Color(0/ 255f, 71/ 255f, 85/ 255f);
                     break;
                 case Ruoli.RefPolizia:
                 case Ruoli.VolPolizia:
                     tmp = new Color(21/ 255f, 53/ 255f, 148/ 255f);
                     break;
                 case Ruoli.VolFuoco:
                 case Ruoli.RefFuoco:
                     tmp = new Color(222/ 255f, 24/ 255f, 24/ 255f);
                     break;
                 case Ruoli.Sindaco:
                     tmp = new Color(65/ 255f, 105/ 255f, 225/ 255f);
                     break;
                 case Ruoli.Medico:
                     tmp = new Color(140/ 255f, 194/ 255f, 230/ 255f);
                     break;
                 case Ruoli.Segreteria:
                     tmp = new Color(200/ 255f, 156/ 255f, 196/ 255f);
                     break;
                 case Ruoli.RefTlc:
                     tmp = new Color(120/ 255f, 105/ 255f, 70/ 255f);
                     break;
                 default:
                     tmp = new Color(0,0,0);
                     break;
             }
             return tmp;
         }
         private void MuoviPlayer(JSONObject value)
         {
             String nome = value.GetField("path").stringValue.Split("/")[2];
             Vector2 newPos = value.GetField("data").ToVector2();
             newPos.x = newPos.x!=0 ? newPos.x : playerList[nome].transform.position.x ;
             newPos.y = newPos.y!=0 ? newPos.y : playerList[nome].transform.position.y ;
             playerList[nome].transform.position = newPos;
         }

        private void Assegna(int value, string info = "")
        {
            if (value == 6)
            {
                NuovaNotifica("Informazioni da referente telecomunicazioni: " + info);
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
            }
            if (value == 16001)
            {
                togliAcqua.Rimuovi();
                return;
            }
            if (value == 57)
            {
                spegniIncendio.Genera(false);
            }
            if (value == 57001)
            {
                spegniIncendio.Rimuovi();
                return;
            }
            if (value == 27)
            {
                togliMonnezza.Genera(false);
            }
            if (value == 27001)
            {
                togliMonnezza.Rimuovi();
                return;
            }
            if (value == 47)
            {
                incidente.Genera(false);
            }
            if (value == 47001)
            {
                incidente.Rimuovi();
                return;
            }
            if (value == 36)
            {
                primoSoccorso.Genera(false);
            }
            if (value == 36001)
            {
                primoSoccorso.Rimuovi();
                return;
            }
            if (value == 46)
            {
                percorsi.Genera(0);
            }
            if (value == 46000)
            {
                percorsi.Genera(2);
                return;
            }
            
            switch (value)
            {
                case 1:
                    NuovaNotifica("È arrivata una nuova allerta! Convoca il Centro Operativo Comunale!");
                    break;
                case 4:
                    NuovaNotifica("Informazioni da referente telecomunicazioni: " + info);
                    break;
                case 70:
                    NuovaNotifica("Il COC ha richiesto che autorizzi la richiesta per ottenere più volontari PC");
                    break;
                case 71:
                    NuovaNotifica("Il COC ha richiesto che autorizzi la richiesta per ottenere più volontari GGEV");
                    break;
                case 72:
                    NuovaNotifica("Il COC ha richiesto che autorizzi la richiesta per ottenere più volontari CRI");
                    break;
                case 73:
                    NuovaNotifica("Il COC ha richiesto che autorizzi la richiesta per ottenere più vigili");
                    break;
                case 74:
                    NuovaNotifica("Il COC ha richiesto che autorizzi la richiesta per ottenere più pompieri");
                    break;
                case 95:
                    NuovaNotifica("Un cittadino si rifiuta di evacuare, vallo a convincere.");
                    break;
                case 2:
                    NuovaNotifica("Sei stato attivato dal sindaco! Distribuisci in modo corretto i vari incarichi");
                    break;
                case 60:
                    NuovaNotifica("Il referente PC ha bisogno di più volontari");
                    break;
                case 61:
                    NuovaNotifica("Il referente GGEV ha bisogno di più volontari");
                    break;
                case 62:
                    NuovaNotifica("Il referente CRI ha bisogno di più volontari");
                    break;
                case 63:
                    NuovaNotifica("Il referente della polizia ha bisogno di più poliziotti");
                    break;
                case 64:
                    NuovaNotifica("Il referente dei pompieri ha bisogno di più volontari");
                    break;
                case 75:
                    NuovaNotifica("Il sindaco ha approvato la richiesta di volontari PC, ora mandala alla segreteria provinciale");
                    break;
                case 76:
                    NuovaNotifica("Il sindaco ha approvato la richiesta di volontari GGEV, ora mandala alla segreteria provinciale");
                    break;
                case 77:
                    NuovaNotifica("Il sindaco ha approvato la richiesta di volontari CRI, ora mandala alla segreteria provinciale");
                    break;
                case 78:
                    NuovaNotifica("Il sindaco ha approvato la richiesta di polizia, ora mandala alla segreteria provinciale");
                    break;
                case 79:
                    NuovaNotifica("Il sindaco ha approvato la richiesta di pompieri, ora mandala alla segreteria provinciale");
                    break;
                case 38:
                    NuovaNotifica("Qualcuno è svenuto trovalo e aiutalo");
                    break;
                case 30:
                    NuovaNotifica("Il COC ha richiesto l'allestimento di ambienti di prime cure");
                    break;
                case 31:
                    NuovaNotifica("Il COC ha richiesto operazioni di primo soccorso per i feriti");
                    break;
                case 67:
                    NuovaNotifica("Richiesta volontari annullata");
                    break;
                case 87:
                    NuovaNotifica("Volontari ottenuti");
                    break;
                case 1035:
                    NuovaNotifica("Allestimento ambienti CRI");
                    break;
                case 1036:
                    NuovaNotifica("Primo soccorso");
                    break;
                case 1038:
                    NuovaNotifica("soccorri");
                    break;
                case 20:
                    NuovaNotifica("Il COC ha richiesto la rimozione di tutte le tane degli animali");
                    break;
                case 66:
                    NuovaNotifica("Richiesta volontari annullata");
                    break;
                case 86:
                    NuovaNotifica("Volontari ottenuti");
                    break;
                case 1025:
                    NuovaNotifica("Trova le tane sull'argine e chiudile");
                    break;
                case 1027:
                    NuovaNotifica("Rimuove materiale pericoloso\n");
                    break;
                case 10:
                    NuovaNotifica("Il COC ha richiesto di monitorare gli argini per non farli straripare");
                    break;
                case 11:
                    NuovaNotifica("Il COC ha richiesto di svuotare tutte le zone alluvionate mettendo le persone in salvo");
                    break;
                case 12:
                    NuovaNotifica("Il COC ha richiesto l'evacuazione immediata di tutti i cittadini");
                    break;
                case 13:
                    NuovaNotifica("Il COC ha richiesto la creazione di nuovi punti di raccolta");
                    break;
                case 65:
                    NuovaNotifica("Richiesta volontari annullata");
                    break;
                case 85:
                    NuovaNotifica("Volontari ottenuti");
                    break;
                case 1015:
                    NuovaNotifica("Monitora argini");
                    break;
                case 1016:
                    NuovaNotifica("Svuota zone alluvione");
                    break;
                case 1017:
                    NuovaNotifica("Evacuazione persone");
                    break;
                case 1018:
                    NuovaNotifica("Crea punti raccolta");
                    break;
                case 40:
                    NuovaNotifica("Ci sono strade chiuse! Il COC ha richiesto la regolazione del traffico");
                    break;
                case 41:
                    NuovaNotifica("Il COC ha richiesto il tuo intervento! Crea percorsi alternativi");
                    break;
                case 68:
                    NuovaNotifica("Richiesta volontari annullata");
                    break;
                case 88:
                    NuovaNotifica("Volontari ottenuti");
                    break;
                case 1046:
                    NuovaNotifica("modifica le strade");
                    break;
                case 1047:
                    NuovaNotifica("Incidente");
                    break;
                case 50:
                    NuovaNotifica("Il COC ha richiesto urgentemente il tuo aiuto per salvare animale e persone");
                    break;
                case 69:
                    NuovaNotifica("Richiesta volontari annullata");
                    break;
                case 89:
                    NuovaNotifica("Volontari ottenuti");
                    break;
                case 1055:
                    NuovaNotifica("Salva animali/persone");
                    break;
                case 1056:
                    NuovaNotifica("Ricerca dispersi");
                    break;
                case 1057:
                    NuovaNotifica("Spegni Incendio");
                    break;
                case 80:
                    NuovaNotifica("Richiesta di volontari PC");
                    break;
                case 81:
                    NuovaNotifica("Richiesta di volontari GGEV");
                    break;
                case 82:
                    NuovaNotifica("Richiesta di volontari CRI");
                    break;
                case 83:
                    NuovaNotifica("Richiesta di polizia");
                    break;
                case 84:
                    NuovaNotifica("Richiesta di vigili del fuoco");
                    break;
                case 3:
                    NuovaNotifica("Il sindaco ti ha chiesto delle informazioni");
                    break;
                case 5:
                    NuovaNotifica("Il COC ti ha chiesto di informare la popolazione");
                    break;
                case 35:
                    NuovaNotifica("Trova la zona e costruisci un ambiete per fornire le cure mediche durante l'emergenza");
                    break;
                case 36:
                    NuovaNotifica("Trova la persona che ha bisogno di un primo soccorso");
                    break;
                case 15:
                    NuovaNotifica("vai sugli argini a controllare nei punti indicati");
                    break;
                case 16:
                    NuovaNotifica("Cerca e svuolta la zona allagata");
                    break;
                case 17:
                    NuovaNotifica("Vai in tutte le case ad avvisare che è neccessaria l'evacuazione.");
                    break;
                case 18:
                    NuovaNotifica("Trova la zona e contruisci un ambiente sicuro come punto di raccolta per i cittadini");
                    break;
                case 25:
                    NuovaNotifica("Cerca le tane sull'argine e chiudile");
                    break;
                case 27:
                    NuovaNotifica("Cerca i materiali inquinanti sparsi per la mappa");
                    break;
                case 46:
                    NuovaNotifica("Ci sono delle parti della strada allagate, vai a segnalarle circondandole di coni. Per piazzare i coni clicca intorno alla zona da segnalare");
                    break;
                case 47:
                    NuovaNotifica("C'è un incidente, trovalo e riapri la strada il prima possibile");
                    break;
                case 55:
                    NuovaNotifica("Ci sono delle persone o degli animali in pericolo. Cercali per la mappa e cliccali per salvarli");
                    break;
                case 56:
                    NuovaNotifica("Ci sono dei dispersi, cercali.");
                    break;
                case 57:
                    NuovaNotifica("Incendio in corso, spegnilo.");
                    break;
                case 200:
                    NuovaNotifica(info + " ha terminato la task");
                    break;
                case 201:
                    NuovaNotifica(info + " ha terminato la task");
                    break;
                case 202:
                    NuovaNotifica(info + " ha terminato la task");
                    break;
                case 203:
                    NuovaNotifica(info + " ha terminato la task");
                    break;
                case 204:
                    NuovaNotifica(info + " ha terminato la task");
                    break;
            }
        }
        private void NuovaNotifica(string testo)
        {
            notificaManager.AggiungiMessaggi(testo);
        }
    }
}
