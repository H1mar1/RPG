using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuizQuestion", menuName = "Quiz/Question")] 

public class QuizQuestion : ScriptableObject
{
    [Header("問題文")]
    public string questionText;
    [Header("答え")]
    public string[] questionAns;
}
