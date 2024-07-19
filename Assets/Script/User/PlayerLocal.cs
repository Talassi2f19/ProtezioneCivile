using System;
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
        public bool canMove;
        private void Start()
        {
            canMove = true;
            testoNomePlayer.text = "Tu";
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
            
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            castCollisions = new List<RaycastHit2D>();
            LoadServerPosition();
            LoadRole();
        }

        private void LoadRole()
        {
            RestClient.Get(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + "/Role.json").Then(e =>
            {
                Debug.Log(e.Text);
                Ruoli tmp = Ruoli.Null;
                    switch (e.Text)
                    {
                        case "Null":
                            Debug.Log("Il ruolo è Null");
                            tmp = Ruoli.Null;
                            break;
                        case "Sindaco":
                            Debug.Log("Il ruolo è Sindaco");
                            tmp = Ruoli.Sindaco;
                            break;
                        case "Coc":
                            Debug.Log("Il ruolo è Coc");
                            tmp = Ruoli.Coc;
                            break;
                        case "RefCri":
                            Debug.Log("Il ruolo è Referente Croce Rossa Italiana");
                            tmp = Ruoli.RefCri;
                            break;
                        case "VolCri":
                            Debug.Log("Il ruolo è Volontario Croce Rossa Italiana");
                            tmp = Ruoli.VolCri;
                            break;
                        case "Medico":
                            Debug.Log("Il ruolo è Medico");
                            tmp = Ruoli.Medico;
                            break;
                        case "RefPC":
                            Debug.Log("Il ruolo è Referente Protezione Civile");
                            tmp = Ruoli.RefPC;
                            break;
                        case "VolPC":
                            Debug.Log("Il ruolo è Volontario Protezione Civile");
                            tmp = Ruoli.VolPC;
                            break;
                        case "RefTlc":
                            Debug.Log("Il ruolo è Telecomunicazioni");
                            tmp = Ruoli.RefTlc;
                            break;
                        case "RefPolizia":
                            Debug.Log("Il ruolo è Referente Polizia");
                            tmp = Ruoli.RefPolizia;
                            break;
                        case "VolPolizia":
                            Debug.Log("Il ruolo è Volontario Polizia");
                            tmp = Ruoli.VolPolizia;
                            break;
                        case "RefFuoco":
                            Debug.Log("Il ruolo è Referente Vigile del Fuoco");
                            tmp = Ruoli.RefFuoco;
                            break;
                        case "VolFuoco":
                            Debug.Log("Il ruolo è Volontario Vigile del Fuoco");
                            tmp = Ruoli.VolFuoco;
                            break;
                        case "Segreteria":
                            Debug.Log("Il ruolo è Segreteria");
                            tmp = Ruoli.Segreteria;
                            break;
                        case "RefGgev":
                            Debug.Log("Il ruolo è Referente Guardie Giurate Ecologiche Volontarie");
                            tmp = Ruoli.RefGgev;
                            break;
                        case "VolGgev":
                            Debug.Log("Il ruolo è Volontario Guardie Giurate Ecologiche Volontarie");
                            tmp = Ruoli.VolGgev;
                            break;
                    }
                    Debug.Log(tmp.ToString());
                    skin.SetSkin(tmp);
                });

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
                Info.localUser.coord = rb.position;
            }).Catch(Debug.LogError);
        }
        
        private void Update()
        {
            if (!canMove)
            {
                movementInput = Vector2.zero;
            }
                
            Animazione();
        }

        private void FixedUpdate()
        {
            if(!canMove)
                return;
            
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
            Info.localUser.coord = rb.position;
            InviaPosizioneFirebase();
            return true;
        }
        
        private void InviaPosizioneFirebase()
        {
            if (Vector2.Distance(lastPosition, rb.position) < distanzaInvio) 
                return;
            
            string toSend = JsonUtility.ToJson(rb.position);
            RestClient.Patch(
                Info.DBUrl + Info.sessionCode + "/Game/Posizione/" + Info.localUser.name + "/Coord.json", toSend).Catch(Debug.LogError);
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

        public Vector2 GetDirection()
        {
            return movementInput;
        }
    }
}