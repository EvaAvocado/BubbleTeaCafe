using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab;
    public Transform spawnParent;
    public int maxBubbles = 5;
    public Button mainButton;
    public DropSpawner dropSpawner;
    
    private int _currentBubbleCount; // Счётчик активных тапиок
    private bool _isSpawning;
    
    private List<Bubble> _bubbles = new List<Bubble>();

    private void OnEnable()
    {
        // Спавним первую тапиоку при включении
        SpawnBubble();
    }

    private void SpawnBubble()
    {
        if (!_isSpawning)
        {
            _isSpawning = true;
            // Создаём новую тапиоку
            GameObject bubble = Instantiate(bubblePrefab, spawnParent.position, Quaternion.identity, spawnParent);

            // Получаем компонент Bubble
            Bubble bubbleScript = bubble.GetComponent<Bubble>();
            bubbleScript.button = mainButton;

            // Увеличиваем счётчик тапиок
            _currentBubbleCount++;
            //print(_currentBubbleCount);

            // Подписываемся на событие падения тапиоки
            bubbleScript.OnFall += HandleBubbleFall;
            _bubbles.Add(bubbleScript);
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
        
       if (_currentBubbleCount != maxBubbles)
        {
            _isSpawning = false;
            SpawnBubble();
            mainButton.interactable = true;
        }
       else
       {
           dropSpawner.gameObject.SetActive(true);
           enabled = false;
           mainButton.interactable = true;
       }
    }

    private void OnDisable()
    {
        foreach (var bubble in _bubbles)
        {
            if (bubble != null)
            {
                bubble.OnFall -= HandleBubbleFall;
            }
        }
    }
}
