using TMPro;
using UnityEngine;

namespace Script.User
{
    public class VotiCandidato : MonoBehaviour
    {
        private string nomeCandidato;
        private int numeroVoti;

        [SerializeField] private TMP_Text testoNomeCandidato;
        [SerializeField] private TMP_Text testoNumeroVoti;

        public void SetNomeCandidato(string nomeCandidato)
        {
            this.nomeCandidato = nomeCandidato;
            testoNomeCandidato.text = this.nomeCandidato;

            Vector2 dim = new Vector2(testoNomeCandidato.GetComponent<TMP_Text>().preferredWidth + 20, 120);
            gameObject.GetComponent<RectTransform>().sizeDelta = dim;
        }

        public void SetNumeroVoti(int numeroVoti)
        {
            this.numeroVoti = numeroVoti;
            testoNumeroVoti.text = this.numeroVoti.ToString();
        }

        public void HighlightBestCandidate()
        {
            testoNomeCandidato.color = Color.red;
            testoNomeCandidato.fontStyle = FontStyles.Bold;
            testoNumeroVoti.color = Color.red;
            testoNumeroVoti.fontStyle = FontStyles.Bold;
        }
    }
}
