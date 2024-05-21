using System;
using System.Collections.Generic;
using Defective.JSON;
using Script.User.Prefabs;
using Script.Utility;
using UnityEngine;

// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace Script.User
{
    //classe per la gestione dei player
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private GameObject mostraRuoloPrefab;
        [SerializeField] private GameObject infoRuolo;
        [SerializeField] private JoyStick joyStick;
        
        
        public void ShowMostraRuolo()
        {
            mostraRuoloPrefab.SetActive(true);
            infoRuolo.SetActive(false);
            joyStick.Enable(false);
        }

        public void CloseMostraRuolo()
        {
            infoRuolo.SetActive(true);
            joyStick.Enable(true);
        }
    }
}