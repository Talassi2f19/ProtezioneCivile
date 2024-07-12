using System.Collections.Generic;
using Defective.JSON;
using Script.Utility;
using System.IO;
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
            PlayerInfo info = FindRole();
            
            name.text = info.Nome;
            description.text = info.Descrizione;
            roleImage.sprite = info.Immagine;
        }
        
        private PlayerInfo FindRole()
        {
            Ruoli role = Info.localUser.role;
            int i = 0;
            
            while (playerInfo[i].ruolo == role)
                i++;
            
            if (i < playerInfo.Count)
                return playerInfo[i];
            return null;
        }
        
    }
}
