using System.Collections.Generic;
using Proyecto26;
using Script.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace _Scenes.User.telefono
{
    public class Logica : MonoBehaviour
    {
        private List<GameObject> schede = new List<GameObject>();
        
        public void SetListaSchede(List<GameObject> value)
        {
            schede = value;
        }
    
        public void Indietro() //tasto indietro
        {
            transform.parent.parent.gameObject.SetActive(false);
        }

        public void Notifiche() //mostra pagina notifiche
        {
            schede[^1].SetActive(true);
            
        }
        public void NuovaNotifiche(string str) //mostra pagina notifiche
        {
            schede[^1].GetComponent<AddNotifica>().SetMessaggio(str);
        }

        public void NotificheEsci()
        {
            GameObject.Find("ControNotifiche").transform.Find("pallino").gameObject.SetActive(false);
        }

        public void MostraRuoli() //mostra pagina notifiche
        {
            schede[^2].SetActive(true);
        }
        
        public void RefTlcInforma(GameObject tmp)
        {
            tmp.SetActive(false);
            RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":6}").Catch(Debug.Log);
        }
        public void RefTlcInformaSindaco(GameObject tmp)
        {
            tmp.SetActive(false);
            RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":4}").Catch(Debug.Log);
        }
        
        public void SindacoCOC(GameObject tmp) //convoca il coc
        {
            RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":2}").Catch(Debug.Log);
            tmp.SetActive(false);
        }

        public void SindacoRichieste() //richiedi aiuti sindaco
        {
            schede[1].SetActive(true);
        }

        public void SindacoInformazioni(Button tmp) //chiedi informazioni alla giornalista tramite la segretaria
        {
            tmp.interactable = false;
            RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":3}");
        }
        
        public void SegreteriaRichieste()
        {
            schede[1].SetActive(true);
        }

        public void COCChiedeInfo() //il coc chiede informazioni
        {
            RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":5}");
        }

        public void COCGiocoAssegnazione() // minigioco di asseganre la task al referente corretto
        {
            schede[1].SetActive(true);
        }

        public void COCRichieste()
        {
            schede[3].SetActive(true);
        }
        
        public void TaskAssenga(int code)
        {
            Debug.Log(schede[2].name);
            Debug.Log(schede[2].GetComponentInChildren<TaskAssegna>());
            schede[2].GetComponentInChildren<TaskAssegna>().SetCodice(code);
            schede[2].SetActive(true);
        }

        public void ReferentiMostraVolontari()
        {
            schede[1].SetActive(true);
        }

        public void ReferentiAssegnaVolontari()
        {
            schede[2].SetActive(true);
        }
        public void OpenPlInfo()
        {
            schede[^3].SetActive(true);
        }
        
    }
}
