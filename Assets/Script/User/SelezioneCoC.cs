using System.Collections.Generic;
using Defective.JSON;
using Proyecto26;
using Script.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.User
{
    public class SelezioneCoC : MonoBehaviour
    {
        [SerializeField] private GameObject schermataRuoloPrefab;

        [SerializeField] private GameObject buttonPrefab;
        [SerializeField] private Transform buttonParent;
        private Dictionary<string, JSONObject> player = new Dictionary<string, JSONObject>();
        void Start()
        {
            schermataRuoloPrefab.SetActive(true);

            RestClient.Get(Info.DBUrl + Info.sessionCode + "/players.json").Then(e =>
            {
                JSONObject json = new JSONObject(e.Text);
                player = json.ToJsonDictionary();
            });
            Genera();
        }

        private void Genera()
        {
            player.Remove(Info.localUser.name);

            foreach(var pl in player)
            {
                GameObject pulsante =  GameObject.Instantiate(buttonPrefab, buttonParent);
                pulsante.GetComponent<PulsanteGiocatore>().SetName(pl.Key);
                //pl.Key -> nome player
            }
        }

        // il cambio scena se ne occupa il prefab del pulsante
        //public void PlayerHasSelected()
        //{
        //    SceneManager.LoadScene("_Scenes/user/game");
        //}
    }
}
