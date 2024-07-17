using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace _Scenes.User.telefono
{
    public class TaskSeleziona : MonoBehaviour
    {
        [SerializeField]private GameObject prefab;
        [SerializeField]private Button avanti;
        [SerializeField]private Transform parent;
        [SerializeField]private Logica logica;
        [SerializeField]private VolontariTaks volontariTaks;
        [SerializeField]private RichiesteVolontari richiesteVolontari;

        private GameObject button;
        private int codice;
    
    
        public void NuovaTask(string desc, int cod)
        {
            avanti.interactable = true;
            GameObject tmp = Instantiate(prefab, parent);
            tmp.GetComponentInChildren<TextMeshProUGUI>().text = desc;
            tmp.GetComponent<Button>().onClick.AddListener(()=>Select(cod, tmp));
        }

        public void SelectReset()
        {
            if(button != null)
                button.GetComponent<Image>().color = Color.white;
            button = null;
            codice = -100;
            avanti.interactable = false;
        }

        private void Select(int k, GameObject h)
        {
            avanti.interactable = true;
            if(button != null)
                button.GetComponent<Image>().color = Color.white;
            button = h;
            codice = k;
            h.GetComponent<Image>().color = Color.green;
        }
    
        public void ApriAssenga()
        {
            logica.TaskAssenga(codice);
            Rimuovi();
        }
    
        public void AvantiVolontari()
        {
            volontariTaks.Assegna(codice);
        }

        public void ProcediRichiesta()
        {
            richiesteVolontari.ConfermaRichiesta(codice);
            Rimuovi();
        }

        public void Rimuovi()
        {
            if(button != null)
                button.SetActive(false);
            if(parent.childCount <= 1)
                avanti.interactable = false;
            //Destroy(button);
        }

        private void OnDisable()
        {
            SelectReset();
        }
    }
}
