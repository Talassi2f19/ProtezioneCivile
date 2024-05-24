

using System;
using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskSegretaria : MonoBehaviour
{
    private int codice;
    [SerializeField]private GameObject bSindaco;
    [SerializeField]private GameObject bCoc;
    [SerializeField]private GameObject bGiornalista;
    [SerializeField]private GameObject bVolontario;
    [SerializeField] private GameObject sbagliato;
    [SerializeField] private GameObject corretto;
    [SerializeField] private GameObject buttAvanti;
    [SerializeField] private GameObject buttInvia;
    [SerializeField] private TextMeshProUGUI testo;

    private int selected = 0;

    public bool flag;
    
    private void Update()
    {
        if (flag)
        {
            flag = false;
            SetCodice(10);
        }
    }

    public void SetCodice(int value)
    {
        codice = value;
        switch (codice)
        {
            case 10:
                testo.text = "il sindaco ti ha chiesto di convocare il Centro Operativo Comunale, seleziona a chi rivolgerti";
                break;
            case 20:
                testo.text = "il sindaco ti ha chiesto di fornirgli delle informazioni, seleziona a chi rivolgerti";
                break;
            case 22:
                testo.text = "prima qualcuno ti ha chiesto delle infromazioni rimandagliele, seleziona a chi mandarle";
                break;
        }
    }

    public void Click(int n)
    {
        bSindaco.GetComponent<Image>().color = Color.white;
        bCoc.GetComponent<Image>().color = Color.white;
        bGiornalista.GetComponent<Image>().color = Color.white;
        bVolontario.GetComponent<Image>().color = Color.white;
        switch (n)
        {
            case 1:
                selected = 1;
                bSindaco.GetComponent<Image>().color = Color.green;
                break;
            case 2:
                selected = 2;
                bCoc.GetComponent<Image>().color = Color.green;
                break;
            case 3:
                selected = 3;
                bGiornalista.GetComponent<Image>().color = Color.green;
                break;
            case 4:
                selected = 4;
                bVolontario.GetComponent<Image>().color = Color.green;
                break;
        }
    }

    public void Controlla()
    {
        if(selected == 0)
            return;
        
        buttAvanti.SetActive(true);
        buttInvia.SetActive(false);
        
        bSindaco.SetActive(false);
        bGiornalista.SetActive(false);
        bCoc.SetActive(false);
        bVolontario.SetActive(false);

        //1 sindaco
        //2 coc
        //3 giornalista
        //4 volontario
        switch (codice)
        {
            case 10:
                if (selected == 2)
                   corretto.SetActive(true); 
                else
                    sbagliato.SetActive(true);
                RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":11}");
                break;
            case 20:
                if (selected == 3)
                    corretto.SetActive(true); 
                else
                    sbagliato.SetActive(true);
                RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":21}");
                break;
            case 22:
                if (selected == 1)
                    corretto.SetActive(true); 
                else
                    sbagliato.SetActive(true);
                RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":23}");
                break;
        }
        
    }

    public void Avanti()
    {
        transform.parent.gameObject.SetActive(false);
    }
    
}
