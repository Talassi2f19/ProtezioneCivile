using Script.Utility;
using TMPro;
using UnityEngine;

namespace minigame.PuntiRaccolta
{
    public class P2 : MonoBehaviour
    {
        [SerializeField]private GameObject step1, step2, step3;
        [SerializeField] private TextMeshProUGUI text;

        private string[] testi =
        {
            "Trascina la recinzione nelle zone grigie per piazzarla",
            "Trascina il terreno marrone nelle zone grigie per renderle calpestabili",
            "Trascina le tende per piazzarle",
        };
        // Start is called before the first frame update
        void Start()
        {
            step1.SetActive(true);
            step2.SetActive(false);
            step3.SetActive(false);
            text.text = testi[0];
            transform.position = Info.localUser.coord;
            step1.GetComponent<RecinzioneStage>().Completato(FineStep1);
        }

        private void FineStep1()
        {
            text.text = testi[1];
            step1.SetActive(false);
            step2.SetActive(true);
            step3.SetActive(false);
            step2.GetComponent<RecinzioneStage>().Completato(FineStep2);
        }
        private void FineStep2()
        {
            text.text = testi[2];
            step1.SetActive(false);
            step2.SetActive(false);
            step3.SetActive(true);
            step3.GetComponent<Tende>().Completato(FineStep3);
        }
        private void FineStep3()
        {
            completeCallback.Invoke();
        }

        public delegate void CompleteCallback();
        private CompleteCallback completeCallback;
    
        public void Pt2Complete(CompleteCallback tmp)
        {
            completeCallback = tmp;
        }
    }
}
