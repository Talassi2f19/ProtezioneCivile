using System;
using Proyecto26;
using Script.User;
using Script.Utility;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = Script.Utility.Scene;

namespace Script.Master
{
    public class GenRuoli : MonoBehaviour
    {
        private Listeners listeners;
        [SerializeField]private GeneraRuoli gg;

        private void Start()
        {
            listeners = new Listeners(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + ".json");
            listeners.Start(WaitGen);
        }

        private void WaitGen(string str)
        {
            Debug.LogWarning(str);
            if (str.Contains(Ruoli.Coc.ToString()))
            {
                listeners.Stop();
                Genera();
            }
        }
        
        private void Genera()
        {
            gg.Genera();
        }
    }
}
