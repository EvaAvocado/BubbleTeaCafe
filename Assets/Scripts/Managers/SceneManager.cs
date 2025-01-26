using System.Collections;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public string sceneName;

    public void LoadScene()
    {
        DontDestroyOnLoad(gameObject);
        if (CheckerManager.Instance != null) CheckerManager.Instance._timer.Stop();
        StartCoroutine(WaitForSceneLoad(sceneName));
    }

    private IEnumerator WaitForSceneLoad(string sceneName)
    {
        //print(1);
        // Загружаем сцену асинхронно
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

        // Ждем, пока сцена не загрузится
        while (!asyncLoad.isDone)
        {
            //print(2);
            //Debug.Log($"Loading progress: {asyncLoad.progress * 100}%");
            yield return null;
        }

        // Дополнительная проверка
        if (asyncLoad.isDone)
        {
            //Debug.Log("Scene loaded!");

            if (CheckerManager.Instance != null) CheckerManager.Instance.isScene1_1 = false;
            if (CheckerManager.Instance != null)
            {
                CheckerManager.Instance.isSampleScene = false;
                CheckerManager.Instance._isReady = false;
            }

            if (sceneName == "SampleScene")
            {
                CheckerManager.Instance._timer.Start();
                CheckerManager.Instance.isSampleScene = true;
                Cat.Instance.MoveSideCat();
            }
            else if (sceneName == "Scene 1-1")
            {
                CheckerManager.Instance.isScene1_1 = true;
                Cat.Instance.MoveCenterCat();
                TeaManager.Instance.gameObject.SetActive(false);
            }
            else
            {
                if (CheckerManager.Instance != null) CheckerManager.Instance._timer.Stop();
            }

            if (CheckerManager.Instance != null) CheckerManager.Instance.FindCamera();
            
            if (CheckerManager.Instance != null)
            {
                /*if (CheckerManager.Instance.isScene1_1 && !CheckerManager.Instance.isDayChanged)
                {
                    CheckerManager.Instance.teaReady.Ready();
                }*/
            }

            if (this.sceneName == "Scene 3")
            {
                CheckerManager.Instance.moneyAll.gameObject.SetActive(false);
                CheckerManager.Instance.moneyAll2.gameObject.SetActive(false);
            }


            print(1 + sceneName);

            if (!gameObject.GetComponent<Cat>() && !gameObject.GetComponent<CheckerManager>() && !gameObject.CompareTag("DontDestroy")) Destroy(gameObject);
        }
    }

    public void LoadScene(string name, GameObject gameObject)
    {
        DontDestroyOnLoad(gameObject);
        if (CheckerManager.Instance != null) CheckerManager.Instance._timer.Stop();
        StartCoroutine(WaitForSceneLoad2(name, gameObject));
    }

    private IEnumerator WaitForSceneLoad2(string name, GameObject gameObject)
    {
        // Загружаем сцену асинхронно
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

        // Ждем, пока сцена не загрузится
        while (!asyncLoad.isDone)
        {
            //Debug.Log($"Loading progress: {asyncLoad.progress * 100}%");
            yield return null;
        }

        Debug.Log("Scene loaded!");
        if (CheckerManager.Instance != null) CheckerManager.Instance.FindCamera();

        if (!gameObject.GetComponent<Cat>() && !gameObject.GetComponent<CheckerManager>()) Destroy(gameObject);

        print(2 + name);
    }
}