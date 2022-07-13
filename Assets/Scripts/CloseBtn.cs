using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBtn : MonoBehaviour
{
    public GameObject objToClose;

    public void Close()
    {
        objToClose.SetActive(false);
    }
}
