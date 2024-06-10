using Script.Master;
using UnityEngine;

namespace _Scenes.User.telefono
{
    public class AddNotifica : MonoBehaviour
    {
        [SerializeField]private MessaggiMaster _messaggiMaster;

        public void SetMessaggio(string testo)
        {
            _messaggiMaster.AggiungiMessaggi(testo);
        }
    }
}
