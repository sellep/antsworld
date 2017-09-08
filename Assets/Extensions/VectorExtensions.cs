using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class VectorExtensions
{
    public static Vector3 Uncertain(this Vector3 v)
    {
        return new Vector3(v.x + UnityEngine.Random.Range(0, 0.2f), v.y + UnityEngine.Random.Range(0, 0.2f), v.z);
    }
}