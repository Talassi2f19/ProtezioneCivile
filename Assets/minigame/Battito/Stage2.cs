using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace minigame.Battito
{
    public class Stage2 : MonoBehaviour
    {
        [SerializeField] private List<GameObject> battitiImg;
        [SerializeField] private float step = 1f;
        [SerializeField] private Image progressBar;
        // Update is called once per frame

        private void Start()
        {
            progressBar.fillAmount = 0;
        }

        void Update()
        {
            // Sposta ogni immagine lungo l'asse x
            foreach (GameObject tmp in battitiImg)
            {
                tmp.transform.Translate(Vector3.left * step);

                var transform1 = tmp.transform;
                if (transform1.localPosition.x < -1450)
                {
                    transform1.localPosition = new Vector3(1550 + Random.Range(0,10), transform1.localPosition.y,0);
                }
            }
        }

        
        
        public void CuoreClick()
        {
            float minDistance = 10000;
            
            foreach (GameObject tmp in battitiImg)
            {
                float distance = CalculateDistance(tmp.transform.localPosition.x);
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }
            
            progressBar.fillAmount += CalculateScore(minDistance);
            
            if (progressBar.fillAmount >= 1f)
            {
                _st2Callback.Invoke();
            }
        }
        
        private float CalculateDistance(float x)
        {
            return Math.Abs(x); // Distance is the absolute value of x
        }

        // Function that calculates the score based on the distance
        private float CalculateScore(float distance)
        {
            if (distance == 0f)
            {
                return 0.2f;
            }
            if (distance >= 0f && distance <= 9f)
            {
                return 0.15f;
            }
            if (distance >= 10f && distance <= 19f)
            {
                return 0.10f;
            }
            if (distance >= 20f && distance <= 29f)
            {
                return 0.08f;
            }
            if (distance >= 30f && distance <= 39f)
            {
                return 0.05f;
            }
            if (distance >= 40f && distance <= 49f)
            {
                return 0.02f;
            }
            if (distance >= 50f && distance <= 59f)
            {
                return 0.0f;
            }
            if (distance >= 60f && distance <= 69f)
            {
                return -0.04f;
            }
            if (distance >= 70f && distance <= 79f)
            {
                return -0.08f;
            }
            if (distance >= 80f && distance <= 89f)
            {
                 return -0.12f;
            }
            if (distance >= 90f && distance <= 99f)
            {
                 return -0.2f;
            }
            if (distance >= 100f && distance <= 129f)
            {
                 return -0.25f;
            }
            if (distance >= 130f && distance <= 149f)
            {
                return -0.3f;
            }
            
            return -0.4f; // Or any other default score
            
        }
        
        public delegate void St2callback();
        private St2callback _st2Callback;
        public void Completato(St2callback tmp)
        {
            _st2Callback = tmp;
        }
    }
}
