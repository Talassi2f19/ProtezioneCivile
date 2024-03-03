using Proyecto26;
using Script.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = Script.Utility.Scene;

// ReSharper disable CommentTypo IdentifierTypo
namespace Script.User
{
    //classe per cambiare la fase di gioco 
    public class PreGameManager : MonoBehaviour
    {
        [SerializeField] private GameObject wait;
        [SerializeField] private GameObject candidatura;
        [SerializeField] private GameObject votazione;
        
        private Listeners gameIsStarted;
        private Listeners amIRemoved;
        
        private void Start()
        {
            gameIsStarted = new Listeners(Info.DBUrl + Info.sessionCode + "/" + Global.GameStatusCodeKey + ".json");
            gameIsStarted.Start(Game);

            amIRemoved = new Listeners(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + ".json");
            amIRemoved.Start(CheckRemoved);
            
            wait.SetActive(true);
            candidatura.SetActive(false);
            votazione.SetActive(false);
        }

        private void Game(string status)
        {
             if(status.Contains("put"))
                 status = status.Split("\"data\":\"")[1].Split("\"}")[0];
             Debug.Log(status);
             
             switch (status)
             {
                 case GameStatus.WaitPlayer:
                     Set(GameStatus.WaitPlayer);
                     break;
                     
                 case GameStatus.Candidatura:
                     Set(GameStatus.Candidatura);
#if !UNITY_EDITOR
                WebGL.SetCookie("user=" + Info.localUser.name);
#endif
                     break;
                 
                 case GameStatus.Votazione:
                     Set(GameStatus.Votazione);
                     votazione.GetComponent<Votazioni>().MostraCandidati();
                     break;
                 
                 case GameStatus.RisultatiElezioni:
                     gameIsStarted.Stop();
                     SceneManager.LoadScene(Scene.User.RisultatiElezioni);
                     break;
                 
                 case GameStatus.Gioco:
                     gameIsStarted.Stop();
                     SceneManager.LoadScene(Scene.User.Game);
                     break;
                 
                 case GameStatus.End:
                     gameIsStarted.Stop();
                     SceneManager.LoadScene(Scene.User.Login);
                     Info.Reset();
                     break;
             }
        }

        private void Set(string val)
        {
            wait.SetActive(val == GameStatus.WaitPlayer);
            candidatura.SetActive(val == GameStatus.Candidatura);
            votazione.SetActive(val == GameStatus.Votazione);
        }

        private void CheckRemoved(string response)
        {
            if (response.Contains("\"data\":null"))
            {
                Game(GameStatus.End);
                amIRemoved.Stop();
            }
        }
        
        //Quando il giocatore abbandona la sessione
        public void OnLeave()
        {
            RestClient.Delete(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + ".json");
            Game(GameStatus.End);
        }
    }
}

