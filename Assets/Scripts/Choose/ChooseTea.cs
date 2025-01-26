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
    public GameObject text2;
    
    private void OnEnable()
    {
        foreach (var teaButton in teaButtons)
        {
            teaButton.OnMouseEvent += ButtonPressed;
        }
        
        mainButton.interactable = false;
        text2.SetActive(true);
    }

    private void OnDisable()
    {
        foreach (var teaButton in teaButtons)
        {
            if (teaButton != null) teaButton.OnMouseEvent -= ButtonPressed;
        }
    }

    private void ButtonPressed(bool value, GameConfig.WaterColor color)
    {
        if (value)
        {
            foreach (var teaButton in teaButtons)
            {
                teaButton.gameObject.SetActive(false);
            }
            
            switch (color)
            {
                case GameConfig.WaterColor.Brown:
                    waterMesh.material = materials[0];
                    teaButtons[0].gameObject.SetActive(true);
                    break;
                case GameConfig.WaterColor.White:
                    waterMesh.material = materials[1];
                    teaButtons[1].gameObject.SetActive(true);
                    break;
                case GameConfig.WaterColor.Pink:
                    waterMesh.material = materials[2];
                    teaButtons[2].gameObject.SetActive(true);
                    break;
            }
            
            dropSpawner.gameObject.SetActive(true);
            text2.SetActive(false);
            mainButton.interactable = true;
            //gameObject.SetActive(false);
        }
    } 
}
