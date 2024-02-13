using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class elezione : MonoBehaviour
{
    public void TerminaCandidatura()
    {
        SceneManager.LoadScene("_Scenes/master/votazione");
    }

}
