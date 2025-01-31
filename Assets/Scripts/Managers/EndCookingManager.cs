using UnityEngine;


public class EndCookingManager : MonoBehaviour
{
    public GameObject buttonFinish;

    private int _counter;
    private int _earnedMoney;
    private bool _isOnlyBubble;

    public void OnButton()
    {
        buttonFinish.SetActive(true);
    }

    public void CalculateCookEndTime()
    {
        if (PlayerPrefs.GetString("WaterStart") == "false")
        {
            _isOnlyBubble = true;
        }
        
        CalculateCook();
    }

    public void CalculateCook()
    {
        if (PlayerPrefs.GetString("Body") == PlayerPrefs.GetString("Player_Body"))
        {
            print("body-color");
            _counter++;
        }

        if (PlayerPrefs.GetString("Costume") == PlayerPrefs.GetString("Player_Costume"))
        {
            print("costume-cream");
            _counter++;
        }

        if (PlayerPrefs.GetString("Hat") == PlayerPrefs.GetString("Player_Hat"))
        {
            print("hat-topping");
            _counter++;
        }

        if (_isOnlyBubble && _counter == 0 || _isOnlyBubble && PlayerPrefs.GetString("WaterStart") == "false")
        {
            _counter = -1;
        }

        switch (_counter)
        {
            case -1:
                _earnedMoney = 0;
                break;
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
        print("Counter: " + _counter + ", Earned Money: " + _earnedMoney);

        _counter = 0;
        _isOnlyBubble = false;
    }
}