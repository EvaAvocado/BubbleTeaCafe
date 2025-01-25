using UnityEngine;

public class DeleteField : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, есть ли у объекта нужный тег
        if (other.CompareTag("Untagged"))
        {
            // Удаляем объект с сцены
            Destroy(other.gameObject);
        }
    }
}