using UnityEngine;

namespace Toppings
{
    public class Sprinkling : MonoBehaviour
    {
        public SpriteRenderer sprite;

        private void Awake()
        {
            sprite = GetComponent<SpriteRenderer>();
        }

        private void OnMouseDown()
        {
            sprite.enabled = true;
        }
    }
}
