using System;
using System.Collections.Generic;
using Proyecto26;
using Script.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace Script.User
{
    //classe del gameObject di LocalPlayer
    public class PlayerLocal : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private float collisionOffset = 0.05f;
        [SerializeField] private ContactFilter2D movementFilter;
        //differenza tra l'ultima pos inviata e quella attuale per aggiornare
        [SerializeField] private float distanzaInvio = 0.5f;
        
        private Animator anim;
        private Vector2 movementInput;
        private List<RaycastHit2D> castCollisions;
        private Rigidbody2D rb;

        private Vector2 lastPosition;

        private void Start()
        {
            anim = gameObject.GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            lastPosition = rb.position;
        }

        private void FixedUpdate()
        {
            //se ci sono movimenti in input
            if (movementInput != Vector2.zero)
            {
                //collisioni 
                //TODO da controllare il funzionamento delle collisioni 
                // int count = rb.Cast(
                //     movementInput,
                //     movementFilter,
                //     castCollisions,
                //     moveSpeed * Time.fixedDeltaTime * collisionOffset);

                //se non ci sono state collisioni sposta il player
                // if (count == 0)
                {
                    //muove il player
                    rb.MovePosition(rb.position + movementInput * (moveSpeed * Time.fixedDeltaTime));
                    
                    //animazione
                    Animazione();
                    
                    

                    //se la distanza lo consente carica sul server la posizione
                    if (Vector2.Distance(lastPosition, rb.position) >= distanzaInvio)
                    {
                        string toSend = JsonUtility.ToJson(rb.position);
                        RestClient.Patch(Info.DBUrl + Info.sessionCode + "/" + Global.PlayerFolder + "/" + Info.localUser.name + "/" + Global.CoordPlayerKey + ".json", toSend);
                        lastPosition = rb.position;
                    }
                }
            }
            else
            {
                Animazione();
            }
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
        
        
        //BUG Il personaggio si muove in modo strano quando fai impazzire il joystick
        //Si consiglia un fix

        private void Animazione()
        {
            if (movementInput.x == 0 && movementInput.y == 0)
                anim.SetBool("IsStill", true);
            else
                anim.SetBool("IsStill", false);
            
            if (movementInput.x > Math.Sqrt(2) / 2) //destra
                anim.SetBool("IsRight", true);
            else
                anim.SetBool("IsRight", false);
        
            if (movementInput.x < - Math.Sqrt(2) / 2) //sinistra
                anim.SetBool("IsLeft", true);
            else
                anim.SetBool("IsLeft", false);
        
            if (movementInput.y > Math.Sqrt(2) / 2 && (movementInput.x >= - Math.Sqrt(2) / 2 && movementInput.x <= Math.Sqrt(2) / 2)) //avanti
                anim.SetBool("IsUp", true);
            else
                anim.SetBool("IsUp", false);
        
            if (movementInput.y < - Math.Sqrt(2) / 2 && (movementInput.x >= - Math.Sqrt(2) / 2 && movementInput.x <= Math.Sqrt(2) / 2)) //indietro
                anim.SetBool("IsDown", true);
            else
                anim.SetBool("IsDown", false);

      //      Debug.Log(movementInput.x + " - " + movementInput.y);
        }
    }
}