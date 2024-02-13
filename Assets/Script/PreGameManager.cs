using Proyecto26;
using Script.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
// ReSharper disable CommentTypo IdentifierTypo
namespace Script
{
    //classe per cambiare la fase di gioco 
    public class PreGameManager : MonoBehaviour
    {
        [SerializeField] private GameObject wait;
        [SerializeField] private GameObject candidatura;
        [SerializeField] private GameObject votazione;
        private Listeners GameIsStarted;
    
        private void Start()
        {
            wait.SetActive(true);
            candidatura.SetActive(false);
            votazione.SetActive(false);

            GameIsStarted = new Listeners(Info.DBUrl + Info.SessionCode +"/gameStatusCode.json");
            GameIsStarted.Start(GameStatus);
        }

        private void GameStatus(string str)
        {
             Debug.Log(str);
            //controllo stato del gioco
            if (str.Contains(Info.GameStatus.WaitPlayer))
            {
                Set(Info.GameStatus.WaitPlayer);
            } 
            else if (str.Contains(Info.GameStatus.Candidatura))
            {
                Set(Info.GameStatus.Candidatura);
            } 
            else if (str.Contains(Info.GameStatus.Votazione))
            {
                //salva il nome del player per la riconnessione
#if !UNITY_EDITOR
                WebGL.SetCookie("user="+Info.LocalUser.name);
#endif
                Set(Info.GameStatus.Votazione);
            } 
            else if (str.Contains(Info.GameStatus.RisultatiElezioni))
            {
                GameIsStarted.Stop();
                SceneManager.LoadScene("risultatiElezioni");

            }
            else if (str.Contains(Info.GameStatus.Gioco))
            {
                GameIsStarted.Stop();
                SceneManager.LoadScene("game");
            }
            else if (str.Contains(Info.GameStatus.End))
            {
                GameIsStarted.Stop();
                SceneManager.LoadScene("login");
                Info.Reset();
            }
        }

        private void Set(string val)
        {
            wait.SetActive(val == Info.GameStatus.WaitPlayer);
            candidatura.SetActive(val == Info.GameStatus.Candidatura);
            votazione.SetActive(val == Info.GameStatus.Votazione);
        }
        
        //azione per il pulsante dello stato waitPlayer
        public void OnLeave()
        {
            RestClient.Delete(Info.DBUrl + Info.SessionCode + "/players/" + Info.LocalUser.name + ".json");
            GameStatus(Info.GameStatus.End);
        }
        
        
        
    }
}

