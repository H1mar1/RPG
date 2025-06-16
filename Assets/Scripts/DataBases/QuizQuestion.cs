using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuizQuestion", menuName = "Quiz/Question")] 

public class QuizQuestion : ScriptableObject
{
    [Header("–â‘è•¶")]
    public string questionText;
    [Header("“š‚¦")]
    public string questionAns;
}
