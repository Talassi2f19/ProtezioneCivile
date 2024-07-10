using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
// ReSharper disable StringLiteralTypo

namespace minigame.evacuaCittadini
{
    public class stage2 : MonoBehaviour
    {
        [SerializeField]private SpriteRenderer spritePorta;
        private List<Sprite> spriteListPorta;
        [SerializeField]private SpriteRenderer persona;
        [SerializeField]private GameObject vignetta1;
        [SerializeField]private GameObject vignetta2;
        [SerializeField]private GameObject NextButton;
        [SerializeField]private TextMeshProUGUI vignetta1Text;
        [SerializeField]private TextMeshProUGUI vignetta2Text;
        private bool next;
        private int tipo;

        private string[] testiPos =
        {
            "Buongiorno, signore. Sono qui come volontario per informarla che dobbiamo evacuare la zona immediatamente.",
            "Il fiume è in piena e potrebbe straripare da un momento all'altro.",
            
            "Il fiume in piena? Non avevo idea che la situazione fosse così grave!",
            
            "Sì, purtroppo le piogge intense hanno reso la situazione molto pericolosa. Per la sua sicurezza e quella della sua famiglia, è fondamentale evacuare subito.",
            
            "Capisco. Devo raccogliere alcune cose. Dove dobbiamo andare?",
            
            "È stato allestito un centro di accoglienza. Le consiglio di prendere solo l'essenziale e di partire il prima possibile.",
            "La sua sicurezza è la nostra priorità.",
            
            "Grazie per avermi avvisato. Mi preparo immediatamente."
        };

        private string[] testiNeg =
        {
            "Buongiorno, signore. Sono qui come volontario per informarla che dobbiamo evacuare la zona immediatamente.",
            "Il fiume è in piena e potrebbe straripare da un momento all'altro.",
            
            "Il fiume in piena? Non avevo idea che la situazione fosse così grave! Ma non penso di poter lasciare la mia casa adesso.",
            
            "Capisco che sia una decisione difficile, ma la sua sicurezza è la priorità. La situazione è davvero critica e non vogliamo mettere nessuno in pericolo.",
            
            "Non posso lasciare tutto qui. Questa è la mia casa, i miei ricordi. Non posso semplicemente andarmene.",
            
            "La capisco perfettamente, ma deve considerare il rischio. Se il fiume straripa, potrebbe essere molto pericoloso rimanere.",
            
            "No!",
            "Non me ne vado. Resto qui.",
            
            "Sono costretto a mandare il sindaco con il modulo di Dichiarazione di accettazione del rischio.",
            "Sarà necessario che lei firmi per confermare di essere consapevole dei pericoli e che accetta di rimanere a suo rischio e pericolo.",
            
            "Va bene, faccia pure. Sono disposto ad accettare il rischio. Ma non lascerò la mia casa.",
        };
        
        private string[] testiSindaco =
        {
            "Buongiorno, signore. Sono il sindaco. Ho saputo che non intende evacuare la sua casa nonostante il rischio imminente del fiume in piena.",
            
            "Buongiorno, signor sindaco. Sì, è corretto. Non voglio abbandonare la mia casa. È tutto ciò che ho.",
            
            "Capisco quanto sia difficile. Però la situazione è estremamente pericolosa. Se il fiume straripa, potrebbe mettere seriamente a rischio la sua vita.",
            
            "Lo so. Questo è il mio posto e non me ne andrò.",
            
            "Se davvero insiste nel rimanere, ho qui un modulo di Dichiarazione di accettazione del rischio." ,
            "Questo documento conferma che lei è consapevole del pericolo e accetta di restare a suo rischio e pericolo. La prego di leggerlo attentamente e di firmarlo.",
            
            "Va bene, firmo il modulo. Non voglio lasciare la mia casa.",
            
            "La ringrazio per la sua collaborazione.",
            "Se cambiasse idea, il centro di accoglienza è sempre pronto ad accoglierla.",
            
            "Non penso cambierò idea.",
            
            "Arrivederci, buona giornata."
        };

       
        
        [SerializeField]private float pausaFramePorta = 0.1f;
        // Start is called before the first frame update
        void Start()
        {
            spritePorta.sprite = spriteListPorta[0];
            persona.enabled = false;
            
            vignetta1.SetActive(false);
            vignetta2.SetActive(false);
            NextButton.SetActive(false);
            
            StartCoroutine(Animazione());
            next = false;
        }
        
        public void Set(List<Sprite> tmp, int type)
        {
            spriteListPorta = tmp;
            tipo = type;
        }

        
        public void Next()
        {
            next = true;
        }
        
        private IEnumerator Animazione()
        {
            Debug.Log(tipo);
            yield return ApriPorta();
            persona.enabled = true;
            yield return new WaitForSeconds(1);
            NextButton.SetActive(true);
            if (tipo == 0)
            {
                yield return DialogoPositivo();
            }
            else if(tipo == 1)
            {
                yield return DialogoNegativo();
            }
            else if(tipo == 2)
            {
                Debug.Log(next);
                yield return DialogoSindaco();
            }
            NextButton.SetActive(false);
            yield return new WaitForSeconds(1);
            persona.enabled = false;
            yield return ChiudiPorta();
            yield return new WaitForSeconds(1);
            stage2Completato.Invoke();
            yield break;   
        }

        private IEnumerator AspettaNext()
        {
            Coroutine cc = StartCoroutine(AutoNext());
            while (!next)
            {
                yield return new WaitForSeconds(0.1f);
            }
            next = false;
            StopCoroutine(cc);
        }

