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
                    pulsantiVotazioni[i].GetComponent<PulsanteVotazioni>().SetName(nomiGiocatori[i]);

                    pulsantiVotazioni[i].transform.position = new Vector3(100, 100 * i, 0);

                    /*  Vector3 coords = pulsantiVotazioni[i].GetComponent<PulsanteVotazioni>().getCoords();
                      coord.X += 50 + coords.x;
                      if (coord.X > 300)
                      {
                          coord.Y += 50;
                          coord.X = 0;
                      }
                      Debug.Log("cord: "+coord.X  +"   "+ coord.Y);
                      pulsantiVotazioni[i].transform.position = new Vector3(coord.X, coord.Y, 0);*/
                }
            });
        }

        
        public void PlayerHasVoted()
        {
            foreach (GameObject pulsante in pulsantiVotazioni)
                pulsante.GetComponent<PulsanteVotazioni>().SetOff();
        }
    }
}
