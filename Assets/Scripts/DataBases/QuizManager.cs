using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Data.SqlTypes;
using System;

public class QuizManager : MonoBehaviour
{
    [Header("QuizDataBase�̎Q��")]
    public QuizDataBase QuizDataBase;
    [Header("�����\������UI�e�L�X�g")]
    public TMP_Text questionText;
    [Header("���͂����t�B�[���h�̊i�[")]
    public TMP_InputField answerInputField;

    private int currentQuestionIndex;
    private String ans;

    private void Start()
    {
        LoadNextQuestion();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrWhiteSpace(answerInputField.text))
        {
            OnSubmitAnswer();
        }
    }

    private void LoadNextQuestion()
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

        //�������X�g�̒��Ɉ�v������̂����邩�`�F�b�N
        bool isCorrect = currentQuestion.questionAns.Trim().Equals(userInput, System.StringComparison.OrdinalIgnoreCase);

        if (isCorrect)
        {
            Debug.Log("����");
            ans = "����";
        }
        else
        {
            Debug.Log("�s����");
            ans = "�s����";
        }

        currentQuestionIndex++;
        LoadNextQuestion();
    }
}


