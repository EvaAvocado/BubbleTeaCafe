using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ChooseCream : MonoBehaviour
{
    public GameObject[] creams;
    public GameConfig.Cream creamName;
    public Button button;
    public GameObject text5;
    public GameObject parent;
    public ChooseTopping[] toppings;
    public GameObject toppingParent;
    public TeaManager teaManager;
    public List<GameObject> additives = new List<GameObject>();

    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Press);
        text5.SetActive(true);
    }
    
    public void SetChooseTopping()
    {
        foreach (var topping in toppings)
        {
            topping.creamName = creamName;
        }
    }

    private void Press()
    {
        switch (creamName)
        {
            case GameConfig.Cream.Cream1: 
                creams[0].transform.localScale = Vector3.one;
                additives.Add(creams[0]);
                creams[0].transform.localScale = new Vector3(0.8f, 0.8f, 0);
                creams[0].SetActive(true);
                break;
            case GameConfig.Cream.Cream2: 
                creams[1].transform.localScale = Vector3.one;
                additives.Add(creams[1]);
                creams[1].transform.localScale = new Vector3(0.8f, 0.8f, 0);
                creams[1].SetActive(true);
                break;
            case GameConfig.Cream.Cream3: 
                creams[2].transform.localScale = Vector3.one;
                additives.Add(creams[2]);
                creams[2].transform.localScale = new Vector3(0.8f, 0.8f, 0);
                creams[2].SetActive(true);
                break;
        }

        foreach (var additive in additives)
        {
            teaManager.AddTopping(additive);
        }
        SetChooseTopping();
        toppingParent.SetActive(true);
        parent.SetActive(false);
        text5.SetActive(false);
    }
}
