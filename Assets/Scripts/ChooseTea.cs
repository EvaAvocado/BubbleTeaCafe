using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class ChooseTea : MonoBehaviour
{
    public List<TeaButton> teaButtons;
    public MeshRenderer waterMesh;
    public Material[] materials;
    public DropSpawner dropSpawner;
    public Button mainButton;
    
    private void OnEnable()
    {
        foreach (var teaButton in teaButtons)
        {
            teaButton.OnMouseEvent += ButtonPressed;
        }
        
        mainButton.interactable = false;
        print(1);
    }

    private void OnDisable()
    {
        foreach (var teaButton in teaButtons)
        {
            teaButton.OnMouseEvent -= ButtonPressed;
        }
    }

    private void ButtonPressed(bool value, GameConfig.WaterColor color)
    {
        if (value)
        {
            switch (color)
            {
                case GameConfig.WaterColor.Brown:
                    waterMesh.material = materials[0];
                    break;
                case GameConfig.WaterColor.White:
                    waterMesh.material = materials[1];
                    break;
                case GameConfig.WaterColor.Pink:
                    waterMesh.material = materials[2];
                    break;
            }
            
            dropSpawner.gameObject.SetActive(true);
            mainButton.interactable = true;
            gameObject.SetActive(false);
        }
    } 
}
