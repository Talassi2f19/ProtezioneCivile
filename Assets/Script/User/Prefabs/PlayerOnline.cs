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
        [SerializeField] private float speed = 0.8f;
        [SerializeField] private Skin skin;
        
        private Vector2 posizione = Vector2.zero;
        private Animator anim;
        private Vector2 moveDirection = Vector2.zero;
        private GenericUser localUser;
        
        private static readonly int X = Animator.StringToHash("x");
        private static readonly int Y = Animator.StringToHash("y");
        private static readonly int Speed = Animator.StringToHash("speed");


        private void Start()
        {
            anim = gameObject.GetComponent<Animator>();
            skin.SetSkin(localUser.role);
            posizione = localUser.coord;
            gameObject.name = localUser.name;
            nome.GetComponent<TMP_Text>().text = localUser.name;
            nome.GetComponent<RectTransform>().sizeDelta = new Vector2(nome.GetComponent<TMP_Text>().preferredWidth, 25);
        }
    
        public void SetUser(GenericUser user)
        {
            localUser = user;
        }

        public void Move(Vector2 v)
        {
            v.x = v.x!=0 ? v.x : transform.position.x ;
            v.y = v.y!=0 ? v.y : transform.position.y ;
            posizione = v;
        }

        
        private void Update()
        {
            Animazione();
        }
        
        private void FixedUpdate()
        {
            var position = transform.position;
            moveDirection = position - (Vector3)posizione;
            moveDirection.Normalize();
            position = Vector2.MoveTowards(position, posizione, speed * Time.deltaTime);
            transform.position = position;
        }
        
        private void Animazione()
        {
            anim.SetFloat(X,moveDirection.x * -1);
            anim.SetFloat(Y,moveDirection.y * -1);
            anim.SetFloat(Speed,moveDirection.sqrMagnitude);
        }
    }
}
