using Script.Utility;
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
    
            listeners = new Listeners(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + "/" + Global.RuoloPlayerKey + ".json");
            //listeners = new Listeners(Info.DBUrl + "RPWB/players/ddd"  + "/role.json");
            listeners.Start(TrovaRuolo);
        }

        private void TrovaRuolo(string str)
        {
            Debug.Log(str);
            if (!str.Contains("null"))
            {
                listeners.Stop();
                str = str.Split("\"data\":\"")[1].Split("\"")[0];
                Info.localUser.role = str;
                SceneManager.LoadScene(Scene.User.Game);
            }
        }

    }
}
