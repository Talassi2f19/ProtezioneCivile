using Script.Utility;
using TMPro;
using UnityEngine;

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
    
        private void Start()
        {
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
    }
}
