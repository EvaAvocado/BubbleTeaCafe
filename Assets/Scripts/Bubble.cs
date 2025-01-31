using System;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    public Button button;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public float moveRange = 5f;
    public bool isStart;
    
    private bool _isFalling = false;
    private float _moveDirection = 1f;

    public Action OnFall;

    private void Start()
    {
        if (isStart)
        {
            transform.position = new Vector3(-moveRange, transform.position.y, transform.position.z);
            moveSpeed = UnityEngine.Random.Range(moveSpeed - 4f, moveSpeed + 4f);
            button.onClick.AddListener(OnClick);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnClick()
    {
        if (!isStart) return;
        
        // Останавливаем движение по оси X, чтобы тапиока падала прямо вниз
        rb.velocity = new Vector2(0, rb.velocity.y);
        
        rb.bodyType = RigidbodyType2D.Dynamic;

        if (!_isFalling)
        {
            _isFalling = true;
            OnFall?.Invoke(); // Вызываем событие падения
        }
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isStart) return;
        
        if (other.CompareTag("Glass"))
        {
            TeaManager teaManager = FindObjectOfType<TeaManager>();
            // Добавляем к чаю - клонируем
            teaManager.AddToppingToParent(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (!_isFalling && isStart)
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
