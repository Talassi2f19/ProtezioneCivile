using System.Collections.Generic;
using Defective.JSON;
using UnityEngine;
// ReSharper disable CommentTypo IdentifierTypo
namespace Script.Utility
{
    //custom template per la conversione dei dati JSONObject
    public static class JsonTemplates
    {
        public static GenericUser ToUser(this JSONObject jsonObject) {
            var name = jsonObject["name"] ? jsonObject["name"].stringValue : "";
            var role = jsonObject["role"] ? jsonObject["role"].stringValue : "";
            var cord = jsonObject["cord"] ? jsonObject["cord"].ToVector2() : Vector2.zero;
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

        
        
        public static Dictionary<string,bool> ToRuoli(this JSONObject jsonObject)
        {
            var di = new Dictionary<string, bool>();
            var lk = jsonObject.keys;
            var lv = jsonObject.list;
            for(int i = 0; i < lk.Count; i++)
                di.Add(lk[i], lv[i].boolValue);
            return di;
        }


        public static Missione toMissione(this JSONObject jsonObject)
        {
            
            string code = jsonObject.keys[0];
            var data = jsonObject.list[0];
            string nome = data["Nome"].stringValue;
            var fasi = data["Fasi"];
            
            Dictionary<string, Fase> di = new Dictionary<string, Fase>();
            var lk = fasi.keys;
            var lv = fasi.list;
            for (int i = 0; i < lk.Count; i++)
            {
                di.Add(lk[i], lv[i].ToFase());
                di[lk[i]].setCodice(lk[i]);
            }
            return new Missione(nome, code, di);
        }

        public static Fase ToFase(this JSONObject jsonObject)
        {
            Dictionary<string, bool> ruolo = jsonObject["Ruoli"].ToRuoli();
            bool status = jsonObject["isCompleted"].boolValue;
            return new Fase(status, ruolo);
        }
        
    }
}