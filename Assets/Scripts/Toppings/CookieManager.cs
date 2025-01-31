using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CookieManager : MonoBehaviour
{
    [FormerlySerializedAs("_counter")] public int counter;
    public GameObject nextText; 
    public Button button;

    public void CountCookie()
    {
        if (counter >= 2)
        {
            // NEXT SCENE
            nextText.SetActive(true);
            button.gameObject.SetActive(false);
        }
    }
}
