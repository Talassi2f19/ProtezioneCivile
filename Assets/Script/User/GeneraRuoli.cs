using System;
using Defective.JSON;
using Proyecto26;
using Script.Utility;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.User
{
    public class GeneraRuoli : MonoBehaviour
    {
        [ContextMenu("hgjhg")]
        public void Genera()
        {
            
            //scarica la lista di tutti i player
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + ".json").Then(e =>
            {
                string str = Aggiungi(e.Text);
                str = Sostituisci(str);
                RestClient.Put(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + ".json", str).Catch(exception => Debug.LogError(exception));
           }).Catch(Debug.LogError);
        
        }
        
        private string Sostituisci(string s)
        {
            string[] jj = s.Split(Ruoli.Null.ToString());
            string def = "";
            if (jj.Length - 1 <= PrecedenzaRuoli.lista.Length)
            {
                int j = 0;
                for (int i = 0; i < jj.Length-1; i++)
                {
                    def += jj[i] + PrecedenzaRuoli.lista[j];
                    j++;
                }
                def += jj[jj.Length-1];
            }
            else
            {
                Debug.LogError("Errore nella generazione dei ruoli");
            }
            return def;
        }

        private string Aggiungi(string s)
        {
            int n = Info.MaxPlayer - s.Split("Role").Length + 1;

            for (int i = 0; i < n; i++)
            {
                string val = ",\"Computer"+i+"\":{\"Name\":\"Computer"+i+"\",\"Role\":\"Null\",\"Virtual\":true}";
                s = s.Insert(s.Length - 2, val);
            }
            return s;
        }
    }
}
