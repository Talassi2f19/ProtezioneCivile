using System;
using UnityEngine;

namespace Script.Utility
{//TODO Definizione ruoli
    // public struct Ruoli
    // {
    //     public const string Sindaco = "Sindaco";
    //     public const string Coc = "COC";
    // }
    
    public enum Ruoli
    {
        Null,
        Sindaco,
        Coc
    }
    
    public static class RuoliConverter
    {
        public static string Converti(Ruoli code)
        {
            
            switch (code)
            {
                case Ruoli.Sindaco:
                    return "Sindaco";
                case Ruoli.Coc:
                    return "COC";
                
                default: return "Error";
            }
        }
    }
}