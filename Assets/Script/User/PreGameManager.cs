using System;
using FirebaseListener;
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
        [SerializeField] private GameObject testoOnClick;
        
        private Listeners gameIsStarted;
        private Listeners amIRemoved;
        
        private void Start()
        {
            wait.SetActive(true);
            candidatura.SetActive(false);
            votazione.SetActive(false);
            gameIsStarted = new Listeners(Info.DBUrl + Info.sessionCode + "/" + Global.GameStatusCodeKey + ".json");
            gameIsStarted.Start(Game);

            amIRemoved = new Listeners(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + ".json");
            amIRemoved.Start(CheckRemoved);
        }

        private void OnApplicationQuit()
        {
            amIRemoved.Stop();
            gameIsStarted.Stop();
        }

        private void Game(string status)
        {
             if(status.Contains("put"))
                 status = status.Split("\"data\":\"")[1].Split("\"}")[0];
             // Debug.Log(status);
             
             switch (status)
             {
                 case GameStatus.WaitPlayer:
                     Set(GameStatus.WaitPlayer);
                     break;
                     
                 case GameStatus.Candidatura:
                     Set(GameStatus.Candidatura);
                     break;
                 
                 case GameStatus.Votazione:
                     Set(GameStatus.Votazione);
                     votazione.GetComponent<Votazioni>().MostraCandidati();
                     break;
                 
                 case GameStatus.RisultatiElezioni:
                     gameIsStarted.Stop();
                     amIRemoved.Stop();
                     SceneManager.LoadScene(Scene.User.RisultatiElezioni);
                     break;
                 
                 case GameStatus.Gioco:
                     gameIsStarted.Stop();
                     amIRemoved.Stop();
                     SceneManager.LoadScene(Scene.User.Game);
                     break;
                 
                 case GameStatus.GenRuoli:
                     gameIsStarted.Stop();
                     amIRemoved.Stop();
                     SceneManager.LoadScene(Scene.User.AttesaRuoli);
                     break;
                 
                 case GameStatus.End:
                     gameIsStarted.Stop();
                     amIRemoved.Stop();
                     SceneManager.LoadScene(Scene.User.Login);
                     Info.Reset();
                     break;
             }
        }

        private void Set(string val)
        {
            // Debug.Log(val);
            wait.SetActive(val == GameStatus.WaitPlayer);
            candidatura.SetActive(val == GameStatus.Candidatura);
            votazione.SetActive(val == GameStatus.Votazione);
            testoOnClick.SetActive(false);
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
            RestClient.Delete(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + ".json").Catch(Debug.LogError);
            Game(GameStatus.End);
        }
        
        
        
        
        public void ClickCandidati()
        {
            //Viene inviato il voto al database
            string str = "{\"" + Info.localUser.name + "\":0}";
            RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.CandidatiFolder + ".json", str).Catch(Debug.LogError);
            candidatura.SetActive(false); //Scompare il pulsante
            testoOnClick.SetActive(true);
        }
        public void ClickNonCandidati()
        {
            candidatura.SetActive(false); //Scompare il pulsante
            testoOnClick.SetActive(true);
        }
        
        
        
    }
}

