using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiAnimalQuiz : MonoBehaviour
{
    public Text questionTxt;
    public Text[] answerTxts;
    public Button[] answerBtns;
    public GameObject quizSection;
    public Action<bool> onClose;
    public AudioSource audioSource;
    public AudioClip okClip;
    public AudioClip badClip;

    Question curQuestion;

    public Color okColor;
    public Color badColor;

    private void Awake()
    {
        answerBtns[0].onClick.AddListener(() => OnBtnClicked(0));
        answerBtns[1].onClick.AddListener(() => OnBtnClicked(1));
        answerBtns[2].onClick.AddListener(() => OnBtnClicked(2));
    }

    private void OnDisable()
    {
        if (quizSection.activeSelf)
            Close(false);
    }

    public void Open(Question question)
    {
        foreach (Button btn in answerBtns)
            btn.GetComponent<Image>().color = Color.white;

        quizSection.SetActive(true);
        curQuestion = question;
        questionTxt.text = question.question;
        for (int i = 0; i < 3; ++i)
            answerTxts[i].text = question.answers[i];
    }

    void Close(bool success)
    {
        quizSection.SetActive(false);
        onClose?.Invoke(success);
    }

    void OnBtnClicked(int btnId)
    {
        SpawnFeedback(btnId, btnId == curQuestion.answerId);
        if (btnId == curQuestion.answerId)
            StartCoroutine(IClose());
    }

    void SpawnFeedback(int btnId, bool correct)
    {
        audioSource.clip = correct ? okClip : badClip;
        audioSource.Play();

        StartCoroutine(IColorBtn(
            answerBtns[btnId].GetComponent<Image>(),
            correct? okColor : badColor));
    }

    IEnumerator IClose()
    {
        yield return new WaitForSeconds(1);
        Close(true);
    }

    IEnumerator IColorBtn(Image img, Color newColor)
    {
        Color startColor = img.color;
        float t = 0;
        while(t != 1)
        {
            t = Mathf.MoveTowards(t, 1, Time.deltaTime * 2);
            img.color = Color.Lerp(startColor, newColor, Tween.InOut(t));
            yield return null;
        }
    }
}
