using System.Collections;
using System.Collections.Generic;
using Defective.JSON;
using Proyecto26;
using Script.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = Script.Utility.Scene;

namespace Script.User
{
    public class GeneraRuoli : MonoBehaviour
    {

        [ContextMenu("fds")]
        public void hh()
        {
            Info.sessionCode = "AAA";
            Genera();
        }
        
        
        public void Genera()
        {
            //scarica la lista di tutti i player
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + ".json").Then(e =>
            {
                Debug.LogWarning("1");
                string fffd = Carica(e.Text);
                Debug.Log(fffd);
                RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + ".json", fffd).Then(e =>
                {
                    Debug.LogWarning("2");
                    StartCoroutine(After2sec());
                    
                }).Catch(Debug.LogError);
           }).Catch(Debug.LogError);
        
        }

        private IEnumerator After2sec()
        {
            yield return new WaitForSeconds(3f);
            RestClient.Patch(Info.DBUrl + Info.sessionCode + ".json", "{\"" + Global.GameStatusCodeKey + "\":\"" + GameStatus.Gioco + "\"}").Then(e =>
            {
                SceneManager.LoadScene(Scene.Master.Game);
            }).Catch(Debug.LogError);
        }
        
        


        private string Carica(string originale)
        {
            int numPlOriginale = originale.Split(Global.NomePlayerKey).Length - 1;

            int listaPos = 0;
            string nuova = "";
            
            string[] arr = originale.Split(Ruoli.Null.ToString());
            for (int i = 0; i < arr.Length - 1 ; i++)
            {
                nuova += arr[i] + PrecedenzaRuoli.lista[listaPos];
                listaPos++;
            }
            nuova += arr[arr.Length-1];
            
            for (int i = 0; i < 30 - numPlOriginale; i++)
            {
                // Debug.Log("npc"+i);
                nuova = nuova.Insert(nuova.Length - 1, ",\"Computer"+i+"\":{\"Name\":\"Computer"+i+"\",\"Role\":\"" + PrecedenzaRuoli.lista[listaPos]+ "\",\"Virtual\":true}");
                listaPos++;
            }
            
            return nuova;
        }
    }
}
