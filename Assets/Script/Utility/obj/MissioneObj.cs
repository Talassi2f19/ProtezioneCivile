using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Utility.GestioneEventi
{
    [CreateAssetMenu(fileName = "Missione", menuName = "Missione", order = 1)]
    public class MissioneObj : ScriptableObject
    {
        public string nomeMissione;
        public List<FaseObj> fasi;
    }

    [Serializable]
    public class FaseObj
    {
        public List<Ruoli> Ruoli;
    }
}