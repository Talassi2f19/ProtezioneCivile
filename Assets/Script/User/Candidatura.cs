using Proyecto26;
using Script.Utility;
using UnityEngine;

namespace Script.User
{
    public class Candidatura : MonoBehaviour
    {
        [SerializeField] private GameObject pulsanteCandidatura;
        [SerializeField] private GameObject testoOnClick;
        void Start()
        {
            pulsanteCandidatura.SetActive(true);
            testoOnClick.SetActive(false);
        }

        public void Click()
        {
            //Viene inviato il voto al database
            string str = "{\"" + Info.localGenericUser.name + "\":0}";
            RestClient.Patch(Info.DBUrl + Info.SessionCode + "/candidati.json", str).Catch(exception =>
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