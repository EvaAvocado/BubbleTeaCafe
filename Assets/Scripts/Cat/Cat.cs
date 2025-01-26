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
    
    public void OnEnable()
    {
        print(11);
        // Создаем синглтон и сохраняем объект при переходе между сценами
        if (Instance == null)
        {
            print(22);
            Instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем объект между сценами
        }
        else
        {
            print(33);
            Destroy(gameObject);
        }
        
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

    public void MoveSideCat()
    {
        canvas.gameObject.SetActive(false);
        var animator = GetComponent<Animator>();
        animator.enabled = false;
        transform.position = new Vector3(-17.85f, 1.01f, -3.006697f);
    }

    public void MoveCenterCat()
    {
        transform.position = new Vector3(-3.48f, 1.01f, -3.006697f);
    }
}
