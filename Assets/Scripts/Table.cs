using UnityEngine;

public class Table : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, если объект, с которым произошел триггер, имеет тег Untagged
        if (collision.CompareTag("Untagged"))
        {
            // Устанавливаем родителя объекта как Table
            collision.transform.SetParent(transform);
        }
    }
}