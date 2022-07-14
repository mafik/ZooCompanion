using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBtn : MonoBehaviour
{
    public UIAnimalMain objToClose;
    public AudioSource closeAudio;

    public void Close()
    {
        objToClose.Close();
        closeAudio.Play();
    }
}
