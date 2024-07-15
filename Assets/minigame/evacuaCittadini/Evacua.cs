using System;
using System.Collections;
using System.Collections.Generic;
using Script.User;
using UnityEngine;

public class Evacua : MonoBehaviour
{
    private bool inside;

    private GameObject objInside;
    
    private void Start()
    {
        inside = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        inside = true;
        objInside = other.gameObject;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        inside = false;
    }

    private void Update()
    {
        if (inside && objInside.GetComponent<PlayerLocal>().GetDirection().y > 0.8f)
        {
            inside = false;
            fineStage1Callback.Invoke();
        }
    }

    public delegate void FineStage1Callback();
    private FineStage1Callback fineStage1Callback;

    public void FineStage1(FineStage1Callback tmp)
    {
        fineStage1Callback = tmp;
    }
}
