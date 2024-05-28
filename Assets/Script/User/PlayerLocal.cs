using System.Collections.Generic;
using Defective.JSON;
using Proyecto26;
using Script.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace Script.User
{
    //classe del gameObject di LocalPlayer
    public class PlayerLocal : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float collisionOffset;
        [SerializeField] private ContactFilter2D movementFilter;
        [SerializeField] private float distanzaInvio;
        [SerializeField] private Skin skin;
        
        [SerializeField] private TextMeshProUGUI testoNomePlayer;

        private Animator animator;
        private Vector2 movementInput;
        private List<RaycastHit2D> castCollisions;
        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        private Vector2 lastPosition;
        private static readonly int X = Animator.StringToHash("x");
        private static readonly int Y = Animator.StringToHash("y");
        private static readonly int Speed = Animator.StringToHash("speed");

        private void Start()
        {
            testoNomePlayer.text = "Tu";
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
            
            skin.SetSkin(Info.localUser.role);
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            castCollisions = new List<RaycastHit2D>();
            LoadServerPosition();
        }
        
        private void LoadServerPosition()
        {
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/Game/Posizione/"+ Info.localUser.name+"/Coord.json").Then(
            e =>
            {
                JSONObject json = new JSONObject(e.Text);
                lastPosition = rb.position = json.ToVector2();
                gameObject.SetActive(true);
                spriteRenderer.enabled = true;
            });
        }
        
        private void Update()
        {
            Animazione();
        }

        private void FixedUpdate()
        {
            //se ci sono movimenti in input
            if (movementInput == Vector2.zero)
                return;
            if(TryMove(movementInput))
                return;
            if(TryMove(new Vector2(movementInput.x, 0)))
               return;
            TryMove(new Vector2(0, movementInput.y));
        }

        private bool TryMove(Vector2 direction)
        {
            //collisioni 
            int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime * collisionOffset);
            // se non ci sono state collisioni sposta il player
            if (count != 0) 
                return false;
            //muove il player
            rb.MovePosition(rb.position + direction * (moveSpeed * Time.fixedDeltaTime));
            InviaPosizioneFirebase();
            return true;
        }
        
        private void InviaPosizioneFirebase()
        {
            if (Vector2.Distance(lastPosition, rb.position) < distanzaInvio) 
                return;
            
            string toSend = JsonUtility.ToJson(rb.position);
            RestClient.Patch(
                Info.DBUrl + Info.sessionCode + "/Game/Posizione/" + Info.localUser.name + "/Coord.json", toSend);
            lastPosition = rb.position;
        }

        //funzione che viene richiamata da input system
        private void OnMove(InputValue moveValue)
        {
            movementInput = moveValue.Get<Vector2>();
        }

        //funzione richiamata dal joystick
        public void OnMove(Vector2 moveValue)
        {
            movementInput = moveValue;
        }
        
        private void Animazione()
        {
            animator.SetFloat(X,movementInput.x);
            animator.SetFloat(Y,movementInput.y);
            animator.SetFloat(Speed,movementInput.sqrMagnitude);
        }
    }
}