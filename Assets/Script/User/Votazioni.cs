using System.Collections.Generic;
using Defective.JSON;
using Proyecto26;
using Script.Utility;
using UnityEngine;

namespace Script.User
{
    public class Votazioni : MonoBehaviour
    {
        private List<string> nomiGiocatori;
        private List<GameObject> pulsantiVotazioni = new List<GameObject>();
        [SerializeField] private GameObject pulsantePrefab;
        [SerializeField] private Transform contenitore;
        
        private void Start()
        {
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/candidati.json").Then(onReceived =>
            {
                JSONObject listaCandidati = new JSONObject(onReceived.Text);
                nomiGiocatori = listaCandidati.keys;
                
                for (int i = 0; i < nomiGiocatori.Count; i++)
                {
                    Debug.Log(nomiGiocatori[i]);
                    pulsantiVotazioni.Add(GameObject.Instantiate(pulsantePrefab, contenitore));
                    pulsantiVotazioni[i].GetComponent<PulsanteGiocatore>().SetName(nomiGiocatori[i]);
                }
            });
        }

        
        public void PlayerHasSelected()
        {
            foreach (GameObject pulsante in pulsantiVotazioni)
                pulsante.GetComponent<PulsanteGiocatore>().SetOff();
        }
    }
}
