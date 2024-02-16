using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnPulsanteCliccato()
    {
        // Trova tutti i figli del gameobject padre (presumibilmente i pulsanti)
        //Transform tt = gameObject.transform.parent;
        //Debug.Log(tt.childCount);
        //foreach (Transform child in tt.transform)
        //{
        //    // Disattiva ogni figlio (pulsante)
        //    child.gameObject.SetActive(false);
        //}
        HideAll();
    }

    private void Start()
    {
        Transform tt = transform;
        Debug.Log("--" + tt.childCount);
    }


    private void HideAll()
    {
        foreach (Transform child in gameObject.transform.parent.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

}
