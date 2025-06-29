using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuiestionPanelUI : MonoBehaviour
{
    
    public QuizManager quizManager;
    public void Open()
    {
        gameObject.SetActive(true);
        quizManager.LoadNextQuestion();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
