using Script.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class attesaRuoli : MonoBehaviour
{
    Listeners listeners;
    void Start()
    {
    
        listeners = new Listeners(Info.DBUrl + Info.SessionCode + "/players/" + Info.LocalUser.name + "/role.json");
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
            Info.LocalUser.role = str;
            SceneManager.LoadScene("_Scenes/user/game");
        }
    }

}
