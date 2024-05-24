using System;
using System.Collections.Generic;
using Script.Utility;
using Unity.VisualScripting;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace Script.test
{
    public class TaskManager : MonoBehaviour
    {
        [SerializeField]private GameObject prefGenerico;
        [SerializeField]private GameObject prefCOC;
        [SerializeField]private GameObject prefSindaco;
        [SerializeField]private GameObject prefGiornalista;
        [SerializeField]private GameObject prefSegretaria;
        [SerializeField]private GameObject prefReferenti;
        [SerializeField]private GameObject prefAttivita;
        [SerializeField]private GameObject prefNotifiche;
        [SerializeField]private GameObject prefTaskSegr;
        [SerializeField] private GameObject pallinoRosso;
        
        [SerializeField]private Transform parent;

        public bool flag;
        public bool flag1;

        public int nn;
        public Ruoli rsda;

        private void Start()
        {
            Inizializza();
        }

        private void Update(){
            if (flag)
            {
                flag = false;
                Info.localUser.role = rsda;
                Inizializza();
            }

            if (flag1)
            {
                flag1 = false;
                Assegna(nn);
            }
        }

        private List<GameObject> schede = new List<GameObject>();

        private void Inizializza()
        {
            switch (Info.localUser.role)
            {
                case Ruoli.Sindaco:
                    schede.Add( GameObject.Instantiate(prefSindaco, parent));
                    break;
                case Ruoli.Coc:
                    schede.Add( Instantiate(prefCOC, parent));
                    break;
                case Ruoli.Giornalista:
                    schede.Add( Instantiate(prefGiornalista, parent));
                    break;
                case Ruoli.Segretaria:
                    schede.Add( Instantiate(prefSegretaria, parent));
                    schede.Add( Instantiate(prefTaskSegr, parent));
                    break;
                case Ruoli.RefCri:
                case Ruoli.RefFuoco:
                case Ruoli.RefPC:
                case Ruoli.RefPolizia:
                case Ruoli.RefGgev:
                    schede.Add( Instantiate(prefReferenti, parent));
                    break;
                default:
                    schede.Add( Instantiate(prefGenerico, parent));
                    break;
            }
            schede.Add( Instantiate(prefAttivita, parent));
            schede.Add( Instantiate(prefNotifiche, parent));
            
            foreach (var var in schede)
            {
                try
                {
                    var.GetComponentInChildren<logica>().SetListaSchede(schede);
                }
                catch (Exception e)
                {
                    // ignored
                }
            }
            
        }
        public void Assegna(int value)
        { 
            switch (Info.localUser.role)
            {
                case Ruoli.Sindaco:
                    switch (value)
                    {
                        case 1:
                            pallinoRosso.SetActive(true);
                            schede[0].transform.GetChild(0).GetChild(5).GetComponent<Button>().interactable = true;
                            break;
                        case 23:
                            pallinoRosso.SetActive(true);
                            Debug.LogWarning("NOTIFICA DELLE INFORMAZIONI");
                            break;
                    }
                   break;
                case Ruoli.Segretaria:
                    switch (value)
                    {
                        case 10:
                            pallinoRosso.SetActive(true);
                            schede[0].transform.GetChild(0).GetChild(4).GetComponent<Button>().interactable = true;
                            schede[1].transform.GetChild(1).GetComponent<TaskSegretaria>().SetCodice(10);
                            break;
                        case 20:
                            pallinoRosso.SetActive(true);
                            schede[0].transform.GetChild(0).GetChild(4).GetComponent<Button>().interactable = true;
                            schede[1].transform.GetChild(1).GetComponent<TaskSegretaria>().SetCodice(20);
                            break;
                        case 22:
                            pallinoRosso.SetActive(true);
                            schede[0].transform.GetChild(0).GetChild(4).GetComponent<Button>().interactable = true;
                            schede[1].transform.GetChild(1).GetComponent<TaskSegretaria>().SetCodice(22);
                            break;
                    }
                    break;
                case Ruoli.Coc:
                    switch (value)
                    {
                        case 11:
                            pallinoRosso.SetActive(true);
                            schede[0].transform.GetChild(0).GetChild(5).GetComponent<Button>().interactable = true;
                            break;
                        case 19:
                            pallinoRosso.SetActive(true);
                            Debug.LogWarning("NOTIFICA DELLE INFORMAZIONI");
                            break;
                    }
                    break;
                case Ruoli.Giornalista:
                    switch (value)
                    {
                        case 21:
                            pallinoRosso.SetActive(true);
                            schede[0].transform.GetChild(0).GetChild(5).GetComponent<Button>().interactable = true;
                            break;
                        case 18:
                            pallinoRosso.SetActive(true);
                            schede[0].transform.GetChild(0).GetChild(6).GetComponent<Button>().interactable = true;
                            break;
                    }
                    break;
            }
        }

        public void OpenManager()
        {
            schede[0].SetActive(true);
            pallinoRosso.SetActive(false);
        }

    }
}