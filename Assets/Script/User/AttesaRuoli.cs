using System;
using FirebaseListener;
using Proyecto26;
using Script.Utility;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = Script.Utility.Scene;

namespace Script.User
{
    public class AttesaRuoli : MonoBehaviour
    {
        Listeners listeners;

        private void Start()
        {
            listeners = new Listeners(Info.DBUrl + Info.sessionCode + "/" + Global.GameStatusCodeKey + ".json");
            listeners.Start(IniziaPartita);
        }

        private void OnApplicationQuit()
        {
            listeners.Stop();
        }

        private void IniziaPartita(string str)
        {
            if (str.Contains(GameStatus.Gioco))
            {
                listeners.Stop();
                RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + "/Role.json").Then(e =>
                {
                    Info.localUser.role = Enum.Parse<Ruoli>(e.Text.Replace("\"", ""));
                    SceneManager.LoadScene(Scene.User.Game);
                });
                
            }
        }
    }
}
