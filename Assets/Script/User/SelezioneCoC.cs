using System.Collections.Generic;
using Defective.JSON;
using Proyecto26;
using Script.User.Prefabs;
using Script.Utility;
using UnityEngine;


namespace Script.User
{
    public class SelezioneCoC : MonoBehaviour
    {
        [SerializeField] private GameObject buttonPrefab;
        [SerializeField] private Transform buttonParent;
        private List<string> players = new List<string>();
        
        void Start()
        {
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + ".json").Then(e =>
            {
                JSONObject json = new JSONObject(e.Text);
               // player = json.ToJsonDictionary();
                players = json.keys;
                Genera();
            }).Catch(Debug.LogError);
            
        }

        private void Genera()
        {
            // Debug.Log("Genera selezione COC");
            players.Remove(Info.localUser.name);
            // Debug.Log("Numero player:" + players.Count);
            
            foreach(var pl in players)
            {
                GameObject pulsante =  GameObject.Instantiate(buttonPrefab, buttonParent);
                pulsante.GetComponent<PulsanteGiocatore>().SetName(pl);
                // Debug.Log("Nome giocatore: " + pl);
            }
        }

        // il cambio scena se ne occupa il prefab del pulsante
        //public void PlayerHasSelected()
        //{
        //    SceneManager.LoadScene("_Scenes/user/game");
        //}
    }
}
