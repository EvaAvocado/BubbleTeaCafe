using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookieManager : MonoBehaviour
{
    public int _counter;
    public GameObject nextText; 
    public Button button;

    public void CountCookie()
    {
        if (_counter >= 2)
        {
            // NEXT SCENE
            nextText.SetActive(true);
            button.gameObject.SetActive(false);
        }
    }
}
