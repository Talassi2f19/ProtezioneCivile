using System.Collections;
using System.Collections.Generic;
using System.Text;
using Defective.JSON;
using Script;
using TMPro;
using UnityEngine;

public class votiCandidato : MonoBehaviour
{
    private string nomeCandidato;
    private int numeroVoti;

    [SerializeField] private TMP_Text testoNomeCandidato;
    [SerializeField] private TMP_Text testoNumeroVoti;

    public void setNomeCandidato(string nomeCandidato)
    {
        this.nomeCandidato = nomeCandidato;
        testoNomeCandidato.text = this.nomeCandidato;
    }

    public void setNumeroVoti(int numeroVoti)
    {
        this.numeroVoti = numeroVoti;
        testoNumeroVoti.text = this.numeroVoti.ToString();
    }

    public void highlightBestCandidate()
    {
        testoNomeCandidato.color = Color.red;
        testoNomeCandidato.fontStyle = FontStyles.Bold;
        testoNumeroVoti.color = Color.red;
        testoNumeroVoti.fontStyle = FontStyles.Bold;
    }
}
