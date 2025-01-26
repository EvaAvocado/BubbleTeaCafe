using System;
using DefaultNamespace;
using DG.Tweening;
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
    public ChooseTea chooseTea;
    public GameObject milkMark;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;
    public GameObject chooseCream;

    private Color _waterColor;
    private Color _milkColor;
    private float _cooldown = 0;
    private Camera _mainCamera;
    private int _counter;
    private float _t;
    private bool _isColorChange;
    
    private bool _isFirstTime = false;    // Флаг для отслеживания молока
    private bool _isWaterFlowed = false;   // Флаг для отслеживания воды
    private bool _isPressed;

    public Action OnReady;
    private MixerSlider _mixerSlider;
    private Button _button;

    private void Start()
    {
        _button = mainButton.GetComponent<Button>();
        _mixerSlider = shaker.GetComponent<MixerSlider>();
    }

    private void OnEnable()
    {
        _mainCamera = Camera.main;
        contour.SetActive(true);
        mainButton.OnMouseEvent += OnButtonPressed;
        _waterColor = waterMesh.material.color;
        _milkColor = milkMesh.material.color;
        text2.SetActive(true);
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
        if (!_isWaterFlowed && _isFirstTime && !_isPressed && _counter > 100)
        {
            // Кнопка не нажата в первый раз - отключаем воду
            chooseTea.gameObject.SetActive(false);
            milkMark.SetActive(true);
            contour.SetActive(false);
            _isWaterFlowed = true;
            text2.SetActive(false);
            text3.SetActive(true);
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
            text3.SetActive(false);
            text4.SetActive(true);
            milkMark.SetActive(false);
            _button.interactable = false;
            shaker.SetActive(true);
        }

        if (_counter >= 50 && _t < 1.1f && _isColorChange && _isWaterFlowed)
        {
            _cooldown -= Time.deltaTime;
            while (_cooldown < 0)
            {
                _cooldown += 0.1f;
                switch (water.color)
                {
                    case GameConfig.WaterColor.Brown:
                        waterMesh.material.color = Color.Lerp(_waterColor, new Color(0.6f, 0.4471f, 0.5059f), _t);
                        milkMesh.material.color = Color.Lerp(_milkColor, new Color(0.6f, 0.4471f, 0.5059f), _t);
                        // Закончился шейк! Можно к новому этапу
                        if (waterMesh.material.color == new Color(0.6f, 0.4471f, 0.5059f))
                        {
                            Ready();
                        }
                        break;
                    case GameConfig.WaterColor.White:
                        waterMesh.material.color = Color.Lerp(_waterColor, new Color(0.9725f, 0.8549f, 0.8510f), _t);
                        milkMesh.material.color = Color.Lerp(_milkColor, new Color(0.9725f, 0.8549f, 0.8510f), _t);
                        // Закончился шейк! Можно к новому этапу
                        if (waterMesh.material.color == new Color(0.9725f, 0.8549f, 0.8510f))
                        {
                            Ready();
                        }
                        break;
                    case GameConfig.WaterColor.Pink:
                        waterMesh.material.color = Color.Lerp(_waterColor, new Color(0.9529f, 0.3647f, 0.5804f), _t);
                        milkMesh.material.color = Color.Lerp(_milkColor, new Color(0.9529f, 0.3647f, 0.5804f), _t);
                        // Закончился шейк! Можно к новому этапу
                        if (waterMesh.material.color == new Color(0.9529f, 0.3647f, 0.5804f))
                        {
                            Ready();
                        }
                        break;
                }
                
                _t += 0.03f;
                //print(_t);
                _counter++;
            }
        }
    }

    private void Ready()
    {
        chooseCream.gameObject.SetActive(true);
        text4.SetActive(false);
        contour.SetActive(false);
        shaker.SetActive(false);
        gameObject.SetActive(false);
        _mixerSlider.glass.DORotate(Vector3.zero, 0.8f)
            .SetEase(Ease.InOutSine) // Добавляем плавность
            .OnComplete(() =>
            {
                Debug.Log("Поворот завершён!");
            });
    }
}
