using Script.Master;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scenes.User.telefono
{
    public class AddNotifica : MonoBehaviour
    {
        [SerializeField]private NotificaManager notificaManager;

        public void SetMessaggio(string testo)
        {
            notificaManager.AggiungiMessaggi(testo);
        }
    }
}
