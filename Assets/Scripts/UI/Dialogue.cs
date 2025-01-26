using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public Sprite[] answers;
    public Sprite[] questions;
    public SpriteRenderer spriteRenderer;
    public GameObject buttonQuestion;
    public GameObject buttonAnswer;

    private int _counter;

    public void OpenQuestion()
    {
        spriteRenderer.sprite = questions[_counter];
        buttonQuestion.SetActive(true);
    }
    
    //Вызывается из buttonQuestion
    public void OpenAnswer()
    {
        buttonQuestion.SetActive(false);
        buttonAnswer.SetActive(true);
        spriteRenderer.sprite = answers[_counter];
        _counter++;
        if (_counter == answers.Length)
        {
            _counter = 0;
        }
    }
    
    public void CloseAnswer()
    {
        buttonAnswer.SetActive(false);
        spriteRenderer.sprite = null;
    }
}