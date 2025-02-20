using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class MainButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool myBool;
        public Action<bool> OnMouseEvent;

        public void OnPointerDown(PointerEventData eventData)
        {
            myBool = true;
            OnMouseEvent?.Invoke(myBool);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            myBool = false;
            OnMouseEvent?.Invoke(myBool);
        }
    }
}