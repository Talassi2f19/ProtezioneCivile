using System.Collections;
using System.Collections.Generic;
using Script.Utility;
using UnityEngine;

public class Missione
{
    private string codice; //Codice univovo della missione
    private string nome; //Nome della missione

    //Dizionario che contiene la coppia CodiceFase - Fase
    //CodiceFase è il codice interno alla missione della fase
    //Fase è l'oggetto fase in se che contiene tutti i dati legati a quella fase
    private Dictionary<int, Fase> elencoFasi { get; set; }

    public Missione()
    {
        nome = "";
        codice = "";
        elencoFasi = new Dictionary<int, Fase>();
    }

    public string getCodice()
    {
        return codice;
    }

    public string getNome()
    {
        return nome;
    }
}