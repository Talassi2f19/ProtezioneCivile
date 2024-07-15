using System;
using TMPro;
using UnityEngine;

namespace minigame.Incidente
{
    public class Pt2 : MonoBehaviour
    {
        [SerializeField] private GameObject p1, p2, p3, p4;
        [SerializeField] private TextMeshProUGUI text;

        private string[] testi =
        {
            "Chiudi le strade vicino all'incidente, trascina il cono nelle caselle rosse",
            "Sposta i veiccoli fuori dalla strada",
            "Pulisci la strada, trascina la scopa sulle macchie della strada",
            "Apri la strada, clicca sui coni nella strada per rimuoverle",
        };
        
        private void Start()
        {
            p1.SetActive(true);
            p2.SetActive(false);
            p3.SetActive(false);
            p4.SetActive(false);
            text.text = testi[0];
            p1.GetComponent<Pt2A>().Completato(P1Fine);
        }

        private void P1Fine()
        {
            p1.SetActive(false);
            p2.SetActive(true);
            p3.SetActive(false);
            p4.SetActive(false);
            text.text = testi[1];
            p2.GetComponent<Pt2B>().Completato(P2Fine);
        }
        private void P2Fine()
        {
            p1.SetActive(false);
            p2.SetActive(false);
            p3.SetActive(true);
            p4.SetActive(false);
            text.text = testi[2];
            p3.GetComponent<Pt2C>().Completato(P3Fine);
            
        }
        private void P3Fine()
        {
            p1.SetActive(false);
            p2.SetActive(false);
            p3.SetActive(false);
            p4.SetActive(true);
            text.text = testi[3];
            p4.GetComponent<Pt2D>().Completato(P4Fine);
        }
        private void P4Fine()
        {
            _completeCallBack.Invoke();
        }

        public delegate void Pt2CompletedCallBack();
        private Pt2CompletedCallBack _completeCallBack;
        public void OnComplete(Pt2CompletedCallBack tmp)
        {
            _completeCallBack = tmp;
        }
    }
}
