using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UrlButton : MonoBehaviour
{
    public string url;
    public Button button;

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        Application.OpenURL(url);
    }
}
