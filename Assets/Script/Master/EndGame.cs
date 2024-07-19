using Proyecto26;
using Script.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = Script.Utility.Scene;

namespace Script.Master
{
    public class EndGame : MonoBehaviour
    {

        [SerializeField]private TextMeshProUGUI testo;

        private void Start()
        {
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/score.json").Then(e =>
            {
                testo.text = "Avete ottenuto " + e.Text == "null" ? "0" : e.Text + " punti!";
            });
        }

        public void FineButton()
        {
            Debug.Log("fine");
            Info.Reset();
            SceneManager.LoadScene(Scene.Master.Start);
        }
    
    }
}
