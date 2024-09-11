using System.Collections.Generic;
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
using Script.User;
using Script.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Scene = Script.Utility.Scene;

// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace _Scenes.User.telefono
{
    public class TaskManager : MonoBehaviour
    {
        [Header("TELEFONO")]
        [SerializeField]private GameObject prefGenerico;
        [SerializeField]private GameObject prefCOC;
        [SerializeField]private GameObject prefSindaco;
        [SerializeField]private GameObject prefRefTlc;
        [SerializeField]private GameObject prefSegreteria;
        [SerializeField]private GameObject prefReferenti;
        [SerializeField]private GameObject prefNotifiche;
        [SerializeField]private GameObject pallinoRosso;
        [SerializeField]private GameObject playerRuoli;
        [SerializeField]private GameObject taskAssegna;
        [SerializeField]private GameObject taskSeleziona;
        [SerializeField]private GameObject mostraVolontari;
        [SerializeField]private GameObject assegnaVolontari;
        [SerializeField]private GameObject richiesteVolontari;
        [SerializeField]private GameObject plInfo;
        [SerializeField]private Transform parent;
        
        [Header("MINIGIOCHI")]
        [SerializeField] private TrovaDispersi trovaDispersi;
        [SerializeField] private TogliTane rimuoviTane;
        [SerializeField] private TogliAcqua togliAcqua;
        [SerializeField] private SpegniIncendio spegniIncendio;
        [SerializeField] private PuntiRaccolta puntiRaccolta;
        [SerializeField] private AllestimentoCRI allestimentoCri;
        [SerializeField] private TogliMonnezza togliMonnezza;
        [SerializeField] private SalvaCose salvaPersoneAnimale;
        [SerializeField] private EvacuaMain evacuaCittadini;
        [SerializeField] private Incidente incidente;
        [SerializeField] private PrimoSoccorso primoSoccorso;
        [SerializeField] private Battito battito;
        [SerializeField] private Percorsi percorsi;
        [SerializeField] private MonitoraArgini monitoraArgini;
        
        
        [Header("ALTRO")]
        [SerializeField] private PlayerLocal _playerLocal;
        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private GameObject darkBack;
        
        // [Header("MAPPA")]
        // [SerializeField]private Transform mappa;
        
        
        [Header("setRuolo")]
        [SerializeField]private Ruoli r;
                
        private List<GameObject> schede = new List<GameObject>();
        
       
        private void Start()
        {
            
            Inizializza();
        }
        
       

        [ContextMenu("setRuolo")]
        public void Hh()
        {
            Info.localUser.role = r;
            foreach (var var in schede)
            {
                DestroyImmediate(var);
            }
        
            schede = new List<GameObject>();
            Start();
        }
        
        private void Inizializza()
        {
            switch (Info.localUser.role)
            {
                case Ruoli.Sindaco:
                    schede.Add( Instantiate(prefSindaco, parent)); //0
                    schede.Add( Instantiate(richiesteVolontari, parent)); //1
                    break;
                case Ruoli.Coc:
                    schede.Add( Instantiate(prefCOC, parent)); //0
                    schede.Add( Instantiate(taskSeleziona, parent)); //1
                    schede.Add( Instantiate(taskAssegna, parent)); //2
                    schede.Add( Instantiate(richiesteVolontari, parent)); //3
                    break;
                case Ruoli.RefTlc:
                    schede.Add( Instantiate(prefRefTlc, parent)); //0
                    break;
                case Ruoli.Segreteria:
                    schede.Add( Instantiate(prefSegreteria, parent)); //0
                    schede.Add( Instantiate(richiesteVolontari, parent)); //1
                    break;
                case Ruoli.RefCri:
                case Ruoli.RefFuoco:
                case Ruoli.RefPC:
                case Ruoli.RefPolizia:
                case Ruoli.RefGgev:
                    schede.Add( Instantiate(prefReferenti, parent)); //0
                    schede.Add( Instantiate(mostraVolontari, parent)); //1
                    schede.Add( Instantiate(assegnaVolontari, parent)); //2
                    break;
                default:
                    schede.Add( Instantiate(prefGenerico, parent)); //0
                    break;
            }   
            schede.Add( Instantiate(plInfo, parent)); //[^3]
            schede.Add( Instantiate(playerRuoli, parent)); //[^2]
            schede.Add( Instantiate(prefNotifiche, parent)); //[^1]
            foreach (var var in schede)
            { 
                var.SetActive(false);
                var.GetComponentInChildren<Logica>().SetListaSchede(schede);
            }
            schede[0].GetComponentInChildren<Logica>().SetBackCallback(IndietroCall);
        }

        private void IndietroCall()
        {
            _playerLocal.canMove = true;
            mainCanvas.enabled = true;
            darkBack.SetActive(false);
        }
        
        public void OpenMessaggi()
        {
            OpenManager();
            schede[^1].SetActive(true);
        }
        
        public void OpenManager()
        { 
            _playerLocal.canMove = false;
            mainCanvas.enabled = false;
            darkBack.SetActive(true);
            schede[0].SetActive(true);
        }
        
        public void Assegna(int value, string plName = "")
        {
            if (value == -1)
            {
                SceneManager.LoadScene(Scene.User.EndGame);
                return;
            }
            if (value == 6)
            {
                NuovaNotifica("Informazioni dal referente Telecomunicazioni: " + plName);
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
            
            switch (Info.localUser.role)
            {
                case Ruoli.Sindaco:
                    switch (value)
                    {
                        case 1:
                            NuovaNotifica("È arrivata una nuova allerta! Convoca il Centro Operativo Comunale!");
                            schede[0].transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
                            break;
                        case 4:
                            NuovaNotifica("Informazioni dal referente Telecomunicazioni: "+plName);
                            schede[0].transform.GetChild(0).GetChild(3).GetComponent<Button>().interactable = true;
                            break;
                        case 70:
                            NuovaNotifica("Il COC ha richiesto che autorizzi la richiesta per ottenere più volontari PC");
                            schede[1].GetComponentInChildren<TaskSeleziona>().NuovaTask("Autorizza più PC", 70);
                            // altri volontari step 2 pc
                            break;
                        case 71:
                            NuovaNotifica("Il COC ha richiesto che autorizzi la richiesta per ottenere più volontari GGEV");
                            schede[1].GetComponentInChildren<TaskSeleziona>().NuovaTask("Autorizza più GGEV",71);
                            // altri volontari step 2 ggev
                            break;
                        case 72:
                            NuovaNotifica("Il COC ha richiesto che autorizzi la richiesta per ottenere più volontari CRI");
                            schede[1].GetComponentInChildren<TaskSeleziona>().NuovaTask("Autorizza più CRI",72);
                            // altri volontari step 2 cri
                            break;
                        case 73:
                            NuovaNotifica("Il COC ha richiesto che autorizzi la richiesta per ottenere più vigili");
                            schede[1].GetComponentInChildren<TaskSeleziona>().NuovaTask("Autorizza più vigili",73);
                            // altri volontari step 2 polizia
                            break;
                        case 74:
                            NuovaNotifica("Il COC ha richiesto che autorizzi la richiesta per ottenere più pompieri");
                            schede[1].GetComponentInChildren<TaskSeleziona>().NuovaTask("Autorizza più pompieri",74);
                            // altri volontari step 2 pompiere
                            break;
                        case 95:
                            NuovaNotifica("Un cittadino si rifiuta di evacuare, vai a convincerlo.");
                            evacuaCittadini.Genera(plName); //plname sarà il numero della casa da visitare
                            break;
                    }
                   break;
                case Ruoli.Coc:
                    switch (value)
                    {
                        case 2:
                            NuovaNotifica("Sei stato attivato dal sindaco! Distribuisci in modo corretto i vari incarichi");
                            TaskSeleziona tmp = schede[1].GetComponentInChildren<TaskSeleziona>();
                            tmp.NuovaTask("Monitora Argini",1110);
                            tmp.NuovaTask("Svuota zone alluvionate",1111);
                            tmp.NuovaTask("Evacuazione cittadini",1112);
                            tmp.NuovaTask("Crea punto di raccolta",1113);
                            tmp.NuovaTask("Cerca tane sull'argine",1120);
                            tmp.NuovaTask("Crea campo CRI",1130);
                            tmp.NuovaTask("Primo soccorso ferito",1131);
                            tmp.NuovaTask("Chiudi strade",1141);
                            tmp.NuovaTask("Salva persone/animali",1150);
                            //Lista di task da abilitare per il coc
                            break;
                        case 60:
                            NuovaNotifica("Il referente PC ha bisogno di più volontari");
                            schede[3].GetComponentInChildren<TaskSeleziona>().NuovaTask("Altri volontari PC",60);
                            // altri volontari step 1 pc
                            break;
                        case 61:
                            NuovaNotifica("Il referente GGEV ha bisogno di più volontari");
                            schede[3].GetComponentInChildren<TaskSeleziona>().NuovaTask("Altri volontari GGEV",61);
                            // altri volontari step 1 ggev
                            break;
                        case 62:
                            NuovaNotifica("Il referente CRI ha bisogno di più volontari");
                            schede[3].GetComponentInChildren<TaskSeleziona>().NuovaTask("Altri volontari CRI",62);
                            // altri volontari step 1 cri
                            break;
                        case 63:
                            NuovaNotifica("Il referente della polizia ha bisogno di più vigili");
                            schede[3].GetComponentInChildren<TaskSeleziona>().NuovaTask("Altri volontari vigili",63);
                            // altri volontari step 1 polizia
                            break;
                        case 64:
                            NuovaNotifica("Il referente dei pompieri ha bisogno di più volontari");
                            schede[3].GetComponentInChildren<TaskSeleziona>().NuovaTask("Altri volontari pompieri",64);
                            // altri volontari step 1 pompiere
                            break;
                        case 75:
                            NuovaNotifica("Il sindaco ha approvato la richiesta di volontari PC, ora mandala alla segreteria provinciale");
                            schede[3].GetComponentInChildren<TaskSeleziona>().NuovaTask("Inoltra richiesta PC",75);
                            // altri volontari step 3 pc
                            break;
                        case 76:
                            NuovaNotifica("Il sindaco ha approvato la richiesta di volontari GGEV, ora mandala alla segreteria provinciale");
                            schede[3].GetComponentInChildren<TaskSeleziona>().NuovaTask("Inoltra richiesta GGEV",76);
                            // altri volontari step 3 ggev
                            break;
                        case 77:
                            NuovaNotifica("Il sindaco ha approvato la richiesta di volontari CRI, ora mandala alla segreteria provinciale");
                            schede[3].GetComponentInChildren<TaskSeleziona>().NuovaTask("Inoltra richiesta CRI",77);
                            // altri volontari step 3 cri
                            break;
                        case 78:
                            NuovaNotifica("Il sindaco ha approvato la richiesta di vigili, ora mandala alla segreteria provinciale");
                            schede[3].GetComponentInChildren<TaskSeleziona>().NuovaTask("Inoltra richiesta vigili",78);
                            // altri volontari step 3 polizia
                            break;
                        case 79:
                            NuovaNotifica("Il sindaco ha approvato la richiesta di pompieri, ora mandala alla segreteria provinciale");
                            schede[3].GetComponentInChildren<TaskSeleziona>().NuovaTask("Inoltra richiesta pompieri",79);
                            // altri volontari step 3 pompiere
                            break;
                        case 6:
                            schede[0].transform.GetChild(0).GetChild(2).GetComponent<Button>().interactable = true;
                            break;
                    }
                    break;
                case Ruoli.Medico:
                    switch (value)
                    {
                        case 38:
                            //svenuto
                            NuovaNotifica("Qualcuno è svenuto, trovalo e aiutalo");
                            battito.Genera();
                            break;
                    }
                    break;
                case Ruoli.RefCri:
                    switch (value)
                    {
                        case 30:
                            NuovaNotifica("Il COC ha richiesto l'allestimento di un ambiente di prime cure");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Allestimento ambienti CRI", 30);
                            break;  
                        case 31:
                            NuovaNotifica("Il COC ha richiesto operazioni di primo soccorso per i feriti");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Operazioni soccorso", 31);
                            break;
                        case 67:
                            NuovaNotifica("Richiesta volontari annullata");
                            break;
                        case 87:
                            NuovaNotifica("Volontari ottenuti");
                            break;
                        case 1035:
                            NuovaNotifica("È richiesto l'allestimento di un ambiente di prime cure");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Allestimento ambienti CRI", 1035);
                            break;
                        case 1036:
                            NuovaNotifica("È richiesto un interevento di primo soccorso per un ferito");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Primo soccorso", 1036);
                            break;
                        case 1038:
                            NuovaNotifica("È richiesto un interevento di primo soccorso");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Soccorri", 1038);
                            break;
                        case 202:
                            NuovaNotifica(plName + " ha terminato il task");
                            break;
                    }
                    break;
                case Ruoli.RefGgev:
                    switch (value)
                    {
                        case 20:
                            NuovaNotifica("Il COC ha richiesto la rimozione di tutte le tane degli animali");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Rimuovi tane", 20);
                            break;
                        case 66:
                            NuovaNotifica("Richiesta volontari annullata");
                            break;
                        case 86:
                            NuovaNotifica("Volontari ottenuti");
                            break;
                        case 1025:
                            NuovaNotifica("È richiesta la rimozione di tutte le tane degli animali");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Rimuovi tane", 1025);
                            break;
                        case 1027:
                            NuovaNotifica("È richiesta la rimozione di materiale pericoloso");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Rimuove materiale pericoloso", 1027);
                            break;
                        case 201:
                            NuovaNotifica(plName + " ha terminato il task");
                            break;
                    }
                    break;
                case Ruoli.RefPC:
                    switch (value)
                    {
                        case 10:
                            NuovaNotifica("Il COC ha richiesto di monitorare gli argini per non farli straripare");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Monitora argini", 10);
                            break;
                        case 11:
                            NuovaNotifica("Il COC ha richiesto di svuotare la zona alluvionata mettendo le persone in salvo");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Svuota zone alluvionate", 11);
                            break;
                        case 12:
                            NuovaNotifica("Il COC ha richiesto l'evacuazione immediata di tutti i cittadini");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Evacuazione cittadini", 12);
                            break;
                        case 13:
                            NuovaNotifica("Il COC ha richiesto l'allestimento di nuovi punti di raccolta");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Crea punti raccolta", 13);
                            break;
                        case 65:
                            NuovaNotifica("Richiesta volontari annullata");
                            break;
                        case 85:
                            NuovaNotifica("Volontari ottenuti");
                            break;
                        case 1015:
                            NuovaNotifica("È richiesto di monitorare gli argini per non farli straripare");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Monitora argini", 1015);
                            break;
                        case 1016:
                            NuovaNotifica("È richiesto di svuotare la zona alluvionata");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Svuota zone alluvionate", 1016);
                            break;
                        case 1017:
                            NuovaNotifica("È richiesta l'evacuazione immediata di tutti i cittadini");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Evacuazione cittadini", 1017);
                            break;
                        case 1018:
                            NuovaNotifica("È richieto l'allestimento di un punto di raccolta");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Crea punto raccolta", 1018);
                            break;
                        case 200:
                            NuovaNotifica(plName + " ha terminato il task");
                            break;
                    }
                    break;
                case Ruoli.RefPolizia:
                    switch (value)
                    {
                        case 40:
                            NuovaNotifica("Ci sono strade chiuse! Il COC ha richiesto la regolazione del traffico");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Regola traffico", 40);
                            break;
                        case 41:
                            NuovaNotifica("Il COC ha richiesto il tuo intervento! Crea percorsi alternativi");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Percorsi secondari",41);
                            break;
                        case 68:
                            NuovaNotifica("Richiesta volontari annullata");
                            break;
                        case 88:
                            NuovaNotifica("Volontari ottenuti");
                            break;
                        case 1046:
                            NuovaNotifica("È richiesto l'intervento per mettere in sicurezza alcuni tratti di strada");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Modifica le strade", 1046);
                            break;
                        case 1047:
                            NuovaNotifica("È richiesto l'intervento per risolvere un incidente");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Incidente", 1047);
                            break;
                        case 203:
                            NuovaNotifica(plName + " ha terminato il task");
                            break;
                    }
                    break;
                case Ruoli.RefFuoco:
                    switch (value)
                    {
                        case 50:
                            NuovaNotifica("Il COC ha richiesto urgentemente il tuo aiuto per salvare animali e persone");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Salva animali/persone", 50);
                            break;
                        case 69:
                            NuovaNotifica("Richiesta volontari annullata");
                            break;
                        case 89:
                            NuovaNotifica("Volontari ottenuti");
                            break;
                        case 1055:
                            NuovaNotifica("È richiesto il tuo aiuto per salvare animali e persone");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Salva animali/persone", 1055);
                            break;
                        case 1056:
                            NuovaNotifica("È richiesto il tuo aiuto per cercare i dispersi");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Ricerca dispersi", 1056);
                            break;
                        case 1057:
                            NuovaNotifica("È richiesto il tuo aiuto per spegnere un incendio");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask("Spegni Incendio", 1057);
                            break;
                        case 204:
                            NuovaNotifica(plName + " ha terminato il task");
                            break;
                    }
                    break;
                case Ruoli.Segreteria:
                    switch (value)
                    {
                        case 80:
                            NuovaNotifica("Richiesta di nuovi volontari per la Protezione Civile");
                            schede[1].GetComponentInChildren<TaskSeleziona>().NuovaTask("Richiesta PC", 80);
                            break;
                        case 81:
                            NuovaNotifica("Richiesta di nuovi volontari per le Guardie Ecologiche");
                            schede[1].GetComponentInChildren<TaskSeleziona>().NuovaTask("Richiesta GGEV",81);
                            break;
                        case 82:
                            NuovaNotifica("Richiesta di nuovi volontari per la croce rossa");
                            schede[1].GetComponentInChildren<TaskSeleziona>().NuovaTask("Richiesta CRI",82);
                            break;
                        case 83:
                            NuovaNotifica("Richiesta di nuovi vigili");
                            schede[1].GetComponentInChildren<TaskSeleziona>().NuovaTask("Richiesta vigili",83);
                            break;
                        case 84:
                            NuovaNotifica("Richiesta di nuovi vigili del fuoco");
                            schede[1].GetComponentInChildren<TaskSeleziona>().NuovaTask("Richiesta pompieri",84);
                            break;
                    }
                    break;
                case Ruoli.RefTlc:
                    switch (value)
                    {
                        case 3:
                            NuovaNotifica("Il sindaco ti ha chiesto delle informazioni");
                            schede[0].transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
                            break;
                        case 5:
                            NuovaNotifica("Il COC ti ha chiesto di informare la popolazione");
                            schede[0].transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
                            break;
                    }
                    break;
                case Ruoli.VolCri:
                    switch (value)
                    {
                        case 35:
                            if(Info.localUser.name != plName)
                                return;
                            NuovaNotifica("Trova la zona e costruisci un ambiente per fornire le cure mediche durante l'emergenza");
                            allestimentoCri.Genera(true);
                            break;
                        case 36:
                            if(Info.localUser.name != plName)
                                return;
                            NuovaNotifica("Trova la persona che ha bisogno di primo soccorso");
                            primoSoccorso.Genera(true);
                            break;
                    }
                    break;
                case Ruoli.VolPC:
                    switch (value)
                    {
                        case 15:
                            if(Info.localUser.name != plName)
                                return;
                            NuovaNotifica("Vai sugli argini a controllare i punti indicati");
                            monitoraArgini.Genera();
                            break;
                        case 16:
                            if(Info.localUser.name != plName)
                                return;
                            NuovaNotifica("Cerca e svuota la zona allagata");
                            togliAcqua.Genera(true);
                            break;
                        case 17:
                            if(Info.localUser.name != plName)
                                return;
                            NuovaNotifica("Vai in tutte le case ad avvisare che è neccessaria l'evacuazione.");
                            evacuaCittadini.Genera();
                            break;
                        case 18:
                            if(Info.localUser.name != plName)
                                return;
                            NuovaNotifica("Trova la zona e costruisci un ambiente sicuro come punto di raccolta per i cittadini");
                            puntiRaccolta.Genera(true);
                            break;
                    }
                    break;
                case Ruoli.VolGgev:
                    switch (value)
                    {
                        case 25:
                            if(Info.localUser.name != plName)
                                return;
                            NuovaNotifica("Cerca le tane sull'argine e chiudile");
                            rimuoviTane.GeneraTane();
                            break;
                        case 27:
                            
                            if(Info.localUser.name != plName)
                                return;
                            NuovaNotifica("Cerca i materiali inquinanti sparsi per la mappa");
                            togliMonnezza.Genera(true);
                            break;
                    }
                    break;
                case Ruoli.VolPolizia:
                    switch (value)
                    {
                        case 46:
                            if(Info.localUser.name != plName)
                                return;
                            percorsi.Genera(1);
                            NuovaNotifica("Ci sono delle parti della strada allagate, vai a segnalarle circondandole di coni. Per piazzare i coni clicca intorno alla zona da segnalare");
                            break;
                        case 47:
                            if(Info.localUser.name != plName)
                                return;
                            NuovaNotifica("C'è un incidente, trovalo e riapri la strada il prima possibile");
                            incidente.Genera(true);
                            break;
                    }
                    break;
                case Ruoli.VolFuoco:
                    switch (value)
                    {
                        case 55:
                            if(Info.localUser.name != plName)
                                return;
                            NuovaNotifica("Ci sono delle persone o degli animali in pericolo. Cercali per la mappa e cliccali per salvarli");
                            salvaPersoneAnimale.Genera();
                            break;
                        case 56:
                            if(Info.localUser.name != plName)
                                return;
                            NuovaNotifica("Ci sono delle persone disperse. Cercale per la mappa e cliccale per trovarle.");
                            trovaDispersi.GeneraDaTrovare();
                            break;
                        case 57:
                            if(Info.localUser.name != plName)
                                return;
                            NuovaNotifica("Una casa ha preso fuoco. Trovala e spegni l'incedio.");
                            spegniIncendio.Genera(true);
                            break;
                    }
                    break;
                default:
                    Debug.Log("cose");
                    break;
            }
        }
        public void NuovaNotifica(string testo)
        {
            pallinoRosso.SetActive(true);
            schede[^1].GetComponent<AddNotifica>().SetMessaggio(testo);
        }
    }
}
