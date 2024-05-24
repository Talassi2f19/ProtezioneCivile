using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using Script.Utility;
using UnityEngine;
using UnityEngine.UI;

public class logica : MonoBehaviour
{
    private List<GameObject> schede = new List<GameObject>();

    public void SetListaSchede(List<GameObject> value)
    {
        schede = value;
    }
    
    public void Indietro() //tasto indietro
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void Notifiche() //mostra pagina notifiche
    {
        schede[^1].SetActive(true);
    }

    public void Attivita() //mostr pagina con attività da fare
    {
        schede[^2].SetActive(true);
    }

    public void GiornalistaInforma() //inizia task della giornalista per informare la popolazione
    {

    }
    public void GiornalistaInformaSndaco() //inizia task della giornalista per informare la popolazione
    {
        RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":22}");
        transform.parent.GetChild(0).GetChild(5).GetComponent<Button>().interactable = false;
    }
    public void GiornalistaInformaCoc() //inizia task della giornalista per informare la popolazione
    {
         RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":19}");
         transform.parent.GetChild(0).GetChild(6).GetComponent<Button>().interactable = false;
    }

    public void SindacoCOC() //convoca il coc
    {
        RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":10}");
        transform.parent.GetChild(0).GetChild(5).GetComponent<Button>().interactable = false;
    }

    public void SindacoAiuti() //richiedi aiuti sindaco
    {

    }

    public void SindacoInformazioni() //chiedi informazioni alla giornalista tramite la segretaria
    {
        RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":20}");
    }

    public void SegretariaTask() //gioco smista task della segretaria
    {
        schede[1].SetActive(true);
        transform.parent.GetChild(0).GetChild(4).GetComponent<Button>().interactable = false;
    }

    public void COCChiedeInfo() //il coc chiede informazioni alla giornalista direttamente
    {
        RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":18}");
    }

    public void COCGiocoAssegnazione() // minigioco di asseganre la task al referente corretto
    {
        
    }
    
    
}
