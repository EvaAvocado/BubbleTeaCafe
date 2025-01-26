using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cat : MonoBehaviour
{
    public List<Body> bodies = new List<Body>();
    public List<Costume> costumes = new List<Costume>();
    public List<Hat> hats = new List<Hat>();
    public List<GameObject> faces = new List<GameObject>();
    public GameObject button;
    public Canvas canvas;

    public static Cat Instance; // Синглтон для доступа из других скриптов

    private void Awake()
    {
        // Создаем синглтон и сохраняем объект при переходе между сценами
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем объект между сценами
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void OnEnable()
    {
        Shuffle(bodies);
        Shuffle(costumes);
        Shuffle(hats);
        Shuffle(faces);
        
        bodies[0].gameObject.SetActive(true);
        PlayerPrefs.SetString("Body", bodies[0].color.ToString());
        
        costumes[0].gameObject.SetActive(true);
        PlayerPrefs.SetString("Costume", costumes[0].cream.ToString());
        
        hats[0].gameObject.SetActive(true);
        PlayerPrefs.SetString("Hat", hats[0].topping.ToString());
        
        faces[0].SetActive(true);
        
        canvas.worldCamera = Camera.main;
    }
    
    public void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(0, list.Count); // Генерируем случайный индекс
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }

    // Вызывается в конце анимации
    public void StartOrder()
    {
        button.SetActive(true);
    }
}
