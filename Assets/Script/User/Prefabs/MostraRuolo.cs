using System.Collections.Generic;
using Defective.JSON;
using Script.Utility;
using System.IO;
using Proyecto26;
using Script.Utility.obj;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.User.Prefabs
{
    public class MostraRuolo : MonoBehaviour
    {
        [SerializeField] private List<PlayerInfo> playerInfo;
        [SerializeField] private Image roleImage;
        [SerializeField] private new TextMeshProUGUI name;
        [SerializeField] private TextMeshProUGUI description;
        
        private void Start()
        {
           LoadRole();
            
        }
        private void LoadRole()
        {
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + "/Role.json").Then(e =>
            {
                Ruoli tmp = Ruoli.Null;
                    switch (e.Text)
                    {
                        case "Null":
                            Debug.Log("Il ruolo è Null");
                            tmp = Ruoli.Null;
                            break;
                        case "Sindaco":
                            Debug.Log("Il ruolo è Sindaco");
                            tmp = Ruoli.Sindaco;
                            break;
                        case "Coc":
                            Debug.Log("Il ruolo è Coc");
                            tmp = Ruoli.Coc;
                            break;
                        case "RefCri":
                            Debug.Log("Il ruolo è Referente Croce Rossa Italiana");
                            tmp = Ruoli.RefCri;
                            break;
                        case "VolCri":
                            Debug.Log("Il ruolo è Volontario Croce Rossa Italiana");
                            tmp = Ruoli.VolCri;
                            break;
                        case "Medico":
                            Debug.Log("Il ruolo è Medico");
                            tmp = Ruoli.Medico;
                            break;
                        case "RefPC":
                            Debug.Log("Il ruolo è Referente Protezione Civile");
                            tmp = Ruoli.RefPC;
                            break;
                        case "VolPC":
                            Debug.Log("Il ruolo è Volontario Protezione Civile");
                            tmp = Ruoli.VolPC;
                            break;
                        case "RefTlc":
                            Debug.Log("Il ruolo è Telecomunicazioni");
                            tmp = Ruoli.RefTlc;
                            break;
                        case "RefPolizia":
                            Debug.Log("Il ruolo è Referente Polizia");
                            tmp = Ruoli.RefPolizia;
                            break;
                        case "VolPolizia":
                            Debug.Log("Il ruolo è Volontario Polizia");
                            tmp = Ruoli.VolPolizia;
                            break;
                        case "RefFuoco":
                            Debug.Log("Il ruolo è Referente Vigile del Fuoco");
                            tmp = Ruoli.RefFuoco;
                            break;
                        case "VolFuoco":
                            Debug.Log("Il ruolo è Volontario Vigile del Fuoco");
                            tmp = Ruoli.VolFuoco;
                            break;
                        case "Segreteria":
                            Debug.Log("Il ruolo è Segreteria");
                            tmp = Ruoli.Segreteria;
                            break;
                        case "RefGgev":
                            Debug.Log("Il ruolo è Referente Guardie Giurate Ecologiche Volontarie");
                            tmp = Ruoli.RefGgev;
                            break;
                        case "VolGgev":
                            Debug.Log("Il ruolo è Volontario Guardie Giurate Ecologiche Volontarie");
                            tmp = Ruoli.VolGgev;
                            break;
                    }
                    Debug.Log(tmp.ToString());
                    
                    PlayerInfo info = FindRole(tmp);
            
                    name.text = info.Nome;
                    description.text = info.Descrizione;
                    roleImage.sprite = info.Immagine;
                    
                    
                });

        }
        private PlayerInfo FindRole(Ruoli r)
        {
            
            int i = 0;
            while (playerInfo[i].ruolo != r)
                i++;
            
            if (i < playerInfo.Count)
                return playerInfo[i];
            return null;
        }
        
    }
}
