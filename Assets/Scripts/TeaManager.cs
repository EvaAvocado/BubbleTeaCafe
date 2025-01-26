using MaskDrawer.Assets;
using Toppings;
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
            
            var scratch = clonedTopping.GetComponent<Scratch>();
            if (scratch != null)
            {
                clonedTopping.transform.localScale = new Vector3(1.25f, 1.25f, 0);
            }
            
            if (clonedTopping.CompareTag("SPARKLING"))
            {
                ActivateChildrenRecursively(clonedTopping.transform);
            }

            if (clonedTopping.CompareTag("COOKIES"))
            {
                clonedTopping.transform.localScale = new Vector3(1.25f, 1.25f, 0);
                
                foreach (Transform child in clonedTopping.transform)
                {
                    // Проверяем, есть ли у объекта компонент Cookie
                    Cookie cookie = child.GetComponent<Cookie>();
                    if (cookie != null)
                    {
                        // Вызываем метод SetInTeaManager у найденного компонента
                        cookie.SetInTeaManager();
                    }
                }
            }
        }
    }
    
    public void ActivateChildrenRecursively(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // Активируем текущего ребенка
            child.GetComponent<SpriteRenderer>().enabled = true;

            // Рекурсивно вызываем метод для детей текущего ребенка
            ActivateChildrenRecursively(child);
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