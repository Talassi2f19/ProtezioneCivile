using System;
using System.Collections.Generic;
using UnityEngine;

namespace minigame.Incidente
{
    [Serializable]
    class Auto
    {
        public GameObject obj;
        [HideInInspector]public SpriteRenderer sprite;
    }
    public class Pt2B : MonoBehaviour
    {
        private bool isDragging = false;
        private int selected;
        [SerializeField]private List<Auto> auto;
        private Vector2 betterPos = Vector3.zero;
        private int autoInside;

        private void Start()
        {
            foreach (var tmp in auto)
            {
                tmp.sprite = tmp.obj.GetComponent<SpriteRenderer>();
            }
        }

        private void Update()
        {
            if (isDragging)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                auto[selected].obj.transform.position = mousePos + betterPos;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                for (var i = 0; i < auto.Count && !isDragging; i++)
                {
                    var tmp = auto[i];
                    if (tmp.sprite.bounds.Contains(mousePos))
                    {
                        betterPos = (Vector2)tmp.obj.transform.position - mousePos;
                        isDragging = true;
                        selected = i;
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                if (autoInside == 0)
                {
                    _pt2BCompleteCallBack.Invoke();
                }
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            autoInside--;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            autoInside++;
        }


        public delegate void Pt2BCompleteCallBack();
        private Pt2BCompleteCallBack _pt2BCompleteCallBack;

        public void Completato(Pt2BCompleteCallBack tmp)
        {
            _pt2BCompleteCallBack = tmp;
        }
    }
}