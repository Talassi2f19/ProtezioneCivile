using System;
using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskAssegna : MonoBehaviour
{
    [SerializeField]private int codice;
    [SerializeField]private GameObject gameObject1;
    [SerializeField]private GameObject gameObject2;
    [SerializeField]private GameObject gameObject3;
    [SerializeField]private GameObject gameObject4;
    [SerializeField]private GameObject gameObject5;
    [SerializeField]private GameObject sbagliato;
    [SerializeField]private GameObject corretto;
    [SerializeField]private GameObject buttAvanti;
    [SerializeField]private GameObject buttInvia;
    [SerializeField]private TextMeshProUGUI testo;

    private int selected = 0;


    // public bool flag;
    //
    // private void Update()
    // {
    //     if (flag)
    //     {
    //         flag = false;
    //         SetCodice(10);
    //     }
    // }

    
    // public void Set(String strTesto, Ruoli ruoloCorretto, int codiceInvio )
    // {
    //     testo.text = strTesto;
    // }

    private void OnEnable()
    {
        buttAvanti.SetActive(false);
        buttInvia.SetActive(true);
    
        gameObject1.SetActive(true);
        gameObject2.SetActive(true);
        gameObject3.SetActive(true);
        gameObject4.SetActive(true);
        gameObject5.SetActive(true);
        
        corretto.SetActive(false);
        sbagliato.SetActive(false);
    }


    public void SetCodice(int value)
    {
        codice = value;
        switch (codice)
        {
            case 1110:
                testo.text = "Monitora argini";
                break;
            case 1111:
                testo.text = "svuota zone alluzionate";
                break;
            case 1112:
                testo.text = "evacuazione cittadini";
                break;
            case 1113:
                testo.text = "punti di raccolta";
                break;
            case 1120:
                testo.text = "cerca-rimuovi tane";
                break;
            case 1121:
                testo.text = "pulizia fossi";
                break;
            case 1130:
                testo.text = "ambiente prime cure";
                break;
            case 1131:
                testo.text = "primo soccorso ferito";
                break;
            case 1140:
                testo.text = "regola traffico";
                break;
            case 1141:
                testo.text = "crea percorsi altrenativi";
                break;
            case 1150:
                testo.text = "salvataggio persone/aniamli";
                break;
        }
    }
    
    public void Click(int n)
    {
        gameObject1.GetComponent<Image>().color = Color.white;
        gameObject2.GetComponent<Image>().color = Color.white;
        gameObject3.GetComponent<Image>().color = Color.white;
        gameObject4.GetComponent<Image>().color = Color.white;
        gameObject5.GetComponent<Image>().color = Color.white;
        switch (n)
        {
            case 1:
                selected = 1;
                gameObject1.GetComponent<Image>().color = Color.green;
                break;
            case 2:
                selected = 2;
                gameObject2.GetComponent<Image>().color = Color.green;
                break;
            case 3:
                selected = 3;
                gameObject3.GetComponent<Image>().color = Color.green;
                break;
            case 4:
                selected = 4;
                gameObject4.GetComponent<Image>().color = Color.green;
                break;
            case 5:
                selected = 5;
                gameObject5.GetComponent<Image>().color = Color.green;
                break;
        }
    }

    public void Controlla()
    {
        if(selected == 0)
            return;
    
        buttAvanti.SetActive(true);
        buttInvia.SetActive(false);
    
        gameObject1.SetActive(false);
        gameObject2.SetActive(false);
        gameObject3.SetActive(false);
        gameObject4.SetActive(false);
        gameObject5.SetActive(false);

    
        //1 refPC
        //2 refCri
        //3 refGGEV
        //4 refPolizia
        //5 refFuoco
        switch (codice)
        {
            case 1110:
                if (selected == 1)
                    corretto.SetActive(true); 
                else
                    sbagliato.SetActive(true);
                RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":10}");
                break;
            case 1111:
                if (selected == 1)
                    corretto.SetActive(true); 
                else
                    sbagliato.SetActive(true);
                RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":11}");
                break;
            case 1112:
                if (selected == 1)
                    corretto.SetActive(true); 
                else
                    sbagliato.SetActive(true);
                RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":12}");
                break;
            case 1113:
                if (selected == 1)
                    corretto.SetActive(true); 
                else
                    sbagliato.SetActive(true);
                RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":13}");
                break;
            case 1120:
                if (selected == 3)
                    corretto.SetActive(true); 
                else
                    sbagliato.SetActive(true);
                RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":20}");
                break;
            case 1121:
                if (selected == 3)
                    corretto.SetActive(true); 
                else
                    sbagliato.SetActive(true);
                RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":21}");
                break;
            case 1130:
                if (selected == 2)
                    corretto.SetActive(true); 
                else
                    sbagliato.SetActive(true);
                RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":30}");
                break;
            case 1131:
                if (selected == 2)
                    corretto.SetActive(true); 
                else
                    sbagliato.SetActive(true);
                RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":31}");
                break;
            case 1140:
                if (selected == 4)
                    corretto.SetActive(true); 
                else
                    sbagliato.SetActive(true);
                RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":40}");
                break;
            case 1141:
                if (selected == 4)
                    corretto.SetActive(true); 
                else
                    sbagliato.SetActive(true);
                RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":41}");
                break;
            case 1150:
                if (selected == 5)
                    corretto.SetActive(true); 
                else
                    sbagliato.SetActive(true);
                RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":50}");
                break;
        }
    
    }

    public void Avanti()
    {
        transform.parent.gameObject.SetActive(false);
        
        
        gameObject1.GetComponent<Image>().color = Color.white;
        gameObject2.GetComponent<Image>().color = Color.white;
        gameObject3.GetComponent<Image>().color = Color.white;
        gameObject4.GetComponent<Image>().color = Color.white;
        gameObject5.GetComponent<Image>().color = Color.white;
        
        buttAvanti.SetActive(false);
        buttInvia.SetActive(true);
    
        gameObject1.SetActive(true);
        gameObject2.SetActive(true);
        gameObject3.SetActive(true);
        gameObject4.SetActive(true);
        gameObject5.SetActive(true);
        
        corretto.SetActive(false);
        sbagliato.SetActive(false);
    }
    
}
