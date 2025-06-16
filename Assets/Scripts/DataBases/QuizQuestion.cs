using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuizQuestion", menuName = "Quiz/Question")] 

public class QuizQuestion : ScriptableObject
{
    [Header("��蕶")]
    public string questionText;
    [Header("����")]
    public string questionAns;
}
