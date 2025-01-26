using UnityEngine;


public class EndCookingManager : MonoBehaviour
{
    public GameObject buttonFinish;

    private int _counter;
    private int _earnedMoney;

    public void OnButton()
    {
        buttonFinish.SetActive(true);
    }

    public void CalculateCook()
    {
        if (PlayerPrefs.GetString("Body") == PlayerPrefs.GetString("Player_Body"))
        {
            _counter++;
        }
        
        if (PlayerPrefs.GetString("Costume") == PlayerPrefs.GetString("Player_Costume"))
        {
            _counter++;
        }
        
        if (PlayerPrefs.GetString("Hat") == PlayerPrefs.GetString("Player_Hat"))
        {
            _counter++;
        }

        switch (_counter)
        {
            case 0:
                _earnedMoney = 5;
                break;
            case 1:
                _earnedMoney = Random.Range(10, 16);
                break;
            case 2:
                _earnedMoney = Random.Range(20, 26);
                break;
            case 3:
                _earnedMoney = Random.Range(30, 36);
                break;
        }
        
        CheckerManager.Instance.currentMoney += _earnedMoney;
        PlayerPrefs.SetInt("Earned Money", _earnedMoney);
    }

}