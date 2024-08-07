using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = Script.Utility.Scene;

namespace Script.User
{
    public class EndGame : MonoBehaviour
    {

        [SerializeField]private TextMeshProUGUI testo;

        private void Start()
        {
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/score.json").Then(e =>
            {
                testo.text = "Avete ottenuto " + e.Text.ToLower() == "null" ? "0" : e.Text + " punti!";
            });
        }
        
        public void FineButton()
        {
            Info.Reset();
            SceneManager.LoadScene(Scene.User.Login);
        }
    
    }
}
