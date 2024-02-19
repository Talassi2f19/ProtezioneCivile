using Proyecto26;
using Script.Utility;
using UnityEngine;

namespace Script.User
{
    public class Candidatura : MonoBehaviour
    {
        [SerializeField] private GameObject pulsanteCandidatura;
        [SerializeField] private GameObject testoOnClick;
        
        private void Start()
        {
            pulsanteCandidatura.SetActive(true);
            testoOnClick.SetActive(false);
        }

        public void Click()
        {
            //Viene inviato il voto al database
            string str = "{\"" + Info.localUser.name + "\":0}";
            RestClient.Patch(Info.DBUrl + Info.sessionCode + "/candidati.json", str).Catch(exception =>
            {
                Debug.Log(exception);
                Debug.Log(exception.Message);
            });
            pulsanteCandidatura.SetActive(false); //Scompare il pulsante
            testoOnClick.SetActive(true);
        }
    }
}
//{"":""}