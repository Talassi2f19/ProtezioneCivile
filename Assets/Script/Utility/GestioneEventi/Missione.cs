﻿using System.Collections.Generic;

namespace Script.Utility.GestioneEventi
{
    public class Missione
    {
        private string codice; //Codice univovo della missione
        private string nome; //Nome della missione

        //Dizionario che contiene la coppia CodiceFase - Fase
        //CodiceFase è il codice interno alla missione della fase
        //Fase è l'oggetto fase in se che contiene tutti i dati legati a quella fase
        private Dictionary<string, Fase> elencoFasi;

        public Missione()
        {
            nome = "";
            codice = "";
            elencoFasi = new Dictionary<string, Fase>();
        }
    
        public Missione(string nome, string codice, Dictionary<string, Fase> elencoFasi)
        {
            this.nome = nome;
            this.codice = codice;
            this.elencoFasi = elencoFasi;
        }

        public string getCodice()
        {
            return codice;
        }

        public string getNome()
        {
            return nome;
        }
    
        public Dictionary<string, Fase> getElencoFasi()
        {
            return elencoFasi;
        }

        public void setNome(Dictionary<string, Fase> elencoFasi)
        {
            this.elencoFasi = elencoFasi;
        }
    

        public string printData()
        {
            string response = "";

            response += "CodiceMissione: " + codice;
            response += "\nNomeMissione: " + nome;
            response += "\nFasi";

            foreach (var element in elencoFasi)
            {
                response += "\n\n" + element.Key + " - " + "Fase";
                response += "\n" + element.Value.printData();
            }

            return response;
        }
    }
}