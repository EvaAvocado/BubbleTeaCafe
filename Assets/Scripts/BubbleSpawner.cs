using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab;
    public Transform spawnParent;
    public int maxBubbles = 5;
    public Button mainButton;
    
    private int _currentBubbleCount; // Счётчик активных тапиок

    private void OnEnable()
    {
        // Спавним первую тапиоку при включении
        SpawnBubble();
    }

    private void SpawnBubble()
    {
        if (_currentBubbleCount < maxBubbles)
        {
            // Создаём новую тапиоку
            GameObject bubble = Instantiate(bubblePrefab, spawnParent.position, Quaternion.identity, spawnParent);

            // Получаем компонент Bubble
            Bubble bubbleScript = bubble.GetComponent<Bubble>();
            bubbleScript.button = mainButton;

            // Увеличиваем счётчик тапиок
            _currentBubbleCount++;

            // Подписываемся на событие падения тапиоки
            bubbleScript.OnFall += HandleBubbleFall;
        }

        if (_currentBubbleCount == maxBubbles)
        {
            mainButton.interactable = true;
        }
    }

    private void HandleBubbleFall()
    {
        mainButton.interactable = false;
        // Запускаем корутину, чтобы спавнить новую тапиоку с задержкой
        StartCoroutine(SpawnBubbleWithDelay());
    }

    private IEnumerator SpawnBubbleWithDelay()
    {
        // Задержка перед спавном новой тапиоки (1 секунда)
        yield return new WaitForSeconds(0.5f);

        // Спавним новую тапиоку
        SpawnBubble();
        mainButton.interactable = true;
    }
}
