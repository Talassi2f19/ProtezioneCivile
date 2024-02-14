using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using Script.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

public class elezione : MonoBehaviour
{
    public void TerminaCandidatura()
    {
        string str = "{\"gameStatusCode\":\"" + Info.GameStatus.Votazione + "\"}";
        RestClient.Patch(Info.DBUrl + Info.SessionCode + ".json", str);
        SceneManager.LoadScene("_Scenes/master/votazione");
    }

}
