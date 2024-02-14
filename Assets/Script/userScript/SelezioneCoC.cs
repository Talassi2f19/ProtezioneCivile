using Defective.JSON;
using Proyecto26;
using Script.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelezioneCoC : MonoBehaviour
{

    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform parent;
    private Dictionary<string, JSONObject> player = new Dictionary<string, JSONObject>();
    void Start()
    {
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
           GameObject hh =  GameObject.Instantiate(buttonPrefab, parent);
           
        }
    }


    public void PlayerHasSelected()
    {
        SceneManager.LoadScene("_Scenes/user/game");
    }

}
