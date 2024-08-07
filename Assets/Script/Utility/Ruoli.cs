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
        RefTlc, //telecomunicazioni
        RefPolizia, //referente Polizia
        VolPolizia, //"volontario" polizia 
        RefFuoco, //referente vigile del Fuoco
        VolFuoco, //volontario Vigile del Fuovo
        Segreteria,
        // Giornalista, //rimuovere
        RefGgev, // referente Guardie Giurate Ecologiche Volontarie
        VolGgev //volontario Guardie Giurate Ecologiche Volontarie
    }



    public static class PrecedenzaRuoli
    {
        //per cambaiare l'oridne di generazione cambiare l'oridine degli elementi dell'arrey
        public static readonly Ruoli[] lista =
        {
            //obbligtori per ogni partita
            // sindaco, coc
            //2 
            Ruoli.RefTlc,
            Ruoli.RefPC,
            Ruoli.RefPolizia,
            Ruoli.RefFuoco,
            Ruoli.Segreteria,
            Ruoli.Medico,
            Ruoli.RefCri,
            Ruoli.RefGgev,
            //10
            //facolatativi in base al numero di player
            Ruoli.VolPC,
            Ruoli.VolCri,
            Ruoli.VolGgev,
            Ruoli.VolPolizia,
            Ruoli.VolFuoco,
            //15
            Ruoli.VolPC,
            Ruoli.VolCri,
            Ruoli.VolGgev,
            Ruoli.VolPolizia,
            Ruoli.VolFuoco,
            //20
            Ruoli.VolPC,
            Ruoli.VolCri,
            Ruoli.VolGgev,
            Ruoli.VolPolizia,
            Ruoli.VolFuoco,
            //25
            Ruoli.VolPC,
            Ruoli.VolCri,
            Ruoli.VolGgev,
            Ruoli.VolPolizia,
            Ruoli.VolFuoco,
            //30
            Ruoli.VolPC,
            Ruoli.VolCri,
            Ruoli.VolGgev,
            Ruoli.VolPolizia,
            Ruoli.VolFuoco,
            //35
        };
    }
}