using UnityEngine;

public class DeleteField : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        print(1);
        // Проверяем, есть ли у объекта нужный тег
        if (other.CompareTag("Untagged"))
        {
            print(2);
            // Удаляем объект с сцены
            Destroy(other.gameObject);
        }
    }
}