        private IEnumerator AutoNext()
        {
            yield return new WaitForSeconds(20f);
            next = true;
        }
        
        private IEnumerator DialogoPositivo()
        {
            vignetta1.SetActive(true);
            vignetta1Text.text = testiPos[0];
            yield return AspettaNext();
            vignetta1Text.text = testiPos[1];
            yield return AspettaNext();
            vignetta1.SetActive(false);
            
            vignetta2.SetActive(true);
            vignetta2Text.text = testiPos[2];
            yield return AspettaNext();
            vignetta2.SetActive(false);
            
            vignetta1.SetActive(true);
            vignetta1Text.text = testiPos[3];
            yield return AspettaNext();
            vignetta1.SetActive(false);
            
            vignetta2.SetActive(true);
            vignetta2Text.text = testiPos[4];
            yield return AspettaNext();
            vignetta2.SetActive(false);
            
            vignetta1.SetActive(true);
            vignetta1Text.text = testiPos[5];
            yield return AspettaNext();
            vignetta1Text.text = testiPos[6];
            yield return AspettaNext();
            vignetta1.SetActive(false);
            
            vignetta2.SetActive(true);
            vignetta2Text.text = testiPos[7];
            yield return AspettaNext();
            vignetta2.SetActive(false);
            yield break;
        }
        private IEnumerator DialogoNegativo()
        {
            vignetta1.SetActive(true);
            vignetta1Text.text = testiNeg[0];
            yield return AspettaNext();
            vignetta1Text.text = testiNeg[1];
            yield return AspettaNext();
            vignetta1.SetActive(false);
            
            vignetta2.SetActive(true);
            vignetta2Text.text = testiNeg[2];
            yield return AspettaNext();
            vignetta2.SetActive(false);
            
            vignetta1.SetActive(true);
            vignetta1Text.text = testiNeg[3];
            yield return AspettaNext();
            vignetta1.SetActive(false);
            
            vignetta2.SetActive(true);
            vignetta2Text.text = testiNeg[4];
            yield return AspettaNext();
            vignetta2.SetActive(false);
            
            vignetta1.SetActive(true);
            vignetta1Text.text = testiNeg[5];
            yield return AspettaNext();
            vignetta1.SetActive(false);
            
            vignetta2.SetActive(true);
            vignetta2Text.text = testiNeg[6];
            yield return AspettaNext();
            vignetta2Text.text = testiNeg[7];
            yield return AspettaNext();
            vignetta2.SetActive(false);
            
            vignetta1.SetActive(true);
            vignetta1Text.text = testiNeg[8];
            yield return AspettaNext();
            vignetta1Text.text = testiNeg[9];
            yield return AspettaNext();
            vignetta1.SetActive(false);
            
            vignetta2.SetActive(true);
            vignetta2Text.text = testiNeg[10];
            yield return AspettaNext();
            vignetta2.SetActive(false);
            
            yield break;
        }
        
        private IEnumerator DialogoSindaco()
        {
            
            Debug.Log("aaaaaa");
            vignetta1.SetActive(true);
            vignetta1Text.text = testiSindaco[0];
            yield return AspettaNext();
            vignetta1.SetActive(false);
            
            vignetta2.SetActive(true);
            vignetta2Text.text = testiSindaco[1];
            yield return AspettaNext();
            vignetta2.SetActive(false);
            
            vignetta1.SetActive(true);
            vignetta1Text.text = testiSindaco[2];
            yield return AspettaNext();
            vignetta1.SetActive(false);
            
            vignetta2.SetActive(true);
            vignetta2Text.text = testiSindaco[3];
            yield return AspettaNext();
            vignetta2.SetActive(false);
            
            vignetta1.SetActive(true);
            vignetta1Text.text = testiSindaco[4];
            yield return AspettaNext();
            vignetta1Text.text = testiSindaco[5];
            yield return AspettaNext();
            vignetta1.SetActive(false);
            
            vignetta2.SetActive(true);
            vignetta2Text.text = testiSindaco[6];
            yield return AspettaNext();
            vignetta2.SetActive(false);
            
            vignetta1.SetActive(true);
            vignetta1Text.text = testiSindaco[7];
            yield return AspettaNext();
            vignetta1Text.text = testiSindaco[8];
            yield return AspettaNext();
            vignetta1.SetActive(false);
            
            vignetta2.SetActive(true);
            vignetta2Text.text = testiSindaco[9];
            yield return AspettaNext();
            vignetta2.SetActive(false);
            
            vignetta1.SetActive(true);
            vignetta1Text.text = testiSindaco[10];
            yield return AspettaNext();
            vignetta1.SetActive(false);
            Debug.Log("bbbbbb");
            yield break;
        }
        private IEnumerator ApriPorta()
        {
            foreach (var ss in spriteListPorta)
            {
                spritePorta.sprite = ss;
                yield return new WaitForSeconds(pausaFramePorta);
            }
        }
        private IEnumerator ChiudiPorta()
        {
            for (int i = spriteListPorta.Count -1; i >= 0;i--)
            {
                spritePorta.sprite = spriteListPorta[i];
                yield return new WaitForSeconds(pausaFramePorta);
            }
        }



        public delegate void Stage2Completato();
        private Stage2Completato stage2Completato;

        public void Stage2Fine(Stage2Completato tmp)
        {
            stage2Completato = tmp;
        }

    }
}
