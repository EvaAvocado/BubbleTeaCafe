using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TeaReady : MonoBehaviour
{
    public GameObject nextButton; //Кнопка отдать заказ
    public TMP_Text moneyText;
    public TeaManager tea;
    public Cat cat;

    public void OnStart()
    {
        tea = Resources.FindObjectsOfTypeAll<TeaManager>()[0];
        cat = Resources.FindObjectsOfTypeAll<Cat>()[0];
    }

    public void Ready()
    {
        nextButton.SetActive(true);
        tea.transform.localPosition = new Vector3(tea.transform.localPosition.x, tea.transform.localPosition.y, -5f);
        tea.teaConfiguration.SetActive(true);
        tea.gameObject.SetActive(true);
        cat.gameObject.SetActive(true);
    }
    
    // При нажатии на отдать заказ
    public void Next()
    {
        moneyText.text = "+ " + PlayerPrefs.GetInt("Earned Money") + "$";
        moneyText.gameObject.SetActive(true);
        Destroy(tea.gameObject);
        Destroy(cat.gameObject);
        nextButton.SetActive(false);
        StartCoroutine(MoneyDelay());
    }
    
    IEnumerator MoneyDelay()
    {
        yield return new WaitForSeconds(3f);
        moneyText.gameObject.SetActive(false);
        
        CheckerManager.Instance.SpawnCat();
    }
}