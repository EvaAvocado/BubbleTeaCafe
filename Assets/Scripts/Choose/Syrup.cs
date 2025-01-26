using MaskDrawer.Assets;
using UnityEngine;

namespace DefaultNamespace.Choose
{
    public class Syrup : MonoBehaviour
    {
        public SyrupMask syrupMask;
        public TouchDraw touchDraw;

        public void DisabledTouchDraw()
        {
            touchDraw = GetComponent<TouchDraw>();
            touchDraw.enabled = false;
        }
    }
}