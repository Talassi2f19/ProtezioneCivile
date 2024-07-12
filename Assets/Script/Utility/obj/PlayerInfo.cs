using UnityEngine;
using UnityEngine.UI;

namespace Script.Utility.obj
{
    
    [CreateAssetMenu(fileName = "PlayerInfo", menuName = "PlayerInfo", order = 1)]
    public class PlayerInfo : ScriptableObject
    {
        public Ruoli ruolo;
        public string Nome;
        public Sprite Immagine;
        [TextArea(3, 20)]
        public string Descrizione;
    }
}