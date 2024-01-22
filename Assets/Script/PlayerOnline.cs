using TMPro;
using UnityEngine;
// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace Script
{
    //classe del gameObject playerOnline
    public class PlayerOnline : MonoBehaviour
    {
        public TextMeshProUGUI nome;
        public float speed = 1;
        private Rigidbody2D rb;
        private User user;
        private Vector2 posizione = Vector2.zero;
    
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            //sr.color = new Color(Random.value,Random.value,Random.value);
            gameObject.name = user.name;
            rb.position = posizione = user.cord;
        }
    
        public void SetUser(User user)
        {
            this.user = user;
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
