using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Utility
{
    public enum Ruoli
    {
        Null,
        Sindaco,
        Coc,
        RefCri, //referente Croce Rossa Italiana
        VolCri, //volontario Croce Rossa Italiana
        Medico,
        RefPC, //referente Protezione Civile
        VolPC, //volontario Protezione Civile
        Giornalista,
        Tlc, //telecomunicazioni
        RefPolizia, //referente Polizia
        VolPolizia, //"volontario" polizia 
        RefFuoco, //referente vigile del Fuoco
        VolFuoco, //volontario Vigile del Fuovo
        Segretaria,
        RefGgev, // referente Guardie Giurate Ecologiche Volontarie
        VolGgev //volontario Guardie Giurate Ecologiche Volontarie
    }



    public static class PrecedenzaRuoli
    {
        //per cambaiare l'oridne di generazione cambiare l'oridine degli elementi dell'arrey
        public static readonly Ruoli[] lista =
        {
            //obbligtori per ogni partita
            //sindaco, coc
            //2 
            Ruoli.RefPC,
            Ruoli.Giornalista,
            Ruoli.Tlc,
            Ruoli.RefPolizia,
            Ruoli.RefFuoco,
            Ruoli.Segretaria,
            Ruoli.Medico,
            Ruoli.RefCri,
            Ruoli.RefGgev,
            //11
            //facolatativi in base al numero di player
            Ruoli.VolPC,
            Ruoli.VolPolizia,
            Ruoli.VolFuoco,
            Ruoli.VolCri,
            Ruoli.VolGgev,
            //16
            Ruoli.VolPC,
            Ruoli.VolPolizia,
            Ruoli.VolFuoco,
            Ruoli.VolCri,
            Ruoli.VolGgev,
            //21
            Ruoli.VolPC,
            Ruoli.VolPolizia,
            Ruoli.VolFuoco,
            Ruoli.VolCri,
            Ruoli.VolGgev,
            //26
            Ruoli.VolPC,
            Ruoli.VolPolizia,
            Ruoli.VolFuoco,
            Ruoli.VolCri
            //30
        };
    }
}