using System;
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
            listeners.Start(TrovaRuolo);
        }

        private void OnApplicationQuit()
        {
            listeners.Stop();
        }

        private void TrovaRuolo(string str)
        {
            if (str.Contains(GameStatus.Gioco))
            {
                listeners.Stop();
                SceneManager.LoadScene(Scene.User.Game);
            }
        }
    }
}
