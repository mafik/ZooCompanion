using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewCenterer : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Transform minPoint;
    public Transform maxPoint;

    public Transform followTarget;
    public UIAnimalMain mainUi;

    bool canFollow;
    bool animating;

    private void Update()
    {
        if (mainUi.gameObject.activeInHierarchy)
        {
            canFollow = false;
            return;
        }

        if (!followTarget.gameObject.activeInHierarchy)
            return;

        Vector2 userPos = GetNormalizedPos(followTarget.position);
        if (!canFollow && !animating)
            StartCoroutine(AnimateToPos(userPos));

        if (canFollow)
        {
            scrollRect.horizontalNormalizedPosition = userPos.x;
            scrollRect.verticalNormalizedPosition = userPos.y;
        }   
    }

    public void Focus(AnimalPen pen)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateToPos(GetNormalizedPos(pen.panelOpenPosition.position)));
    }

    IEnumerator AnimateToPos(Vector2 pos)
    {
        animating = true;
        float startHori = scrollRect.horizontalNormalizedPosition;
        float startVert = scrollRect.verticalNormalizedPosition;
        float t = 0;
        while(t != 1)
        {
            t = Mathf.MoveTowards(t, 1, Time.deltaTime * 2);
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(startHori, pos.x, Tween.InOut(t));
            scrollRect.verticalNormalizedPosition = Mathf.Lerp(startVert, pos.y, Tween.InOut(t));
            yield return null;
        }

        animating = false;
        canFollow = true;
    }

    public Vector2 GetNormalizedPos(Vector3 pos)
    {
        Vector3 scale = maxPoint.position - minPoint.position;
        Vector3 posRelative = pos - minPoint.position;

        return new Vector2(posRelative.x / scale.x, posRelative.z / scale.z);
    }
}
