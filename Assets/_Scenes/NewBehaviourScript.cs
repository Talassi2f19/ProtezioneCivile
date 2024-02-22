using System;
using System.Collections;
using System.Collections.Generic;
using Script.Utility;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void Start()
    {
        string ss = "Null";
       Debug.Log(Enum.Parse<Ruoli>(ss) == Ruoli.Null);
    }




}
