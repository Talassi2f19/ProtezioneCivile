using Proyecto26;
using Script.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = Script.Utility.Scene;

// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
public class LogicaGame : MonoBehaviour
{
    [SerializeField] private GameObject menuIcon;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject terminaSelection;
    [SerializeField] private GameObject giocatori;
    [SerializeField] private GameObject azioni;
    [SerializeField] private GameObject messaggi;
    [SerializeField] private GameObject back;


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
        menu.SetActive(false);
        terminaSelection.SetActive(true);
    }

    public void TerminaConferma()
    {
        RestClient.Post(Info.DBUrl + Info.sessionCode + "/Game/Task.json", "{\"CodeTask\":-1}").Catch(Debug.LogError);
        string str = "{\"" + Global.GameStatusCodeKey + "\":\"" + GameStatus.End + "\"}";
        RestClient.Patch(Info.DBUrl + Info.sessionCode + ".json", str).Catch(Debug.LogError);
        SceneManager.LoadScene(Scene.Master.EndGame);
    }

    public void TerminaAnnulla()
    {
        CloseMenu();
        terminaSelection.SetActive(false);
    }

    public void GiocatoriMostra()
    {
        CloseMenu();
        back.SetActive(true);
        giocatori.SetActive(true);
    }

    public void AzioniMostra()
    {
        CloseMenu();
        back.SetActive(true);
        azioni.SetActive(true);
    }

    public void MessaggiMostra()
    {
        CloseMenu();
        back.SetActive(true);
        messaggi.SetActive(true);
    }
    
    public void GiocatoriChiudi()
    {
        back.SetActive(false);
        giocatori.SetActive(false);
    }

    public void AzioniChiudi()
    {
        back.SetActive(false);
        azioni.SetActive(false);
    }

    public void MessaggiChiudi()
    {
        back.SetActive(false);
        messaggi.SetActive(false);
    }
}
