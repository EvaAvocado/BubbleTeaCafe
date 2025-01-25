using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> audioClips;    // Список аудиофайлов
    public AudioSource audioSource;       // Аудио источник, через который будем проигрывать клипы

    private void Awake()
    {
        // Убедимся, что объект не будет уничтожен при смене сцены
        DontDestroyOnLoad(gameObject);

        // Проверим, если аудио источник не назначен, то создаём новый
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Перемешиваем список аудиоклипов при старте
        ShuffleAudioList();
    }

    private void Start()
    {
        // Начинаем играть аудио
        StartCoroutine(PlayAudioClips());
    }

    private void ShuffleAudioList()
    {
        // Простой алгоритм перемешивания списка (Fisher-Yates)
        for (int i = 0; i < audioClips.Count; i++)
        {
            AudioClip temp = audioClips[i];
            int randomIndex = Random.Range(i, (int)audioClips.Count);
            audioClips[i] = audioClips[randomIndex];
            audioClips[randomIndex] = temp;
        }
    }

    private IEnumerator PlayAudioClips()
    {
        foreach (AudioClip clip in audioClips)
        {
            // Проигрываем клип
            audioSource.clip = clip;
            audioSource.Play();

            // Ждём, пока клип не закончится, прежде чем начать следующий
            yield return new WaitForSeconds(clip.length);
        }

        // После того как все клипы были проиграны, можно начать с самого начала
        StartCoroutine(PlayAudioClips());
    }
}