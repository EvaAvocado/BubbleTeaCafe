using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Dialogue : MonoBehaviour
{
    public Sprite[] answers;
    public Sprite[] questions;
    public SpriteRenderer spriteRenderer;
    public GameObject buttonQuestion;
    public GameObject buttonAnswer;

    private List<int> _shuffledIndices = new List<int>();
    private int _counter;

    private void Start()
    {
        InitializeDialogue();
    }

    private void InitializeDialogue()
    {
        _shuffledIndices = Enumerable.Range(0, questions.Length).OrderBy(x => Random.value).ToList();
        _counter = 0;
    }

    public void OpenQuestion()
    {
        if (_counter >= _shuffledIndices.Count) return;

        int index = _shuffledIndices[_counter];
        spriteRenderer.sprite = answers[index]; // Показываем ответ
        buttonQuestion.SetActive(true);
    }

    public void OpenAnswer()
    {
        if (_counter >= _shuffledIndices.Count) return;

        int index = _shuffledIndices[_counter];
        buttonQuestion.SetActive(false);
        buttonAnswer.SetActive(true);
        spriteRenderer.sprite = questions[index]; // Показываем вопрос
        _counter++;

        if (_counter == _shuffledIndices.Count)
        {
            _counter = 0; // Сбрасываем, если дошли до конца
        }
    }

    public void CloseAnswer()
    {
        buttonAnswer.SetActive(false);
        spriteRenderer.sprite = null;
    }
}