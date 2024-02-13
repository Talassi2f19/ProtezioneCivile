using Proyecto26;
using Script.Utility;
using UnityEngine;

namespace Script
{
    public class Candidatura : MonoBehaviour
    {
        [SerializeField] private GameObject pulsante;
        [SerializeField] private GameObject testo;
        
        void Start()
        {
            pulsante.SetActive(true);
            testo.SetActive(false);
        }

        public void Click()
        {
            //Viene inviato il voto al database
            string str = "{\"" + Info.LocalUser.name + "\":0}";
            RestClient.Patch(Info.DBUrl + Info.SessionCode + "/candidati.json", str).Catch(exception =>
            {
                Debug.Log(exception);
                Debug.Log(exception.Message);
            });
            pulsante.SetActive(false); //Scompare il pulsante
            testo.SetActive(true);
        }
    }
}
//{"":""}