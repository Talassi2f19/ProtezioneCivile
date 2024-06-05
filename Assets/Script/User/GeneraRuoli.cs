using Proyecto26;
using Script.Utility;
using UnityEngine;

namespace Script.User
{
    public class GeneraRuoli
    {
        public void Genera()
        {
            //scarica la lista di tutti i player
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + ".json").Then(e =>
            {
                //sostisuisci i valori "null" con il ruolo desiganto e ricarica la lista di tutti i player
                RestClient.Put(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + ".json", Sostituisci(e.Text)).Catch(exception => Debug.LogError(exception));
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
    }
}
