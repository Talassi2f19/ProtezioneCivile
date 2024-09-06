using System;
using System.Collections.Generic;
using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Master
{
    [Serializable]
    class Lista
    {
        public string testo;
        public int codice;
    }
    public class CaricaAzioniMaster : MonoBehaviour
    {
        [SerializeField] private List<Lista> bottoni;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Button inviaButton;
        [SerializeField] private Transform parent;
    
        private int selezioantoVal;
        private GameObject selezioantoObj;
    
        private void Start()
        {
            inviaButton.interactable = false;
            foreach (var tmp in bottoni)
            {
                GameObject obj = Instantiate(prefab, parent);
                obj.GetComponent<Button>().onClick.AddListener(() => OnClick(obj, tmp.codice));
                obj.GetComponentInChildren<TextMeshProUGUI>().text = tmp.testo;
            }
        }
    
        private void OnClick(GameObject tmp, int val)
        {
            if(selezioantoObj != null)
                selezioantoObj.GetComponent<Image>().color = Color.white;
            selezioantoObj = tmp;
            selezioantoObj.GetComponent<Image>().color = Color.green;
            selezioantoVal = val;
            inviaButton.interactable = true;
        
        }

        public void Invia()
        {
            selezioantoObj.GetComponent<Image>().color = Color.white;
            selezioantoObj = null;
            inviaButton.interactable = false;
            RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":"+selezioantoVal+"}").Catch(Debug.LogError);
        }
    
    }
}