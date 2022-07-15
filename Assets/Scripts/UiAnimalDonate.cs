using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiAnimalDonate : MonoBehaviour
{
    public GameObject donateSection;
    public Button donateBtn;
    public Action onClose;
    public Text hintText;

    private void Awake()
    {
        donateBtn.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        if (donateSection.activeSelf)
            Close();
    }

    public void Open(string hint)
    {
        donateSection.SetActive(true);
        hintText.text = hint;
    }

    void Close()
    {
        // no need for extra action as button opens URL itself
        donateSection.SetActive(false);
        onClose?.Invoke();
    }
}
