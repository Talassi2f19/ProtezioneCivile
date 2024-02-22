using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GenericPopUp : MonoBehaviour
{
    private string testo;
    [SerializeField] private TMP_Text testoPopUp;

    public void setTestoPopUp(string testo)
    {
        this.testo = testo;
        testoPopUp.text = this.testo;
    }
    
    public void ExitPopUp()
    {
        gameObject.SetActive(false);
    }
}
