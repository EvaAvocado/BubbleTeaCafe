using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCream : MonoBehaviour
{
    public GameObject[] creams;
    public GameConfig.Cream creamName;
    public Button button;
    public GameObject text5;
    public GameObject parent;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Press);
        text5.SetActive(true);
    }

    private void Press()
    {
        switch (creamName)
        {
            case GameConfig.Cream.Cream1: 
                creams[0].SetActive(true);
                break;
            case GameConfig.Cream.Cream2: 
                creams[1].SetActive(true);
                break;
            case GameConfig.Cream.Cream3: 
                creams[2].SetActive(true);
                break;
        }
        
        parent.SetActive(false);
        text5.SetActive(false);
    }
}
