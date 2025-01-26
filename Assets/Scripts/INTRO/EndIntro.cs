using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class EndIntro : MonoBehaviour
{
    public GameObject thatIsAll;
    public GameObject goodEnd;
    public GameObject badEnd;
    public Button button;

    // Вызывается из кнопки
    public void CheckEnd()
    {
        button.gameObject.SetActive(false);
        
        if (CheckerManager.Instance == null) return;
        
        if (CheckerManager.Instance.currentMoney < CheckerManager.Instance.neededMoney)
        {
            badEnd.SetActive(true);
            thatIsAll.SetActive(false);
        } else
        {
            goodEnd.SetActive(true);
            thatIsAll.SetActive(false);
        }
    }
}
