using UnityEngine;
using UnityEngine.SceneManagement;

public class TeaManager : MonoBehaviour
{
    public static TeaManager Instance; // Синглтон для доступа из других скриптов
    public GameObject teaConfiguration; // Объект TeaConfiguration

    private void Awake()
    {
        // Создаем синглтон и сохраняем объект при переходе между сценами
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем объект между сценами
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Делаем TeaConfiguration неактивным при старте
        DisableTeaConfiguration();
    }

    // Метод для сохранения наполнения в TeaConfiguration
    public void AddTopping(GameObject topping)
    {
        if (teaConfiguration != null)
        {
            // Клонируем объект наполнения и добавляем его внутрь TeaConfiguration
            GameObject clonedTopping = Instantiate(topping, teaConfiguration.transform);
            clonedTopping.transform.localPosition = topping.transform.localPosition;
            clonedTopping.transform.localRotation = topping.transform.localRotation;
            clonedTopping.transform.localScale = Vector3.one;
        }
    }

    // Метод для активации TeaConfiguration
    public void ActivateTeaConfiguration()
    {
        if (teaConfiguration != null)
        {
            teaConfiguration.SetActive(true);
        }
    }
    
    // Метод для деактивации TeaConfiguration
    public void DisableTeaConfiguration()
    {
        if (teaConfiguration != null)
        {
            teaConfiguration.SetActive(false);
        }

        ClearConfiguration();
    }

    // Метод для очистки текущей конфигурации
    public void ClearConfiguration()
    {
        if (teaConfiguration != null)
        {
            foreach (Transform child in teaConfiguration.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}