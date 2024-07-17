using UnityEngine;

namespace minigame.Battito
{
    public class Stage2 : MonoBehaviour
    {
        [SerializeField] private Transform battitiImg;
        [SerializeField] private float speed = 1f;
        [SerializeField] private GameObject cuore;
        // Update is called once per frame
        void Update()
        {
            float step = speed * Time.deltaTime;

            // Sposta ogni immagine lungo l'asse x
            foreach (Transform child in battitiImg)
            {
                child.Translate(Vector3.left * step);

                var transform1 = child.transform;
                if (transform1.localPosition.x < -1450)
                {
                    transform1.localPosition = new Vector3(1550 + Random.Range(0,20), transform1.localPosition.y,0);
                }
            }

            CuoreClick();
        }

        public void CuoreClick()
        {
            Vector3 buttonPosition = cuore.transform.position;
            Transform closestObject = null;
            float minDistance = Mathf.Infinity;

            // Itera su ogni figlio nel gruppo per trovare il più vicino
            foreach (Transform child in battitiImg)
            {
                float distance = Vector3.Distance(buttonPosition, child.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestObject = child;
                }
            }

            if (closestObject != null)
            {
                //if (minDistance < 20)
                {
                    Debug.Log( (int)minDistance);
                }
              //  Debug.Log("Oggetto più vicino: " + closestObject.name + " a distanza: " + minDistance);
                

            }
            else
            {
                Debug.Log("Nessun oggetto trovato nel gruppo.");
            }
        }
        
        //TODO incermento decremento
        
        
        public delegate void St2callback();
        private St2callback _st1Callback;
        public void Completato(St2callback tmp)
        {
            _st1Callback = tmp;
        }
    }
}
