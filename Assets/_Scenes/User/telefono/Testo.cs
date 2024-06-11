using System.Collections.Generic;
using System.IO;
using Defective.JSON;
using UnityEngine;

namespace _Scenes.User.telefono
{
    public static class Testo
    {
        public static Dictionary<int, string> testi = null;

        public static void Load()
        {
            if (testi != null)
                return;
            
            Debug.Log("Load");
            testi = new Dictionary<int, string>();
            
            string filePath = Path.Combine(Application.dataPath, "_Scenes/User/telefono/Testi.json");

                // Leggi il contenuto del file JSON
            JSONObject json = new JSONObject(File.ReadAllText(filePath));
            
            for (int i = 0; i < json.keys.Count; i++)
            {
                testi.Add(int.Parse(json.keys[i]),json.list[i].stringValue);
            }
        }
    }
}
