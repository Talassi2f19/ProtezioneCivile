using System;
using System.Collections.Generic;
using Defective.JSON;
using Script.Utility.GestioneEventi;
using UnityEngine;
// ReSharper disable CommentTypo IdentifierTypo
namespace Script.Utility
{
    //custom template per la conversione dei dati JSONObject
    public static class JsonTemplates
    {
        public static GenericUser ToUser(this JSONObject jsonObject) {
            var name = jsonObject[Global.NomePlayerKey] ? jsonObject[Global.NomePlayerKey].stringValue : "";
            var role = jsonObject[Global.RuoloPlayerKey] ? Enum.Parse<Ruoli>(jsonObject[Global.RuoloPlayerKey].stringValue) : Ruoli.Null;
            var cord = jsonObject[Global.CoordPlayerKey] ? jsonObject[Global.CoordPlayerKey].ToVector2() : Vector2.zero;
            return name == "" ? null : new GenericUser(name, role, cord);
        }
        
        public static Dictionary<string, GenericUser> ToUserDictionary(this JSONObject jsonObject)
        {
            var di = new Dictionary<string, GenericUser>();
            var lk = jsonObject.keys;
            var lv = jsonObject.list;
            for(int i = 0; i < lk.Count; i++)
                di.Add(lk[i], lv[i].ToUser());
            return di;
        }

        public static Dictionary<string, JSONObject> ToJsonDictionary(this JSONObject jsonObject)
        {
            var di = new Dictionary<string, JSONObject>();
            var lk = jsonObject.keys;
            var lv = jsonObject.list;
            for(int i = 0; i < lk.Count; i++)
                di.Add(lk[i], lv[i]);
            return di;
        }
        
        public static Dictionary<string,bool> ToBoolDictionary(this JSONObject jsonObject)
        {
            var di = new Dictionary<string, bool>();
            var lk = jsonObject.keys;
            var lv = jsonObject.list;
            for(int i = 0; i < lk.Count; i++)
                di.Add(lk[i], lv[i].boolValue);
            return di;
        }

        public static Dictionary<string, Missione> ToMissioneDictionary(this JSONObject jsonObject)
        {
            Dictionary<string, Missione> di = new Dictionary<string, Missione>();
            var lk = jsonObject.keys;
            var lv = jsonObject.list;
            for (int i = 0; i < lk.Count; i++)
            {
                di.Add(lk[i], lv[i].ToMissione(lk[i]));
            }
            return di;
        }

        private static Missione ToMissione(this JSONObject jsonObject, string code)
        {
            string nome = jsonObject[Global.NomeMissioneKey].stringValue;
            var fasi = jsonObject[Global.FasiFolder];
            
            Dictionary<string, Fase> di = new Dictionary<string, Fase>();
            var lk = fasi.keys;
            var lv = fasi.list;
            for (int i = 0; i < lk.Count; i++)
            {
                di.Add(lk[i], lv[i].ToFase(lk[i]));
            }
            return new Missione(nome,code, di);
        }

        private static Fase ToFase(this JSONObject jsonObject, string code)
        {
            Dictionary<string, bool> ruolo = jsonObject[Global.RuoliFolder].ToBoolDictionary();
            bool status = jsonObject[Global.IsCompletedKey].boolValue;
            return new Fase(status, code, ruolo);
        }
        
    }
}