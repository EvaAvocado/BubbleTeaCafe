using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; } // Статическая ссылка на текущий экземпляр
    public List<AudioClip> audioClips;                       // Список аудиофайлов
    public AudioSource audioSource;                          // Аудио источник

    private void Awake()
    {
        // Проверяем, существует ли уже экземпляр AudioManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Уничтожаем новый экземпляр, если он дублируется
            return;
        }

        // Назначаем текущий объект как Instance
        Instance = this;

        // Убедимся, что объект не уничтожается при смене сцены
        DontDestroyOnLoad(gameObject);

        // Если аудио источник не назначен, создаём его
        if (audioSource == null)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }

        // Перемешиваем список аудиоклипов
        ShuffleAudioList();
    }

    private void Start()
    {
        // Начинаем проигрывать аудио
        StartCoroutine(PlayAudioClips());
    }

    private void ShuffleAudioList()
    {
        // Алгоритм перемешивания списка (Fisher-Yates)
        for (int i = 0; i < audioClips.Count; i++)
        {
            AudioClip temp = audioClips[i];
            int randomIndex = Random.Range(i, audioClips.Count);
            audioClips[i] = audioClips[randomIndex];
            audioClips[randomIndex] = temp;
        }
    }

    private IEnumerator PlayAudioClips()
    {
        foreach (AudioClip clip in audioClips)
        {
            // Проигрываем текущий клип
            audioSource.clip = clip;
            audioSource.Play();

            // Ждём завершения клипа
            yield return new WaitForSeconds(clip.length);
        }

        // Перезапускаем воспроизведение с начала
        StartCoroutine(PlayAudioClips());
    }
}