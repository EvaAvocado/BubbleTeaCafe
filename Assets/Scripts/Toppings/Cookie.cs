using DG.Tweening;
using UnityEngine;

namespace Toppings
{
    public class Cookie : MonoBehaviour
    {
        public Vector3 endPosition; // Конечная позиция печенья
        private Vector3 _startPosition; // Исходная позиция печенья
        private bool _isDragging = false; // Указывает, тащим ли мы сейчас печенье
        private Transform _targetItem = null; // Ссылка на предмет с тегом item
        private bool _isGame = true;
        public CookieManager cookieManager;
        private bool _isInEndPos;

        private void Start()
        {
            if(!_isGame) return;
            // Сохраняем исходную позицию печенья
            _startPosition = transform.position;
        }

        private void Update()
        {
            if(!_isGame) return;
            
            // Если зажата мышь и мы тащим печенье
            if (_isDragging)
            {
                // Перемещаем печенье за курсором
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0; // Устанавливаем Z-координату в 0 (2D пространство)
                transform.position = mousePosition;
            }

            // Если отпустили мышь
            if (Input.GetMouseButtonUp(0) && _isDragging)
            {
                _isDragging = false;

                if (_targetItem != null)
                {
                    _isInEndPos = true;
                    cookieManager.counter++;
                    // Если печенье в зоне предмета item, ставим его в координаты endPosition
                    transform.DOLocalMove(endPosition, 5f).SetSpeedBased().SetEase(Ease.OutCubic).OnComplete((() => cookieManager.CountCookie()));
                }
                else
                {
                    // Если нет, возвращаем на исходное место
                    transform.DOLocalMove(_startPosition, 10f).SetSpeedBased().SetEase(Ease.OutCubic);
                    
                }
            }
        }

        private void OnMouseDown()
        {
            if(!_isGame || _isInEndPos) return;
            // Начинаем перетаскивание при нажатии на печенье
            _isDragging = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(!_isGame || _isInEndPos) return;
            // Проверяем, если вошли в зону предмета с тегом item
            if (collision.CompareTag("Item"))
            {
                _targetItem = collision.transform;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(!_isGame || _isInEndPos) return;
            // Если выходим из зоны предмета с тегом item
            if (collision.CompareTag("Item") && _targetItem == collision.transform)
            {
                _targetItem = null;
            }
        }

        public void SetInTeaManager()
        {
            _isGame = false;
            transform.DOLocalMove(endPosition, 0f);
        }
    }
}