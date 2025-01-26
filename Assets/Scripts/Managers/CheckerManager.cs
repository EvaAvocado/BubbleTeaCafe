using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CheckerManager : MonoBehaviour
{
    public Timer _timer;
    public float timerDuration;
    public int maxDays;
    public int currentDay = 1;
    public int neededMoney = 200;
    public int currentMoney = 0;
    public Canvas canvas;
    public CatSpawner catSpawner;

    [Header("UI")] public TMP_Text textTimer;
    public GameObject itsTime; // Надпись + лапка дальше
    public TMP_Text itsTimeText;
    public GameObject moneyAll; // Надпись + лапка дальше + день
    public TMP_Text currentDayText;
    public TMP_Text moneyText;
    public GameObject moneyAll2; // After Next Time
    public TMP_Text currentDayText2;
    public TMP_Text moneyText2;

    [Header("Bools")] public bool isSampleScene;
    public bool isScene1_1;
    public bool isDayChanged;

    public static CheckerManager Instance { get; private set; }

    private SceneManager _sceneManager;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _sceneManager = new SceneManager();

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _timer = new Timer(timerDuration);

        var currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (currentSceneName == "SampleScene")
        {
            isSampleScene = true;
        }
        else if (currentSceneName == "Scene 1-1")
        {
            isScene1_1 = true;
            isDayChanged = true;
        }
    }

    public void FindCamera()
    {
        canvas.worldCamera = Camera.main;
    }

    private void Update()
    {
        if (isScene1_1 && isDayChanged)
        {
            currentDayText.text = "День " + currentDay.ToString() + " из " + maxDays.ToString();
            moneyText.text = "Заработано: " + currentMoney.ToString() + "$ / " + neededMoney.ToString() + "$";
            moneyAll.SetActive(true);
            isDayChanged = false;
        }

        if (!isSampleScene) return;

        // Обновляем таймер
        _timer.Update(Time.deltaTime);

        // Проверяем, закончился ли таймер
        if (_timer.IsFinished())
        {
            if (currentDay < maxDays)
            {
                itsTimeText.text = "Ну вот и настал новый день!";
                isDayChanged = true;
            }
            else
            {
                itsTimeText.text = "Ну вот и все...";
            }

            itsTime.SetActive(true);
            Debug.Log("Timer is finished!");
            _timer.Stop(); // Останавливаем таймер
            _timer.Reset();
        }
        else
        {
            textTimer.text = _timer.GetRemainingTime().ToString() + " сек";
            textTimer.gameObject.SetActive(true);
            //Debug.Log($"Remaining Time: {_timer.GetRemainingTime()}");
        }
    }

    public void SpawnCat()
    {
        catSpawner.SpawnCat();
    }

    public void OnMoneyAll()
    {
        moneyAll.SetActive(true);
    }

    public void OffMoneyAll()
    {
        moneyAll.SetActive(false);
    }

    public void NextAfterItsTime()
    {
        if (currentDay < maxDays)
        {
            //
        }
        else
        {
            moneyText2.text = "Заработано: " + currentMoney.ToString() + "$ / " + neededMoney.ToString() + "$";
            currentDayText2.text = "День " + currentDay.ToString() + " из " + maxDays.ToString();
            itsTime.SetActive(false);
            moneyAll2.SetActive(true);
        }

        currentDay++;
    }

    public void TheEnd()
    {
        _sceneManager.LoadScene("Scene 3");
    }
}