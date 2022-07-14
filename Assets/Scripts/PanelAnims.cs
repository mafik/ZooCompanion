using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAnims : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    Vector3 baseScale = Vector3.one / 2.5f;

    public void Open()
    {
        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(IOpen());
    }

    public void Close()
    {
        StopAllCoroutines();
        StartCoroutine(IClose());
    }

    IEnumerator IOpen()
    {
        gameObject.SetActive(true);
        float t = 0;
        while(t != 1)
        {
            t = Mathf.MoveTowards(t, 1, Time.deltaTime * 3);
            transform.localScale = baseScale * Tween.Out(t);
            canvasGroup.alpha = Tween.Out(t);
            yield return null;
        }
    }

    IEnumerator IClose()
    {
        float t = 0;
        while (t != 1)
        {
            t = Mathf.MoveTowards(t, 1, Time.deltaTime * 3);
            transform.localScale = baseScale * Tween.In(1 - t);
            canvasGroup.alpha = Tween.In(1 - t);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
