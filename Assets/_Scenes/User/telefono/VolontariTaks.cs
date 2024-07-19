using System;
using System.Collections.Generic;
using Defective.JSON;
using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scenes.User.telefono
{
    public class VolontariTaks : MonoBehaviour
    {
        [SerializeField]private GameObject part1;
        [SerializeField]private GameObject part1A;
        [SerializeField]private GameObject part2;
        [SerializeField]private GameObject prefab;
        [SerializeField]private Transform container;
        [SerializeField] private Logica logica;
        
        [SerializeField]private Button bottoneConferma;
        [SerializeField]private TextMeshProUGUI numeroVolontariText;
        private int codice;
        private int nVolontari;

        public void Assegna(int cod)
        {
            codice = cod;
            nVolontari = GenNumVolontari();
            numeroVolontariText.text = "Volontari richiesti: " + nVolontari;
            ResetLista();
            Aggiorna();
            part1.SetActive(false);
            part1A.SetActive(false);
            part2.SetActive(true);
        }
    
        private void OnEnable()
        {
            Fine();
        }

        private void ResetLista()
        {
            bottoneConferma.interactable = false;
            selezionati = new List<string>();
            foreach (Transform child in container)
            {
                if(child.gameObject.activeSelf)
                    child.GetComponent<Image>().color = Color.white;
            }
        }
    

        public void Fine()
        {
            part1.SetActive(true);
            part1A.SetActive(true);
            part2.SetActive(false);
        }
    
        private Ruoli roleType;
        private void Start()
        {
            switch (Info.localUser.role)
            {
                case Ruoli.RefPC:
                    roleType = Ruoli.VolPC;
                    break;
                case Ruoli.RefCri:
                    roleType = Ruoli.VolCri;
                    break;
                case Ruoli.RefFuoco:
                    roleType = Ruoli.VolFuoco;
                    break;
                case Ruoli.RefGgev:
                    roleType = Ruoli.VolGgev;
                    break;
                case Ruoli.RefPolizia:
                    roleType = Ruoli.VolPolizia;
                    break;
            }
        }

        public void Aggiorna()
        {
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + ".json").Then(e =>
            {
                ResetLista();
                foreach (Transform figlio in container)
                {
                    // Destroy(figlio.gameObject);
                    figlio.gameObject.SetActive(false);
                }
                Mostra(new JSONObject(e.Text));    
            }).Catch(Debug.Log);
        }
    
        private void Mostra(JSONObject json)
        {
            if(!json)
                return;
            foreach (var var in json.list)
            {
                if (var.GetField("Role").stringValue == roleType.ToString())
                {
                    if (!var.GetField("Occupato") || var.GetField("Occupato").boolValue == false)
                    {
                        GameObject tmp = Instantiate(prefab, container);
                        tmp.GetComponentInChildren<Button>().onClick.AddListener(()=>Selected(var.GetField("Name").stringValue, tmp));
                        tmp.GetComponentInChildren<TextMeshProUGUI>().text = var.GetField("Name").stringValue;
                    }
                }
            }
        }

        private List<String> selezionati;
    
        private void Selected(String nome, GameObject bottone)
        {
            if (selezionati.Contains(nome))
            {
                bottone.GetComponentInChildren<Image>().color = Color.white;
                selezionati.Remove(nome);
            }
            else
            {
                bottone.GetComponentInChildren<Image>().color = Color.green;
                selezionati.Add(nome);
            }
            
            bottoneConferma.interactable = selezionati.Count == nVolontari;
        }

        private int GenNumVolontari()
        {
            switch (codice)
            {
                case 10:
                    return 2;
                case 11:
                    return 2;
                case 12:
                    return 2;
                case 13:
                    return 2;
                case 20:
                    return 2;
                case 21:
                    return 2;
                case 30:
                    return 2;
                case 31:
                    return 2;
                case 40:
                    return 2;
                case 41:
                    return 2;
                case 50:
                    return 2;
                case 100:
                    return 2;
                case 101:
                    return 2;
                case 102:
                    return 2;
                case 103:
                    return 2;
                case 104:
                    return 2;
                case 105:
                    return 2;
                case 106:
                    return 2;
                case 107:
                    return 2;
                default:
                    return 1;
            }
        }
    
        private int GenCodice()
        {
            switch (codice)
            {
                case 10:
                    return 15;
                case 11:
                    return 16;
                case 12:
                    return 17;
                case 13:
                    return 18;
                case 20:
                    return 25;
                case 21:
                    return 26;
                case 30:
                    return 35;
                case 31:
                    return 36;
                case 40:
                    return 45;
                case 41:
                    return 46;
                case 50:
                    return 55;
                case 1015:
                    return 15;
                case 1016:
                    return 16;
                case 1017:
                    return 17;
                case 1018:
                    return 18;
                case 1025:
                    return 25;
                case 1027:
                    return 27;
                case 1035:
                    return 35;
                case 1036:
                    return 36;
                case 1038:
                    return 38;
                case 1046:
                    return 46;
                case 1047:
                    return 47;
                case 1055:
                    return 55;
                case 1056:
                    return 56;
                case 1057:
                    return 57;
                default:
                    return -100;
            }
        }

        public void Invia()
        {
            int codiceDaInviare = GenCodice();
            foreach (string nome in selezionati)
            {
                String json = "{\"CodeTask\":"+codiceDaInviare+",\"Player\":\""+nome+"\"}";
                RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", json).Catch(Debug.Log);
                RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + nome + ".json", "{\"Occupato\":true}").Catch(Debug.Log);
            } 
            Fine();
        }

        public void RichiediVolontari()
        {
            int codiceDaInviare = -100;
            switch (Info.localUser.role)
            {
                case Ruoli.RefCri:
                    codiceDaInviare = 62;
                    break;
                case Ruoli.RefFuoco:
                    codiceDaInviare = 64;
                    break;
                case Ruoli.RefGgev:
                    codiceDaInviare = 61;
                    break;
                case Ruoli.RefPolizia:
                    codiceDaInviare = 63;
                    break;
                case Ruoli.RefPC:
                    codiceDaInviare = 60;
                    break;
            }
        
            Fine();
            logica.NuovaNotifiche("Hai richiesto più volontari");
            String json = "{\"CodeTask\":"+codiceDaInviare+"}";
            RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", json).Catch(Debug.Log);
        }
    }
}
