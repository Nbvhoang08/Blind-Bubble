using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private string[] questions;
    [SerializeField] private TextMeshProUGUI txtQuestion;
    [SerializeField] private float showSpeed;
    private Coroutine currentCoroutine;
    public static QuestionManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        txtQuestion.text = "";
        
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

        currentCoroutine = StartCoroutine(ShowQuestionCroutine(questions[index]));
    }

    IEnumerator ShowQuestionCroutine(string question)
    {
        yield return new WaitForSeconds(1f);
        foreach (char letter in question)
        {
            txtQuestion.text += letter;
            yield return new WaitForSeconds(showSpeed);
        }
    }
}
