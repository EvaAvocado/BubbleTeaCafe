using System.Collections;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public string sceneName;
    
    public void LoadScene()
    {
        CheckerManager.Instance._timer.Stop();
        StartCoroutine(WaitForSceneLoad(sceneName));
    }

    private IEnumerator WaitForSceneLoad(string sceneName)
    {
        // Загружаем сцену асинхронно
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

        // Ждем, пока сцена не загрузится
        while (!asyncLoad.isDone)
        {
            Debug.Log($"Loading progress: {asyncLoad.progress * 100}%");
            yield return null;
        }

        Debug.Log("Scene loaded!");
        
        CheckerManager.Instance.isScene1_1 = false;
        CheckerManager.Instance.isSampleScene = false;
        
        if (sceneName == "SampleScene")
        {
            CheckerManager.Instance._timer.Start();
            CheckerManager.Instance.isSampleScene = true;
            Cat.Instance.gameObject.SetActive(false);
        }
        else if (sceneName == "Scene 1-1")
        {
            CheckerManager.Instance.isScene1_1 = true;
            TeaManager.Instance.gameObject.SetActive(false);
        }
        else
        {
            CheckerManager.Instance._timer.Stop();
        }
        
        CheckerManager.Instance.FindCamera();
        
        if (CheckerManager.Instance.isScene1_1 && !CheckerManager.Instance.isDayChanged)
        {
            CheckerManager.Instance.teaReady.Ready();
        }
    }
    
    public void LoadScene(string name)
    {
        CheckerManager.Instance._timer.Stop();
        StartCoroutine(WaitForSceneLoad2(name));
    }

    private IEnumerator WaitForSceneLoad2(string name)
    {
        // Загружаем сцену асинхронно
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

        // Ждем, пока сцена не загрузится
        while (!asyncLoad.isDone)
        {
            Debug.Log($"Loading progress: {asyncLoad.progress * 100}%");
            yield return null;
        }

        Debug.Log("Scene loaded!");
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
        CheckerManager.Instance.FindCamera();
    }
}
