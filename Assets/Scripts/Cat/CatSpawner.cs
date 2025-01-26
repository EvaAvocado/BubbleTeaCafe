using System;
using UnityEngine;


public class CatSpawner : MonoBehaviour
{
    public Cat cat;

    private void Start()
    {
        //SpawnCat();
    }

    public void SpawnCat()
    {
        Instantiate(cat.gameObject, transform.position, Quaternion.identity);
    }
}