using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewCenterer : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Transform minPoint;
    public Transform maxPoint;

    public void Focus(AnimalPen pen)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateToPos(GetNormalizedPos(pen.panelOpenPosition.position)));
    }

    IEnumerator AnimateToPos(Vector2 pos)
    {
        float startHori = scrollRect.horizontalNormalizedPosition;
        float startVert = scrollRect.verticalNormalizedPosition;
        float t = 0;
        while(t != 1)
        {
            t = Mathf.MoveTowards(t, 1, Time.deltaTime * 2);
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(startHori, pos.x, SmoothLerp(t));
            scrollRect.verticalNormalizedPosition = Mathf.Lerp(startVert, pos.y, SmoothLerp(t));
            yield return null;
        }
    }

    Vector2 GetNormalizedPos(Vector3 pos)
    {
        Vector3 scale = maxPoint.position - minPoint.position;
        Vector3 posNormalized = pos - minPoint.position;

        return new Vector2(posNormalized.x / scale.x, posNormalized.z / scale.z);
    }

    float SmoothLerp(float t)
    {
        return 3 * t * t - 2 * t * t * t;
    }
}
