using System.Collections.Generic;
using minigame.AllestimentoCRI;
using minigame.evacuaCittadini;
using minigame.incendio;
using minigame.MaterialePericoloso;
using minigame.PuntiRaccolta;
using minigame.SalvaPersone;
using minigame.svuotaAcqua;
using minigame.TogliTane;
using minigame.TrovaDispersi;
using Script.User;
using Script.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
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
        
        
        
        // [Header("MAPPA")]
        // [SerializeField]private Transform mappa;
        
        
        [Header("setRuolo")]
        [SerializeField]private Ruoli r;
                
        private List<GameObject> schede = new List<GameObject>();
        
       
        private void Start()
        {
            Inizializza();
            Testo.Load();
            Debug.Log(Testo.testi[1]);
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
                var.GetComponentInChildren<Logica>().SetListaSchede(schede);
            }
            
        }

        public void OpenMessaggi()
        {
            OpenManager();
            schede[^1].SetActive(true);
        }
        
        public void OpenManager()
        {
         schede[0].SetActive(true);
        }
        
        public void Assegna(int value, string plName = "")
        {
            if (value == -1)
            {
                //termina partita
                SceneManager.LoadScene(Scene.User.EndGame);
                return;
            }
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
            if (value == 16000)
            {
                togliAcqua.Genera(false);
                return;
            }
            if (value == 16001)
            {
                togliAcqua.Rimuovi();
                return;
            }
            if (value == 57000)
            {
                spegniIncendio.Genera(false);
                return;
            }
            if (value == 57001)
            {
                spegniIncendio.Rimuovi();
                return;
            }
            if (value == 27000)
            {
                togliMonnezza.Genera(false);
                return;
            }
            if (value == 27001)
            {
                togliMonnezza.Rimuovi();
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
                            NuovaNotifica("Informazioni dal referente telecomunicazioni");
                            break;
                        case 70:
                            NuovaNotifica("Il COC ha richiesto che autorizzi la richiesta per ottenere più volontari PC");
                            schede[1].GetComponentInChildren<TaskSeleziona>().NuovaTask(70);
                            // altri volontari step 2 pc
                            break;
                        case 71:
                            NuovaNotifica("Il COC ha richiesto che autorizzi la richiesta per ottenere più volontari GGEV");
                            schede[1].GetComponentInChildren<TaskSeleziona>().NuovaTask(71);
                            // altri volontari step 2 ggev
                            break;
                        case 72:
                            NuovaNotifica("Il COC ha richiesto che autorizzi la richiesta per ottenere più volontari CRI");
                            schede[1].GetComponentInChildren<TaskSeleziona>().NuovaTask(72);
                            // altri volontari step 2 cri
                            break;
                        case 73:
                            NuovaNotifica("Il COC ha richiesto che autorizzi la richiesta per ottenere più vigili");
                            schede[1].GetComponentInChildren<TaskSeleziona>().NuovaTask(73);
                            // altri volontari step 2 polizia
                            break;
                        case 74:
                            NuovaNotifica("Il COC ha richiesto che autorizzi la richiesta per ottenere più pompieri");
                            schede[1].GetComponentInChildren<TaskSeleziona>().NuovaTask(74);
                            // altri volontari step 2 pompiere
                            break;
                        case 95:
                            NuovaNotifica("Un cittadino si rifiuta di evacuare, vallo a convincere.");
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
                            tmp.NuovaTask(1110);
                            tmp.NuovaTask(1111);
                            tmp.NuovaTask(1112);
                            tmp.NuovaTask(1113);
                            tmp.NuovaTask(1120);
                            tmp.NuovaTask(1130);
                            tmp.NuovaTask(1131);
                            tmp.NuovaTask(1140);
                            tmp.NuovaTask(1141);
                            tmp.NuovaTask(1150);
                            //Lista di task da abilitare per il coc
                            break;
                        case 60:
                            NuovaNotifica("Il referente PC ha bisogno di più volontari");
                            schede[3].GetComponentInChildren<TaskSeleziona>().NuovaTask(60);
                            // altri volontari step 1 pc
                            break;
                        case 61:
                            NuovaNotifica("Il referente GGEV ha bisogno di più volontari");
                            schede[3].GetComponentInChildren<TaskSeleziona>().NuovaTask(61);
                            // altri volontari step 1 ggev
                            break;
                        case 62:
                            NuovaNotifica("Il referente CRI ha bisogno di più volontari");
                            schede[3].GetComponentInChildren<TaskSeleziona>().NuovaTask(62);
                            // altri volontari step 1 cri
                            break;
                        case 63:
                            NuovaNotifica("Il referente della polizia ha bisogno di più poliziotti");
                            schede[3].GetComponentInChildren<TaskSeleziona>().NuovaTask(63);
                            // altri volontari step 1 polizia
                            break;
                        case 64:
                            NuovaNotifica("Il referente dei pompieri ha bisogno di più volontari");
                            schede[3].GetComponentInChildren<TaskSeleziona>().NuovaTask(64);
                            // altri volontari step 1 pompiere
                            break;
                        case 75:
                            NuovaNotifica("Il sindaco ha approvato la richiesta di volontari PC, ora mandala alla segreteria provinciale");
                            schede[3].GetComponentInChildren<TaskSeleziona>().NuovaTask(75);
                            // altri volontari step 3 pc
                            break;
                        case 76:
                            NuovaNotifica("Il sindaco ha approvato la richiesta di volontari GGEV, ora mandala alla segreteria provinciale");
                            schede[3].GetComponentInChildren<TaskSeleziona>().NuovaTask(76);
                            // altri volontari step 3 ggev
                            break;
                        case 77:
                            NuovaNotifica("Il sindaco ha approvato la richiesta di volontari CRI, ora mandala alla segreteria provinciale");
                            schede[3].GetComponentInChildren<TaskSeleziona>().NuovaTask(77);
                            // altri volontari step 3 cri
                            break;
                        case 78:
                            NuovaNotifica("Il sindaco ha approvato la richiesta di polizia, ora mandala alla segreteria provinciale");
                            schede[3].GetComponentInChildren<TaskSeleziona>().NuovaTask(78);
                            // altri volontari step 3 polizia
                            break;
                        case 79:
                            NuovaNotifica("Il sindaco ha approvato la richiesta di pompieri, ora mandala alla segreteria provinciale");
                            schede[3].GetComponentInChildren<TaskSeleziona>().NuovaTask(79);
                            // altri volontari step 3 pompiere
                            break;
                    }
                    break;
                case Ruoli.Medico:
                    switch (value)
                    {
                        case 37:
                            //visita generica
                            break;
                        case 38:
                            //cura ferito primo soccorso
                            break;
                    }
                    break;
                case Ruoli.RefCri:
                    switch (value)
                    {
                        case 30:
                            NuovaNotifica("task 30");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask(30);
                            break;
                        case 31:
                            NuovaNotifica("task 31");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask(31);
                            break;
                        case 67:
                            NuovaNotifica("Richiesta volontari annulata");
                            break;
                        case 87:
                            NuovaNotifica("Volontari ottenuti");
                            break;
                        case 102:
                            NuovaNotifica("task 102");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask(102);
                            break;
                        case 105:
                            NuovaNotifica("task 105");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask(105);
                            break;
                        case 106:
                            NuovaNotifica("task 106");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask(106);
                            break;
                    }
                    break;
                case Ruoli.RefGgev:
                    switch (value)
                    {
                        case 20:
                            NuovaNotifica("task 10");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask(10);
                            break;
                        case 66:
                            NuovaNotifica("Richiesta volontari annulata");
                            break;
                        case 86:
                            NuovaNotifica("Volontari ottenuti");
                            break;
                        case 104:
                            NuovaNotifica("task 104");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask(104);
                            break;
                    }
                    break;
                case Ruoli.RefPC:
                    switch (value)
                    {
                        case 10:
                            NuovaNotifica("task 10");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask(10);
                            break;
                        case 11:
                            NuovaNotifica("task 11");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask(11);
                            break;
                        case 12:
                            NuovaNotifica("task 12");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask(12);
                            break;
                        case 13:
                            NuovaNotifica("task 13");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask(13);
                            break;
                        case 65:
                            NuovaNotifica("Richiesta volontari annulata");
                            break;
                        case 85:
                            NuovaNotifica("Volontari ottenuti");
                            break;
                    }
                    break;
                case Ruoli.RefPolizia:
                    switch (value)
                    {
                        case 40:
                            NuovaNotifica("task 40");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask(40);
                            break;
                        case 41:
                            NuovaNotifica("task 41");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask(41);
                            break;
                        case 68:
                            NuovaNotifica("Richiesta volontari annulata");
                            break;
                        case 88:
                            NuovaNotifica("Volontari ottenuti");
                            break;
                        case 107:
                            NuovaNotifica("task 107");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask(107);
                            break;
                    }
                    break;
                case Ruoli.RefFuoco:
                    switch (value)
                    {
                        case 50:
                            NuovaNotifica("task 50");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask(50);
                            break;
                        case 69:
                            NuovaNotifica("Richiesta volontari annulata");
                            break;
                        case 89:
                            NuovaNotifica("Volontari ottenuti");
                            break;
                        case 100:
                            NuovaNotifica("task 100");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask(100);
                            break;
                        case 101:
                            NuovaNotifica("task 101");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask(101);
                            break;
                        case 103:
                            NuovaNotifica("task 103");
                            schede[2].GetComponentInChildren<TaskSeleziona>().NuovaTask(103);
                            break;
                    }
                    break;
                case Ruoli.Segreteria:
                    switch (value)
                    {
                        case 80:
                            NuovaNotifica("Richiesta di volontari PC");
                            schede[1].GetComponentInChildren<TaskSeleziona>().NuovaTask(80);
                            break;
                        case 81:
                            NuovaNotifica("Richiesta di volontari GGEV");
                            schede[1].GetComponentInChildren<TaskSeleziona>().NuovaTask(81);
                            break;
                        case 82:
                            NuovaNotifica("Richiesta di volontari CRI");
                            schede[1].GetComponentInChildren<TaskSeleziona>().NuovaTask(82);
                            break;
                        case 83:
                            NuovaNotifica("Richiesta di polizia");
                            schede[1].GetComponentInChildren<TaskSeleziona>().NuovaTask(83);
                            break;
                        case 84:
                            NuovaNotifica("Richiesta di vigili del fuoco");
                            schede[1].GetComponentInChildren<TaskSeleziona>().NuovaTask(84);
                            break;
                    }
                    break;
                case Ruoli.RefTlc:
                    switch (value)
                    {
                        case 3:
                            NuovaNotifica("Il sindaco ti ha chiesto delle informazioni");
                            schede[0].transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
                            //task informa
                            break;
                        case 5:
                            NuovaNotifica("Il coc ti ha chiesto di informare la popolazione");
                            schede[0].transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
                            //task informa
                            break;
                    }
                    break;
                case Ruoli.VolCri:
                    switch (value)
                    {
                        case 35:
                            if(Info.localUser.name != plName)
                                return;
                            NuovaNotifica("Trova la zona e costruisci un ambiete per fornire le cure mediche durante l'emergenza");
                            allestimentoCri.Genera(true);
                            break;
                        case 36:
                            if(Info.localUser.name != plName)
                                return;
                            break;
                    }
                    break;
                case Ruoli.VolPC:
                    switch (value)
                    {
                        case 15:
                            if(Info.localUser.name != plName)
                                return;
                            break;
                        case 16:
                            if(Info.localUser.name != plName)
                                return;
                            Assegna(16000);
                            NuovaNotifica("Cerca e svuolta la zona allagata");
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
                            NuovaNotifica("Trova la zona e contruisci un ambiente sicuro come punto di raccolta per i cittadini");
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
                            Assegna(27000);
                            break;
                    }
                    break;
                case Ruoli.VolPolizia:
                    switch (value)
                    {
                        case 45:
                            if(Info.localUser.name != plName)
                                return;
                            break;
                        case 46:
                            if(Info.localUser.name != plName)
                                return;
                            break;
                        case 47:
                            if(Info.localUser.name != plName)
                                return;
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
                            Assegna(57000);
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
        private void NuovaNotifica(string testo)
        {
            pallinoRosso.SetActive(true);
            schede[^1].GetComponent<AddNotifica>().SetMessaggio(testo);
        }
    }
}
