using System;
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
        Polizia, //"volontario" polizia 
        RefFuoco, //referente vigile del Fuoco
        VolFuoco, //volontario Vigile del Fuovo
        Segretaria,
        RefGgev, // referente Guardie Giurate Ecologiche Volontarie
        VolGgev //volontario Guardie Giurate Ecologiche Volontarie
    }
}