using System.Collections.Generic;
using System.Linq;
using Defective.JSON;
using Proyecto26;
using Script;
using Script.Utility;
using TMPro;
using UnityEngine;

// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
//classe per la gestione della partita (master)
public class Master : MonoBehaviour
{

    public GameObject createGame;
    public GameObject gameManager;
    public TMP_Text sessionText;

    public GameObject playerPrefab;
    public Transform parent;
    
    private Listeners playerJoin;

    private Dictionary<string, GameObject> playerList = new Dictionary<string, GameObject>();


    public int gridSizeX = 5;
    public int gridSizeY = 3;
    public float spacing = 2f;
    
    private void Start()
    {
        createGame.SetActive(true);
        gameManager.SetActive(false);
    }


    //pulsante per creare una stanza
    public void CreaStanza()
    {
        string codice = GeneraCodiceCasuale(Info.SessionCodeLength);
        Debug.Log(codice);
        string toSend = "{\"" + codice + "\":{\"gameStatusCode\":\"" + Info.GameStatus.WaitPlayer + "\"}}";
        RestClient.Patch(Info.DBUrl + ".json", toSend).Then(r =>
        {
            Debug.Log("Stanza creata");
            Info.SessionCode = codice;
            
            createGame.SetActive(false);
            gameManager.SetActive(true);

            sessionText.text = Info.SessionCode;
                
            playerJoin = new Listeners(Info.DBUrl + Info.SessionCode + "/players.json");   
            playerJoin.Start(PlayerAdd);
        }).Catch(Debug.Log);
    }
    
    private string GeneraCodiceCasuale(int lunghezza)
    {
        // Utilizza il tempo corrente come seme per il generatore casuale
        System.Random random = new System.Random((int)System.DateTime.Now.Ticks);

        // Genera una stringa casuale di lettere
        const string caratteri = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        char[] codice = new char[lunghezza];

        for (int i = 0; i < lunghezza; i++)
        {
            codice[i] = caratteri[random.Next(caratteri.Length)];
        }
        
        return new string(codice);
    }
    
    //cambia la fase della partita
    //viene richiamata dai 4 pulsanti che passano una parametro diverso pe ressere identificati
    public void ChangeStatusGame(string str)
    {
        string toSend = "{\"gameStatusCode\":\"" + str + "\"}";
        RestClient.Patch(Info.DBUrl + Info.SessionCode + ".json", toSend);
        if (str == Info.GameStatus.End)
        {
            Reset();
        }
    }


    private void Reset()
    {
        Info.SessionCode = "";
        
        playerList = new Dictionary<string, GameObject>();
        
        playerJoin.Stop();
        
        createGame.SetActive(true);
        gameManager.SetActive(false);
    }
    
    //TODO finire di implementare la possibilità di visualizzare a schermo i player quando entrano o escono, aggiungere possibilità di rimuoveli 
    private void PlayerAdd(string str)
    {
        // event: put
        // data: {"path":"/s","data":{"cord":{"x":0.0,"y":0.0},"name":"s","role":"null"}}
        
        // event: put
        // data: {"path":"/s","data":null}
        if (str.Contains("event: put"))
        {
            str = Normalize(str.Split("data: ",2)[1]);
            Debug.Log("normalize:"+str);
            KeyValuePair<string, User> tmp = (new JSONObject(str).ToUserDictionary()).First();
            
            if (tmp.Value == null)
            {
                //utente da rimuovere
                playerList.Remove(tmp.Key);
                Debug.Log("player left:" + tmp.Key);
            }
            else
            {
                //utente da aggiungere
                playerList.Add(tmp.Key, GameObject.Instantiate(playerPrefab,parent));
                playerList[tmp.Key].GetComponent<PlayerMaster>().Set(tmp.Key,tmp.Value);
                playerList[tmp.Key].SetActive(false);
                Debug.Log("player join:" + tmp.Key);
            }
            // GridDisplay();
        }
        Debug.Log(str);
    }

    private void GridDisplay()
    {
        // TODO visualizzazione dei vari giocatori a schermo
        int count = 0;
        List<string> s = playerList.Keys.ToList();
        
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                if (count < s.Count)
                {
                 // Vector3 position = new Vector3(x * spacing, y * spacing, 0);
                    playerList[s[count]].SetActive(true);
                   // playerList[s[count]].transform.position = position;
                }
                else
                {
                    break;
                }
                count++;
            }
        }
        
        
        
    }

    private string Normalize(string str)
    {
        return "{\"" + str.Split("/",2)[1].Split("\"",2)[0] + "\"" + str.Split("\"data\"")[1];
    }
    
}
