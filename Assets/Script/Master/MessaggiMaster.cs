using System.Collections.Generic;
using Script.Master.Prefabs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Master
{
    public class MessaggiMaster : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI displayTesto;
        [SerializeField] private Transform container;
        [SerializeField] private GameObject prefab;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private GameObject scrollbar;

        private List<MessaggioBox> messaggiObjects = new List<MessaggioBox>();

        private int count = 0;
        public void AggiungiMessaggi(string testo)
        {
            GameObject tmp = Instantiate(prefab, container);
            messaggiObjects.Add(tmp.GetComponent<MessaggioBox>());
            messaggiObjects[^1].SetText(testo, count);
            tmp.transform.SetAsFirstSibling();
            MessaggioSelezionato(count);
            count++;
            AggiornaContainer();
        }
    
        private void AggiornaContainer()
        {
            RectTransform rectTransform = container.GetComponent<RectTransform>();
            VerticalLayoutGroup verticalLayoutGroup = container.GetComponent<VerticalLayoutGroup>();
        
            float height = count * (verticalLayoutGroup.spacing + prefab.GetComponent<RectTransform>().sizeDelta.y); 
        
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x,height);
            rectTransform.anchoredPosition = new Vector2(0, height * -1 / 2);

            if (height > 620)
                AbilitaScroll();
        }

        private void AbilitaScroll()
        {
            scrollbar.SetActive(true);
            scrollRect.enabled = true;
        }
    
        public void MessaggioSelezionato(int value)
        {
            foreach (var var in messaggiObjects)
            {
                var.Selected(false);
            }
            messaggiObjects[value].Selected(true);
            displayTesto.text = messaggiObjects[value].GetTesto();
        }
    
    
        // public bool ff;
        // public string tt;
        // private void Update()
        // {
        //     if (ff)
        //     {
        //         ff = false;
        //         AggiungiMessaggi(tt);
        //     }
        // }
    }
}
