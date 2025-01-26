using System;
using UnityEngine;

public class Sprinkling : MonoBehaviour
{
    public SpriteRenderer sprite;
    public SprinklingManager sprinklingManager;
    private bool _isOpen;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if(_isOpen) return;
        
        sprite.enabled = true;
        sprinklingManager._counter++;
        sprinklingManager.CountSprinkling();
        _isOpen = true;
    }
}
