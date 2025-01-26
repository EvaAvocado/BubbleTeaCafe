using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public string sceneName;
    
    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        CheckerManager.Instance.isScene1_1 = false;
        CheckerManager.Instance.isSampleScene = false;
        
        if (sceneName == "SampleScene")
        {
            CheckerManager.Instance._timer.Start();
            CheckerManager.Instance.isSampleScene = true;
        }
        else if (sceneName == "Scene 1-1")
        {
            CheckerManager.Instance._timer.Stop();
            CheckerManager.Instance.isScene1_1 = true;
        }
        else
        {
            CheckerManager.Instance._timer.Stop();
        }
        
        CheckerManager.Instance.FindCamera();
    }
    
    public void LoadScene(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
        CheckerManager.Instance.FindCamera();
    }
}
