using System.Collections.Generic;
using Defective.JSON;
using UnityEngine;
// ReSharper disable CommentTypo IdentifierTypo
namespace Script.Utility
{
    //custom template per la conversione dei dati JSONObject
    public static partial class JsonTemplates
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

        public static Dictionary<string, JSONObject> ToJSONDictionary(this JSONObject jsonObject)
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