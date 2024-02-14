using System;
using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using Script.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

public class elezione : MonoBehaviour
{
    [SerializeField] private GameObject terminaVotazioni;
    [SerializeField] private GameObject avviaVotazioni;

    private void Start()
    {
        terminaVotazioni.SetActive(false);
        avviaVotazioni.SetActive(true);
    }

    public void AvviaVotazioni()
    {
        string str = "{\"gameStatusCode\":\"" + Info.GameStatus.Votazione + "\"}";
        RestClient.Patch(Info.DBUrl + Info.SessionCode + ".json", str);
        terminaVotazioni.SetActive(true);
        avviaVotazioni.SetActive(false);
    }
    
    public void MostraRisultati()
    {
        string str = "{\"gameStatusCode\":\"" + Info.GameStatus.RisultatiElezioni + "\"}";
        RestClient.Patch(Info.DBUrl + Info.SessionCode + ".json", str);
        SceneManager.LoadScene("_Scenes/master/risultatiElezioni");
    }
}
