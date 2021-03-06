using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoClose : MonoBehaviour
{
    public Transform[] objsToTest;
    public UIAnimalMain targetPanel;

    float cooldown;

    private void OnEnable()
    {
        cooldown = Time.time + 1f;
    }

    private void Update()
    {
        if (cooldown > Time.time)
            return;

        foreach (Transform trans in objsToTest)
        {
            if (!IsVisible(trans))
            {
                Hide();
                return;
            }
        }
    }

    bool IsVisible(Transform trans)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(trans.position);
        return 
            pos.x > 0 && pos.x < Screen.width &&
            pos.y > 0 && pos.y < Screen.height;
    }

    void Hide()
    {
        targetPanel.Close();
        cooldown = Time.time + 1f;
    }
}
