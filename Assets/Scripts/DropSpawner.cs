using DefaultNamespace;
using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    public GameObject dropPrefab;
    public GameObject milkPrefab;
    public GameObject dropParent;
    public GameObject milkParent;
    public MeshRenderer waterMesh;
    public MeshRenderer milkMesh;
    public Water water; 

    private float _cooldown = 0;
    private Camera _mainCamera;
    private int _counter;
    private float _t;
    private bool _isColorChange;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    public void SetIsColorChange(bool value)
    {
        _isColorChange = value;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _counter < 150)
        {
            _cooldown -= Time.deltaTime;
            while (_cooldown < 0)
            {
                _cooldown += 0.02f;
                Instantiate(dropPrefab, (Vector2)_mainCamera.ScreenToWorldPoint(Input.mousePosition)+Random.insideUnitCircle*.2f, Quaternion.identity, dropParent.transform);
                _counter++;
                print(_counter);
            }
        }
        
        if (Input.GetMouseButton(0) && _counter is >= 150 and < 200)
        {
            _cooldown -= Time.deltaTime;
            while (_cooldown < 0)
            {
                _cooldown += 0.02f;
                Instantiate(milkPrefab, (Vector2)_mainCamera.ScreenToWorldPoint(Input.mousePosition)+Random.insideUnitCircle*.2f, Quaternion.identity, milkParent.transform);
                _counter++;
                print(_counter);
            }
        }

        if (_counter >= 200 && _t < 1 && _isColorChange)
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
    }
}
