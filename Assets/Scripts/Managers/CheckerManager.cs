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
    public TeaReady teaReady;
    public SceneManager sceneManager1_1;
    public SceneManager sceneManager3;
    public Dialogue dialogue;

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
    private bool _isTimer;
    public bool _isReady;

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
            currentDayText.text = "День " + currentDay.ToString() + " из " + maxDays.ToString();
            moneyText.text = "Заработано: " + currentMoney.ToString() + "$ / " + neededMoney.ToString() + "$";
            moneyAll.SetActive(true);
            isScene1_1 = true;
            isDayChanged = true;
            _isReady = true;
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
            teaReady.OnDestroy();
            _timer.Reset();
            isDayChanged = false;
            _isTimer = false;
        }

        if (isScene1_1 && !isDayChanged && !_isReady)
        {
            teaReady.Ready();
            _isReady = true;
        }

        if (!isSampleScene) return;

        // Обновляем таймер
        _timer.Update(Time.deltaTime);

        // Проверяем, закончился ли таймер
        if (_timer.IsFinished() && !_isTimer)
        { 
            itsTime.SetActive(true);
            textTimer.text = "0 сек";
            Debug.Log("Timer is finished!");
            //_timer.Reset();
            _timer.Stop(); // Останавливаем таймер
            _isTimer = true;
            
            EndCookingManager endCookingManager = FindObjectOfType<EndCookingManager>();
            if (endCookingManager != null) endCookingManager.CalculateCook();
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
        if (catSpawner == null) catSpawner = FindObjectOfType<CatSpawner>();
        if (catSpawner == null)
        {
            sceneManager1_1.LoadScene();
        }
        if (catSpawner!=null) catSpawner.SpawnCat();
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
        print(145);
        itsTime.SetActive(false);
        currentDayText.text = "День " + currentDay.ToString() + " из " + maxDays.ToString();
        moneyText.text = "Заработано: " + currentMoney.ToString() + "$ / " + neededMoney.ToString() + "$";
        moneyAll.SetActive(true);
        
        if (currentDay < maxDays)
        {
            currentDay++;
            isDayChanged = true;
        }
        else
        {
            currentDayText2.text = "День " + currentDay.ToString() + " из " + maxDays.ToString();
            moneyText2.text = "Заработано: " + currentMoney.ToString() + "$ / " + neededMoney.ToString() + "$";
            moneyAll2.SetActive(true);
            //sceneManager3.LoadScene();
        }
    }

    public void TheEnd()
    {
        sceneManager3.LoadScene();
    }
}