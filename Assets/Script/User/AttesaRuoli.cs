using Script.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.User
{
    public class AttesaRuoli : MonoBehaviour
    {
        Listeners listeners;
        void Start()
        {
    
            listeners = new Listeners(Info.DBUrl + Info.SessionCode + "/players/" + Info.localGenericUser.name + "/role.json");
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
                Info.localGenericUser.role = str;
                SceneManager.LoadScene("_Scenes/user/game");
            }
        }

    }
}
