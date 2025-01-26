using UnityEngine;

namespace MaskDrawer.Assets
{
    public class Scratch : MonoBehaviour
    {
        public SpriteMask spriteMask;
        public Camera spriteCam;
        private Texture2D texture; // Текстура для маски
        public Transform cream;

        public void AssignScreenAsMask() 
        {
            int height = Screen.height;
            int width = Screen.width;
            int depth = 1;

            RenderTexture renderTexture = new RenderTexture(width, height, depth);
            Rect rect = new Rect(0, 0, width, height);

            // Создаём текстуру для маски
            texture = new Texture2D(width, height, TextureFormat.RGBA32, false);

            // Рендеринг камеры в RenderTexture
            spriteCam.targetTexture = renderTexture;
            spriteCam.Render();

            // Чтение данных из RenderTexture
            RenderTexture currentRenderTexture = RenderTexture.active;
            RenderTexture.active = renderTexture;
            texture.ReadPixels(rect, 0, 0);
            texture.Apply();

            // Возвращаем активный RenderTexture и освобождаем ресурсы
            spriteCam.targetTexture = null;
            RenderTexture.active = currentRenderTexture;
            Destroy(renderTexture);

            // Создаём спрайт из текстуры и назначаем его маске
            Sprite sprite = Sprite.Create(texture, rect, new Vector2(.5f, .5f), Screen.height / 10);
            spriteMask.sprite = sprite;
        }
    }
}
