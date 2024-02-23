using Script.Utility;
using TMPro;
using UnityEngine;
using System;

// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace Script.User.Prefabs
{
    //classe del gameObject playerOnline
    public class PlayerOnline : MonoBehaviour
    {
        [SerializeField] private GameObject nome;
        public float speed = 1;
        private Rigidbody2D rb;
        private GenericUser genericUser;
        private Vector2 posizione = Vector2.zero;
        private Animator anim;
        
        private void Start()
        {
            anim = gameObject.GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            //sr.color = new Color(Random.value,Random.value,Random.value);
            gameObject.name = genericUser.name;
            rb.position = posizione = genericUser.coord;
        }
    
        public void SetUser(GenericUser user)
        {
            this.genericUser = user;
            nome.GetComponent<TMP_Text>().text = user.name;
            nome.GetComponent<RectTransform>().sizeDelta = new Vector2(nome.GetComponent<TMP_Text>().preferredWidth, 25);
        }

        public void Move(Vector2 v)
        {
            v.x = v.x!=0 ? v.x : rb.position.x ;
            v.y = v.y!=0 ? v.y : rb.position.y ;
            posizione = v;
        }

        private void FixedUpdate()
        {
            if (rb.position != posizione)
            {
                transform.position = Vector2.MoveTowards(transform.position, posizione, speed * Time.deltaTime);
            }
        }
        
        private void Animazione()
        {
            if (posizione.x == 0 && posizione.y == 0)
                anim.SetBool("IsStill", true);
            else
                anim.SetBool("IsStill", false);
            
            if (posizione.x > Math.Sqrt(2) / 2) //destra
                anim.SetBool("IsRight", true);
            else
                anim.SetBool("IsRight", false);
        
            if (posizione.x < - Math.Sqrt(2) / 2) //sinistra
                anim.SetBool("IsLeft", true);
            else
                anim.SetBool("IsLeft", false);
        
            if (posizione.y > Math.Sqrt(2) / 2 && (posizione.x >= - Math.Sqrt(2) / 2 && posizione.x <= Math.Sqrt(2) / 2)) //avanti
                anim.SetBool("IsUp", true);
            else
                anim.SetBool("IsUp", false);
        
            if (posizione.y < - Math.Sqrt(2) / 2 && (posizione.x >= - Math.Sqrt(2) / 2 && posizione.x <= Math.Sqrt(2) / 2)) //indietro
                anim.SetBool("IsDown", true);
            else
                anim.SetBool("IsDown", false);

            //      Debug.Log(movementInput.x + " - " + movementInput.y);
        }
    }
}
