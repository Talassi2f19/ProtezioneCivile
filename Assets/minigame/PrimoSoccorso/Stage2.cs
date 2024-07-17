using System.Collections;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.UI;

namespace minigame.PrimoSoccorso
{
    public class Stage2 : MonoBehaviour
    {
        [SerializeField] private GameObject PallaVita;
        [SerializeField] private Image progressBar;
        [SerializeField] private Transform palline;
        [SerializeField] private float vitaXpallina;
        [SerializeField]private Sprite piu;
        [SerializeField]private Sprite meno;

        private Coroutine coroutine;
        
        
        void Start()
        {
            progressBar.fillAmount = 0f;
            coroutine = StartCoroutine(Genera());
        }
        

        private IEnumerator Genera()
        {
            while (true)
            {
                bool type = Random.Range(0, 100) > 35;
                
                GameObject tmp = Instantiate(PallaVita, palline);
                
                tmp.transform.position = new Vector3(Random.Range(-0.5f, 0.5f),Random.Range(-0.5f, 0.5f),0) + palline.position;

                float value = Random.Range(0.3f, 1f);
                tmp.transform.localScale = new Vector3(value, value, 1);
                
                tmp.GetComponentInChildren<Image>().sprite = type ? piu : meno;
                
                tmp.GetComponentInChildren<Button>().onClick.AddListener(()=>BollaPremuta(tmp, type));
                
                yield return new WaitForSeconds(Random.Range(0.5f,1.5f));
            }
        }

        private void BollaPremuta(GameObject obj, bool type)
        {
            Destroy(obj);
            progressBar.fillAmount += vitaXpallina * (type ? 1 : -1);
            
            Controlla();
        }

        private void Controlla()
        {
            if (progressBar.fillAmount >= 1f)
            {
                StopCoroutine(coroutine);
                _stage2FineCallBack.Invoke();
            }
        }

        public delegate void Stage2FineCallBack();
        private Stage2FineCallBack _stage2FineCallBack;

        public void Completato(Stage2FineCallBack tmp)
        {
            _stage2FineCallBack = tmp;
        }
    }
}
