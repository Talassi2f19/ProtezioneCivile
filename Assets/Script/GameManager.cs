using Proyecto26;
using Script.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
// ReSharper disable CommentTypo IdentifierTypo
namespace Script
{
    //classe per cambiare la fase di gioco 
    public class GameManager : MonoBehaviour
    {
        public GameObject game;
        public GameObject wait;
        public GameObject candidatura;
        public GameObject votazione;
        public GameObject genericWait;

        private Listeners GameIsStarted;
    
        private void Start()
        {
            wait.SetActive(true);
            game.SetActive(false);
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
            } else if (str.Contains(Info.GameStatus.Votazione))
            {
                Set(Info.GameStatus.Votazione);;
            } else if (str.Contains(Info.GameStatus.Candidatura))
            {
                //salva il nome del player per la riconnessione
#if !UNITY_EDITOR
                WebGL.SetCookie("user="+Info.LocalUser.name);
#endif
                Set(Info.GameStatus.Candidatura);
            } else if (str.Contains(Info.GameStatus.Gioco))
            {
                Set(Info.GameStatus.Gioco);
                
            } else if (str.Contains(Info.GameStatus.End))
            {
                GameIsStarted.Stop();
                SceneManager.LoadScene("login");
                Info.Reset();
            } else if (str.Contains(Info.GameStatus.GenericWait))
            {
                Set(Info.GameStatus.GenericWait);
            }
        }

        private void Set(string val)
        {
            wait.SetActive(val == Info.GameStatus.WaitPlayer);
            game.SetActive(val == Info.GameStatus.Gioco);
            candidatura.SetActive(val == Info.GameStatus.Candidatura);
            votazione.SetActive(val == Info.GameStatus.Votazione);
            genericWait.SetActive(val == Info.GameStatus.GenericWait);
        }
        
        //azione per il pulsante dello stato waitPlayer
        public void OnLeave()
        {
            RestClient.Delete(Info.DBUrl + Info.SessionCode + "/players/" + Info.LocalUser.name + ".json");
            GameStatus(Info.GameStatus.End);
        }
        
    }
}

