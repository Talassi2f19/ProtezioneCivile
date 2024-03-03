using UnityEngine;
using UnityEngine.EventSystems;

// ReSharper disable CommentTypo IdentifierTypo StringLiteralTypo
namespace Script.User
{
    //classe per la gestione del joystick
    //classe reimplementata da una già esistente non funzionanate per webGL mobile
    public sealed class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private GameObject player;

        private Vector2 relativePos;
        private Vector2 absolutePos;
        [SerializeField] private float movementRange = 80;

        
        private void Start()
        {
            
            relativePos = ((RectTransform)transform).anchoredPosition;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            absolutePos = transform.position;
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ((RectTransform)transform).anchoredPosition = relativePos;
            Send(Vector2.zero);
        }

        public void OnDrag(PointerEventData eventData)
        {
            MoveStick(eventData.position);
        }
    
        private void MoveStick(Vector2 pointerPosition)
        {
            pointerPosition = pointerPosition - absolutePos;

            pointerPosition /= transform.parent.GetComponentInParent<RectTransform>().localScale;
            var delta = pointerPosition - relativePos;
            delta  = Vector2.ClampMagnitude(delta, movementRange);
            ((RectTransform)transform).anchoredPosition = relativePos + delta;
        
            var newPos = new Vector2(delta.x / movementRange, delta.y / movementRange);
            Send(newPos);
        }

        private void Send(Vector2 moveValue)
        {
            player.GetComponent<PlayerLocal>().OnMove(moveValue);
        }
    }
}