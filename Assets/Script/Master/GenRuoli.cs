using System;
using FirebaseListener;
using Proyecto26;
using Script.User;
using Script.Utility;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Scene = Script.Utility.Scene;

namespace Script.Master
{
    public class GenRuoli : MonoBehaviour
    {
        private Listeners listeners;
        [FormerlySerializedAs("gg")] [SerializeField]private GeneraRuoli generaRuoli;

        private void Start()
        {
            listeners = new Listeners(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + ".json");
            listeners.Start(WaitGen);
        }

        private void WaitGen(string str)
        {
            if (str.Contains(Ruoli.Coc.ToString()))
            {
                listeners.Stop();
                generaRuoli.Genera();
            }
        }
    }
}
