using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DropSpawner : MonoBehaviour
{
    public GameObject dropPrefab;
    public GameObject milkPrefab;
    public GameObject dropParent;
    public GameObject milkParent;
    public MeshRenderer waterMesh;
    public MeshRenderer milkMesh;
    public Water water;
    public MainButton mainButton;
    public GameObject contour;
    public GameObject shaker;
    public Transform spawnPont;

    private float _cooldown = 0;
    private Camera _mainCamera;
    private int _counter;
    private float _t;
    private bool _isColorChange;
    
    private bool _isFirstTime = false;    // Флаг для отслеживания молока
    private bool _isWaterFlowed = false;   // Флаг для отслеживания воды
    private bool _isPressed;

    public Action OnReady;

    private void OnEnable()
    {
        _mainCamera = Camera.main;
        contour.SetActive(true);
        mainButton.OnMouseEvent += OnButtonPressed;
    }

    private void OnDisable()
    {
        mainButton.OnMouseEvent -= OnButtonPressed;
    }

    public void SetIsColorChange(bool value)
    {
        _isColorChange = value;
    }
    
    private void OnButtonPressed(bool value)
    {
        _isPressed = value;
    }
    
    private void Update()
    {
        // Проверяем, если кнопка НЕ нажата
        if (!_isWaterFlowed && _isFirstTime && !_isPressed)
        {
            // Кнопка не нажата в первый раз - отключаем воду
            _isWaterFlowed = true;
            _counter = 0;
        }
        
        if (_counter < 350 && _isPressed && !_isWaterFlowed)
        {
            _cooldown -= Time.deltaTime;
            while (_cooldown < 0)
            {
                _cooldown += 0.02f;
                Instantiate(dropPrefab, new Vector2(spawnPont.position.x, spawnPont.position.y)+Random.insideUnitCircle*.2f, Quaternion.identity, dropParent.transform);
                _counter++;
                //print(_counter);
            }

            _isFirstTime = true;
        }
        
        if (Input.GetMouseButton(0) && _counter < 50 && _isPressed && _isWaterFlowed)
        {
            _cooldown -= Time.deltaTime;
            while (_cooldown < 0)
            {
                _cooldown += 0.02f;
                Instantiate(milkPrefab, new Vector2(spawnPont.position.x, spawnPont.position.y)+Random.insideUnitCircle*.2f, Quaternion.identity, milkParent.transform);
                _counter++;
                //print(_counter);
            }
        }

        if (_counter >= 50 && _isWaterFlowed)
        {
            shaker.SetActive(true);
            _isColorChange = true;
        }

        if (_counter >= 50 && _t < 1 && _isColorChange && _isWaterFlowed)
        {
            _cooldown -= Time.deltaTime;
            while (_cooldown < 0)
            {
                _cooldown += 0.1f;
                switch (water.color)
                {
                    case GameConfig.WaterColor.Brown:
                        waterMesh.material.color = Color.Lerp(waterMesh.material.color, new Color(0.6f, 0.4471f, 0.5059f), _t);
                        milkMesh.material.color = Color.Lerp(milkMesh.material.color, new Color(0.6f, 0.4471f, 0.5059f), _t);
                        break;
                    case GameConfig.WaterColor.White:
                        waterMesh.material.color = Color.Lerp(waterMesh.material.color, new Color(0.9725f, 0.8549f, 0.8510f), _t);
                        milkMesh.material.color = Color.Lerp(milkMesh.material.color, new Color(0.9725f, 0.8549f, 0.8510f), _t);
                        break;
                    case GameConfig.WaterColor.Pink:
                        waterMesh.material.color = Color.Lerp(waterMesh.material.color, new Color(0.9529f, 0.3647f, 0.5804f), _t);
                        milkMesh.material.color = Color.Lerp(milkMesh.material.color, new Color(0.9529f, 0.3647f, 0.5804f), _t);
                        break;
                }
                
                
                _t += 0.004f;
                _counter++;
            }
        }

        // Закончился шейк! Можно к новому этапу
        if (_t >= 1)
        {
            contour.SetActive(false);
            shaker.SetActive(false);
        }
    }
}
