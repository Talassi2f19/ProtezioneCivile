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

        private void Start()
        {
            SelectReset();
        }

        public void NuovaTask(string desc, int cod)
        {
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
            SelectReset();
            avanti.interactable = true;
            if(button != null)
                button.GetComponent<Image>().color = Color.white;
            button = h;
            codice = k;
            h.GetComponent<Image>().color = Color.green;
        }
    
        public void ApriAssenga()
        {
            logica.TaskAssenga(codice, button.GetComponentInChildren<TextMeshProUGUI>().text);
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
            SelectReset();
        }

        public void Rimuovi()
        {
            if(button != null)
                Destroy(button);
            if(parent.childCount <= 1)
                avanti.interactable = false;
            SelectReset();
        }

        private void OnDisable()
        {
            SelectReset();
        }
    }
}
