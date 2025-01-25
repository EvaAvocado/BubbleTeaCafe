using UnityEngine;
using UnityEngine.UI;

public class MixerSlider : MonoBehaviour
{
    public DropSpawner dropSpawner;
    public Slider slider;
    private float _lastValueChangeTime;
    private readonly float _delayTime = 0.1f; // Задержка перед определением окончания изменения
    public Transform glass;
    public AnimationCurve rotationCurve;

    void Start()
    {
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    void Update()
    {
        if (Time.time - _lastValueChangeTime > _delayTime)
        {
            // Запрещаем смешивать
            dropSpawner.SetIsColorChange(false);
        }
    }

    private void OnValueChanged(float value)
    {
        // Обновляем время последнего изменения
        _lastValueChangeTime = Time.time;
        
        // Разрешаем смешивать
        dropSpawner.SetIsColorChange(true);
        
        // Получаем угол поворота из AnimationCurve и применяем поворот к стакану (по оси Z)
        float angle = rotationCurve.Evaluate(value);
        glass.rotation = Quaternion.Euler(0, 0, angle);
    }

}
