using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBtn : MonoBehaviour
{
    public GameObject objToClose;
    public AudioSource closeAudio;

    public void Close()
    {
        objToClose.SetActive(false);
        closeAudio.Play();
    }
}
