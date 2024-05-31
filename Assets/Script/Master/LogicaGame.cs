using System.Collections;
using System.Collections.Generic;
using Script.Master;
using UnityEngine;

public class LogicaGame : MonoBehaviour
{
    [SerializeField] private GameObject menuIcon;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject terminaSelection;
    [SerializeField] private GameObject giocatori;
    [SerializeField] private GameObject azioni;
    [SerializeField] private GameObject messaggi;


    public void OpenMenu()
    {
        menuIcon.SetActive(false);
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        menuIcon.SetActive(true);
        menu.SetActive(false);
    }

    public void TerminaMenu()
    {
        terminaSelection.SetActive(true);
    }

    public void TerminaConferma()
    {
        Debug.Log("conferma");
    }

    public void TerminaAnnulla()
    {
        CloseMenu();
        terminaSelection.SetActive(false);
    }

    public void GiocatoriMostra()
    {
        CloseMenu();
        giocatori.SetActive(true);
    }

    public void AzioniMostra()
    {
        CloseMenu();
        azioni.SetActive(true);
    }

    public void MessaggiMostra()
    {
        CloseMenu();
        messaggi.SetActive(true);
    }
    
    public void GiocatoriChiudi()
    {
        giocatori.SetActive(false);
    }

    public void AzioniChiudi()
    {
        azioni.SetActive(false);
    }

    public void MessaggiChiudi()
    {
        azioni.SetActive(false);
    }
}
