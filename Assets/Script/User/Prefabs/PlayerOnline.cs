using TMPro;
using UnityEngine;
using Script.Utility;

// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace Script.User
{
    //classe del gameObject playerOnline
    public class PlayerOnline : MonoBehaviour
    {
        public TextMeshProUGUI nome;
        public float speed = 1;
        private Rigidbody2D rb;
        private GenericUser genericUser;
        private Vector2 posizione = Vector2.zero;
    
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            //sr.color = new Color(Random.value,Random.value,Random.value);
            gameObject.name = genericUser.name;
            rb.position = posizione = genericUser.cord;
        }
    
        public void SetUser(GenericUser user)
        {
            this.genericUser = user;
            nome.text = user.name;
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
    }
}
