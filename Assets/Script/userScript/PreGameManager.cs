using Proyecto26;
using Script.Utility;
using Unity.VisualScripting;
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
        private Listeners AmIRemoved;
        
        private void Start()
        {
            wait.SetActive(true);
            candidatura.SetActive(false);
            votazione.SetActive(false);

            GameIsStarted = new Listeners(Info.DBUrl + Info.SessionCode +"/gameStatusCode.json");
            GameIsStarted.Start(GameStatus);

            AmIRemoved = new Listeners(Info.DBUrl + Info.SessionCode + "/players/" + Info.LocalUser.name + ".json");
            AmIRemoved.Start(CheckRemoved);
        }

        private void GameStatus(string str)
        {
             Debug.Log(str);
             if(str.Contains("put"))
                 str = str.Split("\"data\":\"")[1].Split("\"}")[0];
             Debug.Log(str);
             switch (str)
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
                     GameIsStarted.Stop();
                     SceneManager.LoadScene("_Scenes/user/risultatiElezioni");
                     break;
                 
                 case Info.GameStatus.Gioco:
                     GameIsStarted.Stop();
                     SceneManager.LoadScene("_Scenes/user/game");
                     break;
                 
                 case Info.GameStatus.End:
                     GameIsStarted.Stop();
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

        private void CheckRemoved(string str)
        {
            if (str.Contains("\"data\":null"))
                GameStatus(Info.GameStatus.End);
                
        }
        
        //azione per il pulsante dello stato waitPlayer
        public void OnLeave()
        {
            RestClient.Delete(Info.DBUrl + Info.SessionCode + "/players/" + Info.LocalUser.name + ".json");
            GameStatus(Info.GameStatus.End);
        }
        
    }
}

