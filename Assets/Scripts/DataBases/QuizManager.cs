using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Data.SqlTypes;
using System;
using System.Linq; 

public class QuizManager : MonoBehaviour
{
    [Header("QuizDataBaseの参照")]
    public QuizDataBase QuizDataBase;
    [Header("質問を表示するUIテキスト")]
    public TMP_Text questionText;
    [Header("入力されるフィールドの格納")]
    public TMP_InputField answerInputField;
    [SerializeField] QuiestionPanelUI quiestionPanelUI;
    [SerializeField]
    private Image maruImage;
    [SerializeField]
    private Image batuImage;


    private int currentQuestionIndex;
    private String ans;
    public bool _isAnswered = false;           
    
    //private void Start()
    //{
    //    LoadNextQuestion();
    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrWhiteSpace(answerInputField.text))
        {
            OnSubmitAnswer();
        }
    }
public void LoadNextQuestion()
    {
        if (currentQuestionIndex >= QuizDataBase.questions.Length)
        {
            Debug.Log("quiz out of stock");
            return;
        }
        QuizQuestion currentQuestion = QuizDataBase.questions[currentQuestionIndex];
        questionText.text = currentQuestion.questionText;

        // 入力欄をクリア
        answerInputField.text = "";
        answerInputField.ActivateInputField(); // フォーカスを戻す
    }
    

    private void OnOptionSelected(int index)
    {
        QuizQuestion currentQuestion = QuizDataBase.questions[currentQuestionIndex];
        //ans = (index == currentQuestion.correctAnswerIndex) ? "正解" : "不正解";
        Debug.Log(ans);

        currentQuestionIndex++;
    }

    public void OnSubmitAnswer()
    {
        string userInput = answerInputField.text.Trim();
        QuizQuestion currentQuestion = QuizDataBase.questions[currentQuestionIndex];
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // 正解配列にユーザーの入力と一致するものがあるか判定（大文字小文字を無視）
            bool isCorrect = currentQuestion.questionAns.Any(ans =>
                ans.Trim().Equals(userInput, StringComparison.OrdinalIgnoreCase)
            );

            if (isCorrect)
            {
                Debug.Log("正解");
                ans = "正解";
                _isAnswered = true;
            }
            else
            {
                Debug.Log("不正解");
                ans = "不正解";
                _isAnswered = false;
            }
            quiestionPanelUI.Close();

        }
        currentQuestionIndex++;
        LoadNextQuestion();
    }

    public bool GetIsAnswered(bool isAnswered_)
    {
        isAnswered_ = _isAnswered;
        return isAnswered_;
    }
}


