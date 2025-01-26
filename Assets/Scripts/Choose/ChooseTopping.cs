using System.Collections.Generic;
using DefaultNamespace;
using MaskDrawer.Assets;
using UnityEngine;
using UnityEngine.UI;

public class ChooseTopping : MonoBehaviour
{
    public GameObject[] toppingsSparkling;
    public GameObject[] toppingsSyrup;
    public GameObject[] toppingsCookie;
    public GameConfig.Topping toppingName;
    public Button button;
    public GameObject text6;
    public GameObject parent;
    public GameObject[] texts7;
    public TeaManager teaManager;
    public List<GameObject> additives = new List<GameObject>();
    
    public GameConfig.Cream creamName;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Press);
        text6.SetActive(true);
    }

    private void Press()
    {
        switch (toppingName)
        {
            case GameConfig.Topping.Sprinkling:
                switch (creamName)
                {
                    case GameConfig.Cream.Cream1:
                        toppingsSparkling[0].SetActive(true);
                        additives.Add(toppingsSparkling[0]);
                        break;
                    case GameConfig.Cream.Cream2:
                        toppingsSparkling[1].SetActive(true);
                        additives.Add(toppingsSparkling[1]);
                        break;
                    case GameConfig.Cream.Cream3:
                        toppingsSparkling[2].SetActive(true);
                        additives.Add(toppingsSparkling[2]);
                        break;
                }
                texts7[0].SetActive(true);
                break;

            case GameConfig.Topping.Syrup:
                switch (creamName)
                {
                    case GameConfig.Cream.Cream1:
                        toppingsSyrup[0].GetComponent<Scratch>().cream.localScale = Vector3.one;
                        additives.Add(toppingsSyrup[0]);
                        toppingsSyrup[0].GetComponent<Scratch>().cream.localScale = new Vector3(0.8f, 0.8f, 0);
                        toppingsSyrup[0].SetActive(true);
                        break;
                    case GameConfig.Cream.Cream2:
                        toppingsSyrup[1].GetComponent<Scratch>().cream.localScale = Vector3.one;
                        additives.Add(toppingsSyrup[1]);
                        toppingsSyrup[1].GetComponent<Scratch>().cream.localScale = new Vector3(0.8f, 0.8f, 0);
                        toppingsSyrup[1].SetActive(true);
                        break;
                    case GameConfig.Cream.Cream3:
                        toppingsSyrup[2].GetComponent<Scratch>().cream.localScale = Vector3.one;
                        additives.Add(toppingsSyrup[2]);
                        toppingsSyrup[2].GetComponent<Scratch>().cream.localScale = new Vector3(0.8f, 0.8f, 0);
                        toppingsSyrup[2].SetActive(true);
                        break;
                }
                texts7[1].SetActive(true);
                break;
            
            case GameConfig.Topping.Cookies:
                switch (creamName)
                {
                    case GameConfig.Cream.Cream1:
                        toppingsCookie[0].SetActive(true);
                        additives.Add(toppingsCookie[0]);
                        break;
                    case GameConfig.Cream.Cream2:
                        toppingsCookie[1].SetActive(true);
                        additives.Add(toppingsCookie[1]);
                        break;
                    case GameConfig.Cream.Cream3:
                        toppingsCookie[2].SetActive(true);
                        additives.Add(toppingsCookie[2]);
                        break;
                }
                texts7[2].SetActive(true);
                break;
        }
        
        text6.SetActive(false);
        foreach (var additive in additives)
        {
            teaManager.AddTopping(additive);
        }
        parent.SetActive(false);
        text6.SetActive(false);
    }
}