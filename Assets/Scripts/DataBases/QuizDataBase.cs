using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuizDatabase", menuName = "Quiz/Database")]

public class QuizDataBase : ScriptableObject
{
    public QuizQuestion[] questions;
}
