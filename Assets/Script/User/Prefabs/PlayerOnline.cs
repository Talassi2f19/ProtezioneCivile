using Script.Utility;
using TMPro;
using UnityEngine;
using System;
using Unity.VisualScripting;

// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace Script.User.Prefabs
{
    //classe del gameObject playerOnline
    public class PlayerOnline : MonoBehaviour
    {
        [SerializeField] private GameObject nome;
        public float speed = 1;
        private Rigidbody2D playerOnlineHitbox;
        private GenericUser genericUser;
        private Vector2 posizione = Vector2.zero;
        private Animator anim;
        
        private void Start()
        {
            anim = gameObject.GetComponent<Animator>();
            playerOnlineHitbox = GetComponent<Rigidbody2D>();
            //sr.color = new Color(Random.value,Random.value,Random.value);
            gameObject.name = genericUser.name;
            playerOnlineHitbox.position = posizione = genericUser.coord;
        }
    
        public void SetUser(GenericUser user)
        {
            this.genericUser = user;
            nome.GetComponent<TMP_Text>().text = user.name;
            nome.GetComponent<RectTransform>().sizeDelta = new Vector2(nome.GetComponent<TMP_Text>().preferredWidth, 25);
        }

        public void Move(Vector2 v)
        {
            v.x = v.x!=0 ? v.x : playerOnlineHitbox.position.x ;
            v.y = v.y!=0 ? v.y : playerOnlineHitbox.position.y ;
            posizione = v;
        }

        private void FixedUpdate()
        {
            double dist;
            if (playerOnlineHitbox.position != posizione)
            {
                dist = GetDistance();
                
                transform.position = Vector2.MoveTowards(transform.position, posizione, speed * Time.deltaTime);
            }
            else
                dist = 0;
            Animazione((float)dist);
        }

        private double GetDistance()
        {
            return Math.Sqrt(Math.Pow(playerOnlineHitbox.position.x - posizione.x, 2) + Math.Pow(playerOnlineHitbox.position.y - posizione.y, 2));
        }
        
        private void Animazione(float distance)
        {

            if (posizione.x == 0 && posizione.y == 0)
                anim.SetBool("IsStill", true);
            else
                anim.SetBool("IsStill", false);
            
            if (posizione.x > (Math.Sqrt(2) / 2 * distance)) //destra
                anim.SetBool("IsRight", true);
            else
                anim.SetBool("IsRight", false);
        
            if (posizione.x < - Math.Sqrt(2) / 2) //sinistra
                anim.SetBool("IsLeft", true);
            else
                anim.SetBool("IsLeft", false);
        
            if (posizione.y > Math.Sqrt(2) / 2 && (posizione.x >= (- Math.Sqrt(2) / 2 * distance) && posizione.x <= (Math.Sqrt(2) / 2 * distance))) //avanti
                anim.SetBool("IsUp", true);
            else
                anim.SetBool("IsUp", false);
        
            if (posizione.y < - Math.Sqrt(2) / 2 && (posizione.x >= (- Math.Sqrt(2) / 2 * distance) && posizione.x <= (Math.Sqrt(2) / 2 * distance))) //indietro
                anim.SetBool("IsDown", true);
            else
                anim.SetBool("IsDown", false);

            //      Debug.Log(movementInput.x + " - " + movementInput.y);
        }
    }
}
