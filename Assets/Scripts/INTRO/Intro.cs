using UnityEngine;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Ссылка на VideoPlayer
    public VideoClip clip;
    public AudioSource audioSource;
    public GameObject button;      // Ссылка на объект кнопки
    public GameObject buttonLeft;

    private void Start()
    {
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, audioSource);
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = clip;
        videoPlayer.Play();
        //audioSource.Play();
        
        button.SetActive(false);

        // Подписываемся на событие завершения видео
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // Активируем кнопку, когда видео закончилось
        button.SetActive(true);
        buttonLeft.SetActive(false);
    }

    private void OnDestroy()
    {
        // Отписываемся от события, чтобы избежать ошибок
        videoPlayer.loopPointReached -= OnVideoEnd;
    }
}
