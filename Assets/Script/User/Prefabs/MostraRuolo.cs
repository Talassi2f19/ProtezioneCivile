using Defective.JSON;
using Script.Utility;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace Script.User.Prefabs
{
    public class MostraRuolo : MonoBehaviour
    {
        [SerializeField] private Image roleImage;
        [SerializeField] private new TMP_Text name;
        [SerializeField] private TMP_Text description;

        private string roleImageName;

        //private string roleImagePath;
        //private string roleName;
        //private string roleDescription;


        private void Start()
        {  
            name.text = Info.localUser.role.ToString();

            JSONObject json = new JSONObject(File.ReadAllText("../../../FileUtili/infoRuoli.json"));

            string keyRole = searchRoleKey(json);

            Dictionary<string, JSONObject> dizionario = json.ToJsonDictionary();
            
            name.text = dizionario[keyRole].GetField("name").ToString();
            description.text = dizionario[keyRole].GetField("Descrizione").ToString();
            roleImageName = dizionario[keyRole].GetField("Sprite").ToString();

            roleImage.sprite = Resources.Load<Sprite>(roleImageName);
        }

        private string searchRoleKey(JSONObject json)
        {
            int j = 0;
            while (json.keys[j] != (Info.localUser.role.ToString() + ".json"))
                j++;
            return json.keys[j];
        }

        //public void SetRoleName(string roleName)
        //{
        //    this.roleName = roleName;
        //    name.text = this.roleName;
        //}

        //public void SetRoleDescription(string roleDescription)
        //{
        //    this.roleDescription = roleDescription;
        //    description.text = this.roleDescription;
        //}
    
        //public void SetRoleImage(string roleImagePath)
        //{
        //    this.roleImagePath = roleImagePath;
        //    Sprite immagine = Resources.Load<Sprite>(this.roleImagePath);
        //    roleImage.sprite = immagine;
        //}
    
        public void ExitRoleInfo()
        {
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
