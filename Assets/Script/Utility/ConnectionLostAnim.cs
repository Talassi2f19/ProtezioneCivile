using System.Collections;
using TMPro;
using UnityEngine;

namespace Script.Utility
{
    public class ConnectionLostAnim : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        private int count;
        private string[] stage = { "<color=red>Connessione persa</color>\nAttendi", "<color=red>Connessione persa</color>\n Attendi.", "<color=red>Connessione persa</color>\n  Attendi..", "<color=red>Connessione persa</color>\n   Attendi..." };
        private Coroutine coroutine;
    
        private void OnEnable()
        {
            coroutine = StartCoroutine(Animazione());
        }

        private void OnDisable()
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    

    
        private IEnumerator Animazione()
        {
            while (true)
            {
                count++;
    
                if (count > 3)
                    count = 0;
    
                text.text = stage[count];
                yield return new WaitForSeconds(1);
            }
        }
    }
}
