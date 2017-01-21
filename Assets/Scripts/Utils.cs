using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static float
    Map(float t, float mina, float maxa, float minb, float maxb)
    {
        return (t - mina) * (maxb - minb) / (maxa - mina) + minb;
    }
}
