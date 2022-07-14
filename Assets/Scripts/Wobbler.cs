using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobbler : MonoBehaviour
{
    public float magnitude = 1;
    public float speed = 1;

    float offset;

    private void Start()
    {
        offset = Random.Range(0f, 100f);
    }

    private void Update()
    {
        Vector3 angles = transform.eulerAngles;
        angles.z = Mathf.Sin(offset + speed * Time.time) * magnitude;
        transform.eulerAngles = angles;
    }
}
