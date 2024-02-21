using Proyecto26;
using Script.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Master
{
    public class Start : MonoBehaviour
    {

        public void CreaStanza()
        {
            string codice = GeneraCodiceCasuale(Info.SessionCodeLength);
            Debug.Log(codice);
            string toSend = "{\"" + codice + "\":{\"gameStatusCode\":\"" + Info.GameStatus.WaitPlayer + "\"}}";
            RestClient.Patch(Info.DBUrl + ".json", toSend).Then(r =>
            {
                Debug.Log("Stanza creata");
                Info.sessionCode = codice;

                SceneManager.LoadScene("_Scenes/Master/playerLogin");

            }).Catch(Debug.Log);
        }

        private string GeneraCodiceCasuale(int lunghezza)
        {
            // ReSharper disable once CommentTypo
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
    }
}
