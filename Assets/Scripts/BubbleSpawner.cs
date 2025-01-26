using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab;
    public Transform spawnParent;
    public int maxBubbles = 5;
    public Button mainButton;
    public ChooseTea chooseTea;
    public TMP_Text text;
    public GameObject text1;
    public TeaManager teaManager;
    public List<GameObject> additives;
    
    private int _currentBubbleCount; // Счётчик активных тапиок
    private bool _isSpawning;
    private int _counter;
    
    private List<Bubble> _bubbles = new List<Bubble>();

    private void OnEnable()
    {
        // Спавним первую тапиоку при включении
        SpawnBubble();
        text.text = "Осталось: " + maxBubbles;
        text.gameObject.SetActive(true);
        text1.SetActive(true);
        _counter = maxBubbles;
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
            bubbleScript.isStart = true;

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
        _counter--;
        text.text = "Осталось: " + _counter;
        
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
           foreach (var additive in additives)
           {
               var bubbleSpawner = additive.GetComponent<BubbleSpawner>();
               if (bubbleSpawner != null)
               {
                   bubbleSpawner.enabled = false;
                   foreach (var bubble in bubbleSpawner._bubbles)
                   {
                       bubble.isStart = false;
                   }
               }
               
               teaManager.AddTopping(additive);
           }
           text1.SetActive(false);
           chooseTea.gameObject.SetActive(true);
           text.gameObject.SetActive(false);
           mainButton.interactable = false;
           enabled = false;
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
