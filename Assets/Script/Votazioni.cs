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
        private List<string> keyName;
        private List<GameObject> pulsantiCandidati = new List<GameObject>();
        public GameObject pulsantePrefab;

        public Transform parent;
        // Start is called before the first frame update
        void Start()
        {
            RestClient.Get(Info.DBUrl + Info.SessionCode + "/candidati.json").Then(s =>
            {
                JSONObject jsonCandidati = new JSONObject(s.Text);
                keyName = jsonCandidati.keys;
                Vector2 coord = Vector2.Zero; 
                for (int i = 0; i < keyName.Count; i++)
                {
                    Debug.Log(keyName[i]);
                    pulsantiCandidati.Add(GameObject.Instantiate(pulsantePrefab, parent));
                    pulsantiCandidati[i].GetComponent<PulsanteVotazioni>().SetName(keyName[i]);

                    Vector3 coords = pulsantiCandidati[i].GetComponent<PulsanteVotazioni>().getCoords();
                    coord.X += 50 + coords.x;
                    if (coord.X > 300)
                    {
                        coord.Y += 50;
                        coord.X = 0;
                    }
                    Debug.Log("cord: "+coord.X  +"   "+ coord.Y);
                    pulsantiCandidati[i].transform.position = new Vector3(coord.X, coord.Y, 0);
                }
            });
        }

        
        public void HasVoted()
        {
            foreach (GameObject pulsante in pulsantiCandidati)
                pulsante.GetComponent<PulsanteVotazioni>().SetOff();
        }
    }
}
