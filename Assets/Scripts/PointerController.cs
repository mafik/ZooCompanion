using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerController : MonoBehaviour
{
    public UIAnimalMain panelToOpen;
    public ViewCenterer centerer;

    Vector2 cursorDownPos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            cursorDownPos = Input.mousePosition;

        if (Input.GetMouseButtonUp(0) &&
            !IsPointerOverUi() &&
            Vector2.Distance(cursorDownPos, Input.mousePosition) < 30)
                ExecuteClick();
    }

    void ExecuteClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hitInfo))
            return;

        AnimalPen pen = hitInfo.collider.GetComponentInParent<AnimalPen>();
        if (pen)
        {
            panelToOpen.Open(pen);
            centerer.Focus(pen);
        }
    }

    public static List<RaycastResult> RaycastMouse()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            pointerId = -1,
        };

        pointerData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results;
    }

    public static bool IsPointerOverUi()
    {
        List<RaycastResult> elements = RaycastMouse();
        if (elements.Count == 0) return false;
        if (elements.Count > 1) return true;
        if (elements.Count == 1 && elements[0].gameObject.name == "MapImage") return false;

        return true;
    }
}
