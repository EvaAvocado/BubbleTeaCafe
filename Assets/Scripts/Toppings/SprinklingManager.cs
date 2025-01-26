using UnityEngine;
using UnityEngine.UI;

public class SprinklingManager : MonoBehaviour
{
    public int _counter;
    public GameObject nextText; 
    public Button button;

    public void CountSprinkling()
    {
        if (_counter == 16)
        {
            // NEXT SCENE
            nextText.SetActive(true);
            button.gameObject.SetActive(false);
        }
    }
}
