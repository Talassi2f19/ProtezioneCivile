using UnityEngine;
using UnityEngine.EventSystems;

namespace Script
{
    public sealed class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        private Vector2 pos;
        public float movementRange = 50;
        
        private void Start()
        {
           pos = ((RectTransform)transform).anchoredPosition;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ((RectTransform)transform).anchoredPosition = pos;
            Send(Vector2.zero);
        }

        public void OnDrag(PointerEventData eventData)
        {
            MoveStick(eventData.position, eventData.pressEventCamera);
        }
    
        private void MoveStick(Vector2 pointerPosition, Camera uiCamera)
        {
            pointerPosition /= transform.parent.GetComponentInParent<RectTransform>().localScale;
            var delta = pointerPosition - pos;
            delta  = Vector2.ClampMagnitude(delta, movementRange);
            ((RectTransform)transform).anchoredPosition = pos + delta;
        
            var newPos = new Vector2(delta.x / movementRange, delta.y / movementRange);
            Send(newPos);
        }

        private void Send(Vector2 vv)
        {
            SendMessage("OnMove", vv, SendMessageOptions.DontRequireReceiver);
        }
    }
}