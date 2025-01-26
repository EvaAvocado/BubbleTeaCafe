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
    }
    
    public void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(0, list.Count); // Генерируем случайный индекс
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }
}
