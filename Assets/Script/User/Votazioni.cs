using System.Collections.Generic;
using Defective.JSON;
using Proyecto26;
using Script.User.Prefabs;
using Script.Utility;
using UnityEngine;
// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo
namespace Script.User
{
    public class Votazioni : MonoBehaviour
    {
        private List<string> nomiGiocatori;
        private List<GameObject> pulsantiVotazioni = new List<GameObject>();
        [SerializeField] private GameObject pulsantePrefab;
        [SerializeField] private Transform contenitore;
        
        
        public void MostraCandidati()
        {
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.CandidatiFolder + ".json").Then(onReceived =>
            {
                JSONObject listaCandidati = new JSONObject(onReceived.Text);
                nomiGiocatori = listaCandidati.keys;
                
                Debug.Log("Sei passato per mostra candidati, quindi teoricamente dovresti vedere che tutti i giocatori" +
                          "si sono candidati, li vedi? Ti conviene mannaggia a te e Unity e alla protezione civile...");
                
              //  GameObject tmp;
                for (int i = 0; i < nomiGiocatori.Count; i++)
                {
                    pulsantiVotazioni.Add(GameObject.Instantiate(pulsantePrefab, contenitore));
                    pulsantiVotazioni[i].GetComponent<PulsanteGiocatore>().SetName(nomiGiocatori[i]);
                    /*tmp = GameObject.Instantiate(pulsantePrefab, contenitore);
                    tmp.GetComponent<PulsanteGiocatore>().SetName(nomiGiocatori[i]);*/
                }
            });
        }

     /*   
        public void PlayerHasSelected()
        {
            foreach (GameObject pulsante in pulsantiVotazioni)
                pulsante.GetComponent<PulsanteGiocatore>().SetOff();
        }
        
        */
    }
}
