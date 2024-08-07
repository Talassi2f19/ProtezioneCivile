using Proyecto26;
using Script.Utility;
using UnityEngine;

namespace Script.User
{
    public class Candidatura : MonoBehaviour
    {
        [SerializeField] private GameObject candidatura;
        [SerializeField] private GameObject testoOnClick;
        
        private void Start()
        {
            candidatura.SetActive(false);
            testoOnClick.SetActive(false);
        }

        public void ClickCandidati()
        {
            //Viene inviato il voto al database
            string str = "{\"" + Info.localUser.name + "\":0}";
            RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.CandidatiFolder + ".json", str).Catch(Debug.LogError);
            candidatura.SetActive(false); //Scompare il pulsante
            testoOnClick.SetActive(true);
        }
        public void ClickNonCandidati()
        {
            candidatura.SetActive(false); //Scompare il pulsante
            testoOnClick.SetActive(true);
        }
    }
}