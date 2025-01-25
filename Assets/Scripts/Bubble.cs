using System;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    public Button button;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public float moveRange = 5f;
    
    private bool _isFalling = false;
    private float _moveDirection = 1f;

    public Action OnFall;

    private void Start()
    {
        button.onClick.AddListener(OnClick);
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnClick()
    {
        // При нажатии на кнопку активируем физику падения
        _isFalling = true;
        
        // Останавливаем движение по оси X, чтобы тапиока падала прямо вниз
        rb.velocity = new Vector2(0, rb.velocity.y);
        
        rb.bodyType = RigidbodyType2D.Dynamic;
        
        OnFall?.Invoke(); // Вызываем событие падения
    }
    
    private void FixedUpdate()
    {
        if (!_isFalling)
        {
            // Движение тапиоки влево-вправ в пределах диапазона
            float horizontalMovement = moveSpeed * _moveDirection * Time.deltaTime;

            // Получаем текущую позицию по оси X
            float newXPosition = transform.position.x + horizontalMovement;

            // Ограничиваем движение в пределах диапазона от -moveRange до moveRange
            newXPosition = Mathf.Clamp(newXPosition, -moveRange, moveRange);

            // Применяем ограниченное движение
            transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

            // Проверка, чтобы менять направление, когда достигаем границ
            if (newXPosition >= moveRange || newXPosition <= -moveRange)
            {
                _moveDirection = -_moveDirection; // Меняем направление движения
            }
        }
    }
}
