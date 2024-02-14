using Proyecto26;
using Script.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            gameIsStarted = new Listeners(Info.DBUrl + Info.sessionCode +"/gameStatusCode.json");
            gameIsStarted.Start(GameStatus);

            amIRemoved = new Listeners(Info.DBUrl + Info.sessionCode + "/players/" + Info.localGenericUser.name + ".json");
            amIRemoved.Start(CheckRemoved);
            
            wait.SetActive(true);
            candidatura.SetActive(false);
            votazione.SetActive(false);
        }

        private void GameStatus(string status)
        {
             if(status.Contains("put"))
                 status = status.Split("\"data\":\"")[1].Split("\"}")[0];
             Debug.Log(status);
             
             switch (status)
             {
                 case Info.GameStatus.WaitPlayer:
                     Set(Info.GameStatus.WaitPlayer);
                     break;
                 
                 case Info.GameStatus.Candidatura:
                     Set(Info.GameStatus.Candidatura);
#if !UNITY_EDITOR
                WebGL.SetCookie("user="+Info.LocalUser.name);
#endif
                     break;
                 
                 case Info.GameStatus.Votazione:
                     Set(Info.GameStatus.Votazione);
                     break;
                 
                 case Info.GameStatus.RisultatiElezioni:
                     gameIsStarted.Stop();
                     SceneManager.LoadScene("_Scenes/user/risultatiElezioni");
                     break;
                 
                 case Info.GameStatus.Gioco:
                     gameIsStarted.Stop();
                     SceneManager.LoadScene("_Scenes/user/game");
                     break;
                 
                 case Info.GameStatus.End:
                     gameIsStarted.Stop();
                     SceneManager.LoadScene("_Scenes/user/login");
                     Info.Reset();
                     break;
             }
        }

        private void Set(string val)
        {
            wait.SetActive(val == Info.GameStatus.WaitPlayer);
            candidatura.SetActive(val == Info.GameStatus.Candidatura);
            votazione.SetActive(val == Info.GameStatus.Votazione);
        }

        private void CheckRemoved(string response)
        {
            if (response.Contains("\"data\":null"))
            {
                GameStatus(Info.GameStatus.End);
                amIRemoved.Stop();
            }
        }
        
        //Quando il giocatore abbandona la sessione
        public void OnLeave()
        {
            RestClient.Delete(Info.DBUrl + Info.sessionCode + "/players/" + Info.localGenericUser.name + ".json");
            GameStatus(Info.GameStatus.End);
        }
    }
}

