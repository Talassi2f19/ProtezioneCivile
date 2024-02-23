using UnityEngine;

namespace Script.Utility.GestioneEventi
{
    [CreateAssetMenu(fileName = "Ruolo", menuName = "Ruolo", order = 1)]
    public class RuoloObj : ScriptableObject
    {
        public Ruoli ruolo;
        public string descrizione;
        public string immagine;
    }
}