using System.Collections.Generic;
using Defective.JSON;
using UnityEngine;
// ReSharper disable CommentTypo IdentifierTypo
namespace Script.Utility
{
    //custom template per la conversione dei dati JSONObject
    public static partial class JsonTemplates
    {
        public static User ToUser(this JSONObject jsonObject) {
            var name = jsonObject["name"] ? jsonObject["name"].stringValue : "";
            var role = jsonObject["role"] ? jsonObject["role"].stringValue : "";
            var cord = jsonObject["cord"] ? jsonObject["cord"].ToVector2() : Vector2.zero;
            return name == "" ? null : new User(name, role, cord);
        }
        
        public static Dictionary<string, User> ToUserDictionary(this JSONObject jsonObject)
        {
            var di = new Dictionary<string, User>();
            var lk = jsonObject.keys;
            var lv = jsonObject.list;
            for(int i = 0; i < lk.Count; i++)
                di.Add(lk[i], lv[i].ToUser());
            return di;
        }

        public static Dictionary<string, JSONObject> ToDictionary(this JSONObject jsonObject)
        {
            var di = new Dictionary<string, JSONObject>();
            var lk = jsonObject.keys;
            var lv = jsonObject.list;
            for(int i = 0; i < lk.Count; i++)
                di.Add(lk[i], lv[i]);
            return di;
        }
    }
}