using System.Collections;
using System.Collections.Generic;
using Script.Master;
using UnityEngine;

public class AddNotifica : MonoBehaviour
{
    [SerializeField]private MessaggiMaster _messaggiMaster;

    public void SetMessaggio(string testo)
    {
        _messaggiMaster.AggiungiMessaggi(testo);
    }
}
