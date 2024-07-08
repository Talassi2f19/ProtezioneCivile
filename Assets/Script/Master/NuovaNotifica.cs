using System;
using UnityEngine;

namespace Script.Master
{
    public class NotificationManager : MonoBehaviour
    {
        // Evento statico per le notifiche
        public static event Action<string> OnNotificationReceived;

        // Metodo statico per inviare notifiche
        public static void Notify(string message)
        {
            OnNotificationReceived?.Invoke(message);
        }
    }
    
    
    public class NotificationReceiver : MonoBehaviour
    {
        [SerializeField] private NotificaManager notificaManager;
        void OnEnable()
        {
            NotificationManager.OnNotificationReceived += HandleNotification;
        }

        void OnDisable()
        {
            NotificationManager.OnNotificationReceived -= HandleNotification;
        }

        void HandleNotification(string message)
        {
            Debug.Log("Notifica ricevuta: " + message);
           // notificaManager.AggiungiMessaggi(message);
        }
    }

}