using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tween
{
    static public float InOut(float t)
    {
        return 3 * t * t - 2 * t * t * t;
    }

    static public float In(float t)
    {
        return t * t;
    }

    static public float Out(float t)
    {
        return Mathf.Sqrt(t);
    }
}
