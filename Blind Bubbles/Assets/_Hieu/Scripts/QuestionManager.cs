using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionManager : Singleton<QuestionManager>
{
    [SerializeField] private string[] questions;
    [SerializeField] private TextMeshProUGUI txtQuestion;
    [SerializeField] private float showSpeed;
    private Coroutine currentCoroutine;
    private bool isTyping = false;
    private int click = 0;

    protected override void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopCoroutine(currentCoroutine);
                txtQuestion.text = questions[click];
                isTyping = false;
            }
            else
            {
                click++;
                if (click < questions.Length)
                {
                    ShowQuestion(click);
                }
            }
        }
    }

    public void HideQuestion()
    {
        txtQuestion.gameObject.SetActive(false);
    }

    public void ShowQuestion(int index)
    {
        if (index >= questions.Length) return;

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        txtQuestion.text = "";
        currentCoroutine = StartCoroutine(ShowQuestionCroutine(questions[index]));
    }

    IEnumerator ShowQuestionCroutine(string question)
    {
        isTyping = true;
        foreach (char letter in question)
        {
            txtQuestion.text += letter;
            yield return new WaitForSeconds(showSpeed);
        }
        isTyping = false;
    }
}
