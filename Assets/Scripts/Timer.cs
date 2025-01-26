using UnityEngine;

public class Timer
{
    private float _duration; // Продолжительность таймера в секундах
    private float _elapsedTime; // Время, прошедшее с момента старта
    private bool _isRunning; // Указывает, работает ли таймер

    public Timer(float duration)
    {
        _duration = duration;
        _elapsedTime = 0f;
        _isRunning = false;
    }

    // Установить время таймера
    public void SetDuration(float duration)
    {
        _duration = duration;
        Reset();
    }

    // Запустить таймер
    public void Start()
    {
        _isRunning = true;
    }

    // Остановить таймер
    public void Stop()
    {
        _isRunning = false;
    }

    // Сбросить таймер
    public void Reset()
    {
        _elapsedTime = 0f;
        _isRunning = false;
    }

    // Проверить, закончился ли таймер
    public bool IsFinished()
    {
        return _elapsedTime >= _duration;
    }

    // Обновить таймер (вызывать в Update)
    public void Update(float deltaTime)
    {
        if (_isRunning)
        {
            _elapsedTime += deltaTime;

            // Если таймер закончился, сбрасываем его
            if (_elapsedTime >= _duration)
            {
                _elapsedTime = 0f; // Сброс прошедшего времени
            }
        }
    }

    // Получить оставшееся время в секундах (целое число)
    public int GetRemainingTime()
    {
        return Mathf.Max(0, Mathf.CeilToInt(_duration - _elapsedTime));
    }

    // Получить прогресс таймера (от 0 до 1)
    public float GetProgress()
    {
        return Mathf.Clamp01(_elapsedTime / _duration);
    }
}