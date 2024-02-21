using System.Collections;
using System.Collections.Generic;
using Script.Utility;
using UnityEngine;

public class Fase
{
    private bool isCompleted; //Determina se la fase è stata completata o meno
    private string codice; //Codice della fase

    //Dizionario che contiene la coppia nomeRuolo: hasFinished
    //nomeRuolo corrisponde al nome del ruolo coinvolto nella fase
    //hasFinished è un booleano che indica se i compiti assegnati a questo ruolo sono stati TUTTI portati a termine o meno
    private Dictionary<string, bool> ruoli;


    public Fase()
    {
        isCompleted = false;
        codice = "";
        ruoli = new Dictionary<string, bool>();
    }

    public Fase(bool isCompleted, Dictionary<string, bool> ruoli)
    {
        codice = "";
        this.isCompleted = isCompleted;
        this.ruoli = ruoli;
    }
    
    public bool getIsCompleted()
    {
        return isCompleted;
    }

    public string getCodice()
    {
        return codice;
    }

    public Dictionary<string, bool> getRuoli()
    {
        return ruoli;
    }

    //Ritorna true se con questa esecuzione ha chiuso la task
    //Ritorna false se la task era già finita
    public bool taskFinished()
    {
        if (!isCompleted)
            return false;
        
        return (isCompleted = true);
    }

    public void setCodice(string codice)
    {
        this.codice = codice;
    }

    public void insertRuolo(string nomeRuolo, bool hasFinished)
    {
        ruoli.Add(nomeRuolo, hasFinished);
    }
    
    //Ritorna false se non è stato trovato il ruolo
    public bool ruoloFinished(string nomeRuolo)
    {
        if (!ruoli.ContainsKey(nomeRuolo))
            return false;
        
        return (ruoli[nomeRuolo] = true);
    }

    public string printData()
    {
        string response = "";

        response += "CodiceFase: " + codice;
        response += "\nIsCompleted: " + isCompleted;
        response += "\nRuoli";
        foreach (var element in ruoli)
            response += "\n\t" + element.Key + " - " + element.Value;

        return response;
    }
}