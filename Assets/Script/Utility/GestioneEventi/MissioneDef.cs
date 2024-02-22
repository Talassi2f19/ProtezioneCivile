using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Utility.GestioneEventi
{
    [CreateAssetMenu(fileName = "Missione", menuName = "Missione", order = 1)]
    public class MissioneDef : ScriptableObject
    {
        public string nome;
        public List<FaseDef> fasi;
    }

    [Serializable]
    public class FaseDef
    {
        public List<RuoliEnum> Ruoli;
    }
}