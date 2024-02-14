using System;
using Defective.JSON;
using Proyecto26;
using Script.Utility;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using Vector2 = System.Numerics.Vector2;

namespace Script
{
    public class Votazioni : MonoBehaviour
    {
        private List<string> nomiGiocatori;
        private List<GameObject> pulsantiVotazioni = new List<GameObject>();
        [SerializeField] private GameObject pulsantePrefab;
        [SerializeField] private Transform contenitore;
        
        void Start()
        {
            RestClient.Get(Info.DBUrl + Info.SessionCode + "/candidati.json").Then(OnReceived =>
            {
                JSONObject listaCandidati = new JSONObject(OnReceived.Text);
                nomiGiocatori = listaCandidati.keys;
                Vector2 coord = Vector2.Zero;
                
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
