using Defective.JSON;
using Proyecto26;
using Script.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelezioneCoC : MonoBehaviour
{
    [SerializeField] private GameObject mostraRuoloPrefab;
    [SerializeField] private Transform mostraRuoloPrefabParent;

    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform buttonParent;
    private Dictionary<string, JSONObject> player = new Dictionary<string, JSONObject>();
    void Start()
    {

        GameObject mostraRuolo =  GameObject.Instantiate(mostraRuoloPrefab, mostraRuoloPrefabParent);
    //    mostraRuolo.GetComponent<MostraRuolo>().setRoleName(Info.LocalUser.role);
       /* mostraRuolo.GetComponent<MostraRuolo>().setRoleDescription("aasfafasfasfasf");
        mostraRuolo.GetComponent<MostraRuolo>().setRoleName("path/:.jojo/aihsfias");*/

        RestClient.Get(Info.DBUrl + Info.SessionCode + "/players.json").Then(e =>
        {
            JSONObject json = new JSONObject(e.Text);
            player = json.ToJSONDictionary();
        });
        Genera();
    }

    private void Genera()
    {
        player.Remove(Info.LocalUser.name);

        foreach(var pl in player)
        {
           GameObject pulsante =  GameObject.Instantiate(buttonPrefab, buttonParent);
           pulsante.GetComponent<PulsanteGiocatore>().SetName(pl.Key);
           //pl.Key -> nome player
        }
    }

    public void PlayerHasSelected()
    {
        SceneManager.LoadScene("_Scenes/user/game");
    }
}
