using DefaultNamespace;
using UnityEngine;

namespace MaskDrawer.Assets
{
    public class SyrupMask : MonoBehaviour
    {
        public GameConfig.Topping topping;
        public SpriteRenderer syrupSprite; // Спрайт сиропа
        public Camera maskCamera; // Камера для рендеринга маски
        public RenderTexture maskRenderTexture; // RenderTexture маски
        public float fillThreshold = 95f; // Порог заполнения в процентах

        private Texture2D maskTexture;

        void Start()
        {
            // Создаем Texture2D из RenderTexture
            maskTexture = new Texture2D(maskRenderTexture.width, maskRenderTexture.height, TextureFormat.RGBA32, false);
            SetTopping(topping);
        }

        public void SetTopping(GameConfig.Topping newTopping)
        {
            topping = newTopping;
            var fill = 2f;
            switch (topping)
            {
                case GameConfig.Topping.Syrup1:
                    fill = 2.045766f;
                    break;
                case GameConfig.Topping.Syrup2:
                    fill = 1.909336f;
                    break;
                case GameConfig.Topping.Syrup3:
                    fill = 1.514902f;
                    break;
            }
            fillThreshold = fill * 86 / 100;
        }

        void Update()
        {
            // Обновляем маску перед проверкой заполненности
            UpdateMaskTexture();

            // Проверяем заполнение
            float fillPercent = CalculateFillPercentage();
            Debug.Log("Fill Percentage: " + fillPercent);

            if (fillPercent >= fillThreshold)
            {
                syrupSprite.maskInteraction = SpriteMaskInteraction.None;
            }
        }

        private void UpdateMaskTexture()
        {
            // Сохраняем текущий RenderTexture
            RenderTexture currentRT = RenderTexture.active;
            
            // Устанавливаем наш maskRenderTexture как активный для чтения
            RenderTexture.active = maskRenderTexture;

            // Считываем пиксели из maskRenderTexture в maskTexture
            maskTexture.ReadPixels(new Rect(0, 0, maskRenderTexture.width, maskRenderTexture.height), 0, 0);
            maskTexture.Apply();

            // Восстанавливаем оригинальный RenderTexture
            RenderTexture.active = currentRT;
        }

        private float CalculateFillPercentage()
        {
            // Подсчитываем заполненные пиксели
            Color[] pixels = maskTexture.GetPixels();
            int filledPixels = 0;

            foreach (var pixel in pixels)
            {
                // Проверяем, является ли пиксель "заполненным" (например, альфа > 0)
                if (pixel.a > 0.1f) // Пороговое значение для определения заполненности
                {
                    filledPixels++;
                }
            }

            // Вычисляем процент заполнения
            int totalPixels = maskTexture.width * maskTexture.height;
            return (filledPixels / (float)totalPixels) * 100f;
        }
    }
}