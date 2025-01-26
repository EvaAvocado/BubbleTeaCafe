using System;
using UnityEngine;

public class Sprinkling : MonoBehaviour
{
    public SpriteRenderer sprite;
    public SprinklingManager sprinklingManager;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        sprite.enabled = true;
        sprinklingManager._counter++;
        sprinklingManager.CountSprinkling();
    }
}
