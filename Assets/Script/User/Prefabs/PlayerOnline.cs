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
        [SerializeField] private float speed = 1;
        private Rigidbody2D playerOnlineHitbox;
        private Vector2 posizione = Vector2.zero;
        private Animator anim;
        private Vector2 moveDirection = Vector2.zero;


        [SerializeField] private float dist = -50;
        private static readonly int X = Animator.StringToHash("x");
        private static readonly int Y = Animator.StringToHash("y");
        private static readonly int Speed = Animator.StringToHash("speed");


        private void Start()
        {
            anim = gameObject.GetComponent<Animator>();
            playerOnlineHitbox = GetComponent<Rigidbody2D>();
        }
    
        public void SetUser(GenericUser user)
        {
            posizione = user.coord;
            gameObject.name = user.name;
            nome.GetComponent<TMP_Text>().text = user.name;
            nome.GetComponent<RectTransform>().sizeDelta = new Vector2(nome.GetComponent<TMP_Text>().preferredWidth, 25);
        }

        public void Move(Vector2 v)
        {
            v.x = v.x!=0 ? v.x : playerOnlineHitbox.position.x ;
            v.y = v.y!=0 ? v.y : playerOnlineHitbox.position.y ;
            posizione = v;
        }

        
        private void Update()
        {
            Animazione();
        }
        
        private void FixedUpdate()
        {
            //if (playerOnlineHitbox.position == posizione) 
              //  return;
            
            moveDirection = playerOnlineHitbox.position - posizione;
            moveDirection.Normalize();
            transform.position = Vector2.MoveTowards(transform.position, posizione, speed * Time.deltaTime);
        }
        
        private void Animazione()
        {
            anim.SetFloat(X,moveDirection.x);
            anim.SetFloat(Y,moveDirection.y);
            anim.SetFloat(Speed,moveDirection.sqrMagnitude);
        }
    }
}
