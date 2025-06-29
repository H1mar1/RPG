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
    [Header("QuizDataBase�̎Q��")]
    public QuizDataBase QuizDataBase;
    [Header("�����\������UI�e�L�X�g")]
    public TMP_Text questionText;
    [Header("���͂����t�B�[���h�̊i�[")]
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

        // ���͗����N���A
        answerInputField.text = "";
        answerInputField.ActivateInputField(); // �t�H�[�J�X��߂�
    }
    

    private void OnOptionSelected(int index)
    {
        QuizQuestion currentQuestion = QuizDataBase.questions[currentQuestionIndex];
        //ans = (index == currentQuestion.correctAnswerIndex) ? "����" : "�s����";
        Debug.Log(ans);

        currentQuestionIndex++;
    }

    public void OnSubmitAnswer()
    {
        string userInput = answerInputField.text.Trim();
        QuizQuestion currentQuestion = QuizDataBase.questions[currentQuestionIndex];
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // ����z��Ƀ��[�U�[�̓��͂ƈ�v������̂����邩����i�啶���������𖳎��j
            bool isCorrect = currentQuestion.questionAns.Any(ans =>
                ans.Trim().Equals(userInput, StringComparison.OrdinalIgnoreCase)
            );

            if (isCorrect)
            {
                Debug.Log("����");
                ans = "����";
                _isAnswered = true;
            }
            else
            {
                Debug.Log("�s����");
                ans = "�s����";
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


