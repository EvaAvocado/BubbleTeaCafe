using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class TeaButton: MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool myBool;
        public GameConfig.WaterColor waterColor;
        
        public Action<bool, GameConfig.WaterColor> OnMouseEvent;

        public void OnPointerDown(PointerEventData eventData)
        {
            myBool = true;
            OnMouseEvent?.Invoke(myBool, waterColor);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            myBool = false;
            OnMouseEvent?.Invoke(myBool, waterColor);
        }
    }
}