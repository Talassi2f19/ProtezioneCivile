using System;
using System.Collections;
using Proyecto26;
using UnityEngine;
using UnityEngine.UI;

namespace minigame
{
    public class Danni : MonoBehaviour
    {
        [SerializeField]private Image image;
        private Coroutine dannoPunti;
        private Coroutine dannoVisuale;

        private void Start()
        {
            image.color = new Color(0f, 0f, 0f, 0f);
        }

        public void Inizia()
        {
            dannoPunti = StartCoroutine(DannoPunti());
            dannoVisuale = StartCoroutine(DannoVisuale());
        }

        public void Fine()
        {
            if(dannoPunti != null)
                StopCoroutine(dannoPunti);
            if(dannoVisuale != null)
                StopCoroutine(dannoVisuale);
            image.color = new Color(0f, 0f, 0f, 0f);
        }

        private IEnumerator DannoVisuale()
        {
            while (true)
            {
                image.color = new Color(1f, 0f, 0f, 0.4f);
                yield return new WaitForSeconds(0.2f);
                image.color = new Color(0f, 0f, 0f, 0f);
                yield return new WaitForSeconds(1f);
            }
        }
        private IEnumerator DannoPunti()
        {
            while (true)
            {
                //TODO togli i punti rest
                yield return new WaitForSeconds(5f);
            }
        }
    }
}
